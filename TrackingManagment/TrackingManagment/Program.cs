using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using TrackingManagment;
using TrackingManagment.Endpoints;
using TrackingManagment.Identity;
using TrackingManagment.Models;
using TrackingManagment.Models.DTO.DTOMapping;
using TrackingManagment.Repository;
using TrackingManagment.Services;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("conStr"));
});



//cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "thisismypolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IUserService, UserServices>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IInviteUserRepository,InviteUseRepository >();
builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddScoped<ITrackingRepository,TrackingRepository>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddAuthorization(); // Add this line to include the required authorization service
//Jwt services

var appSettingSection = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(appSettingSection);
var appsetting = appSettingSection.Get<JwtSettings>();
var key = Encoding.ASCII.GetBytes(appsetting.SecretKey);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{

    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
});

builder.Services.AddAuthorization();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Endpoints grouped in Endpoints folder
app.UserEndPoints();

app.MapGroup("/minimalAPI")
   .LoginRegisterAPI()
    .WithTags("Services");

app.MapGroup("/minimalAPI")
    .INVITATION_API()
    .WithTags("Invitation");

/*app.MapGroup("/minimalAPI")
    .TrackingUserAPI();
*/
app.UseHttpsRedirection();
//cors
app.UseCors("thisismypolicy");


app.Run();app.UseAuthentication();
app.UseAuthorization();


