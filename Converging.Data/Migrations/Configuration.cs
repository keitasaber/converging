namespace Converging.Data.Migrations
{
    using Converging.Model.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Converging.Data.ConveringDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Converging.Data.ConveringDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            CreateProductCategoriesSample(context);
        }

        private void CreateProductCategoriesSample(Converging.Data.ConveringDbContext context)
        {
            List<ProductCategory> listProductCategory = new List<ProductCategory>
            {
                new ProductCategory(){ Name="Lập trình", Alias = "lap-trinh", Status = true},
                new ProductCategory(){ Name="Thiết kế", Alias = "thiet-ke", Status = true},
                new ProductCategory(){ Name="CV", Alias = "cv", Status = true},
                new ProductCategory(){ Name="Ngoại ngữ", Alias = "ngoai-ngu", Status = true}
            };

            context.ProductCategories.AddRange(listProductCategory);
            context.SaveChanges();
        }
    }
}
