namespace ConsoleApp1.Entites
{

    public class WantedUserBook : Entity
    {
        public int UserId { get; set; }

        public int BookId { get; set; }

        public WantedUserBook(int bookId, int userId)
        {
            BookId = bookId;
            UserId = userId;
        }
    }
}
