using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using Signature.Model.Model;
using Signature.Models;

namespace Signature
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //Initialize
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<CategoryViewModel, Category>();
                cfg.CreateMap<Category, CategoryViewModel>();

                cfg.CreateMap<ProductViewModel, Product>();
                cfg.CreateMap<Product, ProductViewModel>();

                cfg.CreateMap<CustomerViewModel, Customer>();
                cfg.CreateMap<Customer, CustomerViewModel>();

                cfg.CreateMap<SupplierViewModel, Supplier>();
                cfg.CreateMap<Supplier, SupplierViewModel>();

                cfg.CreateMap<PurchaseViewModel, Purchases>();
                cfg.CreateMap<Purchases, PurchaseViewModel>();

                cfg.CreateMap<SalesViewModel, Sales>();
                cfg.CreateMap<Sales, SalesViewModel>();

                cfg.CreateMap<SalesViewModel, SalesDetails>();
                cfg.CreateMap<SalesDetails, SalesViewModel>();
            });
        }
    }
}
