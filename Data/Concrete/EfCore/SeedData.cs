using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete.EfCore
{
    public static class SeedData
    {
        public static void TestVerileriniDoldur(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogContext>();
            if(context != null)
            {
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }

                if (!context.Tags.Any())
                {
                    context.Tags.AddRange(
                        new Tag { Text = "Web Programlama" },
                        new Tag { Text = "Mobil Programlama" },
                        new Tag { Text = "Masaüstü Programlama" },
                        new Tag { Text = "Siber Güvenlik" }
                    );
                    context.SaveChanges();
                }
                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User { UserName = "eyupcelix7" }
                    );
                    context.SaveChanges();
                }
                if (!context.Posts.Any())
                {
                    context.Posts.AddRange(
                        new Post
                        {
                            Title = ".Net Core",
                            Content = ".Net Core Dersleri İçeriği",
                            IsActive = true,
                            Image = "aspnetcore.jpg",
                            PublishedOn = DateTime.Now,
                            Tags = context.Tags.Take(3).ToList(),
                            UserId = 1
                        },
                        new Post
                        {
                            Title = "Php Core",
                            Content = "Php Dersleri İçeriği",
                            IsActive = true,
                            Image = "php.png",
                            PublishedOn = DateTime.Now,
                            Tags = context.Tags.Take(3).ToList(),
                            UserId = 1
                        }
                    );
                    context.SaveChanges();
                }

            }
        }
    }
}
