using BookStore.Context;
using BookStore.Dto;
using BookStore.Entity;
using BookStore.Service.Abstracts;
using BookStore.Util;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Service.Concretes
{
    public class BookService : IBookService
    {
        BsContext context;
        IUserService userService;
       

        public BookService(BsContext context,IUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }

        public BookResponseDto addBook(BookSaveRequestDto dto,string token)
        {
            User user1= userService.getUserById(token);
            if (user1.Role == 0)
            {

                Book book = new Book();
                book.Isbn = dto.Isbn;
                book.Title = dto.Title;
                book.Author = dto.Author;
                book.Price = dto.Price;
                book.Stock = dto.Stock;
                context.Books.Add(book);
                context.SaveChanges();

                BookResponseDto bookResponseDto = new BookResponseDto();
                bookResponseDto.Isbn = book.Isbn;
                bookResponseDto.Author = book.Author;
                bookResponseDto.Title = book.Title;
                bookResponseDto.Price = book.Price;
                bookResponseDto.Stock = book.Stock;
                return bookResponseDto;
            }
            else
            {
                throw new Exception("You do not have authority to add books.");
            }
                        
        }

        public void deleteBook(string token, string isbn)
        {
            User user1 = userService.getUserById(token);
            if (user1.Role == 0)
            {


                Book? book = context.Books.FirstOrDefault(x => x.Isbn == isbn);
                if (book != null)
                {
                    context.Books.Remove(book);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Book not found");
                }
            }
            else
            {
                throw new Exception("You do not have the authority to delete the book.");
            }
           

        }

        public BookResponseDto getBookByIsbn(string isbn)
        {
            Book? book = context.Books.Find(isbn);
            if (book != null)
            {
                BookResponseDto bookResponseDto = new BookResponseDto();
                bookResponseDto.Isbn = book.Isbn;
                bookResponseDto.Author = book.Author;
                bookResponseDto.Title = book.Title;
                bookResponseDto.Price = book.Price;
                bookResponseDto.Stock = book.Stock;
                return bookResponseDto;
            }
            else
            {
                throw new Exception("Book not found");
            }
        }

        public List<BookResponseDto> getBooksByCreationDate()
        {
            List<Book> books = context.Books.OrderByDescending(x => x.CreatedAt).ToList();
            List<BookResponseDto> bookResponseDtos = new List<BookResponseDto>();
            foreach (var book in books)
            {
                BookResponseDto bookResponseDto = new BookResponseDto();
                bookResponseDto.Isbn = book.Isbn;
                bookResponseDto.Author = book.Author;
                bookResponseDto.Title = book.Title;
                bookResponseDto.Price = book.Price;
                bookResponseDto.Stock = book.Stock;
                bookResponseDtos.Add(bookResponseDto);
            }
            return bookResponseDtos;
        }

        public BookResponseDto updateBook(BookUpdateRequestDto dto,string isbn,string token)
        {
            User user1 = userService.getUserById(token);
            if (user1.Role == 0)
            {


                Book? book = context.Books.FirstOrDefault(x => x.Isbn == isbn);
                if (book != null)
                {
                    book.Title = dto.Title;
                    book.Author = dto.Author;
                    book.Price = dto.Price;
                    book.Stock = dto.Stock;
                    book.UpdatedAt = DateTime.Now;
                    context.SaveChanges();

                    BookResponseDto bookResponseDto = new BookResponseDto();
                    bookResponseDto.Isbn = book.Isbn;
                    bookResponseDto.Author = book.Author;
                    bookResponseDto.Title = book.Title;
                    bookResponseDto.Price = book.Price;
                    bookResponseDto.Stock = book.Stock;
                    return bookResponseDto;
                }
                else
                {
                    throw new Exception("Book not found");
                }
            }
            else
            {
                throw new Exception("You do not have the authority to update book information.");
            }
        }
        public List<Book> getListBookByIsbn (List<string> isbn)
        {
            
            return context.Books.Where(x => isbn.Contains(x.Isbn)).ToList();
        }

    }
}
