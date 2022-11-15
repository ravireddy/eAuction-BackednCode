using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SellerAPI.Models;
using SellerAPI.Repository;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SellerAPI.Controllers
{
    [ApiVersion("1")]
    [Route("e-auction/api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly ISellerRepository _sellerService;
        public SellerController(ISellerRepository sellerService)
        {
            _sellerService = sellerService ??
                throw new ArgumentNullException(nameof(sellerService));
        }
        [HttpGet]
        [Route("GetSeller")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> Get()
        {
            IEnumerable<SellerDetails> result = null;
            try
            {
                Log.Information("Start calling fetching all Seller details function");
                result = await _sellerService.GetSellerDetails();
            }
            catch (Exception ex)
            {
                Log.Error("There is an exception ", ex);
            }
            finally
            {
                Log.Information("End calling fetching all Seller details function");
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("GetSeller/{Id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GetSellerId(int Id)
        {
            SellerDetails result = null;
            try
            {
                Log.Information("Start calling fetching Seller details by sellerId function");
                result = await _sellerService.GetSellerDetailsByID(Id);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                    return Ok("Given Product doesn't exists!!");
            }
            catch (Exception ex)
            {
                Log.Error("There is an exception ", ex);
            }
            finally
            {
                Log.Information("End calling fetching Seller details by sellerId function");
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("Add-product")]
        public async Task<IActionResult> Post(ProductSellerDetails productSellerDetails)
        {
            try
            {
                Log.Information("Start calling adding product function");

                //await _sellerService.Insert_Product_Seller(productSellerDetails);

                var productResult = await _sellerService.InsertProduct(productSellerDetails);
                if (productResult.productDetails.ProductId != 0)
                {
                    productSellerDetails.sellerDetails.ProductId = productResult.productDetails.ProductId;
                    var sellerResult = await _sellerService.InsertSeller(productSellerDetails);
                   if (sellerResult.sellerDetails.SellerId != 0)
                   {
                        return Ok("Product Added Successfully");
                    }
                  else { return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong while adding seller information"); }
                }
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong while adding product");
            }
            catch (Exception ex)
            {
                Log.Error("There is an exception ", ex);
                throw;
            }
            finally
            {
                Log.Information("End calling adding product function");
            }
        }

        [HttpDelete]
        [Route("Delete/{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            try
            {
                Log.Information("Start calling deleting product function");
                var product = _sellerService.GetProductDetailsByID(productId);

                if (product.Result != null)
                {
                    var productBidEndDate = product.Result.BidEndDate;
                    if (productBidEndDate > DateTime.Now.Date)
                    {
                        var productBidList = _sellerService.GetBidDetailsList().Result.Where(c => c.ProductId == productId);
                        if (productBidList.Count() == 0)
                        {
                            await _sellerService.DeleteSeller(productId);
                            await _sellerService.DeleteProduct(productId);
                            return Ok("Product Deleted Successfully");
                        }
                        else
                            return Ok("This Product can't deleted as Bid is already placed.");
                    }
                    else
                        return Ok("This Product can't deleted as Bid EndDate is passed.");
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
                Log.Information("End calling deleting product function");
            }

        }

        [HttpGet]
        [Route("show-bids/{productId}")]
        public async Task<IActionResult> GetBidDetailsByProductId(int productId)
        {
            try
            {
                Log.Information("Start calling showing bids function");
                ShowBidDetails showBidDetails = new ShowBidDetails();
                var productResult = await _sellerService.GetProductDetailsByID(productId);
                if (productResult != null)
                {
                    showBidDetails.productDetails = productResult;
                    var productBidList = _sellerService.GetBidDetailsList();// GetBidDetailsList();
                    if (productBidList.Result.Count() != 0)
                    {
                        var bidDetails = productBidList.Result.Where(c => c.ProductId == productId).ToList().OrderByDescending(c => c.BidAmount);
                        if (bidDetails != null)
                        {
                            showBidDetails.bidDetails = bidDetails.ToList();
                        }
                        return Ok(showBidDetails);
                    }

                    return Ok(showBidDetails);
                }
                else
                    return Ok("Given Product doesn't exists!!");
            }
            catch (Exception ex)
            {
                Log.Error("There is an exception ", ex);
                throw;
            }
            finally
            {
                Log.Information("End calling showing bids function");
            }
        }

        [HttpGet]
        [Route("GetBidDetails")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GetBidDetails()
        {
            try
            {
                Log.Information("Start calling fetching bids function");
                return Ok(await _sellerService.GetBidDetailsList());
            }
            catch (Exception ex)
            {
                Log.Error("There is an exception ", ex);
                throw;
            }
            finally
            {
                Log.Information("End calling fetching bids function");
            }
        }

        [HttpGet]
        [Route("GetProducts")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                Log.Information("Start calling fetching all products function");
                return Ok(await _sellerService.GetProductDetails());
            }
            catch (Exception ex)
            {
                Log.Error("There is an exception ", ex);
                throw;
            }
            finally
            {
                Log.Information("End calling all products function");
            }
        }
    }
}
