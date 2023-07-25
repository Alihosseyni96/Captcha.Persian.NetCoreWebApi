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
    MaxRotationDegrees = 70,
    CaptchaValueSendType = CaptchaConfigurations.CaptchaOptionsDTO.CaptchaValueSendType.InHeader,
    CaptchaCharacter = 4,
    FontStyle = FontStyle.Italic,
    FontFamilies = new string[] {"Arial"}
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
