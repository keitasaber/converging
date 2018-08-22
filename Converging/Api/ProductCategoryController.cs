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
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Converging.Api
{
    [RoutePrefix("api/productcategory")]
    [Authorize]
    public class ProductCategoryController : ApiControllerBase
    {
        private IProductCategoryService _productCategoryService;

        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService)
            : base(errorService)
        {
            this._productCategoryService = productCategoryService;
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage requestMessage)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                var model = _productCategoryService.GetAll();

                var listProductCategoryViewModel = AutoMapperConfiguration.Mapping.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
                HttpResponseMessage responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, listProductCategoryViewModel);

                return responseMessage;
            });
        }

        [Route("get")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage requestMessage, string keyword, int page, int pageSize)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                int totalRow = 0;
                var model = _productCategoryService.GetAll(keyword);
                totalRow = model.Count();

                var query = model.OrderBy(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var listProductCategoryViewModel = AutoMapperConfiguration.Mapping.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(query);

                PaginationSet<ProductCategoryViewModel> paginationSet = new PaginationSet<ProductCategoryViewModel>()
                {
                    Items = listProductCategoryViewModel,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)(totalRow * 1.0 / pageSize))
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
                var model = _productCategoryService.GetAll();

                var listProductCategoryViewModel = AutoMapperConfiguration.Mapping.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
                HttpResponseMessage responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, listProductCategoryViewModel);

                return responseMessage;
            });
        }

        [Route("getallparentswithoutnochild")]
        [HttpGet]
        public HttpResponseMessage GetParentsWithoutNoChild(HttpRequestMessage requestMessage)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                var model = _productCategoryService.GetAllParentsWithoutNoChild();

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
                    _productCategoryService.Add(productCategory);
                    _productCategoryService.Save();

                    var responseData = AutoMapperConfiguration.Mapping.Map<ProductCategory, ProductCategoryViewModel>(productCategory);

                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, productCategory);
                }
                return responseMessage;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage requestMessage, ProductCategoryViewModel productCategoryViewModel)
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
                    ProductCategory oldProductCategory = _productCategoryService.GetById(productCategoryViewModel.ID);
                    oldProductCategory.UpdateProductCategory(productCategoryViewModel);
                    oldProductCategory.UpdatedDate = DateTime.Now;
                    _productCategoryService.Update(oldProductCategory);
                    _productCategoryService.Save();

                    var responseData = AutoMapperConfiguration.Mapping.Map<ProductCategory, ProductCategoryViewModel>(oldProductCategory);
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
                ProductCategory productCategory = _productCategoryService.GetById(id);

                var responseData = AutoMapperConfiguration.Mapping.Map<ProductCategory, ProductCategoryViewModel>(productCategory);

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
                    var oldProductCategory = _productCategoryService.Delete(id);
                    _productCategoryService.Save();

                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, oldProductCategory);
                }
                return responseMessage;
            });
        }

        [Route("deletemultiple")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMultiple(HttpRequestMessage requestMessage, string checkedProductCategories)
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
                    var ids = new JavaScriptSerializer().Deserialize<List<int>>(checkedProductCategories);
                    foreach (var id in ids)
                    {
                        var oldProductCategory = _productCategoryService.Delete(id);
                    }
                    _productCategoryService.Save();

                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, ids.Count);
                }
                return responseMessage;
            });
        }
    }
}