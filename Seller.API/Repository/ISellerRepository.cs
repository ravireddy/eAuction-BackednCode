using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SellerAPI.Models;

namespace SellerAPI.Repository
{
   public interface ISellerRepository
    {
        Task<IEnumerable<Models.ProductDetails>> GetProductDetails();
        Task<Models.ProductDetails> GetProductDetailsByID(int ID);
        Task<IEnumerable<Models.SellerDetails>> GetSellerDetails();
        Task<Models.SellerDetails> GetSellerDetailsByID(int ID);
        Task<Models.ProductSellerDetails> InsertProduct(Models.ProductSellerDetails objProductSellerDetails);
        Task<Models.ProductSellerDetails> InsertSeller(Models.ProductSellerDetails objProductSellerDetails);
        Task<Models.ProductDetails> UpdateSeller(Models.ProductDetails objSeller);
        Task<Models.ProductDetails> DeleteProduct(int ID);
        Task<Models.SellerDetails> DeleteSeller(int ID);
        Task<Models.BidDetails> GetBidDetailsByProductID(int ID);
        Task<IEnumerable<Models.BidDetails>> GetBidDetailsList();
    }
}
