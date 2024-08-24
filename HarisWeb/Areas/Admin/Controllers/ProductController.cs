using Haris.Models;
using Haris.DataAccess.Data;
using Haris.Models.ViewModels;
using Haris.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Haris.Utility;

namespace HarisWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        //GET the List of Products
        public IActionResult Index()
        {
            /*Using .Include; includeProperties:"Category"*/
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();
            return View(objProductList);
        }

        //CREATE
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll()
                .Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Product()
            };
            if(id == null || id == 0)
            {
                return View(productVM);
            }
            else
            {
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
                return View(productVM);
            }
           
        }

        
        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if(ModelState.IsValid)
            {
                    //access to wwwroot file
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    if(file != null)
                    {
                        //random name for the file
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        //navigate to product path
                        string productPath = Path.Combine(wwwRootPath, @"images\product");

                    /*Upade IMG; Delete Old One and Set New One*/
                    if(!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        //delete the old image
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.Trim('\\'));
                            
                            if(System.IO.File.Exists(oldImagePath))
                             {
                                System.IO.File.Delete(oldImagePath);
                             }
                    }

                        using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                    }

                    /*Check if we need to Add or Update the Img, we check the ID*/
                    if(productVM.Product.Id == 0)
                    {
                        _unitOfWork.Product.Add(productVM.Product);
                    }
                    else
                    {
                        _unitOfWork.Product.Update(productVM.Product);
                    } 
       
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            } 
            else
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(productVM);
            }
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new {data = objProductList});    
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _unitOfWork.Product.Get(u => u.Id == id);
            if(productToBeDeleted == null)
            {
                return Json(new {success =false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath,
                                             productToBeDeleted.ImageUrl.TrimStart('\\'));

            if(System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.Product.Remove(productToBeDeleted);
            _unitOfWork.Save();

            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objProductList });
        }
        #endregion
    }
}
