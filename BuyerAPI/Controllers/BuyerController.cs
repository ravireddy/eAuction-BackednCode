using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BuyerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuyerAPI.Repository;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using Apache.NMS;
using Serilog;
using Apache.NMS.ActiveMQ;
using Apache.NMS.Util;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuyerAPI.Controllers
{
    //[EnableCors("default")]
    [ApiVersion("1")]
    [Route("e-auction/api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BuyerController : ControllerBase
    {
        private readonly IBuyerRepository _buyerService;
        public BuyerController(IBuyerRepository buyerService)
        {
            _buyerService = buyerService ??
                throw new ArgumentNullException(nameof(buyerService));
        }
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> Get()
        {
            IEnumerable<BidDetails> result = null;
            try
            {
                Log.Information("Start calling fetching all bid details function");
                result = await _buyerService.GetBuyer();
                
            }
            catch(Exception ex)
            {
                Log.Error("There is an exception ", ex);
            }
            Log.Information("End calling fetching all bid details function");
            return Ok(result);
        }

        [HttpPost]
        [Route("place-bid")]
        public async Task<IActionResult> Post(BidDetails bidDetails)
        {
            try
            {
                Log.Information("Start calling place-bid function");
                if (bidDetails.ProductId > 0)
                {
                    var productDetails = _buyerService.GetProductDetailsByProductId(bidDetails.ProductId);

                    if (productDetails.Result != null)
                    {
                        var productBidEndDate = productDetails.Result.BidEndDate;//.BidEndDate;
                        if (productBidEndDate > DateTime.Now.Date)
                        {
                            var productBidDetailsList = _buyerService.GetBuyer();
                            if (productBidDetailsList.Result.Count() != 0)
                            {
                                var productBidUserExistCheck = productBidDetailsList.Result.Where(c => c.ProductId == bidDetails.ProductId).ToList();
                                productBidUserExistCheck = productBidUserExistCheck.Where(c => c.Email == bidDetails.Email).ToList();
                                if (productBidUserExistCheck.Count() == 0)
                                {
                                    var result = await _buyerService.InsertBuyer(bidDetails);
                                    if (result.BuyerId != 0)
                                    {
                                        return Ok("Added Bid Successfully");
                                    }
                                    else
                                        return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");

                                }
                                else
                                    return Ok("This Product can't Bid, as Bid is already placed with same email ID");
                            }
                            else
                            {
                                var result = await _buyerService.InsertBuyer(bidDetails);
                                if (result.BuyerId != 0)
                                {
                                    return Ok("Added Bid Successfully");
                                }
                                else
                                    return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");

                            }
                        }
                        else
                            return Ok("This Product can't Bid, as Bid EndDate is passed.");
                    }
                    else
                        return Ok("This Product doesn't exist.");
                }
                else
                {
                    return Ok("Invalid Product Id");
                }
            }
            catch (Exception ex)
            {
                Log.Error("There is an exception ", ex);
                throw;
            }
            finally
            {
                Log.Information("End calling place-bid function");
            }
        }

        [HttpPut]
        [Route("Update-bid/{productId}/{buyerEmailId}/{newBidAmount}")]
        public async Task<IActionResult> Put(int productId,string buyerEmailId, decimal newBidAmount)
        {
            try
            {
                Log.Information("Start calling Update-bid function");
                var productDetails = _buyerService.GetProductDetailsByProductId(productId);

                if (productDetails.Result != null)
                {
                    var productBidEndDate = productDetails.Result.BidEndDate;
                    if (productBidEndDate > DateTime.Now.Date)
                    {
                        var BidDetails = _buyerService.GetBuyer();
                        var productBidUserExistCheck = BidDetails.Result.Where(c => c.ProductId == productId).ToList();
                        productBidUserExistCheck = productBidUserExistCheck.Where(c => c.Email == buyerEmailId).ToList();
                        if (productBidUserExistCheck.Count() > 0)
                        {
                            var updatedBidDetails = productBidUserExistCheck.SingleOrDefault();
                            updatedBidDetails.BidAmount = newBidAmount;
                            await _buyerService.UpdateBuyer(updatedBidDetails);
                            return Ok("BidAmount Updated Successfully");
                        }
                        else
                            return Ok("EmailId is not correct.");
                    }
                    else
                        return Ok("This Product can't Bid, as Bid EndDate is passed.");
                }
                else
                    return Ok("This Product doesn't exist.");
            }
            catch (Exception ex)
            {
                Log.Error("There is an exception ", ex);
                throw;
            }
            finally
            {
                Log.Information("End calling Update-bid function");
            }
        }
    }
}
