using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public abstract class Entity
    {
        public int Id { get; set; }
    }

    public class Book : Entity
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Isbn { get; set; }
    }

    public class UsersBook : Book
    {
        public enum State
        {
            Bad,
            Moderate,
            Good
        }

        public State BookState { get; set; }

        public UsersBook(Book book, State state)
        {
            this.Id = book.Id;
            this.Title = book.Title;
            this.Author = book.Author;
            this.Isbn = book.Isbn;
            this.BookState = state;
        }
    }

    public class User : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public interface IRepository<T> where T : Entity
    {
        T GetById(int id);
        IList<T> FindAll();

        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }

    public class UserBookRelation
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<UsersBook> _wantedBookRepository;
        private readonly IRepository<UsersBook> _givenOutBookRepository;
        private readonly IRepository<Book> _availableBookRepository;

        public UserBookRelation(IRepository<User> userRepository, IRepository<UsersBook> wantedBookRepository,
            IRepository<UsersBook> givenOutBookRepository, IRepository<Book> availableBookRepository)
        {
            _userRepository = userRepository;
            _wantedBookRepository = wantedBookRepository;
            _givenOutBookRepository = givenOutBookRepository;
            _availableBookRepository = availableBookRepository;
        }

        public bool AddBookToWanted(int userId, int bookId, UsersBook.State state)
        {
            User user = _userRepository.GetById(userId);
            Book book = _availableBookRepository.GetById(bookId);

            if (user == null || book == null || _givenOutBookRepository.GetById(bookId) != null)
            {
                return false;
            }

            UsersBook newBook = new UsersBook(book, state);
            _wantedBookRepository.Add(newBook);

            return true;
        }
    }
}
