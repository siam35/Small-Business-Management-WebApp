using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Signature.BLL.BLL;
using Signature.Models;

namespace Signature.Controllers
{
    public class ReportOnSaleController : Controller
    {
        SalesManager _salesManager = new SalesManager();
        PurchaseManager _purchaseManager = new PurchaseManager();
        ProductManager _productManager = new ProductManager();
        CategoryManager _categoryManager = new CategoryManager();

        [HttpGet]
        public ActionResult Search()
        {
            ReportOnSaleViewModel reportOnSaleViewModel = new ReportOnSaleViewModel();
            
            return View(reportOnSaleViewModel);
        }

        [HttpPost]
        public ActionResult Search(ReportOnSaleViewModel reportOnSaleViewModel)
        {
            var salesDetailses = _salesManager.GetAll();

            var purchasesDetailses = _purchaseManager.GetAll();

            var sales = _salesManager.SGetAll();

            var purchases = _purchaseManager.PGetAll();

            var category = _categoryManager.GetAll();

            var product = _productManager.GetAll();

            
            //productNcategoryList
            var productNcategoryList = from pr in product
                                       join ca in category on pr.CategoryId equals ca.Id
                                       select new { productId = pr.Id, categoryId = ca.Id, pr.Code, pr.Name, category = ca.Name };
            //productNcategoryList.Dump();

            //salesList
            var salesList = from s in sales
                            join sd in salesDetailses on s.Id equals sd.SalesId
                            select new { s.Date, sd.Quantity, sd.ProductId, SP = sd.MRP };
            //salesList.Dump();

            var pCP = from r in purchasesDetailses
                      group r by r.ProductId into rGroup
                      select new { productId = rGroup.Key, pdcp = rGroup.Average(c => c.UnitPrice) };

            ////purchaseNsalesList
            var purchaseNsalesList = from p in pCP
                                     join s in salesList on p.productId equals s.ProductId
                                     select new { purchaseProductId = s.ProductId, CP = p.pdcp, s.Date, s.Quantity, saleProductId = s.ProductId, s.SP };
            ////purchaseNsalesList.Dump();

            // //purchaseNsalesList N productNcategoryList				 
            var results = from pc in productNcategoryList
                          join ps in purchaseNsalesList on pc.productId equals ps.saleProductId
                          select new { pc.productId, pc.categoryId, pc.Code, pc.Name, pc.category, ps.Date, SoldQty = ps.Quantity, ps.saleProductId, ps.SP, ps.purchaseProductId, ps.CP, Profit = ps.SP - ps.CP };
            // results.Dump();
            // var id = results.Distinct();

            //var id = from r in results.Where(c => c.productId == reportOnSaleViewModel.ProductId)
            //    select new { r.productId, r.categoryId, r.Code, r.Name, r.category, r.Date, r.SoldQty, r.saleProductId, r.SP, r.purchaseProductId, r.CP, r.Profit };

            var sq1 = from r in results.Where(c => reportOnSaleViewModel.StartDate <= c.Date)
                      select new { r.productId, r.categoryId, r.Code, r.Name, r.category, r.Date, r.SoldQty, r.saleProductId, r.SP, r.purchaseProductId, r.CP, r.Profit };

            var sq2 = from r in sq1.Where(c => c.Date <= reportOnSaleViewModel.EndDate)
                      select new { r.productId, r.categoryId, r.Code, r.Name, r.category, r.Date, r.SoldQty, r.saleProductId, r.SP, r.purchaseProductId, r.CP, r.Profit };

            var gb = from r in sq2
                     group r by r.productId into rGroup
                     select new { productId = rGroup.Key, SoldQ = rGroup.Sum(c => c.SoldQty), cp = rGroup.Average(c => c.CP), SalesPrice = rGroup.Average(c => c.SP) };


            var ROS = from a in gb
                      join b in productNcategoryList on a.productId equals b.productId
                      select new { b.Code, b.Name, b.category, a.SoldQ, a.cp, a.SalesPrice, profit = a.SalesPrice - a.cp };

            foreach (var r in ROS)
            {
                ReportOnSaleViewModel rs = new ReportOnSaleViewModel();
                rs.Code = r.Code;
                rs.Name = r.Name;
                rs.Category = r.category;
                rs.SoldQty = r.SoldQ;
                double varcp = r.cp;
                double cp = Math.Round(varcp, 2);
                rs.CP = cp;
                double varSalesPrice = r.SalesPrice;
                double SalesPrice = Math.Round(varSalesPrice, 2);
                rs.SalesPrice = SalesPrice;
                double varProfit = r.SalesPrice;
                double Profit = Math.Round(varProfit, 2);
                rs.Profit = Profit;
                reportOnSaleViewModel.ReportOnSaleViewModels.Add(rs);
            }
            
            return View(reportOnSaleViewModel);
        }
    }
}