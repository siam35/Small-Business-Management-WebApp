using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Signature.Model.Model;
using Signature.BLL.BLL;
using Signature.Models;
using AutoMapper;
using  Rotativa;

namespace Signature.Controllers
{
    public class CategoryController : Controller
    {
        CategoryManager _categoryManager = new CategoryManager();


        public JsonResult IsCodeExist(string Code, int? Id)
        {
            var validateCode = _categoryManager.GetAll().Where(c => c.Code.ToLower() == Code.ToLower() && c.Id != Id).ToList();


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
            var validateName = _categoryManager.GetAll().Where(c => c.Name.ToLower() == name.ToLower() && c.Id != Id).ToList();
            if (validateName.Count > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult IsCodeExistforEdit(string code,int id)
        {
            var categoryList = _categoryManager.GetAll().Where(c => c.Code.ToLower() == code.ToLower() && c.Id != id).ToList();
            bool isExist = false;
            if (categoryList.Count > 0)
            {
                isExist = true;
            }

            return Json(isExist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult IsNameExistforEdit(string name, int id)
        {
            var categoryList = _categoryManager.GetAll().Where(c => c.Name.ToLower() == name.ToLower() && c.Id != id).ToList();
            bool isExist = false;
            if (categoryList.Count > 0)
            {
                isExist = true;
            }

            return Json(isExist, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Add()
        {
            CategoryViewModel categoryViewModel = new CategoryViewModel();
            categoryViewModel.Categories = _categoryManager.GetAll();

            return View(categoryViewModel);
        }

        [HttpPost]
        public ActionResult Add(CategoryViewModel categoryViewModel)
        {
            string message = "<h3>Category info</h3>";

            if (ModelState.IsValid)
            {
                Category category = Mapper.Map<Category>(categoryViewModel);

                if (_categoryManager.Add(category))
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

            categoryViewModel.Categories = _categoryManager.GetAll();

            return View(categoryViewModel);
        }
        

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Category category = _categoryManager.GetById(id);

            CategoryViewModel categoryViewModel = Mapper.Map<CategoryViewModel>(category);

            categoryViewModel.Categories = _categoryManager.GetAll();

            return View(categoryViewModel);
        }
        [HttpPost]
        public ActionResult Edit(CategoryViewModel categoryViewModel)
        {
            string message = "<h3>Category info</h3>";
           
                Category category = Mapper.Map<Category>(categoryViewModel);

                if (_categoryManager.Update(category))
                {
                    message = "Updated";
                }
                else
                {
                    message = "Not Updated";
                }

         
            

          

            ViewBag.Message = message;

            categoryViewModel.Categories = _categoryManager.GetAll();

            return View(categoryViewModel);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            string message = "";
            bool isDeleted = false;

            Category category = _categoryManager.GetById(id);

            if (category != null)
            {
                isDeleted = _categoryManager.Delete(category.Id);
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

        [HttpGet]
        public ActionResult Search()
        {
            CategoryViewModel categoryViewModel = new CategoryViewModel();

            categoryViewModel.Categories = _categoryManager.GetAll();

            return View(categoryViewModel);
        }
        [HttpPost]
        public ActionResult Search(CategoryViewModel categoryViewModel)
        {
            var categories = _categoryManager.GetAll();

            if (categoryViewModel.Name != null)
            {
                categories = categories.Where(c => c.Name.ToLower().Contains(categoryViewModel.Name.ToLower())).ToList();
            }

            if (categoryViewModel.Code != null)
            {
                categories = categories.Where(c => c.Code.ToLower().Contains(categoryViewModel.Code.ToLower())).ToList();
            }

            categoryViewModel.Categories = categories;

            return View(categoryViewModel);
        }


    }
}