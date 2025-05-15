using Application.Constants.Commons;
using Application.Dtos.Books;
using Application.Exceptions.ValidationExceptions;
using Application.Features.Definitions.Books;
using Application.Features.Definitions.Contexts;
using Application.Repositories;
using AutoMapper;
using Domain.Entities.Books;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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

           
            var mappedBook = _mapper.Map<Book>(book);

            try
            {
                await _Context.Set<Book>().AddAsync(mappedBook);
                var result = await _Context.SaveChangesAsync();

             
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

        public async Task<List<BookDto>> GetAllBooksAsync()
        {
            var listbook = await _Context.Set<Book>().ToListAsync();
            var books=_mapper.Map <List<BookDto>>(listbook);
            return books;
        }

        public async Task<string> UpdateAsync(BookDto bookDto)
        {
            if (bookDto == null)
            {
                throw new ArgumentNullException(nameof(bookDto), "BookDto نمی‌تواند null باشد.");
            }

            var existingBook = await _Context.Set<Book>().FirstOrDefaultAsync(b => b.Id == bookDto.Id);
            if (existingBook == null)
            {
                throw new MyArgumentNullException(ErrorType.BookIdNotFound); 
            }

            _mapper.Map(bookDto, existingBook);
            _Context.Set<Book>().Update(existingBook);
            await _Context.SaveChangesAsync();

            return Messages.Success();
        }

        public async Task<string> RemoveAsync(long bookId)
        {
            var existingBook = await _Context.Set<Book>().FirstOrDefaultAsync(b => b.Id == bookId);
            if (existingBook == null)
            {
                throw new MyArgumentNullException(ErrorType.BookIdNotFound);
            }

            try
            {
                _Context.Set<Book>().Remove(existingBook);
                await _Context.SaveChangesAsync();

                return Messages.Success("کتاب با موفقیت حذف شد.");
            }
            catch (Exception ex)
            {
                return Messages.Error($"خطایی در حذف کتاب رخ داد: {ex.Message}");
            }
        }
        public async Task<BookDto> FindAsync(long id)
        {
            if (id <= 0)
            {
                throw new MyArgumentNullException(ErrorType.InvalidInput);
            }

            var book = await _Context.Set<Book>().FindAsync(id);

            if (book == null) return null;
            var books = _mapper.Map<BookDto>(book);
            return books;

        }
        public async Task<List<BookDto>> GetAllReservation()
        {         
            var reservationBooks = await _Context.Set<Book>()
                .Where(b => b.IsAvailable == true)
                .ToListAsync();           
            var list = _mapper.Map<List<BookDto>>(reservationBooks);

            return list;
        }

        public async Task<List<BookDto>> GetAllNotReservation()
        {
            var reservationBooks = await _Context.Set<Book>()
                .Where(b => b.IsAvailable == false)
                .ToListAsync();

            var list = _mapper.Map<List<BookDto>>(reservationBooks);

            return list;
        }

    }
}
