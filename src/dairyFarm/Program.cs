using dairyFarm.Constants;
using dairyFarm.DbContexts;
using dairyFarm.Extensions;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Configure Database
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
string migrationTableName = DatabaseConstants.DfMigrationHistory;
string migrationSchemaName = DatabaseConstants.DfSchema;

// Correct way to add DbContext
builder.Services.AddDbContext<DFdbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.MigrationsHistoryTable(migrationTableName, migrationSchemaName);
    })
);

// CORS configuration
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

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

// Apply migrations at startup (be careful with this in production)
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DFdbContext>();
    dbContext.Database.Migrate();
}

app.UseCors("AllowReactApp");
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Logger.LogInformation($"Connection String: {connectionString}");

app.Run();







