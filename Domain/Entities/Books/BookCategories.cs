using Domain.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Books
{
    public class BookCategories : BaseEntity
    {
        public string Name { get; set; }
        public string ChildName { get; set; }// نام فرزند مرتبط
        public int ChildNumber { get; set; }
        public string Description { get; set; }
        public ICollection<Book> Book { get; set; } = new List<Book>();
        public long bookId { get; set; }
    }
}
