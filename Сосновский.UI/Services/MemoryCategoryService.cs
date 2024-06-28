using Air.Domain.Entities;
using Air.Domain.Models;
namespace Сосновский.UI.Services
{
    public class MemoryCategoryService : ICategoryService
    {
        public Task<ResponseData<List<Category>>> GetCategoryListAsync()
        {
            var categories = new List<Category>
        {
        new Category {Id=1, GroupName="Cамолет 1",
        NormalizedName="Самолет русский"},
        new Category {Id=2, GroupName="Самолет 2",
        NormalizedName="Самолет американский" },
        new Category {Id=3, GroupName="Самолет 3",
        NormalizedName="Самолет французкий "}

        };
            var result = new ResponseData<List<Category>>();
            result.Data = categories;
            return Task.FromResult(result);
        }


    }
}
