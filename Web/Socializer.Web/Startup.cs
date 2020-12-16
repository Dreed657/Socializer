namespace Socializer.Web
{
    using System.Reflection;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Socializer.Data;
    using Socializer.Data.Common;
    using Socializer.Data.Common.Repositories;
    using Socializer.Data.Models;
    using Socializer.Data.Repositories;
    using Socializer.Data.Seeding;
    using Socializer.Services;
    using Socializer.Services.Data.Groups;
    using Socializer.Services.Data.Images;
    using Socializer.Services.Data.Posts;
    using Socializer.Services.Data.Users;
    using Socializer.Services.Mapping;
    using Socializer.Services.Messaging;
    using Socializer.Web.Areas.Admin.Services.Common;
    using Socializer.Web.Areas.Admin.Services.Groups;
    using Socializer.Web.Areas.Admin.Services.Users;
    using Socializer.Web.Areas.Messenger.Services;
    using Socializer.Web.Hubs;
    using Socializer.Web.ViewModels.Common;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(this.configuration);

            services.AddDbContext<ApplicationDbContext>(
                options =>
                {
                    options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection"));
                    options.UseLazyLoadingProxies();
                });

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // services.AddAuthentication()
            //   .AddFacebook(facebookOptions =>
            //   {
            //       facebookOptions.AppId = this.configuration["ExternalAuth:Facebook:AppId"];
            //       facebookOptions.AppSecret = this.configuration["ExternalAuth:Facebook:AppSecret"];
            //   })
            //   .AddGoogle(googleOptions =>
            //   {
            //       googleOptions.ClientId = this.configuration["ExternalAuth:Google:ClientId"];
            //       googleOptions.ClientSecret = this.configuration["ExternalAuth:Google:ClientSecret"];
            //   })
            //   .AddTwitter(twitterOptions =>
            //   {
            //       twitterOptions.ConsumerKey = this.configuration["ExternalAuth:Twitter:ApiKey"];
            //       twitterOptions.ConsumerSecret = this.configuration["ExternalAuth:Twitter:ApiSecretKey"];
            //       twitterOptions.RetrieveUserDetails = true;
            //   })
            //   .AddMicrosoftAccount(microsoftOptions =>
            //   {
            //       microsoftOptions.ClientId = this.configuration["ExternalAuth:Microsoft:ClientId"];
            //       microsoftOptions.ClientSecret = this.configuration["ExternalAuth:Microsoft:ClientSecret"];
            //   });
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddControllersWithViews(
                options =>
                    {
                        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    }).AddRazorRuntimeCompilation();
            services.AddRazorPages();

            var cloudinaryAccount = new CloudinaryDotNet.Account(
                this.configuration["Cloudinary:CloudName"],
                this.configuration["Cloudinary:ApiKey"],
                this.configuration["Cloudinary:ApiSecret"]);
            var cloudinary = new Cloudinary(cloudinaryAccount);
            services.AddSingleton(cloudinary);

            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IEmailSender>(x => new SendGridEmailSender(this.configuration["SendGrid:ApiKey"]));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();
            services.AddTransient<TimeService>();

            // Data services
            services.AddTransient<IPostsService, PostsService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<IMessengerService, MessengerService>();
            services.AddTransient<IImagesService, ImagesService>();

            // Dashboard services
            services.AddTransient<IDashboardService, DashboardService>();
            services.AddTransient<IAdminUsersService, AdminUsersService>();
            services.AddTransient<IAdminGroupsService, AdminGroupsService>();

            services.AddSignalR();
            services.AddApplicationInsightsTelemetry();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
