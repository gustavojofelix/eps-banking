using cnet_oykryo.application.Services;
using cnet_oykryo.Infrastructure.Data;
using cnet_oykryo.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Serilog;
using Microsoft.OpenApi.Models;
using cnet_oykryo.web.api.Data;
using Microsoft.AspNetCore.Identity;
using System.Configuration;
using cnet_oykryo.domain.Services;
using cnet_oykryo.IoCRoot;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        //Serilog Configuration
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.MSSqlServer(
                connectionString: builder.Configuration.GetConnectionString("DbContextConnection"),
                sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs", AutoCreateSqlTable = true },
                restrictedToMinimumLevel: LogEventLevel.Information)
            .CreateLogger();

        builder.Host.UseSerilog();  // Apply Serilog to the host

        // Add services to the container.

        builder.Services.AddControllers();

        //Register your DbContext with the connection string
        //builder.Services.AddDbContext<EPSDBContext>(options =>
        //    options.UseSqlServer(
        //builder.Configuration.GetConnectionString("DbContextConnection"),
        //sqlServerOptions => sqlServerOptions.MigrationsAssembly("cnet-oykryo.web.api")
        // ));

        // Add Swagger
        builder.Services
            .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "EPS Banking API C#", Version = "v1" });
                })
            .RegisterDependencies(builder.Configuration);      
        //.AddDbContext<ApplicationDbContext>(options =>
        //options.UseSqlServer(builder.Configuration.GetConnectionString("AuthDbContextConnection")));

        // Identity
        //builder.Services.AddIdentity<AppUser, IdentityRole>()
        //    .AddEntityFrameworkStores<ApplicationDbContext>()
        //    .AddDefaultTokenProviders();

        //Dependency injection
        //builder.Services.AddScoped<IAccountService, AccountService>();
        //builder.Services.AddScoped<ICustomerService, CustomerService>();
        //builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
        //builder.Services.AddScoped<IBankAccountRepository, BankAccountRepository>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        // Seed the database
        //SeedData.Initialize(app.Services).Wait();

        // Enable Swagger UI
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "EPS Banking API C#");
            c.RoutePrefix = "swagger";
        });

       

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}