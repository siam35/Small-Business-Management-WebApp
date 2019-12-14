using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Signature.Model.Model;
using Signature.BLL.BLL;
using Signature.Models;

namespace Signature.Controllers
{
    public class CustomerController : Controller
    {

        CustomerManager _customerManager = new CustomerManager();
        Customer customer = new Customer();

        // GET: Customer
        public JsonResult IsCodeExist(string Code, int? Id)
        {
            var validateCode = _customerManager.GetAll().Where(c => c.Code.ToLower() == Code.ToLower() && c.Id != Id).ToList();
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
            var validateContact = _customerManager.GetAll().Where(c => c.Contact == Contact && c.Id != Id).ToList();
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
            var validateEmail = _customerManager.GetAll().Where(c => c.Email == email && c.Id != Id).ToList();
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
            var customerList = _customerManager.GetAll().Where(c => c.Code.ToLower() == code.ToLower() && c.Id != id).ToList();
            bool isExist = false;
            if (customerList.Count > 0)
            {
                isExist = true;
            }

            return Json(isExist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult IsContactExistforEdit(string contact, int id)
        {
            var customerList = _customerManager.GetAll().Where(c => c.Contact == contact && c.Id != id).ToList();
            bool isExist = false;
            if (customerList.Count > 0)
            {
                isExist = true;
            }

            return Json(isExist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult IsEmailExistforEdit(string email, int id)
        {
            var customerList = _customerManager.GetAll().Where(c => c.Email == email && c.Id != id).ToList();
            bool isExist = false;
            if (customerList.Count > 0)
            {
                isExist = true;
            }

            return Json(isExist, JsonRequestBehavior.AllowGet);
        }






        [HttpGet]
        public ActionResult Add()
        {
            CustomerViewModel customerViewModel = new CustomerViewModel();
            customerViewModel.Customers = _customerManager.GetAll();
            return View(customerViewModel);
        }
        [HttpPost]
        public ActionResult Add(CustomerViewModel customerViewModel)
        {
            string message = "";

            
                if (ModelState.IsValid)
                {

                   
                    Customer customer = Mapper.Map<Customer>(customerViewModel);
                    if (_customerManager.Add(customer))
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
                    message = "Modelstate failed";
                }
            

            ViewBag.Message = message;
            customerViewModel.Customers = _customerManager.GetAll();
            return View(customerViewModel);
        }

        public ActionResult Search()
        {
            CustomerViewModel customerViewModel = new CustomerViewModel();
            customerViewModel.Customers = _customerManager.GetAll();

            return View(customerViewModel);
        }
        [HttpPost]
        public ActionResult Search(CustomerViewModel customerViewModel)
        {
            var customers = _customerManager.GetAll();

            if (customerViewModel.Contact != null)
            {
                customers = customers.Where(c => c.Contact.Contains(customerViewModel.Contact)).ToList();
            }
            if (customerViewModel.Name != null)
            {
                customers = customers.Where(c => c.Name.ToLower().Contains(customerViewModel.Name.ToLower())).ToList();
            }
            if (customerViewModel.Email != null)
            {
                customers = customers.Where(c => c.Email.ToLower().Contains(customerViewModel.Email.ToLower())).ToList();
            }
            customerViewModel.Customers = customers;


            return View(customerViewModel);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var customer = _customerManager.GetById(id);

            CustomerViewModel customerViewModel = Mapper.Map<CustomerViewModel>(customer);

            customerViewModel.Customers = _customerManager.GetAll();
            return View(customerViewModel);
        }

        [HttpPost]
        public ActionResult Edit(CustomerViewModel customerViewModel)
        {
            string message = "";

            //Student student = new Student();
            //student.RollNo = studentViewModel.RollNo;
            //student.Name = studentViewModel.Name;
            //student.Address = studentViewModel.Address;
            //student.Age = studentViewModel.Age;
            //student.DepartmentId = studentViewModel.DepartmentId;

           
                Customer customer = Mapper.Map<Customer>(customerViewModel);

                if (_customerManager.Update(customer))
                {
                    message = "Updated";
                }
                else
                {
                    message = "Not Updated";
                }

           

            ViewBag.Message = message;
            customerViewModel.Customers = _customerManager.GetAll();

            return View(customerViewModel);
        }

        public ActionResult Delete(int id)
        {
            string message = "";
            bool isDeleted = false;

            Customer customer = _customerManager.GetById(id);

            if (customer != null)
            {
                isDeleted = _customerManager.Delete(customer.Id);
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