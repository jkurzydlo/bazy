using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

app.UseRouting();

app.MapControllerRoute(	
	name: "default",
	pattern: "{controller=EmailAuth}/{action=Index}/{id}"
);

app.Run();
