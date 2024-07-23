using MailTestDataAccess.Context;
using MailTestDataAccess.Repositories.Application.Concrete;
using MailTestDataAccess.Repositories.Concrete;
using MailTestDataAccess.Repositories;
using MailTestDomain.Entities;
using MailTestService;
using MailTestService.Dtos;
using Microsoft.EntityFrameworkCore;
using MailTestService.Abstract;
using MailTestService.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region DB SETTINGS
builder.Services.AddDbContext<mailTestContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("LocalDB")));
#endregion
#region Email Settings
var emailSettings = builder.Configuration.GetSection("EmailSettings").Get<EmailSettings>();
builder.Services.AddSingleton(emailSettings);
#endregion
builder.Services.LoadMyPersistanceServices(builder.Configuration);


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
