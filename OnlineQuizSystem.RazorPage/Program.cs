using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using OnlineQuiz.Repository;
using OnlineQuiz.Repository.Repositories;
using OnlineQuiz.Repository.UnitOfWork;
using OnlineQuiz.Service.Service;
using OnlineQuiz.Service.Service.IService;
using OnlineQuizSystem.RazorPage.Hubs;
using OnlineQuizSystem.RazorPage.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSignalR();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login"; // Đường dẫn tới trang đăng nhập
        options.AccessDeniedPath = "/Auth/AccessDenied"; // Trang bị từ chối truy cập nếu không có quyền
        options.ExpireTimeSpan = TimeSpan.FromDays(7); // Thời gian hết hạn cookie
    });

builder.Services.AddDbContext<OnlineQuizSystemContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IQuizService, QuizService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapHub<SignalRServer>("/signalRServer");
app.MapRazorPages();

app.MapGet("/", async context =>
{
    context.Response.Redirect("/Auth/Login");
});

app.Run();
