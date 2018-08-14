using Converging.Common;
using Converging.Data.Infrastructure;
using Converging.Data.Repositories;
using Converging.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converging.Service
{
    public interface IProductService
    {
        Product Add(Product product);

        void Update(Product product);

        Product Delete(int id);

        IEnumerable<Product> GetAll();

        IEnumerable<Product> GetAll(string keyword);

        Product GetById(int id);

        void Save();
    }
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private ITagRepository _tagRepository;
        private IProductTagRepository _productTagRepository;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, ITagRepository tagRepository, IProductTagRepository productTagRepository)
        {
            this._productRepository = productRepository;
            this._unitOfWork = unitOfWork;
            this._tagRepository = tagRepository;
            this._productTagRepository = productTagRepository;
        }

        public Product Add(Product product)
        {
            var addedProduct = this._productRepository.Add(product);
            _unitOfWork.Commit();
            if (!string.IsNullOrEmpty(product.Tags))
            {
                string[] tags = product.Tags.Split(',');
                for(int i = 0; i < tags.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]);
                    if (_tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tag = new Tag()
                        {
                            ID = tagId,
                            Name = tags[i],
                            Type = CommonConstants.ProductTag
                        };
                        _tagRepository.Add(tag);
                    }

                    ProductTag productTag = new ProductTag()
                    {
                        ProductID = addedProduct.ID,
                        TagID = tagId
                    };

                    _productTagRepository.Add(productTag);
                }
            }
            return addedProduct;
        }

        public void Update(Product product)
        {
            this._productRepository.Update(product);
            if (!string.IsNullOrEmpty(product.Tags))
            {
                _productTagRepository.DeleteMulti(x => x.ProductID == product.ID);
                string[] tags = product.Tags.Split(',');

                for (int i = 0; i < tags.Length; i++)
                {
                    string tagName = tags[i];

                    if (_tagRepository.Count(x => x.ID == tagName) == 0)
                    {
                        Tag tag = new Tag()
                        {
                            ID = tagName,
                            Name = tagName,
                            Type = CommonConstants.ProductTag
                        };
                        _tagRepository.Add(tag);
                    }

                    ProductTag productTag = new ProductTag()
                    {
                        ProductID = product.ID,
                        TagID = tagName
                    };

                    _productTagRepository.Add(productTag);
                }
            }
            _unitOfWork.Commit();
        }

        public Product Delete(int id)
        {
            _productTagRepository.DeleteMulti(x => x.ProductID == id);
            _unitOfWork.Commit();
            return this._productRepository.Delete(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return this._productRepository.GetAll();
        }

        public IEnumerable<Product> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return this._productRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _productRepository.GetAll();
        }

        public Product GetById(int id)
        {
            return this._productRepository.GetSingleById(id);
        }

        public void Save()
        {
            this._unitOfWork.Commit();
        }

    }
}
