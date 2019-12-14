using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Signature.DatabaseContext.DatabaseContext;
using Signature.Model.Model;

namespace Signature.Repository.Repository
{
    public class PurchaseRepository
    {
        ProjectDbContext _dbContext = new ProjectDbContext();
        public bool Add(Purchases purchases)
        {
            _dbContext.Purchaseses.Add(purchases);

            return _dbContext.SaveChanges() > 0;
        }

        public List<Purchases> GetAllPurchases()
        {
            return _dbContext.Purchaseses.ToList();
        }

        public List<PurchasesDetails> GetAllforajax()
        {
            return _dbContext.PurchasesDetailses.Include(c => c.Purchases).Include(c => c.Product.Category).OrderByDescending(c => c.Purchases.Date).ThenByDescending(c=>c.Id).ToList();
        }

        public PurchasesDetails GetById(int id)
        {
            return _dbContext.PurchasesDetailses.FirstOrDefault((c => c.Id == id));
        }

        public List<Purchases> GetallPurchases()
        {
            return _dbContext.Purchaseses.Include(c => c.Supplier).OrderByDescending(c => c.Date).ThenByDescending(c => c.Id).ToList();
        }

        public List<PurchasesDetails> GetallPurchaseDetailsesByPurchaseId(int id)
        {
            return _dbContext.PurchasesDetailses.Include(c=>c.Purchases.Supplier).Include(c => c.Product.Category).Where(c => c.PurchasesId == id).ToList();
        }


        public List<Purchases> PGetAll()
        {

            return _dbContext.Purchaseses.Include(c => c.PurchasesDetailses).ToList();
        }

        public Purchases PGetById(int id)
        {

            return _dbContext.Purchaseses.Include(c => c.PurchasesDetailses).FirstOrDefault((c => c.Id == id));
        }

        public List<PurchasesDetails> GetAll()
        {

            return _dbContext.PurchasesDetailses.ToList();
        }


    }
}
