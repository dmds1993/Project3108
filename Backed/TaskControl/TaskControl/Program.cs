using Domain.DependencyInjection;
using Domain.Infra.SqlServer.DependecyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "enable-cors",
                      policy =>
                      {
                          policy.WithOrigins("*")
                          .WithMethods("*")
                          .WithHeaders("*")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.DocumentFilter<LowerCaseDocumentFilter>();
});
builder.Services.AddDatabase();
builder.Services.AddServices();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UsePathBase("/swagger"); // Defina a rota do Swagger como a URL base
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/v1/swagger.json", "Your API v1");
});

app.UseCors("enable-cors");

app.UseAuthorization();
app.MapControllers();
app.UseHttpsRedirection();

app.Run();
