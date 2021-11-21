using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyEdo.Business.Services.AppAdmin;
using MyEdo.Business.Services.AppSkill;
using MyEdo.Business.Services.AppSkillCategory;
using MyEdo.Business.Services.AppTraining;
using MyEdo.Business.Services.AppUser;
using MyEdo.Core.Models;
using MyEdo.Data;
using MyEdo.Data.Seeding;
using MyEdo.Web.ApiModels.SkillCategories;
using MyEdo.Web.ApiModels.Skills;
using MyEdo.Web.ApiModels.Trainings;
using System;

namespace MyEdo
{
    public class Startup
    {
        private readonly IWebHostEnvironment currentEnv;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            currentEnv = env;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (this.currentEnv.EnvironmentName == "Integration")
            {
                services.AddDbContext<MyEdoDbContext>(options => 
                options.UseInMemoryDatabase(
                    databaseName: Guid.NewGuid().ToString())
                );
            }
            else
            {
                services.AddDbContext<MyEdoDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

                services.AddDatabaseDeveloperPageExceptionFilter();
            }

            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(new[] { typeof(Startup) });
                cfg.CreateMap<CategoryApiModel, SkillCategory>();
                cfg.CreateMap<SkillApiModel, Skill>();
                cfg.CreateMap<TrainingApiModel, Training>();
                cfg.CreateMap<TrainingBaseApiModel, UserTraining>();
                cfg.CreateMap<AddUserTrainingApiModel, UserTraining>();
                cfg.CreateMap<SkillProfileApiModel, UserSkill>();
            });

            IMapper mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);

            services.AddDefaultIdentity<User>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
                .AddRoles<UserRole>()
                .AddEntityFrameworkStores<MyEdoDbContext>();

            if (this.currentEnv.EnvironmentName != "Integration")
            {
                services.AddIdentityServer()
                        .AddApiAuthorization<User, MyEdoDbContext>(); 
            }

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
            })
                .AddIdentityServerJwt();
            services.AddControllersWithViews();
            services.AddRazorPages();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ISkillService, SkillService>();
            services.AddTransient<ISkillCategoryService, SkillCategoryService>();
            services.AddTransient<ITrainingService, TrainingService>();
            services.AddTransient<IAdminService, AdminService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyEdo API", Version = "v1" });
               
            });

            services.AddControllersWithViews().AddNewtonsoftJson(options =>
                     options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<MyEdoDbContext>();

                if (env.IsDevelopment())
                {
                    dbContext.Database.Migrate();
                }

                new MyEdoDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyEdo API V1");

                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseAuthentication();
            if (this.currentEnv.EnvironmentName != "Integration")
            {
                app.UseIdentityServer(); 
            }
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
