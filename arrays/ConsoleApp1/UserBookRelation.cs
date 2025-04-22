using ConsoleApp1.Entites;
using ConsoleApp1.Repository;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class UserBookRelation(
        IRepository<User> userRepository,
        IRepository<UserBook> userBookRepository,
        IRepository<WantedUserBook> wantedUserBookRepository)
    {
        public bool AddBookToWanted(int userId, int bookId)
        {
            User user = userRepository.GetById(userId);
            UserBook book = userBookRepository.GetById(bookId);

            if (
                user == null ||
                book == null ||
                book.UserId == userId ||
                wantedUserBookRepository.GetByPredicate(x => x.UserId == userId && x.BookId == bookId).Any())
            {
                return false;
            }

            WantedUserBook newBook = new WantedUserBook(bookId, userId);
            wantedUserBookRepository.Add(newBook);

            return true;
        }
    }
}
