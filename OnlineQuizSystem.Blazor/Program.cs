using Microsoft.EntityFrameworkCore;
using OnlineQuiz.Repository;
using OnlineQuiz.Repository.Repositories;
using OnlineQuiz.Repository.UnitOfWork;
using OnlineQuiz.Service.Service;
using OnlineQuiz.Service.Service.IService;
using OnlineQuizSystem.Blazor.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<OnlineQuizSystemContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IQuizService, QuizService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IPlayerAnswerService, PlayerAnswerService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IQuestionInRoomService, QuestionInRoomService>();
builder.Services.AddScoped<IRoomService, RoomService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
