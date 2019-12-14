using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Signature.BLL.BLL;
using Signature.Models;

namespace Signature.Controllers
{
    public class ReportOnPurchaseController : Controller
    {
        SalesManager _salesManager = new SalesManager();
        PurchaseManager _purchaseManager = new PurchaseManager();
        ProductManager _productManager = new ProductManager();
        CategoryManager _categoryManager = new CategoryManager();

        [HttpGet]
        public ActionResult Search()
        {
            ReportOnPurchaseViewModel reportOnPurchaseViewModel = new ReportOnPurchaseViewModel();
            
            return View(reportOnPurchaseViewModel);
        }

        [HttpPost]
        public ActionResult Search(ReportOnPurchaseViewModel reportOnPurchaseViewModel)
        {
            var purchasesDetailses = _purchaseManager.GetAll();

            var purchases = _purchaseManager.PGetAll();

            var category = _categoryManager.GetAll();

            var product = _productManager.GetAll();
            
            //productNcategoryList
            var productNcategoryList = from pr in product
                                       join ca in category on pr.CategoryId equals ca.Id
                                       select new { productId = pr.Id, pr.Code, pr.Name, category = ca.Name, categoryId = ca.Id };
            //productNcategoryList.Dump();

            //purchaseList									
            var purchaseList = from p in purchases
                               join pd in purchasesDetailses on p.Id equals pd.PurchasesId
                               select new { p.Date, pd.Quantity, pd.ProductId, CP = pd.UnitPrice, pd.MRP };
            //purchaseList.Dump();


            // //purchaseNsalesList N productNcategoryList				 
            var results = from pl in purchaseList
                          join pc in productNcategoryList on pl.ProductId equals pc.productId
                          select new { pc.productId, pc.categoryId, pc.Code, pc.Name, pc.category, pl.Quantity, pl.CP, pl.MRP, profit = pl.MRP - pl.CP, pl.Date };
            // results.Dump();


            var aq1 = from r in results.Where(c => reportOnPurchaseViewModel.StartDate <= c.Date)
                      select new { r.productId, r.categoryId, r.Code, r.Name, r.category, r.Quantity, r.CP, r.MRP, r.profit, r.Date };

            var aq2 = from r in aq1.Where(c => c.Date <= reportOnPurchaseViewModel.EndDate)
                      select new { r.productId, r.categoryId, productCode = r.Code, r.Name, r.category, r.Quantity, r.CP, r.MRP, r.profit, r.Date };

            var availableQty = from r in aq2
                               group r by r.productId into rGroup
                               select new { productId = rGroup.Key, AvailableQty = rGroup.Sum(c => c.Quantity), cp = rGroup.Average(c => c.CP), mrp = rGroup.Average(c => c.MRP) };
            
            var ROP = from a in productNcategoryList
                      join b in availableQty on a.productId equals b.productId
                      select new { a.Code, a.Name, a.category, b.AvailableQty, b.cp, b.mrp, profit = b.mrp - b.cp };
            // results.Dump();


            foreach (var r in ROP)
            {
                ReportOnPurchaseViewModel rp = new ReportOnPurchaseViewModel();
                rp.Code = r.Code;
                rp.Name = r.Name;
                rp.Category = r.category;
                rp.AvailableQuantity = r.AvailableQty;
                double varcp = r.cp;
                double cp = Math.Round(varcp, 2);
                rp.CP = cp;
                double varmrp = r.mrp;
                double mrp = Math.Round(varmrp, 2);
                rp.MRP = mrp;
                double varprofit = r.profit;
                double profit = Math.Round(varprofit, 2);
                rp.Profit = profit;
                reportOnPurchaseViewModel.ReportOnPurchaseViewModels.Add(rp);
            }
            
            return View(reportOnPurchaseViewModel);
        }
    }
}