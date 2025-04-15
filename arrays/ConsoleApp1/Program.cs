using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp1;

public class InMemoryRepository<T> : IRepository<T> where T : Entity
{
    private readonly List<T> _entities = new List<T>();
    private int _nextId = 1;

    public T GetById(int id)
    {
        return _entities.FirstOrDefault(e => e.Id == id);
    }

    public IList<T> FindAll()
    {
        return _entities.ToList();
    }

    public void Add(T entity)
    {
        if (entity.Id == 0)
        {
            entity.Id = _nextId++;
        }
        _entities.Add(entity);
    }

    public void Delete(T entity)
    {
        _entities.RemoveAll(e => e.Id == entity.Id);
    }

    public void Update(T entity)
    {
        var existingEntity = GetById(entity.Id);
        if (existingEntity != null)
        {
            _entities.Remove(existingEntity);
            _entities.Add(entity);
        }
    }
}

public class Program
{
    public static void Main()
    {
        var userRepository = new InMemoryRepository<User>();
        var wantedBookRepository = new InMemoryRepository<UsersBook>();
        var givenOutBookRepository = new InMemoryRepository<UsersBook>();
        var availableBookRepository = new InMemoryRepository<Book>();

        var bookRelationService = new UserBookRelation(
            userRepository,
            wantedBookRepository,
            givenOutBookRepository,
            availableBookRepository
        );

        var user1 = new User { Name = "Vasily Pupkin", Email = "vasya@example.com" };
        userRepository.Add(user1);

        var book1 = new Book
        {
            Title = "Clean Code",
            Author = "Robert C. Martin",
            Isbn = "9780132350884"
        };
        availableBookRepository.Add(book1);

        var book2 = new Book
        {
            Title = "Design Patterns",
            Author = "Erich Gamma et al.",
            Isbn = "9780201633610"
        };
        availableBookRepository.Add(book2);

        bool success = bookRelationService.AddBookToWanted(user1.Id, book1.Id, UsersBook.State.Good);

        if (success)
        {
            Console.WriteLine($"Book '{book1.Title}' has been added to {user1.Name}'s wanted list.");
            
            Console.WriteLine("\nWanted Books:");
            var wantedBooks = wantedBookRepository.FindAll();
            foreach (var book in wantedBooks)
            {
                Console.WriteLine($"- {book.Title} by {book.Author} (State: {book.BookState})");
            }
        }
        else
        {
            Console.WriteLine("Failed to add book to wanted list.");
        }

        success = bookRelationService.AddBookToWanted(user1.Id, book2.Id, UsersBook.State.Moderate);

        if (success)
        {
            Console.WriteLine($"Book '{book2.Title}' has been added to {user1.Name}'s wanted list.");

            Console.WriteLine("\nWanted Books:");
            var wantedBooks = wantedBookRepository.FindAll();
            foreach (var book in wantedBooks)
            {
                Console.WriteLine($"- {book.Title} by {book.Author} (State: {book.BookState})");
            }
        }
        else
        {
            Console.WriteLine("Failed to add book to wanted list.");
        }
    }
}