using Application.Constants.Commons;
using Application.Dtos.Books;
using Application.Dtos.Commons;
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
       
        public async Task<string> AddAsync(BookCategoriesDto categoriesDto)
        {
            if (categoriesDto == null)
                return Messages.Error("❌ دسته‌بندی نمی‌تواند خالی باشد.");

            // بررسی مقدار Name
            if (string.IsNullOrWhiteSpace(categoriesDto.Name))
                return Messages.Error("❌ نام دسته‌بندی نمی‌تواند خالی باشد.");

           

            // تبدیل DTO به مدل پایگاه داده
            var bookCategory = _mapper.Map<BookCategories>(categoriesDto);

            if (bookCategory == null)
                return Messages.Error("❌ خطا در تبدیل داده‌ها.");

            try
            {
                await _Context.Set<BookCategories>().AddAsync(bookCategory);
                var result = await _Context.SaveChangesAsync();

                // بررسی نتیجه SaveChangesAsync
                if (result <= 0)
                    return Messages.Error("❌ خطا در ثبت اطلاعات پایگاه داده.");

                return Messages.Success("✅ دسته‌بندی با موفقیت ثبت شد.");
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("❌ خطای پایگاه داده: " + ex.InnerException?.Message);
                return Messages.Error("❌ خطا در ثبت دسته‌بندی. لطفاً بررسی کنید که مقادیر ارسالی معتبر باشند.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ خطای کلی: " + ex.Message);
                return Messages.Error("❌ خطای نامشخص. لطفاً دوباره امتحان کنید.");
            }
        }


        public async Task<List<BookCategoriesDto>> GetAllCategoriesAsync(long? parentId)
        {
            var categories = await _Context.Set<BookCategories>()
                .Where(c => c.ParentId == parentId)
                .Select(c => new BookCategoriesDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    
                    ChildNumber = _Context.Set<BookCategories>().Count(sub => sub.ParentId == c.Id),
                    Children = _Context.Set<BookCategories>().Where(sub => sub.ParentId == c.Id)
                        .Select(sub => new BookCategoriesDto
                        {
                            Id = sub.Id,
                            Name = "-- " + sub.Name, // اضافه کردن پیشوند برای نمایش فرزند
                           
                        }).ToList()
                })
                .ToListAsync();

            return categories;
        }



        public async Task<string> UpdateAsync(BookCategoriesDto categoriesDto)
        {
            if (categoriesDto == null)
                throw new ArgumentNullException(nameof(categoriesDto), "BookCategoriesDto نمی‌تواند null باشد.");

            var existingCategory = await _Context.Set<BookCategories>().FirstOrDefaultAsync(c => c.Id == categoriesDto.Id);
            if (existingCategory == null)
                return Messages.Error($"دسته‌بندی با شناسه {categoriesDto.Id} یافت نشد.");

            _mapper.Map(categoriesDto, existingCategory);
            _Context.Set<BookCategories>().Update(existingCategory);
            await _Context.SaveChangesAsync();

            return Messages.Success("✅ دسته‌بندی با موفقیت به‌روزرسانی شد.");
        }


        public async Task<string> RemoveAsync(long categoryId)
        {
            var existingCategory = await _Context.Set<BookCategories>().FirstOrDefaultAsync(c => c.Id == categoryId);

            if (existingCategory == null)
                return Messages.Error($"دسته‌بندی با شناسه {categoryId} یافت نشد.");

          
            _Context.Set<BookCategories>().Remove(existingCategory);

            await _Context.SaveChangesAsync(); 

            return Messages.Success("✅ دسته‌بندی با موفقیت حذف شد.");
        }



        public async Task<List<BookCategoriesDto>> GetBookCategoriesWithChildrenCountAsync()
        {
            var bookCategories = await _Context.Set<BookCategories>().Include(c => c.Book).ToListAsync();
            return bookCategories.Select(category => new BookCategoriesDto
            {
                BookId = category.Id,
                Name = category.Name,
                Description = category.Description,
                //ChildNumber = category.Book.Count
            }).ToList();
        }


        public async Task<string> AddChildAsync(BookCategoriesDto CategoriesDto)
        {
            if (CategoriesDto == null || string.IsNullOrWhiteSpace(CategoriesDto.Name))
                return Messages.Error("عنوان دسته‌بندی نمی‌تواند خالی باشد.");

            // دریافت دسته‌بندی والد با استفاده از شناسه موجود در DTO
            var parentCategory = await _Context.Set<BookCategories>()
                .SingleOrDefaultAsync(c => c.Id == CategoriesDto.ParentId);

            if (parentCategory == null)
                return Messages.Error($"دسته‌بندی والد با شناسه {CategoriesDto.Id} یافت نشد.");

            // نگاشت اطلاعات ورودی با AutoMapper
            var newChildCategory = _mapper.Map<BookCategories>(CategoriesDto);

            


            await _Context.Set<BookCategories>().AddAsync(newChildCategory);
            await _Context.SaveChangesAsync();

            return Messages.Success("✅ زیرمجموعه با موفقیت ثبت شد.");
        }

        public async Task<List<BookCategoriesDto>> GetChildBookCategories(long parentId)
        {
            var children = await _Context.Set<BookCategories>().Where(c => c.Id == parentId).ToListAsync();
            return children.Select(child => new BookCategoriesDto
            {
                Id = child.Id,
                Name = child.Name,
                Description = child.Description
            }).ToList();
        }

        public async Task<List<BookCategoriesDto>> GetBooks(long categoryId)
        {
            var books = await _Context.Set<Book>().Where(b => b.BookCategoriesId == categoryId).ToListAsync();
            return books.Select(book => new BookCategoriesDto
            {
                Id = book.BookCategories.Id,
                Name = book.BookCategories.Name,
                Description = book.BookCategories.Description
            }).ToList();
        }

        public async Task<BookCategoriesDto> FindAsync(long id)
        {
            if (id <= 0)
            {
                throw new MyArgumentNullException(ErrorType.InvalidInput);
            }

            var category = await _Context.Set<BookCategories>().FindAsync(id);

            if (category == null) return null;
            var categorys=_mapper.Map<BookCategoriesDto>(category);
            return categorys;
           
        }

    }
}

