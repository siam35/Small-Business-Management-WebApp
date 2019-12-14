using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Signature.BLL.BLL;
using Signature.Model.Model;
using Signature.Models;

namespace Signature.Controllers
{
    public class SalesController : Controller
    {
        SalesManager _salesManager = new SalesManager();
        PurchaseManager _purchaseManager = new PurchaseManager();
        CustomerManager _customerManager = new CustomerManager();
        ProductManager _productManager = new ProductManager();
        CategoryManager _categoryManager = new CategoryManager();

        // GET: Sales
        public ActionResult Add()
        {
            SalesViewModel salesViewModel = new SalesViewModel();
            salesViewModel.CustomerSelectListItems = _customerManager.
                GetAll().Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

            salesViewModel.CategorySelectListItems = _categoryManager.GetAll()
                .Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

            ViewBag.Category = salesViewModel.CategorySelectListItems;
            return View(salesViewModel);
        }

        [HttpPost]
        public ActionResult Add(Sales sales)
        {
            string message = "";

            if (_salesManager.Add(sales))
            {
                message = "Sales Saved";
            }
            else
            {
                message = "Not saved";
            }

            ViewBag.Message = message;

            SalesViewModel salesViewModel = new SalesViewModel();
            salesViewModel.CustomerSelectListItems = _customerManager.
                GetAll().Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

            salesViewModel.CategorySelectListItems = _categoryManager.GetAll()
                .Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();
            ViewBag.Category = salesViewModel.CategorySelectListItems;

            return View(salesViewModel);
        }


        public JsonResult GetProductByCategoryId(int categoryId)
        {
            var productList = _productManager.GetAll().Where(c => c.CategoryId == categoryId).ToList();
            var products = from p in productList select (new { p.Id, p.Name });
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLoyalityPointByCustomerId(int customerId)
        {
            var customerList = _customerManager.GetAll().Where(c => c.Id == customerId);
            var loyalitypoint = from c in customerList select (c.LoyalityPoint);
            return Json(loyalitypoint, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateLoyalityPoint(double grandTotal, int customerId)
        {
            if (grandTotal<1000)
            {
                grandTotal = 0.0;
            }
            var loyalitypoints = Convert.ToInt32(grandTotal / 1000);
            var customerlist = _customerManager.GetById(customerId);

            Customer customer = new Customer();
            customer.Id = customerId;
            customer.Code = customerlist.Code;
            customer.Name = customerlist.Name;
            customer.Address = customerlist.Address;
            customer.Contact = customerlist.Contact;
            customer.Email = customerlist.Email;
            int l = customerlist.LoyalityPoint;
            int tl = l + loyalitypoints;
            int fl = Convert.ToInt32(tl - (tl / 10));
            customer.LoyalityPoint = fl;
            _customerManager.Update(customer);
            return Json(loyalitypoints, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SalesAvailableQuantity(int productId)
        {
            var purchaseList = _purchaseManager.GetAllforajax().Where(c => c.ProductId == productId).ToList();
            int purchaseTotalQuantity = 0;
            foreach (var purchase in purchaseList)
            {
                int quantity = purchase.Quantity;
                purchaseTotalQuantity = purchaseTotalQuantity + quantity;
            }

            var salesList = _salesManager.GetAllforajax().Where(c => c.ProductId == productId).ToList();
            int saleTotalQuantity = 0;
            foreach (var sale in salesList)
            {
                int quantity = sale.Quantity;
                saleTotalQuantity = saleTotalQuantity + quantity;
            }

            //Get Available Quantity
            int availableQuantity = purchaseTotalQuantity - saleTotalQuantity;

            return Json(availableQuantity, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRorderLevelByProductId(int productId)
        {
            var productList = _productManager.GetAll().Where(c => c.Id == productId);
            var reorderLevel = from c in productList select (c.ReorderLevel);
            return Json(reorderLevel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMrpByProductId(int productId)
        {
            var saleslist = _purchaseManager.GetAllforajax().Where(c => c.ProductId == productId).ToList();
            var MRP = 0.0;
            if (saleslist.Count > 0)
            {
                MRP = saleslist[0].MRP;
            }

            return Json(MRP, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Show()
        {
            SalesViewModel salesViewModel = new SalesViewModel();
            salesViewModel.Salses = _salesManager.GetallSaleses();
            return View(salesViewModel);
        }
        [HttpPost]
        public ActionResult Show(SalesViewModel salesViewModel)
        {
           var salesDetailses = _salesManager.GetallSaleses();

           if (salesViewModel.StartDateTime != null && salesViewModel.EndDateTime !=null)
           {
               salesDetailses = salesDetailses.Where(c => c.Date>=salesViewModel.StartDateTime && c.Date<=salesViewModel.EndDateTime).ToList();
           }
            salesViewModel.Salses = salesDetailses;
            return View(salesViewModel);
        }

        public ActionResult ShowSalesDetails(int id, SalesViewModel salesViewModel)
        {
            var salesdetails = _salesManager.GetallSalesDetailsesBySalesId(id);



            salesViewModel.SalesDetailses = salesdetails;

            return View(salesViewModel);
        }





    }
}