using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Zip.InstallmentsService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddControllers().AddNewtonsoftJson(options =>
        options.SerializerSettings.ContractResolver = new DefaultContractResolver())
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNameCaseInsensitive = true);

builder.Services.AddMvcCore(obj =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        //.RequireRole(Environment.GetEnvironmentVariable("Scope"))
        .Build();
    obj.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddApiVersioning(o => o.ApiVersionReader = new HeaderApiVersionReader("api-version"));
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo { Title = "InstallmentsServices", Version = "v1" });
});

builder.Services.AddScoped<IPlaymentPlanFactory, PaymentPlanFactory>();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
       options.SerializerSettings.ContractResolver = new DefaultContractResolver())
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNameCaseInsensitive = true);

builder.Services.AddApiVersioning(o => o.ApiVersionReader = new HeaderApiVersionReader("api-version"));


var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(c => { c.SwaggerEndpoint("../swagger/v1/swagger.json", "My API V1"); });
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

app.Use(async (context, nextMiddleware) =>
{
    if (context.Request.Method == "OPTIONS")
    {
        //ResponseHeaderOverrideFilter.SetResponseHeaders(context);
    }
    else
    {
        context.Request.IsHttps = true;
        await nextMiddleware();
    }
});

app.UseHttpsRedirection();
app.UseRouting();
if (Convert.ToBoolean(Environment.GetEnvironmentVariable("Auth__Enabled")))
{
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseHttpsRedirection();
}

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();