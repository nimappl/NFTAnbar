using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using NFTAnbarAPI.Models;
using NFTAnbarAPI.Services;

namespace NFTAnbarAPI
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
            services.AddCors();
            services.AddDbContext<NFTAnbarContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<INdepoService, NdepoService>();
            services.AddScoped<INdepoTypeService, NdepoTypeService>();
            services.AddScoped<IBarnameService, BarnameService>();
            services.AddScoped<IContractorService, ContractorService>();
            services.AddScoped<IHavalehService, HavalehService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<INaftkeshService, NaftkeshService>();
            services.AddScoped<INdepoWorkShiftService, NdepoWorkShiftService>();
            services.AddScoped<IPermitTypeService, PermitTypeService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISendTypeService, SendTypeService>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            // app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
