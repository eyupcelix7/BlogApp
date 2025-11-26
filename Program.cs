using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BlogContext>(options =>
{
    var config = builder.Configuration;
    var connectionString = config.GetConnectionString("sqlite_connection");
    //options.UseMySql(connectionString, new MySqlServerVersion(new Version(8,0,44)));
    options.UseSqlite(connectionString);
});
builder.Services.AddScoped<IPostRepository, EfPostRepository>();
builder.Services.AddScoped<ITagRepository, EfTagRepository>();
var app = builder.Build();
SeedData.TestVerileriniDoldur(app);
app.UseStaticFiles();
//app.MapDefaultControllerRoute();
app.MapControllerRoute(
    name: "post_details",
    pattern: "posts/details/{url}",
    defaults: new {controller = "Posts", action = "Details"}
);

app.MapControllerRoute(
    name: "posts_by_tag",
    pattern: "posts/tag/{url}",
    defaults: new { controller = "Posts", action = "Index" }
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);


app.Run();
