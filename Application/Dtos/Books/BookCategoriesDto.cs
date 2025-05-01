using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Books
{
    public class BookCategoriesDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ChildName { get; set; } // نام فرزند مرتبط
        public int ChildNumber { get; set; }
        public string Description { get; set; }
    }
}
