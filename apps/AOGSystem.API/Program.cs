using AOGSystem.API.Authorization;
using AOGSystem.Application;
using AOGSystem.Application.FollowUp.Query;
using AOGSystem.Application.Invoice.Query;
using AOGSystem.Domain.CoreFollowUps;
using AOGSystem.Domain.CostSavings;
using AOGSystem.Domain.FollowUp;
using AOGSystem.Domain.General;
using AOGSystem.Domain.Invoices;
using AOGSystem.Domain.Loans;
using AOGSystem.Domain.Quotation;
using AOGSystem.Domain.Sales;
using AOGSystem.Domain.SOA;
using AOGSystem.Persistence;
using AOGSystem.Persistence.EntityConfigurations.Attachmetns;
using AOGSystem.Persistence.Repository.Attachements;
using AOGSystem.Persistence.Repository.CoreFollowUps;
using AOGSystem.Persistence.Repository.FollowUp;
using AOGSystem.Persistence.Repository.General;
using AOGSystem.Persistence.Repository.Invoices;
using AOGSystem.Persistence.Repository.Loans;
using AOGSystem.Persistence.Repository.Quotation;
using AOGSystem.Persistence.Repository.Sales;
using AOGSystem.Persistence.Repository.SOA;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(option => { option.AddDefaultPolicy(policy => { 
    policy.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
}); }); // "http://localhost:3000" 
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AOGSystemContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("SqlServerConnection");
    options.UseSqlServer(connectionString,
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure();
        });
    //options.UseMySql(builder.Configuration.GetConnectionString("MysqlServerConnection"),
    //    new MySqlServerVersion(new Version(8, 0, 34))
    //    );
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

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminOrCoordinatorOrTLOrFinanceRole", policy => policy.Requirements.Add(new RoleRequirement("Admin", "Coordinator", "TL", "Finance")));
    options.AddPolicy("RequireAdminOrCoordinatorOrTLRole", policy => policy.Requirements.Add(new RoleRequirement("Admin", "Coordinator", "TL")));
    options.AddPolicy("RequireAdminOrCoordinatorRole", policy => policy.Requirements.Add(new RoleRequirement("Admin", "Coordinator")));
    options.AddPolicy("RequireAdminOrTLRole", policy => policy.Requirements.Add(new RoleRequirement("Admin", "TL")));
    options.AddPolicy("RequireAdminOrManagementRole", policy => policy.Requirements.Add(new RoleRequirement("Admin", "Management")));
    options.AddPolicy("RequireAdminOrBuyerRole", policy => policy.Requirements.Add(new RoleRequirement("Admin", "Buyer")));
    options.AddPolicy("RequireAdminOrFinanceRole", policy => policy.Requirements.Add(new RoleRequirement("Admin", "Finance")));
    options.AddPolicy("RequireAdminRole", policy => policy.Requirements.Add(new RoleRequirement("Admin")));

});
builder.Services.AddSingleton<IAuthorizationHandler, RoleAuthorizationHandler>();


builder.Services.AddIdentity<User, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<AOGSystemContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<UserManager<User>>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers()
    .AddNewtonsoftJson(o => o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


builder.Services.AddScoped<IActiveAOGFollowupQuery, ActiveAOGFollowupQuery>();
builder.Services.AddScoped<IInvoiceQuery, InvoiceQuery>();

builder.Services.AddScoped<IAOGFollowUpRepository, AOGFollowUpRepository>();
builder.Services.AddScoped<IFollowUpTabsRepository, FollowUpTabsRepository>();
builder.Services.AddScoped<IRemarkRepository, RemarkRepository>();
builder.Services.AddScoped<IPartRepository, PartRepository>();
builder.Services.AddScoped<IQuotationRepository, QuotationRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICoreFollowUpRepository, CoreFollowUpRepository>();
builder.Services.AddScoped<IAssignmentRepository, AssignmentRepository>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<ISalePartListRepository, SalePartListRepository>();
builder.Services.AddScoped<ILoanRepository, LoanRepository>();
builder.Services.AddScoped<ILoanPartListRepository, LoanPartListRepository>();
builder.Services.AddScoped<IOfferRepository, OfferRepository>();
builder.Services.AddScoped<IVendorRepository, VendorRepository>();
builder.Services.AddScoped<IInvoiceListRepository, InvoiceListRepository>();
builder.Services.AddScoped<ICostSavingRepository, CostSavingRepository>();
builder.Services.AddScoped<IAttachementRepository, AttachementRepository>();


builder.Services.AddMediatR(typeof(ApplicationModule).GetTypeInfo().Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DocExpansion(DocExpansion.None);
    });
}

app.UseCors();

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
