using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Signature.BLL.BLL;
using Signature.Model.Model;
using Signature.Models;

namespace Signature.Controllers
{
    public class StockController : Controller
    {

        SalesManager _salesManager = new SalesManager();
        PurchaseManager _purchaseManager = new PurchaseManager();
        ProductManager _productManager = new ProductManager();
        CategoryManager _categoryManager = new CategoryManager();

        [HttpGet]
        public ActionResult Search()
        {
            StockViewModel stockViewModel = new StockViewModel();

            stockViewModel.CategorySelectListItems = _categoryManager.GetAll()
                .Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

            ViewBag.Category = stockViewModel.CategorySelectListItems;
            return View(stockViewModel);

        }

        [HttpPost]
        public ActionResult Search(StockViewModel stockViewModel)
        {

            var salesDetailses = _salesManager.GetAll();

            var purchasesDetailses = _purchaseManager.GetAll();

            var sales = _salesManager.SGetAll();

            var purchases = _purchaseManager.PGetAll();

            var category = _categoryManager.GetAll();

            var product = _productManager.GetAll();

            //  StockViewModel stockViewModel = new StockViewModel();   

            stockViewModel.CategorySelectListItems = _categoryManager.GetAll()
                .Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();



            //productNcategoryList
            var productNcategoryList = from pr in product
                                       join ca in category on pr.CategoryId equals ca.Id
                                       select new { productId = pr.Id, pr.Code, pr.Name, category = ca.Name, categoryId = ca.Id, pr.ReorderLevel };
            //productNcategoryList.Dump();

            //purchaseList									
            var purchaseList = from p in purchases
                               join pd in purchasesDetailses on p.Id equals pd.PurchasesId
                               select new { p.Date, pd.ExpireDate, pd.Quantity, pd.ProductId };
            //purchaseList.Dump();

            //salesList
            var salesList = from s in sales
                            join sd in salesDetailses on s.Id equals sd.SalesId
                            select new { s.Date, sd.Quantity, sd.ProductId };
            //salesList.Dump();

            //purchaseNsalesList
            var purchaseNsalesList = from p in purchaseList
                                     join s in salesList on p.ProductId equals s.ProductId
                                     select new { purchaseDate = p.Date, p.ExpireDate, purchaseQuantity = p.Quantity, purchaseProductId = p.ProductId, saleDate = s.Date, saleQuantity = s.Quantity, saleProductId = s.ProductId };
            //purchaseNsalesList.Dump();

            // //purchaseNsalesList N productNcategoryList				 
            var results = from ps in purchaseNsalesList
                          join pc in productNcategoryList on ps.purchaseProductId equals pc.productId
                          select new { pc.Code, pc.Name, pc.category, pc.ReorderLevel, ps.purchaseDate, ps.ExpireDate, pq = ps.purchaseQuantity, ps.purchaseProductId, ps.saleDate, sq = ps.saleQuantity, ps.saleProductId, avlQty = ps.purchaseQuantity - ps.saleQuantity };
            // results.Dump();



            var rsOB1 = from r in results.Where(c => c.purchaseProductId == stockViewModel.ProductId)
                            //.Where(c => c.saleProductId == stockViewModel.ProductId)
                            //.Where(c => c.purchaseDate < stockViewModel.StartDate)
                            //.Where(c => c.saleDate < stockViewModel.StartDate)
                        select new { r.Code, r.Name, r.category, r.ReorderLevel, r.purchaseDate, r.ExpireDate, r.pq, r.purchaseProductId, r.saleDate, r.sq, r.saleProductId };

            var rsOBP2 = from r in rsOB1.Where(c => c.purchaseDate < stockViewModel.StartDate)
                             //.Where(c => c.saleDate < stockViewModel.StartDate)
                         select new { r.Code, r.Name, r.category, r.ReorderLevel, r.purchaseDate, r.ExpireDate, r.pq, r.purchaseProductId, r.saleDate, r.sq, r.saleProductId };

            var rsOBS3 = from r in rsOBP2.Where(c => c.saleDate < stockViewModel.StartDate)
                         select new { r.Code, r.Name, r.category, r.ReorderLevel, r.purchaseDate, r.ExpireDate, r.pq, r.purchaseProductId, r.saleDate, r.sq, r.saleProductId };

            int TPQOB = 0;
            int TSQOB = 0;
            int OB = 0;
            foreach (var r in rsOBS3)
            {
                //TPQOB = TPQOB + r.pq;
                TPQOB += r.pq;
            }
            foreach (var r in rsOBS3)
            {
                TSQOB += r.sq;

            }
            OB = TPQOB - TSQOB;

            var rsIn1 = from r in results.Where(c => c.purchaseProductId == stockViewModel.ProductId)
                            //.Where(c => stockViewModel.StartDate <= c.purchaseDate && c.purchaseDate <= stockViewModel.EndDate)
                        select new { r.Code, r.Name, r.category, r.ReorderLevel, r.purchaseDate, r.ExpireDate, r.pq, r.purchaseProductId, r.saleDate, r.sq, r.saleProductId };

            var rsIn2 = from r in rsIn1.Where(c => stockViewModel.StartDate <= c.purchaseDate)
                        select new { r.Code, r.Name, r.category, r.ReorderLevel, r.purchaseDate, r.ExpireDate, r.pq, r.purchaseProductId, r.saleDate, r.sq, r.saleProductId };

            var rsIn3 = from r in rsIn2.Where(c => c.purchaseDate <= stockViewModel.EndDate)
                        select new { r.Code, r.Name, r.category, r.ReorderLevel, r.purchaseDate, r.ExpireDate, r.pq, r.purchaseProductId, r.saleDate, r.sq, r.saleProductId };


            int In = 0;
            foreach (var r in rsIn3)
            {
                In += r.pq;
            }

            var rsOut1 = from r in results.Where(c => c.purchaseProductId == stockViewModel.ProductId)
                             // .Where(c => stockViewModel.StartDate <= c.saleDate && c.saleDate <= stockViewModel.EndDate)
                         select new { r.Code, r.Name, r.category, r.ReorderLevel, r.purchaseDate, r.ExpireDate, r.pq, r.purchaseProductId, r.saleDate, r.sq, r.saleProductId };

            var rsOut2 = from r in rsOut1.Where(c => stockViewModel.StartDate <= c.saleDate && c.saleDate <= stockViewModel.EndDate)
                         select new { r.Code, r.Name, r.category, r.ReorderLevel, r.purchaseDate, r.ExpireDate, r.pq, r.purchaseProductId, r.saleDate, r.sq, r.saleProductId };

            int Out = 0;
            foreach (var r in rsOut2)
            {
                Out += r.sq;
            }

            int CB = OB + In - Out;



            foreach (var r in rsOut2)
            {

                stockViewModel.Code = r.Code;
                stockViewModel.Name = r.Name;
                stockViewModel.Category = r.category;
                stockViewModel.ReorderLevel = r.ReorderLevel;
                stockViewModel.OpeningBalance = OB;
                stockViewModel.In = In;
                stockViewModel.Out = Out;
                stockViewModel.ClosingBalance = CB;


            }

            stockViewModel.StockView.Add(stockViewModel);


            ViewBag.Category = stockViewModel.CategorySelectListItems;
            return View(stockViewModel);
        }

        public JsonResult GetProductByCategoryId(int categoryId)
        {
            var productList = _productManager.GetAll().Where(c => c.CategoryId == categoryId).ToList();
            var products = from p in productList select (new { p.Id, p.Name });
            return Json(products, JsonRequestBehavior.AllowGet);
        }

    }
}