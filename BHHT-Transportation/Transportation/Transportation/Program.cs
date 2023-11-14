using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Transportation.Filters;
using Transportation.HelpingClasses;
using Transportation.Models;
using Transportation.Repositories;
using Rotativa;
using Rotativa.AspNetCore;
using Microsoft.AspNetCore.SignalR;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("CorsPolicy", builder =>
//        builder.AllowAnyMethod()
//               .AllowAnyHeader()
//               .AllowCredentials()
//               .WithOrigins("http://localhost:7124")); // Add your client's URL here
//});
//builder.Services.AddSignalR();



builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.Configure<ProjectVariables>(builder.Configuration.GetSection("ProjectVariables"));
builder.Services.AddScoped<IUserRepo, UserRepo>();

builder.Services.AddScoped<ExceptionFilter>();
builder.Services.AddScoped<ValidationFilter>();

//builder.Services.AddRotativa();

builder.Services.AddScoped<GeneralPurpose>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(x => {
        x.LoginPath = "/Auth/Login";
        x.ExpireTimeSpan = TimeSpan.FromDays(365);
        x.SlidingExpiration = true;
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

RotativaConfiguration.Setup((Microsoft.AspNetCore.Hosting.IHostingEnvironment)app.Environment);

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();
//app.UseRotativa();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

// SignalR dependency
//app.MapHub<Notification_Hub>("/notification_Hub");
//app.UseEndpoints(endpoints => {
//    endpoints.MapHub<NotificationHub>("/notificationHub");
//    endpoints.MapControllers();
//});

app.UseStaticFiles();
app.Run();
