using Application.Features.Definitions.Books;
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
    public class BookService: IBookService
    {
        private readonly IGenericRepository _repository;
        private readonly IMapper _mapper;
        public BookService(IGenericRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public void Add(Book book)
        {
            //_repository.Any<Book>(Book => Book.Id == book.Id);
           //var last= _repository.GetLastRecord<Book>();
            _repository.Add(book);
            _repository.Save();
        }




    }
}
