using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Signature.BLL.BLL;
using Signature.Model.Model;
using Signature.Models;
using AutoMapper;

namespace Signature.Controllers
{
    public class ProductController : Controller
    {
        ProductManager _ProductManager = new ProductManager();
        CategoryManager _CategoryManager = new CategoryManager();

        //GET
        public JsonResult IsCodeExist(string Code, int? Id)
        {
            var validateCode = _ProductManager.GetAll().Where(c => c.Code.ToLower() == Code.ToLower() && c.Id != Id).ToList();
            if (validateCode.Count > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult IsNameExist(string name, int? Id)
        {
            var validateName = _ProductManager.GetAll().Where(c => c.Name.ToLower() == name.ToLower() && c.Id != Id).ToList();
            if (validateName.Count > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult IsCodeExistforEdit(string code, int id)
        {
            var productList = _ProductManager.GetAll().Where(c => c.Code.ToLower() == code.ToLower() && c.Id != id).ToList();
            bool isExist = false;
            if (productList.Count > 0)
            {
                isExist = true;
            }

            return Json(isExist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult IsNameExistforEdit(string name, int id)
        {
            var productList = _ProductManager.GetAll().Where(c => c.Name.ToLower() == name.ToLower() && c.Id != id).ToList();
            bool isExist = false;
            if (productList.Count > 0)
            {
                isExist = true;
            }

            return Json(isExist, JsonRequestBehavior.AllowGet);
        }




        [HttpGet]
        public ActionResult Add()
        {
            ProductViewModel productViewModel = new ProductViewModel();
            productViewModel.Products = _ProductManager.GetAll();

            productViewModel.CategorySelectListItems = _ProductManager
                .GetAllCategory().Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

            return View(productViewModel);
        }

        [HttpPost]
        public ActionResult Add(ProductViewModel productViewModel)
        {
            string message = "<h3>Product info</h3>";

            if (ModelState.IsValid)
            {
                Product product = Mapper.Map<Product>(productViewModel);

                if (_ProductManager.Add(product))
                {
                    message = "Saved";
                }
                else
                {
                    message = "Not Saved";
                }
            }
            else
            {
                message = "ModelState Failed";
            }

            ViewBag.Message = message;

            productViewModel.Products = _ProductManager.GetAll();

            productViewModel.CategorySelectListItems = _ProductManager
                .GetAllCategory().Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

            return View(productViewModel);
        }

        [HttpGet]
        public ActionResult Search()
        {
            ProductViewModel productViewModel = new ProductViewModel();

            productViewModel.Products = _ProductManager.GetAll();

            return View(productViewModel);
        }

        [HttpPost]
        public ActionResult Search(ProductViewModel productViewModel)
        {
            var products = _ProductManager.GetAll();

            if (productViewModel.Name != null)
            {
                products = products.Where(c => c.Name.ToLower().Contains(productViewModel.Name.ToLower())).ToList();
            }

            if (productViewModel.Code != null)
            {
                products = products.Where(c => c.Code.ToLower().Contains(productViewModel.Code.ToLower())).ToList();
            }

            productViewModel.Products = products;

            return View(productViewModel);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Product product = _ProductManager.GetById(id);

            ProductViewModel productViewModel = Mapper.Map<ProductViewModel>(product);

            productViewModel.Products = _ProductManager.GetAll();

            productViewModel.CategorySelectListItems = _ProductManager
                .GetAllCategory().Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

            return View(productViewModel);
        }

        [HttpPost]
        public ActionResult Edit(ProductViewModel productViewModel)
        {
            string message = "<h3>Product info</h3>";
           
                Product product = Mapper.Map<Product>(productViewModel);

                if (_ProductManager.Update(product))
                {
                    message = "Updated";
                }
                else
                {
                    message = "Not Updated";
                }

           
        


            ViewBag.Message = message;

            productViewModel.Products = _ProductManager.GetAll();

            productViewModel.CategorySelectListItems = _ProductManager
                .GetAllCategory().Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

            return View(productViewModel);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            string message = "";
            bool isDeleted = false;

            Product product = _ProductManager.GetById(id);

            if (product != null)
            {
                isDeleted = _ProductManager.Delete(product.Id);
            }

            if (isDeleted)
            {
                message = "Deleted";
            }
            else
            {
                message = "Not Deleted";
            }

            ViewBag.Message = message;

            return RedirectToAction("Add");
        }
    }
}