namespace Library_and_book_recommendations
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var books = Library.LoadBooksFromXml(@"../../../../Source/asd.xml");
            foreach (var book in books)
            {
                Console.WriteLine($"" +
                    $"ISBN: {book.ISBN}\n" +
                    $"Title: {book.Title}\n" +
                    $"Author: {book.Author}\n" +
                    $"Genre: {book.Genre}\n" +
                    $"Year Published: {book.YearPublished}\n");
            }

            Library.SaveBooksToXml(books, @"../../../../Source/output.xml");

            Book book1 = new Book("fasdf", "ertertert", "genree", 7012, "1212-12123145-53");
            Library.AddBookToXml(book1, @"../../../../Source/output.xml");

        }
    }
}
