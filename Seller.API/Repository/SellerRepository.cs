using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SellerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SellerAPI.Repository
{
    public class SellerRepository : ISellerRepository
    {
        private readonly SellerDbContext _appDBContext;
        public SellerRepository(SellerDbContext context)
        {
            _appDBContext = context ??
                throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Models.ProductDetails>> GetProductDetails()
        {
            return await _appDBContext.productDetails.ToListAsync();
        }
        public async Task<Models.ProductDetails> GetProductDetailsByID(int ID)
        {
            return await _appDBContext.productDetails.FindAsync(ID);
        }
        public async Task<IEnumerable<Models.SellerDetails>> GetSellerDetails()
        {
            return await _appDBContext.sellerDetails.ToListAsync();
        }
        public async Task<Models.SellerDetails> GetSellerDetailsByID(int ID)
        {
            return await _appDBContext.sellerDetails.FindAsync(ID);
        }
        public async Task<Models.ProductSellerDetails> InsertSeller(Models.ProductSellerDetails objProductSellerDetails)
        {
            _appDBContext.sellerDetails.Add(objProductSellerDetails.sellerDetails);
            await _appDBContext.SaveChangesAsync();
            return objProductSellerDetails;
        }
        public async Task<Models.ProductSellerDetails> InsertProduct(Models.ProductSellerDetails objProductSellerDetails)
        {

            _appDBContext.productDetails.Add(objProductSellerDetails.productDetails);
            await _appDBContext.SaveChangesAsync();
            return objProductSellerDetails;
        }
        public async Task<Models.ProductDetails> UpdateSeller(Models.ProductDetails objDepartment)
        {
            _appDBContext.Entry(objDepartment).State = EntityState.Modified;
            await _appDBContext.SaveChangesAsync();
            return objDepartment;
        }
        public async Task<Models.ProductDetails> DeleteProduct(int ID)
        {
            //bool result = false;
            var product = _appDBContext.productDetails.Find(ID);
            if (product != null)
            {

                _appDBContext.Entry(product).State = EntityState.Deleted;
                await _appDBContext.SaveChangesAsync();
                return product;
            }
            else
            {
                return product;
            }
            //return result;
        }

        public async Task<Models.SellerDetails> DeleteSeller(int ID)
        {
            //bool result = false;
            var seller = _appDBContext.sellerDetails.Where(c => c.ProductId == ID).SingleOrDefault();
            if (seller != null)
            {

                _appDBContext.Entry(seller).State = EntityState.Deleted;
                await _appDBContext.SaveChangesAsync();
                return seller;
            }
            else
            {
                return seller;
            }
            //return result;
        }

        public async Task<Models.BidDetails> GetBidDetailsByProductID(int ID)
        {
            return await _appDBContext.biddingDetails.FindAsync(ID);
        }
        public async Task<IEnumerable<Models.BidDetails>> GetBidDetailsList()
        {
            return await _appDBContext.biddingDetails.ToListAsync();
        }
    }
}

