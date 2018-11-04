using HLTAT.Business.Factory;
using HLTAT.Business.Service.DI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Compression;

namespace HLTAT
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc();

            services.Configure<GzipCompressionProviderOptions>(option => option.Level = CompressionLevel.Optimal);
            services.AddResponseCompression(option =>
            {
                option.EnableForHttps = true;
                option.Providers.Add<GzipCompressionProvider>();
            });


            services.AddDbContext<ProductContext>(option => option.UseInMemoryDatabase("HLTAT"));

            services.RegisterProjectServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseResponseCompression();

            app.UseMvc();
        }
    }
}
