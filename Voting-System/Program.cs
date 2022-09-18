using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text;
using Voting_System.Application.Interfaces;
using Voting_System.Application.JWTUtil;
using Voting_System.Application.Models.CandidateDto;
using Voting_System.Application.Models.FluentValidation.CandidateValidators;
using Voting_System.Application.Models.FluentValidation.UserValidators;
using Voting_System.Application.Models.MailDto;
using Voting_System.Application.Models.UserDto;
using Voting_System.Application.Services;
using Voting_System.Infrastructure.Contexts;
using Voting_System.Infrastructure.Interfaces;
using Voting_System.Infrastructure.Repositories;
using Voting_System.Mapper;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ICandidateRepository, CandidateRepository>();
builder.Services.AddTransient<ICandidateService, CandidateService>();

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.AddAutoMapper(typeof(CandidateProfile));

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddSingleton<IMailService, MailService>();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddScoped<IJwtUtils, JwtUtils>();


builder.Services.AddDbContext<EFContext>(
      options => options.UseSqlServer(builder.Configuration.GetConnectionString("DBString")),
      ServiceLifetime.Transient,
      ServiceLifetime.Transient);

builder.Services.AddControllers();

builder.Services.AddScoped<IValidator<UserRequestDto>, UserRequestDtoValidator>();
builder.Services.AddScoped<IValidator<UserPatchDto>, UserPatchDtoValidator>();

builder.Services.AddScoped<IValidator<CandidateRequestDto>, CandidateRequestDtoValidator>();
builder.Services.AddScoped<IValidator<CandidatePatchDto>, CandidatePatchDtoValidator>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Secret").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy =>
    policy.RequireAssertion(context => context.User.Claims.First(x => x.Type == "Role").Value == "Admin"));

    options.AddPolicy("User", policy =>
    policy.RequireAssertion(context => context.User.Claims.First(x => x.Type == "Role").Value == "User"));
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.OAuthClientId("clientname");
    options.OAuthRealm("your-real");
});


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<JwtMiddleware>();

app.Run();
