namespace Library_and_book_recommendations
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int YearPublished { get; set; }
        public string ISBN { get; set; }

        public Book() { }
        public Book(string title, string author, string genre, int yearPublished, string iSBN)
        {
            Title = title;
            Author = author;
            Genre = genre;
            YearPublished = yearPublished;
            ISBN = iSBN;
        }
    }
}
