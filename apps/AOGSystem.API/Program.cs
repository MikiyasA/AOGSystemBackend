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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(option => { option.AddDefaultPolicy(policy => { 
    policy.AllowAnyOrigin();
    policy.AllowAnyHeader();
    policy.AllowAnyMethod();
}); }); // "http://localhost:3000" TODO - check cors origin for PUT
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AOGSystemContext>(options =>
{    
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 34))
        );
});


// Configure JWT authentication
var key = Encoding.ASCII.GetBytes(builder.Configuration["JwtSettings:Secret"]);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
    };

});

// Add Authorization policies if needed
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    // Add more policies as needed
});


builder.Services.AddIdentity<User, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<AOGSystemContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<UserManager<User>>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();



builder.Services.AddScoped<IAOGFollowUpRepository, AOGFollowUpRepository>();
builder.Services.AddScoped<IFollowUpTabsRepository, FollowUpTabsRepository>();
builder.Services.AddScoped<IActiveAOGFollowupQuery, ActiveAOGFollowupQuery>();
builder.Services.AddScoped<IRemarkRepository, RemarkRepository>();
builder.Services.AddScoped<IPartRepository, PartRepository>();
builder.Services.AddScoped<IQuotationRepository, QuotationRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICoreFollowUpRepository, CoreFollowUpRepository>();
builder.Services.AddScoped<IAssignmentRepository, AssignmentRepository>();
builder.Services.AddScoped<IJwtService, JwtService>();


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


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
