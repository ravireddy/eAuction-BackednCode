using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuyerAPI.Models;

namespace BuyerAPI.Repository
{
   public interface IBuyerRepository
    {
        Task<IEnumerable<Models.BidDetails>> GetBuyer();
        Task<IEnumerable<Models.ProductDetails>> GetProductDetails();
        Task<Models.ProductDetails> GetProductDetailsByProductId(int ID);
        Task<Models.BidDetails> InsertBuyer(Models.BidDetails objSeller);
        Task<Models.BidDetails> UpdateBuyer(Models.BidDetails objSeller);
        //Task<Models.Seller> DeleteSeller(int ID);

        //Task<Models.BidDetails> GetBidDetailsByProductID(int ID);
        //Task<IEnumerable<Models.BidDetails>> GetBidDetailsList();
    }
}
