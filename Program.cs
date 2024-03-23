using ServerWordle.DAL.Initialization;
using ServerWordle.Services;
using ServerWordle.Services.Interfaces;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Hangfire;
using Microsoft.AspNetCore.Mvc.Filters;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHangfire(opt =>
{
    opt.UseSqlServerStorage(builder.Configuration.GetConnectionString("Develop"))
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings();
});

builder.Services.AddHangfireServer();

builder.Services.AddTransient<IWordService, WordService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000");
                      });
});

DALStartup.Init(builder.Configuration, builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseHangfireDashboard("/hangfire", new DashboardOptions { });

app.UseCors(MyAllowSpecificOrigins);

RecurringJob.AddOrUpdate<IWordService>(x => x.GenerateWordOfTheCurrentDay(), "* 9 * * *");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
