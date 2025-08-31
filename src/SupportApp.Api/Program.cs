using Scalar.AspNetCore;

using SupportApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

builder.Services
    .AddPresentation(builder.Configuration)
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference();
}

app.UseCoreMiddlewares(builder.Configuration);

app.MapControllers();

//app.UseAntiforgery();

app.MapStaticAssets();

app.MapGet("/", () => "hello");

app.UseHttpsRedirection();

app.Run();

