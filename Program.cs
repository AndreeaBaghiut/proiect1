using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using proiect1.Data;
using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
   policy.RequireRole("Admin"));
});

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Recipes");
    options.Conventions.AuthorizeFolder("/SavedRecipes");
    options.Conventions.AllowAnonymousToPage("/Recipes/Index");
    options.Conventions.AllowAnonymousToPage("/Recipes/Details");
    options.Conventions.AllowAnonymousToPage("/Categories/Index");
    options.Conventions.AuthorizeFolder("/Users", "AdminPolicy");
});

builder.Services.AddDbContext<proiect1Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("proiect1Context") ?? throw new InvalidOperationException("Connection string 'proiect1Context' not found.")));



builder.Services.AddDbContext<LibraryIdentityContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("proiect1Context") ?? throw new InvalidOperationException("Connection string 'proiect1Context' not found. ")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<LibraryIdentityContext>();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

app.Run();
