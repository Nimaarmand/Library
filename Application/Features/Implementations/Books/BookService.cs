using Application.Constants.Commons;
using Application.Dtos.Books;
using Application.Features.Definitions.Books;
using Application.Repositories;
using AutoMapper;
using Domain.Entities.Books;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Implementations.Books
{
    /// <summary>
    /// سرویس مدیریت کتاب ها 
    /// </summary>
    public class BookService : IBookService
    {
        private readonly IGenericRepository _repository;
        private readonly IMapper _mapper;

        public BookService(IGenericRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<string> AddAsync(BookDto book)
        {
            if (book != null)
            {
                var mappedBook = _mapper.Map<Book>(book);
                await _repository.AddAsync(mappedBook);
                await _repository.SaveChangesAsync();
                return Messages.Success();
            }
            else
            {
                return Messages.Error("کتاب نمی‌تواند خالی باشد."); // پیام خطا
            }
        }

       
        public async Task<IEnumerable<Book>> GetAllBooksAsync(CancellationToken cancellationToken = default)
        {
            return await _repository.GetAll<Book>(null, null, true, cancellationToken);


        }

        public async Task<string> UpdateAsync(BookDto bookDto)
        {
            if (bookDto == null)
            {
                throw new ArgumentNullException(nameof(bookDto), "BookDto نمی‌تواند null باشد.");
            }

            // پیدا کردن رکورد با شناسه
            var existingBook = await _repository.Find<Book>(
                b => b.Id == bookDto.Id
            );

            if (existingBook == null)
            {
                return Messages.Error($"کتابی با شناسه {bookDto.Id} پیدا نشد.");
            }

            // اعمال تغییرات به رکورد موجود
            _mapper.Map(bookDto, existingBook); // به‌روزرسانی رکورد موجود با مقادیر جدید

            // ذخیره تغییرات با متد UpdateAsync
            await _repository.UpdateAsync(existingBook);
            await _repository.SaveChangesAsync();
            // پیام موفقیت
            return Messages.Success();
        }
        public async Task<string> RemoveAsync(long bookId)
        {
          
            var existingBook = await _repository.Find<Book>(b => b.Id == bookId);

            if (existingBook == null)
            {
                return Messages.Error($"کتابی با شناسه {bookId} پیدا نشد.");
            }

            try
            {
               
                await _repository.RemoveAsync(existingBook);
                await _repository.SaveChangesAsync();

               
                return Messages.Success("کتاب با موفقیت حذف شد.");
            }
            catch (Exception ex)
            {
              
                return Messages.Error($"خطایی در حذف کتاب رخ داد: {ex.Message}");
            }
        }

    }
}
