namespace Library_and_book_recommendations
{
    public class CLI
    {
        private const string FilePath = "library.xml";
        private bool _isRunning = true;

        public void Run()
        {
            // Проверяем существование файла, если его нет - создаем пустую библиотеку
            if (!File.Exists(FilePath))
            {
                InitializeLibraryFile();
            }

            Console.WriteLine("=== СТОБАЛЛЬНЫЙ БИБЛИОТЕКАРЬ ===");
            Console.WriteLine("Добро пожаловать в систему управления библиотекой!\n");

            while (_isRunning)
            {
                ShowMainMenu();
                string choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1":
                        ShowAllBooks();
                        break;
                    case "2":
                        SearchBooks();
                        break;
                    case "3":
                        AddNewBook();
                        break;
                    case "4":
                        _isRunning = false;
                        Console.WriteLine("\nСпасибо за работу! До свидания!");
                        break;
                    default:
                        Console.WriteLine("Некорректный выбор. Пожалуйста, попробуйте снова.\n");
                        break;
                }
            }
        }

        private void ShowMainMenu()
        {
            Console.WriteLine("Главное меню:");
            Console.WriteLine("1. Просмотреть все книги");
            Console.WriteLine("2. Поиск книг");
            Console.WriteLine("3. Добавить новую книгу");
            Console.WriteLine("4. Выйти из программы");
            Console.Write("Выберите действие (1-4): ");
        }

        private void ShowAllBooks()
        {
            try
            {
                var books = Library.LoadBooksFromXml(FilePath);

                if (books.Count == 0)
                {
                    Console.WriteLine("\nБиблиотека пуста.\n");
                    return;
                }

                Console.WriteLine($"\n=== ВСЕ КНИГИ ({books.Count} шт.) ===");
                DisplayBooks(books);
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nОшибка при загрузке книг: {ex.Message}\n");
            }
        }

        private void SearchBooks()
        {
            Console.WriteLine("\n=== ПОИСК КНИГ ===");
            Console.WriteLine("Выберите критерий поиска:");
            Console.WriteLine("1. По жанру");
            Console.WriteLine("2. По автору");
            Console.WriteLine("3. По году издания (после указанного года)");
            Console.WriteLine("4. По году издания (до указанного года)");
            Console.WriteLine("5. По ISBN");
            Console.Write("Выберите критерий (1-5): ");

            string choice = Console.ReadLine()?.Trim();

            try
            {
                switch (choice)
                {
                    case "1":
                        SearchByGenre();
                        break;
                    case "2":
                        SearchByAuthor();
                        break;
                    case "3":
                        SearchAfterYear();
                        break;
                    case "4":
                        SearchBeforeYear();
                        break;
                    case "5":
                        SearchByISBN();
                        break;
                    default:
                        Console.WriteLine("Некорректный выбор критерия поиска.\n");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при поиске: {ex.Message}\n");
            }
        }

        private void SearchByGenre()
        {
            Console.Write("Введите жанр: ");
            string genre = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(genre))
            {
                Console.WriteLine("Жанр не может быть пустым.\n");
                return;
            }

            var books = Library.FindBooksByGenre(genre, FilePath);
            DisplaySearchResults(books, $"найдено по жанру '{genre}'");
        }

        private void SearchByAuthor()
        {
            Console.Write("Введите имя автора: ");
            string author = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(author))
            {
                Console.WriteLine("Имя автора не может быть пустым.\n");
                return;
            }

            var books = Library.FindBooksByAuthor(author, FilePath);
            DisplaySearchResults(books, $"найдено по автору '{author}'");
        }

        private void SearchAfterYear()
        {
            Console.Write("Введите год (показать книги после этого года): ");

            if (int.TryParse(Console.ReadLine()?.Trim(), out int year))
            {
                var books = Library.FindBooksAfter(year, FilePath);
                DisplaySearchResults(books, $"найдено после {year} года");
            }
            else
            {
                Console.WriteLine("Некорректный формат года.\n");
            }
        }

        private void SearchBeforeYear()
        {
            Console.Write("Введите год (показать книги до этого года): ");

            if (int.TryParse(Console.ReadLine()?.Trim(), out int year))
            {
                var books = Library.FindBooksBefore(year, FilePath);
                DisplaySearchResults(books, $"найдено до {year} года");
            }
            else
            {
                Console.WriteLine("Некорректный формат года.\n");
            }
        }

        private void SearchByISBN()
        {
            Console.Write("Введите ISBN: ");
            string isbn = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(isbn))
            {
                Console.WriteLine("ISBN не может быть пустым.\n");
                return;
            }

            var book = Library.FindBookByISBN(isbn, FilePath);

            if (book != null)
            {
                Console.WriteLine("\n=== НАЙДЕННАЯ КНИГА ===");
                DisplayBook(book);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"\nКнига с ISBN '{isbn}' не найдена.\n");
            }
        }

        private void AddNewBook()
        {
            Console.WriteLine("\n=== ДОБАВЛЕНИЕ НОВОЙ КНИГИ ===");

            try
            {
                Console.Write("Название книги: ");
                string title = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(title))
                {
                    Console.WriteLine("Название не может быть пустым.\n");
                    return;
                }

                Console.Write("Автор: ");
                string author = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(author))
                {
                    Console.WriteLine("Автор не может быть пустым.\n");
                    return;
                }

                Console.Write("Жанр: ");
                string genre = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(genre))
                {
                    Console.WriteLine("Жанр не может быть пустым.\n");
                    return;
                }

                Console.Write("Год издания: ");
                if (!int.TryParse(Console.ReadLine()?.Trim(), out int year))
                {
                    Console.WriteLine("Некорректный формат года.\n");
                    return;
                }

                Console.Write("ISBN: ");
                string isbn = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(isbn))
                {
                    Console.WriteLine("ISBN не может быть пустым.\n");
                    return;
                }

                // Проверяем, существует ли книга с таким ISBN
                var existingBook = Library.FindBookByISBN(isbn, FilePath);
                if (existingBook != null)
                {
                    Console.WriteLine($"\nКнига с ISBN '{isbn}' уже существует в библиотеке:\n");
                    DisplayBook(existingBook);
                    Console.WriteLine("Добавление отменено.\n");
                    return;
                }

                Book newBook = new Book(title, author, genre, year, isbn);
                Library.AddBookToXml(newBook, FilePath);

                Console.WriteLine($"\nКнига '{title}' успешно добавлена в библиотеку!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении книги: {ex.Message}\n");
            }
        }

        private void DisplaySearchResults(List<Book> books, string criteria)
        {
            if (books.Count == 0)
            {
                Console.WriteLine($"\nПо вашему запросу ({criteria}) ничего не найдено.\n");
            }
            else
            {
                Console.WriteLine($"\n=== РЕЗУЛЬТАТЫ ПОИСКА ({books.Count} шт. {criteria}) ===");
                DisplayBooks(books);
                Console.WriteLine();
            }
        }

        private void DisplayBooks(List<Book> books)
        {
            if (books.Count == 0)
            {
                Console.WriteLine("Список пуст.");
                return;
            }

            for (int i = 0; i < books.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {FormatBookInfo(books[i])}");
            }
        }

        private void DisplayBook(Book book)
        {
            Console.WriteLine(FormatBookInfo(book));
        }

        private string FormatBookInfo(Book book)
        {
            return $"{book.Title} | {book.Author} | {book.Genre} | {book.YearPublished} г. | ISBN: {book.ISBN}";
        }

        private void InitializeLibraryFile()
        {
            try
            {
                var emptyBooks = new List<Book>();
                Library.SaveBooksToXml(emptyBooks, FilePath);
                Console.WriteLine("Создан новый файл библиотеки.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при создании файла библиотеки: {ex.Message}\n");
            }
        }
    }
}