using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Signature.Model.Model;
using Signature.Repository.Repository;

namespace Signature.BLL.BLL
{
    public class ProductManager
    {
        ProductRepository _ProductRepository = new ProductRepository();

        public bool Add(Product product)
        {
            return _ProductRepository.Add(product);
        }

        public bool Delete(int id)
        {
            return _ProductRepository.Delete(id);
        }
        public bool Update(Product product)
        {
            return _ProductRepository.Update(product);
        }
        public List<Product> GetAll()
        {
            return _ProductRepository.GetAll();
        }
        public List<Category> GetAllCategory()
        {
            return _ProductRepository.GetAllCategory();
        }
        public Product GetById(int id)
        {
            return _ProductRepository.GetById(id);
        }
    }
}
