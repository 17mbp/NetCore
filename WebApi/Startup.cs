using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Northwind.BusinessLogic.Implementations;
using Northwind.BusinessLogic.Interfaces;
using Northwind.DataAccess;
using Northwind.UnitOfWork;
using WebApi.Authentication;
using WebApi.ErrorHandling;
namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ISupplierLogic, SupplierLogic>();
            services.AddTransient<ICustomerLogic, CustomerLogic>();
            services.AddTransient<IOrderLogic, OrderLogic>();
            services.AddTransient<ITokenLogic, Token>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
#if DEBUG
            services.AddSingleton<IUnityOfWork>(ap => new
                  NorthwindUnitOfWork(Configuration.GetConnectionString("Northwind")));
#else
            services.AddSingleton<IUnityOfWork>(ap => new
                  NorthwindUnitOfWork(Configuration.GetConnectionString("Production")));
#endif
            var tokenprovider = new JwtProvider("issuer", "audience", "northwind_2000");
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:51515",
                                        "http://localhost:4200")
                                        .AllowAnyHeader()
                                        .AllowAnyMethod()
                                        .AllowAnyOrigin()
                                        .AllowCredentials();
                });
            });
            services.AddSingleton<ITokenProvider>(tokenprovider);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = tokenprovider.GetValidationParameters();
                });
            services.AddAuthorization(aut =>
            {
                aut.DefaultPolicy = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser().Build();
            });  
            services.AddMvc();
        }                
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {    //https://www.c-sharpcorner.com/article/enabling-cors-in-asp-net-core-api-application/
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.ConfigureExceptionHandler();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseMvc();
        }
    }
}