using CookBook.Domain.Entities;
using CookBook.Domain.Entities.Tree;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();

            services.AddSingleton<Tree<TestClass>>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var tempservie = app.ApplicationServices.GetService<Tree<TestClass>>();
            InitTree(tempservie);


            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            //app.UseHttpsRedirection();


            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void InitTree(Tree<TestClass> tempservie)
        {
            var root = new TestClass(1, 1, 1);
            tempservie.Insert(root, root);
            var node1 = new TestClass(3, 3, 3);
            var node2 = new TestClass(-12, -12, -12);
            var node3 = new TestClass(5, 5, 5);
            var node4 = new TestClass(2, 2, 2);
            var node5 = new TestClass(0, 0, 0);

            tempservie.Insert(root, node1);
            tempservie.Insert(root, node2);
            tempservie.Insert(node2, node3);
            tempservie.Insert(node2, node4);
            tempservie.Insert(node1, node5);
        }
    }
}
