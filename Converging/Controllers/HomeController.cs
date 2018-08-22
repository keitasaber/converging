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
    public class HomeController : Controller
    {
        IProductCategoryService _productCategoryService;
        IFooterService _footerService;
        IProductService _productService;
        public HomeController(IProductCategoryService productCategoryService, IFooterService footerService, IProductService productService)
        {
            this._productCategoryService = productCategoryService;
            this._footerService = footerService;
            this._productService = productService;
        }

        // GET: Home
        public ActionResult Index()
        {
            var showHomeProductList = _productService.GetAll().Where(x => x.HomeFlag == true);
            var hotProductList = _productService.GetAll().Where(x => x.HotFlag == true);
            ViewBag.ShowHomeProductVMList = AutoMapperConfiguration.Mapping.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(showHomeProductList);
            ViewBag.HotProductVMList = AutoMapperConfiguration.Mapping.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(hotProductList);

            return View();
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            var footer = _footerService.GetFooter();
            var footerVM = AutoMapperConfiguration.Mapping.Map<Footer, FooterViewModel>(footer);
            return PartialView(footerVM);
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult ProductCategory()
        {
            var listProductCategory = _productCategoryService.GetAll();
            var listProductCategoryVM = AutoMapperConfiguration.Mapping.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(listProductCategory);
            return PartialView(listProductCategoryVM);
        }

    }
}