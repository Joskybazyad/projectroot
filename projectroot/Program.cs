using AutoMapper;
using BLL.Profiles;
using BLL.Services.AttachmentService;
using BLL.Services.Classes;
using BLL.Services.Interfaces;
using DAL.Data;
using DAL.Data.Repositries.Classes;
using DAL.Data.Repositries.Interfacies;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace projectroot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
            //builder.Services.AddScoped<AppDBContext>(); // Allow DI For AppDBcontext
            builder.Services.AddDbContext<AppDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                options.UseLazyLoadingProxies();
            }); 
            builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentService,DepartmentService>();
            builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>();
            //builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IAttachmentService, AttachmentService>();
            //builder.Services.AddScoped<UserManager<ApplicationUser>>();
            //builder.Services.AddScoped<SignInManager<ApplicationUser>>();
            //builder.Services.AddScoped<RoleManager<IdentityRole>>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options=>
                {
                    //options.User.RequireUniqueEmail = true;
                    //options.Password.RequireUppercase = true;
                    //options.Password.RequireLowercase = true;
            }).AddEntityFrameworkStores<AppDBContext>().AddDefaultTokenProviders();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Register}/{id?}");

            app.Run();
        }
    }
}
