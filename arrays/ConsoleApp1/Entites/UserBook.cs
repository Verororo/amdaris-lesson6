namespace ConsoleApp1.Entites
{

    public class UserBook : Entity
    {
        public int BookId { get; set; }
        public int UserId { get; set; }

        public State BookState { get; set; }

        public UserBook(int bookId, int userId, State state)
        {
            BookId = bookId;
            UserId = userId;
            BookState = state;
        }
    }
}
