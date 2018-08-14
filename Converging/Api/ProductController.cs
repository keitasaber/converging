using Converging.Common;
using Converging.Infrastructure.Core;
using Converging.Infrastructure.Extentions;
using Converging.Mappings;
using Converging.Model.Models;
using Converging.Models;
using Converging.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
namespace Converging.Api
{
    [RoutePrefix("api/product")]
    public class ProductController : ApiControllerBase
    {
        private IProductService _productService;
        public ProductController(IErrorService errorService, IProductService productService)
            : base(errorService)
        {
            this._productService = productService;
        }

        [Route("get")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage requestMessage, string keyword, int page, int pageSize)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                int totalRow = 0;
                var model = _productService.GetAll(keyword);
                totalRow = model.Count();

                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var listProductViewModel = AutoMapperConfiguration.Mapping.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(query);

                PaginationSet<ProductViewModel> paginationSet = new PaginationSet<ProductViewModel>()
                {
                    Items = listProductViewModel,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)(totalRow * 1.0 / pageSize))
                };
                HttpResponseMessage responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, paginationSet);
                return responseMessage;
            });
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage requestMessage, ProductViewModel productViewModel)
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
                    if (ArrayHelper.IsDuplicateTag(productViewModel.Tags))
                    {
                        return responseMessage = requestMessage.CreateResponse(HttpStatusCode.BadRequest, CommonConstants.DUPLICATE_TAG);
                    }

                    Product product = AutoMapperConfiguration.Mapping.Map<ProductViewModel, Product>(productViewModel);
                    _productService.Add(product);
                    _productService.Save();

                    var responseData = AutoMapperConfiguration.Mapping.Map<Product, ProductViewModel>(product);

                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, product);
                }
                return responseMessage;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage requestMessage, ProductViewModel productViewModel)
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
                    if (ArrayHelper.IsDuplicateTag(productViewModel.Tags))
                    {
                        return responseMessage = requestMessage.CreateResponse(HttpStatusCode.BadRequest, CommonConstants.DUPLICATE_TAG);
                    }

                    Product oldProduct = _productService.GetById(productViewModel.ID);
                    oldProduct.UpdateProduct(productViewModel);
                    oldProduct.UpdatedDate = DateTime.Now;
                    _productService.Update(oldProduct);
                    _productService.Save();

                    var responseData = AutoMapperConfiguration.Mapping.Map<Product, ProductViewModel>(oldProduct);
                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, responseData);
                }
                return responseMessage;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage requestMessage, int id)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                HttpResponseMessage responseMessage = null;
                Product product = _productService.GetById(id);

                var responseData = AutoMapperConfiguration.Mapping.Map<Product, ProductViewModel>(product);

                responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, responseData);

                return responseMessage;
            });
        }

        [Route("delete")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage Delete(HttpRequestMessage requestMessage, int id)
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
                    var oldProduct = _productService.Delete(id);
                    _productService.Save();

                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, oldProduct);
                }
                return responseMessage;
            });
        }

        [Route("deletemultiple")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMultiple(HttpRequestMessage requestMessage, string checkedProducts)
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
                    var ids = new JavaScriptSerializer().Deserialize<List<int>>(checkedProducts);
                    foreach (var id in ids)
                    {
                        var oldProductCategory = _productService.Delete(id);
                    }
                    _productService.Save();

                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, ids.Count);
                }
                return responseMessage;
            });
        }
    }
}