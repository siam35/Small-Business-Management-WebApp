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
    public class SalesRepository
    {
        ProjectDbContext _dbContext = new ProjectDbContext();
        public bool Add(Sales sales)
        {
            _dbContext.Saleses.Add(sales);
            //dbContext.SaveChanges();

            return _dbContext.SaveChanges() > 0;
        }

        public List<SalesDetails> GetAll()
        {

            return _dbContext.SalesDetailses.ToList();
        }
        public List<Sales> GetallSaleses()
        {
            return _dbContext.Saleses.Include(c=>c.Customer).OrderByDescending(c => c.Date).ThenByDescending(c=>c.Id).ToList();
        }

        public List<SalesDetails> GetallSalesDetailsesBySalesId(int id)
        {
            return _dbContext.SalesDetailses.Include(c=>c.Sales.Customer).Include(c=>c.Product.Category).Where(c=>c.SalesId==id).ToList();
        }

        public List<SalesDetails> GetAllforajax()
        {
            return _dbContext.SalesDetailses.Include(c=>c.Sales).Include(c=>c.Product.Category).OrderByDescending(c => c.Sales.Date).ThenByDescending(c=>c.Id).ToList();
        }

        public SalesDetails GetById(int id)
        {

            return _dbContext.SalesDetailses.FirstOrDefault((c => c.Id == id));
        }

        public List<Sales> SGetAll()
        {

            return _dbContext.Saleses.Include(c => c.SalesDetailses).ToList();
        }

        public Sales SGetById(int id)
        {

            return _dbContext.Saleses.Include(c => c.SalesDetailses).FirstOrDefault((c => c.Id == id));
        }
    }
}
