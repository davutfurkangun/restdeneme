using BookStore.Dto;
using BookStore.Service.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        IBookService bookService;
        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }
        [HttpPost]
        public BookResponseDto addBook(BookSaveRequestDto dto, string token)
        {
            return bookService.addBook(dto,token);
        }
        [HttpPut("{isbn}")]
        public BookResponseDto updateBook(BookUpdateRequestDto dto, [FromRoute] string isbn, string token)
        {
            return bookService.updateBook(dto, isbn,token);

        }
        [HttpGet("{isbn}")]
        public BookResponseDto getBookByIsbn([FromRoute] string isbn)
        {
            return bookService.getBookByIsbn(isbn);
        }
        [HttpGet()]
        public List<BookResponseDto> getBooksByCreationDate()
        {
            return bookService.getBooksByCreationDate();
        }
        [HttpDelete("{isbn}")]
        public void deleteBook(string token, [FromRoute] string isbn)
        {
            bookService.deleteBook(token, isbn);
        }
    }
}
