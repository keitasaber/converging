using Converging.Data.Infrastructure;
using Converging.Data.Repositories;
using Converging.Model.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converging.Tests.RepsitoryTest
{
    [TestClass]
    public class ProductCategoryRepositoryTest
    {
        private IDbFactory dbFactory;
        private IProductCategoryRepository productCategoryRepository;
        private IUnitOfWork unitOfWork;

        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            productCategoryRepository = new ProductCategoryRepository(dbFactory);
            unitOfWork = new UnitOfWork(dbFactory);
        }

        [TestMethod]
        public void ProductCategory_Repository_Test()
        {
            ProductCategory productCategory = new ProductCategory
            {
                Name = "Test product category",
                Alias = "Test-product-category",
                Status = true
            };

            var result = productCategoryRepository.Add(productCategory);

            unitOfWork.Commit();

            Assert.AreEqual(1, result.ID);
            Assert.IsNotNull(result);
        }
    }
}
