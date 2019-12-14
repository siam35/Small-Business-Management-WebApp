using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Signature.BLL.BLL;
using Signature.Model.Model;
using Signature.Models;

namespace Signature.Controllers
{
    public class PurchaseController : Controller
    {
        PurchaseManager _purchaseManager = new PurchaseManager();
        SalesManager _salesManager = new SalesManager();
        SupplierManager _supplierManager = new SupplierManager();
        ProductManager _productManager = new ProductManager();
        CategoryManager _categoryManager = new CategoryManager();

        // GET: Purchases

        public JsonResult IsBillExists(string bill, int? Id)
        {
            var validateBill = _purchaseManager.GetAllPurchases().Where(c => c.Bill == bill && c.Id != Id).ToList();
            if (validateBill.Count > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Add()
        {
            PurchaseViewModel purchaseViewModel = new PurchaseViewModel();
            purchaseViewModel.SupplierSelectListItems = _supplierManager.
                GetAll().Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();
            
            purchaseViewModel.CategorySelectListItems = _categoryManager.GetAll()
                .Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

            ViewBag.Category = purchaseViewModel.CategorySelectListItems;
            return View(purchaseViewModel);
        }

        [HttpPost]
        public ActionResult Add(Purchases purchases)
        {
            string message = "";
            
            if (ModelState.IsValid)
            {
               
                if (_purchaseManager.Add(purchases))
                {
                    message = "Purchase Saved";
                }
                else
                {
                    message = "Not saved";
                }
            }
            
            ViewBag.Message = message;

            PurchaseViewModel purchaseViewModel = new PurchaseViewModel();
            purchaseViewModel.SupplierSelectListItems = _supplierManager.
                GetAll().Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

            //purchaseViewModel.ProductSelectListItems = _productManager.GetAll()
            //    .Select(c => new SelectListItem()
            //    {
            //        Value = c.Id.ToString(),
            //        Text = c.Name
            //    }).ToList();
            purchaseViewModel.CategorySelectListItems = _categoryManager.GetAll()
                .Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

            ViewBag.Category = purchaseViewModel.CategorySelectListItems;
            return View(purchaseViewModel);
        }


        public JsonResult GetProductByCategoryId(int categoryId)
        {
            var productList = _productManager.GetAll().Where(c => c.CategoryId == categoryId).ToList();
            var products = from p in productList select (new { p.Id, p.Name });
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCodeByProductId(int productId)
        {
            var productList = _productManager.GetAll().Where(c => c.Id == productId);
            var code = from c in productList select (c.Code);
            return Json(code, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PurchaseAvailableQuantity(int productId)
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

        public JsonResult GetPreviousMrpByProductId(int productId)
        {
            var purchaselist = _purchaseManager.GetAllforajax().Where(c => c.ProductId == productId).ToList();
            var previousMrp = 0.0;
            if(purchaselist.Count > 0)
            {
                previousMrp = purchaselist[0].MRP;
            }
            
            return Json(previousMrp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPreviousUnitPriceByProductId(int productId)
        {
            var purchaselist = _purchaseManager.GetAllforajax().Where(c => c.ProductId == productId).ToList();
            var previousUnitPrice = 0.0;
            if (purchaselist.Count > 0)
            {
                 previousUnitPrice = purchaselist[0].UnitPrice;
            }
            return Json(previousUnitPrice, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Show()
        {
            PurchaseViewModel purchaseViewModel = new PurchaseViewModel();
            var purchaseDetailses = _purchaseManager.GetallPurchases();

            purchaseViewModel.Purchaseses = purchaseDetailses;
            return View(purchaseViewModel);
        }
        [HttpPost]
        public ActionResult Show(PurchaseViewModel purchaseViewModel)
        {
            var purchaseDetailses = _purchaseManager.GetallPurchases();
            if (purchaseViewModel.StartDateTime != null && purchaseViewModel.EndDateTime != null)
            {
                purchaseDetailses = purchaseDetailses.Where(c => c.Date >= purchaseViewModel.StartDateTime && c.Date <= purchaseViewModel.EndDateTime).ToList();
            }

            purchaseViewModel.Purchaseses = purchaseDetailses;
            return View(purchaseViewModel);
        }

        public JsonResult IsBillExist(string bill)
        {
            var purchaseList = _purchaseManager.GetAllPurchases().Where(c=>c.Bill==bill).ToList();
            bool isExist = false;
            if (purchaseList.Count>0)
            {
                isExist = true;
            }

            return Json(isExist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowPurchaseDetails(int id, PurchaseViewModel purchaseViewModel)
        {
            var purchasedetails = _purchaseManager.GetallPurchaseDetailsesByPurchaseId(id);



            purchaseViewModel.PurchasesDetailses = purchasedetails;

            return View(purchaseViewModel);
        }
    }
}