using Microsoft.EntityFrameworkCore;
using Voting_System.Application.Interfaces;
using Voting_System.Application.Services;
using Voting_System.Infrastructure.Configurations;
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

builder.Services.AddDbContext<EFContext>(
      options => options.UseSqlServer(builder.Configuration.GetConnectionString("DBString")),
      ServiceLifetime.Transient,
      ServiceLifetime.Transient);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
