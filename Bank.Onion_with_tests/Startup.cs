using Bank.Core.Bank;
using Bank.Core.Clients;
using Bank.Data;
using Bank.Data.Bank;
using Bank.Data.Client;
using Bank.Orchestrators.Bank;
using Bank.Orchestrators.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Bank.Onion_with_tests
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {

            string connString = Configuration.GetConnectionString("BankDB");
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connString));
            services.AddMvc(option =>
            {
                option.EnableEndpointRouting = false;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Spendings Api",
                    Description = ""
                });
            });
            services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(type => type.ToString());
            });
            services.AddControllers();
            services.AddAutoMapper(typeof(OrchBankProfile), typeof(BankOrchProfile), typeof(BankDaoProfile), typeof(DaoBankProfile)
                ,typeof(ClientDaoProfile),typeof(OrchClientProfile),typeof(DaoClientProfile),typeof(ClientOrchProfile));

            services.AddScoped<IBankRepository, BankRepository>();
            services.AddScoped<IBankService, BankService>();
            services.AddScoped<IClientsService, ClientService>();
            services.AddScoped<IClientsRepository, ClientRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //env.EnvironmentName = "Production";
           
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Spendings API V1");
            });

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
