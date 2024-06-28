using Air.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Сосновский.API.Data
{
    public class DbIitializer
    {
        public static async Task SeedData(WebApplication app)
        {

            // Uri проекта
            var uri = "https://localhost:7002/";
            // Получение контекста БД
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            //Выполнение миграций
            await context.Database.MigrateAsync();

            if (!context.Categories.Any() && !context.Airplanes.Any())
            {
                var categories = new Category[] {
                    new Category {
                        GroupName="Cамолет 1",
                        NormalizedName="1"
                    },
                    new Category {
                        GroupName="Самолет 2",
                        NormalizedName="2"
                    },
                    new Category {
                        GroupName="Самолет 3",
                        NormalizedName="3"
                    }
                };

                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();


                var _plane = new List<Airplane> {
                    new Airplane {
                        Name = "Самолет 1",
                        Description = "Самолет 550 мест ",
                        Image = "Images/Airbus.jfif",
                        Category = categories.FirstOrDefault(c=>c.NormalizedName.Equals("1"))
                    },

                    new Airplane {
                        Name = "Самолет 2",
                        Description = "Самолет 770 мест",
                        Image = "Images/Boieng.jfif",
                        Category = categories.FirstOrDefault(c => c.NormalizedName.Equals("2"))
                    },


                    new Airplane {
                        Name = "Самолет 3 ",
                        Description = "Самолет на 330 мест",
                        Image = "Images/TU.jfif",
                        Category = categories.FirstOrDefault(c => c.NormalizedName.Equals("2"))
                    },


                    new Airplane {
                        Name = "Самолет 4",
                        Description = "Самолет на 500 мест ",
                        Image = "Images/IL.jfif",
                        Category = categories.FirstOrDefault(c => c.NormalizedName.Equals("3"))
                    },


                    new Airplane {
                        Name = "Самолет 5",
                        Description = "Самолет на 600 мест ",
                        Image = "Images/SJ.jfif",
                        Category = categories.FirstOrDefault(c => c.NormalizedName.Equals("3"))
                    }
               };

            }
        }
    }
}