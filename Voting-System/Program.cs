using Microsoft.EntityFrameworkCore;
using Voting_System.Application.Interfaces;
using Voting_System.Application.Services;
using Voting_System.Infrastructure.Interfaces;
using Voting_System.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ICandidateRepository, CandidateRepository>();
builder.Services.AddTransient<ICandidateService, CandidateService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
