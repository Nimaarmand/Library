using Application.Constants.Commons;
using Application.Dtos.Books;
using Application.Exceptions.ValidationExceptions;
using Application.Features.Definitions.Books;
using Application.Features.Definitions.Contexts;
using Application.MappingProfile;
using Application.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities.Books;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading;

namespace Application.Features.Implementations.Books
{
    /// <summary>
    /// سرویس مدیریت دسته یندی ها
    /// </summary>
    public class BookCategoriesService : IBookCategories
    {
        private readonly IApplicationDbContext _Context;
        private readonly IMapper _mapper;

        public BookCategoriesService(IApplicationDbContext context, IMapper mapper)
        {
            _Context = context;
            _mapper = mapper;
        }

        // 📌 ثبت دسته‌بندی جدید
        public async Task<string> AddAsync(BookCategoriesDto categoriesDto)
        {
            if (categoriesDto == null)
                return Messages.Error("دسته‌بندی نمی‌تواند خالی باشد.");

            var bookCategory = _mapper.Map<BookCategories>(categoriesDto);

            await _Context.BookCategories.AddAsync(bookCategory);
            await _Context.SaveChangesAsync();

            return Messages.Success("✅ دسته‌بندی با موفقیت ثبت شد.");
        }

        // 📌 دریافت تمام دسته‌بندی‌ها
        public async Task<List<BookCategoriesDto>> GetAllCategoriesAsync()
        {
            var categories = await _Context.BookCategories.ToListAsync();
            return _mapper.Map<List<BookCategoriesDto>>(categories);
        }

        // 📌 به‌روزرسانی دسته‌بندی
        public async Task<string> UpdateAsync(BookCategoriesDto categoriesDto)
        {
            if (categoriesDto == null)
                throw new ArgumentNullException(nameof(categoriesDto), "BookCategoriesDto نمی‌تواند null باشد.");

            var existingCategory = await _Context.BookCategories.FirstOrDefaultAsync(c => c.Id == categoriesDto.Id);
            if (existingCategory == null)
                return Messages.Error($"دسته‌بندی با شناسه {categoriesDto.Id} یافت نشد.");

            _mapper.Map(categoriesDto, existingCategory);
            _Context.BookCategories.Update(existingCategory);
            await _Context.SaveChangesAsync();

            return Messages.Success("✅ دسته‌بندی با موفقیت به‌روزرسانی شد.");
        }

        // 📌 حذف دسته‌بندی
        public async Task<string> RemoveAsync(long categoryId)
        {
            var existingCategory = await _Context.BookCategories.FirstOrDefaultAsync(c => c.Id == categoryId);
            if (existingCategory == null)
                return Messages.Error($"دسته‌بندی با شناسه {categoryId} یافت نشد.");

            _Context.BookCategories.Remove(existingCategory);
            await _Context.SaveChangesAsync();

            return Messages.Success("✅ دسته‌بندی با موفقیت حذف شد.");
        }

        // 📌 دریافت تعداد زیرمجموعه‌های هر دسته‌بندی
        public async Task<List<BookCategoriesDto>> GetBookCategoriesWithChildrenCountAsync()
        {
            var bookCategories = await _Context.BookCategories.Include(c => c.Book).ToListAsync();
            return bookCategories.Select(category => new BookCategoriesDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                ChildNumber = category.Book.Count
            }).ToList();
        }

        // 📌 اضافه کردن زیرمجموعه به دسته‌بندی اصلی
        public async Task<string> AddAChildAsync(BookCategoriesDto categoryDto, long parentId)
        {
            if (categoryDto == null)
                return Messages.Error("دسته‌بندی نمی‌تواند خالی باشد.");

            var parentCategory = await _Context.BookCategories.FindAsync(parentId);
            if (parentCategory == null)
                return Messages.Error($"دسته‌بندی والد با شناسه {parentId} یافت نشد.");

            var newChildCategory = _mapper.Map<BookCategories>(categoryDto);
            newChildCategory.Id = parentId; // تنظیم والد

            await _Context.BookCategories.AddAsync(newChildCategory);
            await _Context.SaveChangesAsync();

            return Messages.Success("✅ زیرمجموعه با موفقیت ثبت شد.");
        }

        // 📌 دریافت زیرمجموعه‌های یک دسته‌بندی
        public async Task<List<BookCategoriesDto>> GetChildBookCategories(long parentId)
        {
            var children = await _Context.BookCategories.Where(c => c.Id == parentId).ToListAsync();
            return children.Select(child => new BookCategoriesDto
            {
                Id = child.Id,
                Name = child.Name,
                Description = child.Description
            }).ToList();
        }

        // 📌 دریافت لیست کتاب‌های مرتبط با یک دسته‌بندی
        public async Task<List<BookCategoriesDto>> GetBooks(long categoryId)
        {
            var books = await _Context.Books.Where(b => b.BookCategoriesId == categoryId).ToListAsync();
            return books.Select(book => new BookCategoriesDto
            {
                Id = book.BookCategories.Id,
                Name = book.BookCategories.Name,
                ChildName = book.Name,
                Description = book.BookCategories.Description
            }).ToList();
        }

       
    }
}
