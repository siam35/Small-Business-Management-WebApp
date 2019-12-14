using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Signature.Model.Model;
using Signature.Repository.Repository;

namespace Signature.BLL.BLL
{
    public class SalesManager
    {
        SalesRepository _salesRepository = new SalesRepository();
        public bool Add(Sales sales)
        {
            return _salesRepository.Add(sales);
        }

        public List<SalesDetails> GetAllforajax()
        {
            return _salesRepository.GetAllforajax();
        }

        public SalesDetails GetById(int id)
        {
            return _salesRepository.GetById(id);
        }

        public List<Sales> GetallSaleses()
        {
            return _salesRepository.GetallSaleses();
        }

        public List<SalesDetails> GetallSalesDetailsesBySalesId(int id)
        {
            return _salesRepository.GetallSalesDetailsesBySalesId(id);
        }

        public List<Sales> SGetAll()
        {
            return _salesRepository.SGetAll();
        }

        public Sales SGetById(int id)
        {
            return _salesRepository.SGetById(id);
        }

        public List<SalesDetails> GetAll()
        {
            return _salesRepository.GetAll();
        }
    }
}
