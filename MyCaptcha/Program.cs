using CaptchaConfigurations.ActionFilter;
using CaptchaConfigurations.Services;
using CaptchaConfigurations.ExtensionMethod;
using MyCaptcha.Attribute;
using SixLabors.Fonts;

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
    MaxRotationDegrees = 0,
    CaptchaValueSendType = CaptchaConfigurations.CaptchaOptionsDTO.CaptchaValueSendType.InBody,
    CaptchaCharacter = 5,
    FontStyle = FontStyle.Italic,
    CaptchaType = CaptchaConfigurations.CaptchaOptionsDTO.CaptchaType.Numbers,
    
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
