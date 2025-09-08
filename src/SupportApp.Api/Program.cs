using QuestPDF.Infrastructure;
using SupportApp.Infrastructure;
using SupportApp.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

QuestPDF.Settings.License = LicenseType.Community;
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddPresentation(builder.Configuration)
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var ct = new CancellationTokenSource().Token;
    await DbSeeder.SeedAsync(scope.ServiceProvider, ct);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SupportApp API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCoreMiddlewares(builder.Configuration);
app.MapControllers();
app.Run();
