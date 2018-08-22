using Converging.Mappings;
using Converging.Model.Models;
using Converging.Models;
using Converging.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Converging.Controllers
{
    public class ProductController : Controller
    {
        IProductCategoryService _productCategoryService;
        IProductService _productService;

        public ProductController(IProductCategoryService productCategoryService, IProductService productService)
        {
            this._productCategoryService = productCategoryService;
            this._productService = productService;
        }

        public ActionResult Index(int id)
        {            
            var product = _productService.GetById(id);
            ViewBag.ProductCategory = _productCategoryService.GetById(product.CategoryID);
            var productVM = AutoMapperConfiguration.Mapping.Map<Product, ProductViewModel>(product);
            return View(productVM);
        }
    }
}