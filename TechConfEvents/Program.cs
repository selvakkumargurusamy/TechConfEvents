using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using TechConfEvents.Interface;
using TechConfEvents.Models;
using TechConfEvents.Repository;
using TechConfEvents.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()           //Fix API ISSUE
                                                                    .AllowAnyMethod()               //Fix API ISSUE
                                                                     .AllowAnyHeader()));           //Fix API ISSUE

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TechConfContext>(
        options => options.UseSqlServer("name=ConnectionStrings:TechConfConnectionString"));

builder.Services.AddScoped<IEventService,EventService>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<ISpeakerService, SpeakerService>();
builder.Services.AddScoped<ISpeakerRepository, SpeakerRepository>();
builder.Services.AddScoped<ISpeakerSessionService, SpeakerSessionService>();
builder.Services.AddScoped<ISpeakerSessionRepository, SpeakerSessionRepository>();
builder.Services.AddScoped<IMailService, MailService>();
builder.Services.AddScoped<ICSVService, CSVService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAll");

app.MapControllers();

app.UseRouting();



app.Run();
