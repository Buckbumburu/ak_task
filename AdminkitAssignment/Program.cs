using AdminkitAssignment.Commands;
using Microsoft.Extensions.FileProviders;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<AddCustomerCommand>();
builder.Services.AddTransient<DeleteCustomerCommand>();
builder.Services.AddTransient<GetCustomersCommand>();
builder.Services.AddTransient<GetCustomerByIdCommand>();
builder.Services.AddTransient<UpdateCustomerCommand>();
builder.Services.AddControllers();

WebApplication app = builder.Build();

//Host files of the built angular application
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "angular", "dist", "angular")),
    RequestPath = new PathString(string.Empty)
});

//Simple redirect to index.html of all requests except those for controllers
app.Use(async (context, next) =>
{
    string requestPath = context.Request.Path.ToString();

    if (!requestPath.StartsWith("/api"))
    {
        context.Response.Redirect("/index.html");
        return;
    }

    await next(context);
});

app.MapControllers();

app.Run();
