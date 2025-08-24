using Library_and_book_recommendations;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Tests
{
    public class LibraryTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("����������", @"../../../../Source/asd.xml")]
        public void FindBooksByGenre_EqualTest(string genre, string filePath)
        {
            var fantasyBooks = Library.FindBooksByGenre(genre, filePath);
            Assert.AreEqual("���������� �� ���������", fantasyBooks[0].Title);
        }

        [TestCase("��� �������", @"../../../../Source/asd.xml")]
        public void FindBooksByAuthor_EqualTest(string author, string filePath)
        {
            var fantasyBooks = Library.FindBooksByAuthor(author, filePath);
            Assert.AreEqual("����� � ���", fantasyBooks[0].Title);
        }

        [TestCase(1936, @"../../../../Source/asd.xml")]
        public void FindBooksAfter_EqualTest(int year, string filePath)
        {
            var books = Library.FindBooksAfter(year, filePath);
            Assert.AreEqual("1984", books[0].Title);
        }

        [TestCase(1936, @"../../../../Source/asd.xml")]
        public void FindBooksBefore_EqualTest(int year, string filePath)
        {
            var books = Library.FindBooksBefore(year, filePath);
            Assert.AreEqual("����� � ���", books[0].Title);
        }

        [TestCase("978-0-00-713680-8", @"../../../../Source/asd.xml")]
        public void FindBookByISBN_EqualTest(string iSBN, string filePath)
        {
            var book = Library.FindBookByISBN(iSBN, filePath);
            Assert.AreEqual("������� � �������", book.Title);
        }
    }
}
