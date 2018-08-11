using Converging.Infrastructure.Core;
using Converging.Mappings;
using Converging.Model.Models;
using Converging.Models;
using Converging.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Converging.Api
{
    [RoutePrefix("api/productcategory")]
    public class ProductCategoryController : ApiControllerBase
    {
        private IProductCategoryService _productCategorySevice;

        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService)
            : base(errorService)
        {
            this._productCategorySevice = productCategoryService;
        }

        [Route("get")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage requestMessage, string keyword, int page, int pageSize)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                int totalRow = 0;
                var model = _productCategorySevice.GetAll(keyword);
                totalRow = model.Count();

                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var listProductCategoryViewModel = AutoMapperConfiguration.Mapping.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(query);


                PaginationSet<ProductCategoryViewModel> paginationSet = new PaginationSet<ProductCategoryViewModel>()
                {
                    Items = listProductCategoryViewModel,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)(totalRow*1.0 / pageSize))
                };
                HttpResponseMessage responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, paginationSet);
                return responseMessage;
            });
        }

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage requestMessage)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
              
                var model = _productCategorySevice.GetAll();

                var listProductCategoryViewModel = AutoMapperConfiguration.Mapping.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
                HttpResponseMessage responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, listProductCategoryViewModel);

                return responseMessage;
            });
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage requestMessage, ProductCategoryViewModel productCategoryViewModel)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                HttpResponseMessage responseMessage = null;
                if (!ModelState.IsValid)
                {
                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    
                    ProductCategory productCategory = AutoMapperConfiguration.Mapping.Map<ProductCategoryViewModel, ProductCategory>(productCategoryViewModel);
                    _productCategorySevice.Add(productCategory);
                    _productCategorySevice.Save();

                    var responseData = AutoMapperConfiguration.Mapping.Map<ProductCategory, ProductCategoryViewModel>(productCategory);

                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, productCategory);
                }
                return responseMessage;
            });
        }
    }
}