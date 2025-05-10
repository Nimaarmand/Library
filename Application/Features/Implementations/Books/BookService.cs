using Application.Constants.Commons;
using Application.Dtos.Books;
using Application.Features.Definitions.Books;
using Application.Features.Definitions.Contexts;
using Application.Repositories;
using AutoMapper;
using Domain.Entities.Books;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Implementations.Books
{
    /// <summary>
    /// سرویس مدیریت کتاب ها 
    /// </summary>
    public class BookService : IBookService
    {
        private readonly IApplicationDbContext _Context;
        private readonly IMapper _mapper;

        public BookService(IApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _Context = context;
        }

        public async Task<string> AddAsync(BookDto book)
        {
            if (book == null)
            {
                return Messages.Error("کتاب نمی‌تواند خالی باشد.");
            }

            // تبدیل `BookDto` به `Book`
            var mappedBook = _mapper.Map<Book>(book);

            try
            {
                await _Context.Books.AddAsync(mappedBook);
                var result = await _Context.SaveChangesAsync();

                // بررسی ذخیره موفقیت‌آمیز
                if (result > 0)
                {
                    return Messages.Success("✅ ثبت اطلاعات با موفقیت انجام شد.");
                }
                else
                {
                    return Messages.Error("❌ ثبت اطلاعات انجام نشد.");
                }
            }
            catch (Exception ex)
            {
                return Messages.Error($"❌ خطا هنگام ثبت اطلاعات: {ex.Message}");
            }
        }

        public async Task<List<Book>> GetAllBooksAsync(CancellationToken cancellationToken = default)
        {
            return await _Context.Books.ToListAsync(cancellationToken);
        }

        public async Task<string> UpdateAsync(BookDto bookDto)
        {
            if (bookDto == null)
            {
                throw new ArgumentNullException(nameof(bookDto), "BookDto نمی‌تواند null باشد.");
            }

            var existingBook = await _Context.Books.FirstOrDefaultAsync(b => b.Id == bookDto.Id);
            if (existingBook == null)
            {
                return Messages.Error($"کتابی با شناسه {bookDto.Id} پیدا نشد.");
            }

            _mapper.Map(bookDto, existingBook);
            _Context.Books.Update(existingBook);
            await _Context.SaveChangesAsync();

            return Messages.Success();
        }

        public async Task<string> RemoveAsync(long bookId)
        {
            var existingBook = await _Context.Books.FirstOrDefaultAsync(b => b.Id == bookId);
            if (existingBook == null)
            {
                return Messages.Error($"کتابی با شناسه {bookId} پیدا نشد.");
            }

            try
            {
                _Context.Books.Remove(existingBook);
                await _Context.SaveChangesAsync();

                return Messages.Success("کتاب با موفقیت حذف شد.");
            }
            catch (Exception ex)
            {
                return Messages.Error($"خطایی در حذف کتاب رخ داد: {ex.Message}");
            }
        }
    }
}
