using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PrescriptionManagerServices.Models;

namespace PrescriptionManagerServices
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
            services.AddAuthentication(sharedOptions => sharedOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme)
                .AddAzureAdBearer(options => Configuration.Bind("AzureAd", options));
            services.AddDbContext<ContosoMedicalDBContext>(options => options.UseSqlServer("Server=tcp:mjk78restlabs2018prescriptionmanagersqlserver.database.windows.net,1433;Initial Catalog=ContosoMedicalDB;Persist Security Info=False;User ID=PrescriptionManagerService;Password=Strong_password1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));
            services.AddScoped(p => new ContosoMedicalDBContext(p.GetService<DbContextOptions<ContosoMedicalDBContext>>()));
            services.AddMvc()
                .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
