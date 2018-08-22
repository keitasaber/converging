using Converging.Mappings;
using Converging.Model.Models;
using Converging.Models;
using Converging.Service;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using Converging.Common;

namespace Converging.Controllers
{
    public class ProductCategoryController : Controller
    {
        IProductCategoryService _productCategoryService;
        IProductService _productService;

        public ProductCategoryController(IProductCategoryService productCategoryService, IProductService productService)
        {
            this._productCategoryService = productCategoryService;
            this._productService = productService;
        }

        // GET: ProductCategory
        public ActionResult Index(int id)
        {
            var category = _productCategoryService.GetById(id);
            ViewData["CategoryName"] = category.Name;
            var productList = _productService.GetByCategoryID(id);
            IEnumerable<ProductViewModel> productVMList = AutoMapperConfiguration.Mapping.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productList);            
            return View(productVMList);
        }
    }
}