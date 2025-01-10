using dairyFarm.Constants;
using dairyFarm.DbContexts;
using dairyFarm.Extensions;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
string migrationTableName = DatabaseConstants.DfMigrationHistory;
string migrationSchemaName = DatabaseConstants.DfSchema;


builder.Services.AddDatabaseContext<DFdbContext>(connectionString, migrationTableName, migrationSchemaName);

var dbContext = builder.Services.BuildServiceProvider().GetRequiredService<DFdbContext>();
dbContext.Database.Migrate();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseCors("AllowReactApp");
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Logger.LogInformation($"Connection String: {connectionString}");

app.Run();