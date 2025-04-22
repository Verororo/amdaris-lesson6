using System;
using System.Collections.Generic;
using System.Linq;

using ConsoleApp1;
using ConsoleApp1.Entites;

public class Program
{
    public static void Main()
    {
        var userRepository = new InMemoryRepository<User>();
        var userBookRepository = new InMemoryRepository<UserBook>();
        var wantedUserBookRepository = new InMemoryRepository<WantedUserBook>();
        var booksRepository = new InMemoryRepository<Book>();

        var bookRelationService = new UserBookRelation(
            userRepository,
            userBookRepository,
            wantedUserBookRepository
        );

        var user1 = new User { Name = "Vasily Pupkin", Email = "vasya@example.com" };
        userRepository.Add(user1);

        var user2 = new User { Name = "Vasily Pupkin", Email = "vasya@example.com" };
        userRepository.Add(user2);

        var user3 = new User { Name = "Vasily Pupkin", Email = "vasya@example.com" };
        userRepository.Add(user3);

        var book1 = new Book
        {
            Title = "Clean Code",
            Author = "Robert C. Martin",
            Isbn = "9780132350884"
        };
        booksRepository.Add(book1);

        var book2 = new Book
        {
            Title = "Design Patterns",
            Author = "Erich Gamma et al.",
            Isbn = "9780201633610"
        };
        booksRepository.Add(book2);

        var userBook1 = new UserBook(book1.Id, user1.Id, State.Good);
        userBookRepository.Add(userBook1);

        var userBook2 = new UserBook(book1.Id, user3.Id, State.Bad);
        userBookRepository.Add(userBook2);

        var userBook3 = new UserBook(book2.Id, user1.Id, State.Good);
        userBookRepository.Add(userBook3);


        bool success = bookRelationService.AddBookToWanted(user2.Id, userBook1.Id);

        if (success)
        {
            Console.WriteLine($"Book '{book1.Title}' has been added to {user1.Name}'s wanted list.");

            Console.WriteLine("\nWanted Books:");
            var wantedBooks = wantedUserBookRepository.FindAll();
            foreach (var wantedBook in wantedBooks)
            {
                var userBook = userBookRepository.GetById(wantedBook.BookId);
                var book = booksRepository.GetById(userBook.BookId);
                Console.WriteLine($"- {book.Title} by {book.Author} (State: {userBook.BookState})");
            }
        }
        else
        {
            Console.WriteLine("Failed to add book to wanted list.");
        }

        success = bookRelationService.AddBookToWanted(user1.Id, userBook2.Id);

        if (success)
        {
            Console.WriteLine($"Book '{book2.Title}' has been added to {user1.Name}'s wanted list.");

            Console.WriteLine("\nWanted Books:");
            var wantedBooks = wantedUserBookRepository.FindAll();
            foreach (var wantedBook in wantedBooks)
            {
                var userBook = userBookRepository.GetById(wantedBook.BookId);
                var book = booksRepository.GetById(userBook.BookId);

                Console.WriteLine($"- {book.Title} by {book.Author} (State: {userBook.BookState})");
            }
        }
        else
        {
            Console.WriteLine("Failed to add book to wanted list.");
        }
    }
}