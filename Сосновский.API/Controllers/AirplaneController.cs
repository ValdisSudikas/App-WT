using Air.Domain.Entities;
using Air.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Сосновский.API.Data;

namespace Сосновский.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KosuhiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public KosuhiController(AppDbContext context, IWebHostEnvironment _environment)
        {
            _context = context;

        }


        // GET: api/Dishes
        [HttpGet]
        public async Task<ActionResult<ResponseData<ListModel<Airplane>>>> GetProductListAsync(
              string? category,
              int pageNo = 1,
              int pageSize = 3)
        {
            // Создать объект результата
            var result = new ResponseData<ListModel<Airplane>>();

            // Фильтрация по категории загрузка данных категории
            var data = _context.Airplanes
            .Include(d => d.Category)
            .Where(d => String.IsNullOrEmpty(category)
            || d.Category.NormalizedName.Equals(category));

            // Подсчет общего количества страниц
            int totalPages = (int)Math.Ceiling(data.Count() / (double)pageSize);
            if (pageNo > totalPages)
                pageNo = totalPages;

            // Создание объекта ProductListModel с нужной страницей данных
            var listData = new ListModel<Airplane>()
            {
                Items = await data
            .Skip((pageNo - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(),
                CurrentPage = pageNo,
                TotalPages = totalPages
            };
            // поместить данные в объект результата
            result.Data = listData;
            // Если список пустой
            if (data.Count() == 0)
            {
                result.Success = false;
                result.ErrorMessage = "Нет объектов в выбранной категории";
            }
            return result;
        }
        // GET: api/Airplane/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Airplane>> GetKosuhi(int id)
        {
            var airplane = await _context.Airplanes.FindAsync(id);

            if (airplane == null)
            {
                return NotFound();
            }

            return airplane;
        }

        // PUT: api/Airplane/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKosuhi(int id, Airplane airplane)
        {
            if (id != airplane.Id)
            {
                return BadRequest();
            }

            _context.Entry(airplane).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KosuhiExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Airplane
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Airplane>> PostKosuhi(Airplane airplane)
        {
            _context.Airplanes.Add(airplane);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKosuhi", new { id = airplane.Id }, airplane);
        }

        // DELETE: api/Airplane/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKosuhi(int id)
        {
            var airplane = await _context.Airplanes.FindAsync(id);
            if (airplane == null)
            {
                return NotFound();
            }

            _context.Airplanes.Remove(airplane);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KosuhiExists(int id)
        {
            return _context.Airplanes.Any(e => e.Id == id);
        }

        [HttpPost("{id}")]

        public async Task<IActionResult> SaveImage(int id, IFormFile image, [FromServices] IWebHostEnvironment env)
        {
            // Найти объект по Id
            var airplane = await _context.Airplanes.FindAsync(id);
            if (airplane == null)
            {
                return NotFound();
            }

            // Путь к папке wwwroot/Images
            var imagesPath = Path.Combine(env.WebRootPath, "Images");

            // получить случайное имя файла
            var randomName = Path.GetRandomFileName();

            // получить расширение в исходном файле
            var extension = Path.GetExtension(image.FileName);

            // задать в новом имени расширение как в исходном файле
            var fileName = Path.ChangeExtension(randomName, extension);

            // полный путь к файлу
            var filePath = Path.Combine(imagesPath, fileName);

            // создать файл и открыть поток для записи
            using var stream = System.IO.File.OpenWrite(filePath);

            // скопировать файл в поток
            await image.CopyToAsync(stream);

            // получить Url хоста
            var host = "https://" + Request.Host;

            // Url файла изображения
            var url = $"{host}/Images/{fileName}";

            // Сохранить url файла в объекте
            airplane.Image = url;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
