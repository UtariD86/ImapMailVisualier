using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Collections.Generic;
using MailTestService.Abstract;
using MailTestService.Concrete;
using MailTestService.Dtos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// EmailSettings'i konfigüre et
var emailSettings = builder.Configuration.GetSection("EmailSettings").Get<EmailSettings>();
builder.Services.AddSingleton(emailSettings);

// EmailService baðýmlýlýðýný ekle
builder.Services.AddScoped<IEmailService>(provider =>
    new EmailService(emailSettings.Email, emailSettings.Password, emailSettings.ImapServer, emailSettings.ImapPort));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
