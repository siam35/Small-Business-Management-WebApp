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
    public class SupplierController : Controller
    {
        Supplier supplier = new Supplier();
        SupplierManager _supplierManager = new SupplierManager();

        // GET: Supplier

        public JsonResult IsCodeExist(string Code, int? Id)
        {
            var validateCode = _supplierManager.GetAll().Where(c => c.Code == Code && c.Id != Id).ToList();
            if (validateCode.Count > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult IsContactExist(string Contact, int? Id)
        {
            var validateContact = _supplierManager.GetAll().Where(c => c.Contact == Contact && c.Id != Id).ToList();
            if (validateContact.Count > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult IsEmailExist(string email, int? Id)
        {
            var validateEmail = _supplierManager.GetAll().Where(c => c.Email == email && c.Id != Id).ToList();
            if (validateEmail.Count > 0)
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
            var supplierList = _supplierManager.GetAll().Where(c => c.Code.ToLower() == code.ToLower() && c.Id != id).ToList();
            bool isExist = false;
            if (supplierList.Count > 0)
            {
                isExist = true;
            }

            return Json(isExist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult IsContactExistforEdit(string contact, int id)
        {
            var supplierList = _supplierManager.GetAll().Where(c => c.Contact == contact && c.Id != id).ToList();
            bool isExist = false;
            if (supplierList.Count > 0)
            {
                isExist = true;
            }

            return Json(isExist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult IsEmailExistforEdit(string email, int id)
        {
            var supplierList = _supplierManager.GetAll().Where(c => c.Email == email && c.Id != id).ToList();
            bool isExist = false;
            if (supplierList.Count > 0)
            {
                isExist = true;
            }

            return Json(isExist, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Add()
        {
            SupplierViewModel supplierViewModel = new SupplierViewModel();
            supplierViewModel.suppliers = _supplierManager.GetAll();
            return View(supplierViewModel);
        }
        [HttpPost]
        public ActionResult Add(SupplierViewModel supplierViewModel)
        {
            string message = "";

            if (ModelState.IsValid)
            {
                Supplier supplier = Mapper.Map<Supplier>(supplierViewModel);
                if (_supplierManager.Add(supplier))
                {
                    message = "Saved";
                }
                else
                {
                    message = "Not saved";
                }
            }
            else
            {
                message = "Modelstate Failed";
            }
           
            
          

            ViewBag.Message = message;
            supplierViewModel.suppliers = _supplierManager.GetAll();
            return View(supplierViewModel);
        }

        public ActionResult Search()
        {
            SupplierViewModel supplierViewModel = new SupplierViewModel();
            supplierViewModel.suppliers = _supplierManager.GetAll();

            return View(supplierViewModel);
        }
        [HttpPost]
        public ActionResult Search(SupplierViewModel supplierViewModel)
        {
            var suppliers = _supplierManager.GetAll();

            if (supplierViewModel.Contact != null)
            {
                suppliers = suppliers.Where(c => c.Contact.Contains(supplierViewModel.Contact)).ToList();
            }
            if (supplierViewModel.Name != null)
            {
                suppliers = suppliers.Where(c => c.Name.ToLower().Contains(supplierViewModel.Name.ToLower())).ToList();
            }
            if (supplierViewModel.Email != null)
            {
                suppliers = suppliers.Where(c => c.Email.ToLower().Contains(supplierViewModel.Email.ToLower())).ToList();
            }
            supplierViewModel.suppliers = suppliers;

            supplierViewModel.Search = supplierViewModel.Contact + supplierViewModel.Name + supplierViewModel.Email;


            return View(supplierViewModel);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var supplier = _supplierManager.GetById(id);

            SupplierViewModel supplierViewModel = Mapper.Map<SupplierViewModel>(supplier);

            supplierViewModel.suppliers = _supplierManager.GetAll();



            return View(supplierViewModel);
        }

        [HttpPost]
        public ActionResult Edit(SupplierViewModel supplierViewModel)
        {
            string message = "";

            //Student student = new Student();
            //student.RollNo = studentViewModel.RollNo;
            //student.Name = studentViewModel.Name;
            //student.Address = studentViewModel.Address;
            //student.Age = studentViewModel.Age;
            //student.DepartmentId = studentViewModel.DepartmentId;

                Supplier supplier = Mapper.Map<Supplier>(supplierViewModel);

                if (_supplierManager.Update(supplier))
                {
                    message = "Updated";
                }
                else
                {
                    message = "Not Updated";
                }


            ViewBag.Message = message;
            supplierViewModel.suppliers = _supplierManager.GetAll();

            return View(supplierViewModel);
        }

        public ActionResult Delete(int id)
        {
            string message = "";
            bool isDeleted = false;

            Supplier supplier = _supplierManager.GetById(id);

            if (supplier != null)
            {
                isDeleted = _supplierManager.Delete(supplier.Id);
            }
            if (isDeleted)
            {
                message = "Deleted Successfully";

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