using BookStore.Dto;
using BookStore.Entity;

namespace BookStore.Service.Abstracts
{
    public interface IBookService
    {
        BookResponseDto addBook(BookSaveRequestDto dto,string token);

        BookResponseDto updateBook(BookUpdateRequestDto dto,string isbn, string token);
        BookResponseDto getBookByIsbn(string isbn);
        List<BookResponseDto> getBooksByCreationDate();
        void deleteBook(string token, string isbn);
        public List<Book> getListBookByIsbn(List<string> isbn);
    }
}
