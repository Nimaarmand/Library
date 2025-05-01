using Application.Dtos.Books;
using Domain.Entities.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Definitions.Books
{
    /// <summary>
    /// سرویس مدیریت دسته یندی ها
    /// </summary>
    public interface IBookCategories
    {
        List<BookCategoriesDto> GetBookCategories();
        List<BookCategoriesDto> ChildBookCategories(long Id);
        List<BookCategoriesDto> GetBooks(long Id);
    }
}
