using CaptchaConfigurations.ActionFilter;
using CaptchaConfigurations.Services;
using CaptchaConfigurations.ExtensionMethod;
using MyCaptcha.Attribute;
using SixLabors.Fonts;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<CustomHeaderSwaggerAttribute>();
});

builder.Services.UseCaptcha(new CaptchaConfigurations.CaptchaOptionsDTO.CaptchaOptions()
{
    CaptchaType = CaptchaConfigurations.CaptchaOptionsDTO.CaptchaType.Numbers,
    CaptchaCharacter = 5,
    FontStyle = SixLabors.Fonts.FontStyle.Italic,
    CaptchaValueSendType = CaptchaConfigurations.CaptchaOptionsDTO.CaptchaValueSendType.InBody,
    Height = 40,
    Width = 100,
    NoiseRate = 200,
    DrawLines = 3,
    //FontFamilies = new string[] { "Hack" , "Verdana" },
    FontFamilies = new string[] { "Test1" , "Test2","Arail", "Verdana" , "Hack" },

    BackgroundColor = new SixLabors.ImageSharp.Color[] { SixLabors.ImageSharp.Color.White }

});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor |
    ForwardedHeaders.XForwardedProto
});


app.UseAuthorization();

app.MapControllers();

app.Run();
