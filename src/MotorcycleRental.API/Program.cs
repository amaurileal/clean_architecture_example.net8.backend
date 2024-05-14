using MotorcycleRental.API.Extensions;
using MotorcycleRental.API.Middlewares;
using MotorcycleRental.Application.Extensions;
using MotorcycleRental.Infrastructure.Extensions;
using MotorcycleRental.Infrastructure.Seeders;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.AddPresentation();
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);

    var app = builder.Build();

    //Seeder execution
    var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<IMotorcycleRentalSeeder>();
    await seeder.Seed();



    app.UseMiddleware<ErrorHandlingMiddleware>();

    app.UseSwagger();
    app.UseSwaggerUI();

    // Configure the HTTP request pipeline.

    app.UseHttpsRedirection();    

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
   
}
catch (Exception)
{

    throw;
}

public partial class Program { }