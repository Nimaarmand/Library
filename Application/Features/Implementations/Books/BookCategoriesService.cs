using Application.Constants.Commons;
using Application.Dtos.Books;
using Application.Features.Definitions.Books;
using Application.Features.Definitions.Contexts;
using Application.Repositories;
using AutoMapper;
using Domain.Entities.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Implementations.Books
{
    /// <summary>
    /// سرویس مدیریت دسته یندی ها
    /// </summary>
    public class BookCategoriesService : IBookCategories
    {
        private readonly IGenericRepository _repository;
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;
        public BookCategoriesService(IGenericRepository repository, IMapper mapper, IApplicationDbContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
        }
        public async Task<string> AddAsync(BookCategoriesDto categories)
        {
            if (categories == null)
            {
                throw new ArgumentNullException(nameof(categories), "دسته‌بندی نمی‌تواند خالی باشد.");
            }

            // مپ کردن DTO به مدل اصلی
            var bookCategory = _mapper.Map<BookCategories>(categories);

            // افزودن به دیتابیس
            await _repository.AddAsync(bookCategory);
            await _repository.SaveChangesAsync();

            // پیام موفقیت
            return Messages.Success("دسته‌بندی با موفقیت اضافه شد.");
        }
        public async Task<string> UpdateAsync(BookCategoriesDto categories)
        {
           
            if (categories == null)
            {
                throw new ArgumentNullException(nameof(categories), "دسته بندی یافت نشد");
            }

            
            var entity = await _repository.Find<BookCategories>(c => c.Id == categories.Id);
            if (entity == null)
            {
                return Messages.Error("دسته بندی با این شناسه یافت نشد");
            }

            try
            {
               
                _mapper.Map(categories, entity);

              
                await _repository.UpdateAsync(entity);
                await _repository.SaveChangesAsync();

                
                return Messages.Success("دسته بندی با موفقیت به روز رسانی شد");
            }
            catch (Exception ex)
            {
               
                return Messages.Error($"خطایی در فرآیند به‌روزرسانی رخ داد: {ex.Message}");
            }
        }
        public List<BookCategoriesDto> GetBookCategories()
        {
            // واکشی لیست دسته‌بندی‌ها از پایگاه داده
            var bookCategories = _context.bookCategories.ToList();

            // تبدیل مدل به DTO و محاسبه تعداد فرزندان هر والد
            var bookCategoriesDto = bookCategories
                .Select(category => new BookCategoriesDto
                {
                    Name = category.Name,
                    ChildName = category.ChildName,
                    Description = category.Description,
                    ChildNumber = bookCategories.Count(c => c.ChildName == category.Name) // محاسبه تعداد فرزندان مرتبط
                })
                .ToList();

            return bookCategoriesDto;
        }
        public async Task<string> AddAChildsync(BookCategoriesDto categoryDto, int parentId)
        {
            if (categoryDto == null)
            {
                throw new ArgumentNullException(nameof(categoryDto), "دسته‌بندی نمی‌تواند خالی باشد.");
            }

            // بررسی موجود بودن آیدی والد
            var parentCategory = await _repository.Find<BookCategories>(c => c.Id == parentId);

            if (parentCategory != null)
            {
                // اگر والد موجود باشد، فرزند ثبت شود
                var newChildCategory = _mapper.Map<BookCategories>(categoryDto);
                newChildCategory.ChildName = parentCategory.Name; // تنظیم ارتباط با والد

                await _repository.AddAsync(newChildCategory);
                await _repository.SaveChangesAsync();

                return Messages.Success("فرزند با موفقیت ثبت شد.");
            }
            else
            {
                // اگر والد موجود نباشد، والد جدید ثبت شود
                var newParentCategory = _mapper.Map<BookCategories>(categoryDto);
                newParentCategory.Id = parentId; // تنظیم آیدی والد

                await _repository.AddAsync(newParentCategory);
                await _repository.SaveChangesAsync();

                return Messages.Success("والد جدید با موفقیت ثبت شد.");
            }
        }
        //نمایش لیست زیر مجموعه ها 
        public List<BookCategoriesDto> ChildBookCategories(long parentId)
        {
            if (parentId <= 0)
            {
                throw new ArgumentException("دسته بندی نمی تواند خالی باشد.");
            }

            // واکشی فرزندهای مرتبط با آیدی والد
            var children = _context.bookCategories
                .Where(c => c.ChildName != null && c.Id == parentId)
                .ToList();

            if (!children.Any())
            {
                throw new Exception("زیر مجموعه برای این دسته بندی پیدا نشد.");
            }

            // مپ کردن مدل به DTO
            var childrenDto = children.Select(child => new BookCategoriesDto
            {
                Name = child.Name,
                ChildName = child.ChildName,
                Description = child.Description
            }).ToList();

            return childrenDto;
        }
        //لیست کتاب های این دسته بندی
        public List<BookCategoriesDto> GetBooks(long categoryId)
        {
            if (categoryId <= 0)
            {
                throw new ArgumentException("آیدی دسته‌بندی نمی‌تواند خالی یا کمتر از صفر باشد.");
            }

            // واکشی کتاب‌هایی که آیدی دسته‌بندی مورد نظر را دارند
            var books = _context.Books
                .Where(b => b.BookCategoriesId == categoryId)
                .ToList();

            if (!books.Any())
            {
                throw new Exception("کتابی برای این دسته‌بندی یافت نشد.");
            }

            // تبدیل مدل به DTO
            var booksDto = books.Select(book => new BookCategoriesDto
            {
                Name = book.BookCategories.Name, // نام دسته‌بندی
                ChildName = book.Name, // نام کتاب (به عنوان فرزند)
                Description = book.BookCategories.Description
            }).ToList();

            return booksDto;
        }
        public async Task<string> RemoveAsync(long bookId)
        {

            var existingBook = await _repository.Find<BookCategories>(b => b.Id == bookId);

            if (existingBook == null)
            {
                return Messages.Error($"دسته بندی با شناسه پیدا نشد.");
            }

            try
            {

                await _repository.RemoveAsync(existingBook);
                await _repository.SaveChangesAsync();


                return Messages.Success("دسته بندی با موفقیت حذف شد.");
            }
            catch (Exception ex)
            {

                return Messages.Error($"خطایی در حذف دسته بندی رخ داد: {ex.Message}");
            }
        }
    }
}
