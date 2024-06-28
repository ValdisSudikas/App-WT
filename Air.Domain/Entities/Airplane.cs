using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air.Domain.Entities
{
    public class Airplane
    {
        [Key]
        public int Id { get; set; } // id самолета
        public string Name { get; set; } // название самолета
        public string Description { get; set; } // описание самолета

        public string? Image { get; set; } // имя файла изображения 

        // Навигационные свойства
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

    }
}
