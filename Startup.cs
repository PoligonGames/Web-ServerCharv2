
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Threading.Tasks;
using WebServer.Services;

namespace WebServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<EngineManager>();
            services.AddDbContextPool<MainDbContext>(options => options.UseSqlServer(Configuration.GetSection("ConnectionStrings")["DefaultConnection"]));
            services.AddIdentity<PlayerUser, IdentityRole>().AddEntityFrameworkStores<MainDbContext>();
            services.AddScoped<IEngineManagaer,EngineManager>();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
            });

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Login Server", Version = "v2" });
            });

            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LoginServer v2"));


            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");
            });

            CreateDefaultAdminRoleAndAdminUser(services).Wait();
        }

        private async Task CreateDefaultAdminRoleAndAdminUser(IServiceProvider serviceProvider)
        {
            RoleManager<IdentityRole> RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            UserManager<PlayerUser> UserManager = serviceProvider.GetRequiredService<UserManager<PlayerUser>>();
            
            string loginForNewAdmin = "Administrator";
            string emailForNewAdmin = "admin@mail.ru";
            string passForNewAdmin = "grefdsrthdfgrbd45";

            bool roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck)
            {
                await RoleManager.CreateAsync(new IdentityRole("Admin"));
            }

            PlayerUser userCheck = await UserManager.FindByEmailAsync(emailForNewAdmin);
            if (userCheck == null)
            {
                PlayerUser newAdminUser = new PlayerUser { UserName = loginForNewAdmin, Email = emailForNewAdmin  };
                await UserManager.CreateAsync(newAdminUser, passForNewAdmin);
                await UserManager.AddToRoleAsync(newAdminUser, "Admin");
            }
        }
    }
}