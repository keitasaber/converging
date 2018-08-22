namespace Converging.Data.Migrations
{
    using Converging.Model.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Converging.Data.ConvergingDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Converging.Data.ConvergingDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //CreateProductCategoriesSample(context);

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ConvergingDbContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ConvergingDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "admin",
                Email = "ketiasaber@gmail.com",
                EmailConfirmed = true,
                BirthDay = DateTime.Now,
                FullName = "Technology Education"

            };

            manager.Create(user, "123@123a");

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            var adminUser = manager.FindByEmail("ketiasaber@gmail.com");

            manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
        }

        private void CreateProductCategoriesSample(Converging.Data.ConvergingDbContext context)
        {
            //List<ProductCategory> listProductCategory = new List<ProductCategory>
            //{
            //    new ProductCategory(){ Name="Lập trình", Alias = "lap-trinh", Status = true},
            //    new ProductCategory(){ Name="Thiết kế", Alias = "thiet-ke", Status = true},
            //    new ProductCategory(){ Name="CV", Alias = "cv", Status = true},
            //    new ProductCategory(){ Name="Ngoại ngữ", Alias = "ngoai-ngu", Status = true}
            //};

            //context.ProductCategories.AddRange(listProductCategory);
            //context.SaveChanges();
        }
    }
}
