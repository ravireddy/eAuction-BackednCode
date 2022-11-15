using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BuyerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerAPI.Repository
{
    public class BuyerRepository :IBuyerRepository
    {
        private readonly BuyerDbContext _appDBContext;
        public BuyerRepository(BuyerDbContext context)
        {
            _appDBContext = context ??
                throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Models.BidDetails>> GetBuyer()
        {
            return await _appDBContext.bidDetails.ToListAsync();
        }
        public async Task<IEnumerable<Models.ProductDetails>> GetProductDetails()
        {
            return await _appDBContext.productDetails.ToListAsync();
        }

        public async Task<Models.ProductDetails> GetProductDetailsByProductId(int ID)
        {
            return await _appDBContext.productDetails.FindAsync(ID);
        }
        public async Task<Models.BidDetails> InsertBuyer(Models.BidDetails objDepartment)
        {
            _appDBContext.bidDetails.Add(objDepartment);
            await _appDBContext.SaveChangesAsync();
            return objDepartment;
        }
        public async Task<Models.BidDetails> UpdateBuyer(Models.BidDetails objDepartment)
        {
            _appDBContext.Entry(objDepartment).State = EntityState.Modified;
            await _appDBContext.SaveChangesAsync();
            return objDepartment;
        }
        //public async Task<Models.Seller> DeleteSeller(int ID)
        //{
        //    //bool result = false;
        //    var product = _appDBContext.Sellers.Find(ID);
        //    if (product != null)
        //    {
                
        //        _appDBContext.Entry(product).State = EntityState.Deleted;
        //        _appDBContext.SaveChanges();
        //        return product;
        //    }
        //    else
        //    {
        //        return product;
        //    }
        //    //return result;
        //}
        //public async Task<Models.BidDetails> GetBidDetailsByProductID(int ID)
        //{
        //    return await _appDBContext.BiddingDetails.FindAsync(ID);
        //}

        //public async Task<IEnumerable<Models.BidDetails>> GetBidDetailsList()
        //{
        //    return await _appDBContext.BiddingDetails.ToListAsync();
        //}
    }
}

