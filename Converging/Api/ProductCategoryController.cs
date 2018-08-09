using Converging.Infrastructure.Core;
using Converging.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
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
        public HttpResponseMessage Get(HttpRequestMessage requestMessage)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                var listProductCategory = _productCategorySevice.GetAll().ToList();
                HttpResponseMessage responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, listProductCategory);
                return responseMessage;
            });
        }


    }
}