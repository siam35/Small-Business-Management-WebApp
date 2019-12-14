using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Signature.Model.Model;
using Signature.Repository.Repository;

namespace Signature.BLL.BLL
{
    public class PurchaseManager
    {
        PurchaseRepository _purchaseRepository = new PurchaseRepository();
        public bool Add(Purchases purchases)
        {
            return _purchaseRepository.Add(purchases);
        }

        public List<Purchases> GetAllPurchases()
        {
            return _purchaseRepository.GetAllPurchases();
        }

        public List<PurchasesDetails> GetAllforajax()
        {
            return _purchaseRepository.GetAllforajax();
        }

        public PurchasesDetails GetById(int id)
        {
            return _purchaseRepository.GetById(id);
        }

        public List<Purchases> GetallPurchases()
        {
            return _purchaseRepository.GetallPurchases();
        }

        public List<PurchasesDetails> GetallPurchaseDetailsesByPurchaseId(int id)
        {
            return _purchaseRepository.GetallPurchaseDetailsesByPurchaseId(id);
        }


        public List<Purchases> PGetAll()
        {
            return _purchaseRepository.PGetAll();
        }

        public Purchases PGetById(int id)
        {
            return _purchaseRepository.PGetById(id);
        }

        public List<PurchasesDetails> GetAll()
        {
            return _purchaseRepository.GetAll();
        }
    }
}
