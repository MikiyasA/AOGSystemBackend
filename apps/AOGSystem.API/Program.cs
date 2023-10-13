using AOGSystem.Application;
using AOGSystem.Application.FollowUp.Query;
using AOGSystem.Domain.CoreFollowUps;
using AOGSystem.Domain.FollowUp;
using AOGSystem.Domain.General;
using AOGSystem.Domain.Quotation;
using AOGSystem.Persistence;
using AOGSystem.Persistence.Repository.CoreFollowUps;
using AOGSystem.Persistence.Repository.FollowUp;
using AOGSystem.Persistence.Repository.General;
using AOGSystem.Persistence.Repository.Quotation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(option => { option.AddDefaultPolicy(policy => { 
    policy.AllowAnyOrigin();
    policy.AllowAnyHeader();
    policy.AllowAnyMethod();
}); }); // "http://localhost:3000" TODO - check cors origin for PUT
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AOGSystemContext>(options =>
{
    //builder.Configuration.AddJsonFile("appsettings.json"); // Load appsettings.json
    //builder.Configuration.AddJsonFile("appsettings.Development.json", true); // Load appsettings.Development.json
    
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 34))
        );
});

builder.Services.AddScoped<IAOGFollowUpRepository, AOGFollowUpRepository>();
builder.Services.AddScoped<IActiveAOGFollowupQuery, ActiveAOGFollowupQuery>();
builder.Services.AddScoped<IRemarkRepository, RemarkRepository>();
builder.Services.AddScoped<IPartRepository, PartRepository>();
builder.Services.AddScoped<IQuotationRepository, QuotationRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICoreFollowUpRepository, CoreFollowUpRepository>();


builder.Services.AddMediatR(typeof(ApplicationModule).GetTypeInfo().Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();
