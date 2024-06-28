using Air.Domain.Entities;
using Air.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Сосновский.UI.Services
{
    public class MemoryProductService : IProductService
    {
        List<Airplane> _plane;
        List<Category> _categories;
        IConfiguration _config;



        public MemoryProductService(ICategoryService categoryService, [FromServices] IConfiguration config)
        {
            _config = config;
            _categories = categoryService.GetCategoryListAsync()
                .Result
                .Data;

            SetupData();
        }





        /// <summary>
        /// Инициализация списков
        /// </summary>
        public void SetupData()
        {

            _plane = new List<Airplane>
        {
            new Airplane { Id = 1, Name = "Самолет 1",
                Description = "Самолет 550 мест ",
                Image = "Images/Airbus.jfif",
                CategoryId  = 3 },

            new Airplane { Id = 1, Name = "Самолет 2",
                Description = "Самолет 770 мест",
                Image = "Images/Boieng.jfif",
                CategoryId =2  },


            new Airplane { Id = 1, Name = "Самолет 3 ",
                Description = "Самолет на 330 мест",
                Image = "Images/TU.jfif",
                CategoryId = 1 },


            new Airplane { Id = 1, Name = "Самолет 4",
                Description = "Самолет на 500 мест ",
                Image = "Images/IL.jfif",
                CategoryId = 1 },


            new Airplane { Id = 1, Name = "Самолет 5",
                Description = "Самолет на 600 мест ",
                Image = "Images/SJ.jfif",
                CategoryId = 1 }


        };
        }


        Task<ResponseData<ListModel<Airplane>>> IProductService.GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
        {


            // Создать объект результата
            var result = new ResponseData<ListModel<Airplane>>();

            // Id категории для фильрации
            int? categoryId = null;

            // если требуется фильтрация, то найти Id категории
            // с заданным categoryNormalizedName
            if (categoryNormalizedName != null)
                categoryId = _categories
                .Find(c =>
                c.NormalizedName.Equals(categoryNormalizedName))
                ?.Id;

            // Выбрать объекты, отфильтрованные по Id категории,
            // если этот Id имеется
            var data = _plane
            .Where(d => categoryNormalizedName == null || d.CategoryId.Equals(categoryNormalizedName))?
            .ToList();

            // получить размер страницы из конфигурации
            int pageSize = _config.GetSection("ItemsPerPage").Get<int>();


            // получить общее количество страниц
            int totalPages = (int)Math.Ceiling(data.Count / (double)pageSize);

            // получить данные страницы
            var listData = new ListModel<Airplane>()
            {
                Items = data.Skip((pageNo - 1) *
            pageSize).Take(pageSize).ToList(),
                CurrentPage = pageNo,
                TotalPages = totalPages
            };

            // поместить ранные в объект результата
            result.Data = listData;



            // Если список пустой
            if (data.Count == 0)
            {
                result.Success = false;
                result.ErrorMessage = "Нет объектов в выбраннной категории";
            }
            // Вернуть результат
            return Task.FromResult(result);

        }

        public Task<ResponseData<Airplane>> CreateProductAsync(Airplane product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<Airplane>> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }



        public Task UpdateProductAsync(int id, Airplane product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }
    }
}
