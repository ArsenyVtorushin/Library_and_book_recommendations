using Library_and_book_recommendations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Library_and_book_recommendations
{
    public static class Library
    {
        public static void SaveBooksToXml(List<Book> books, string filePath)
        {
            XDocument doc = new XDocument();
            XElement root = new XElement("Books");

            foreach (Book book in books)
            {
                XElement elem = new XElement("Book",
                    new XAttribute("ISBN", book.ISBN),
                    new XElement("Title", book.Title),
                    new XElement("Author", book.Author),
                    new XElement("Genre", book.Genre),
                    new XElement("YeaPublished", book.YearPublished));
                root.Add(elem);
            }

            doc.Add(root);
            doc.Save(filePath);
        }


        public static List<Book> LoadBooksFromXml(string filePath)
        {
            XDocument doc = XDocument.Load(filePath);
            XElement? root = doc.Element("Books");

            List<Book> books = new List<Book>();
            if (root is not null)
            {
                foreach (XElement elem in root.Elements("Book"))
                {
                    books.Add(new Book(
                        elem.Element("Title")?.Value,
                        elem.Element("Author")?.Value,
                        elem.Element("Genre")?.Value,
                        elem.Element("YearPublished")?.Value,
                        elem.Attribute("ISBN")?.Value));
                }
            }

            return books;
        }
    }
}
