using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.Configuration;
using EstoqueWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace EstoqueWeb
{
    public class Startup
    {
        /* definindo um objeto de configuração no startup */
        public IConfiguration Configuration {get;}

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            // adicionando runtimecompilation do Razor
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            // adicionando o modulo de sqlite na aplicação. E definindo o nome do BD
            services.AddDbContext<EstoqueWebContext>(options =>
            options.UseSqlite(Configuration.GetConnectionString("EstoqueWebContext")));

            /* NOTA: sqlite so serve para banco de dados pequenos. Para grandes bancos nao é recomendado */
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints( endpoints => {
                // define mapeamento de endpoints com ids opcionais para controllers e actions 
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
