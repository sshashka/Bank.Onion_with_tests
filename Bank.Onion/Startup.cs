using System.Linq;
using Bank.Core.Banks;
using Bank.Core.Clients;
using Bank.Data;
using Bank.Data.Banks;
using Bank.Data.Clients;
using Bank.Orchestrators.Banks;
using Bank.Orchestrators.Clients;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Bank.Onion
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
            services.AddAutoMapper(typeof(BankDaoProfile),  typeof(ClientDaoProfile),
                typeof(BankOrchProfile), typeof(ClientOrchProfile));
            string connString = "Host=localhost;Port=5432;Database=BankDb;Username=postgres;Password=159753";
            services.AddDbContext<BankContext>(options => options.UseNpgsql(connString));

            services.AddScoped<IBankRepository, BankRepository>();
            services.AddScoped<IBankService, BankService>();

            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IClientRepository, ClientRepository>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bank.Onion", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bank.Onion v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
