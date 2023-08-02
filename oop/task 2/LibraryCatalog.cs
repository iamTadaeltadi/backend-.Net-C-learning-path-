using System;
using System.Collections.Generic;

class Library
{
    public string Name { get; set; }
    public string Address { get; set; }
    public List<Book> Books { get; }
    public List<MediaItem> MediaItems { get; }

    public Library(string name, string address)
    {
        Name = name;
        Address = string.IsNullOrEmpty(address) ? "unknown" : address; ;
        Books = new List<Book>();
        MediaItems = new List<MediaItem>();
    }

    public void AddBook(Book book)
    {
        Books.Add(book);
    }

    public void RemoveBook(Book book)
    {
        Books.Remove(book);
    }

    public void AddMediaItem(MediaItem item)
    {
        MediaItems.Add(item);
    }

    public void RemoveMediaItem(MediaItem item)
    {
        MediaItems.Remove(item);
    }

    public void PrintCatalog()
    {
        Console.WriteLine("Library Catalog:");
        Console.WriteLine("Books:");
        foreach (var book in Books)
        {
            Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, ISBN: {book.ISBN}, Publication Year: {book.PublicationYear}");
        }
        Console.WriteLine("\nMedia Items:");
        foreach (var item in MediaItems)
        {
            Console.WriteLine($"Title: {item.Title}, Media Type: {item.MediaType}, Duration: {item.Duration} minutes");
        }
    }
}

class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public int PublicationYear { get; set; }

    public Book(string title, string author, string isbn, int publicationYear)
    {
        Title = title;
        Author = string.IsNullOrEmpty(author) ? "unknown" : author;
        ISBN = isbn;
        PublicationYear = publicationYear;
    }
}

class MediaItem
{
    public string Title { get; set; }
    public string MediaType { get; set; }
    public int Duration { get; set; }

    public MediaItem(string title, string mediaType, int duration)
    {
        Title = title;
        MediaType = string.IsNullOrEmpty(mediaType) ? "unknown" : mediaType;
        Duration = duration;
    }
}

class Program
{
    static void Main()
    {
        Library library = new Library("Sample Library", "123 Main Street");


        // handling inputs

        while (true)
        {
            Console.WriteLine("\nChoose an option:");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Add Media Item");
            Console.WriteLine("3. Print Catalog");
            Console.WriteLine("4. Exit");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                continue;
            }

            
            switch (choice)
            {
                case 1:
                    // Add Book
                    Console.Write("Enter Book Title: ");
                    string bookTitle = Console.ReadLine();
                    Console.Write("Enter Author: ");
                    string author = Console.ReadLine();
                    Console.Write("Enter ISBN: ");
                    string isbn = Console.ReadLine();
                    int publicationYear;
                    Console.Write("Enter Publication Year: ");
                    if (!int.TryParse(Console.ReadLine(), out publicationYear))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid year.");
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(bookTitle))
                    {
                        Console.WriteLine("please book's title can be empty ");
                        continue;
                    }


                    Book newBook = new Book(bookTitle, author, isbn, publicationYear);
                    library.AddBook(newBook);
                    Console.WriteLine("Book added to the library catalog.");
                    break;

                case 2:
                    // Add Media Item
                    Console.Write("Enter Media Title: ");
                    string mediaTitle = Console.ReadLine();
                    Console.Write("Enter Media Type (DVD/CD): ");
                    string mediaType = Console.ReadLine();
                    int duration;
                    Console.Write("Enter Duration (minutes): ");
                    if (!int.TryParse(Console.ReadLine(), out duration))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid duration.");
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(mediaTitle))
                    {
                        Console.WriteLine("please book's title can be empty ");
                        continue;
                    }
                    MediaItem newMediaItem = new MediaItem(mediaTitle, mediaType, duration);
                    library.AddMediaItem(newMediaItem);
                    Console.WriteLine("Media item added to the library catalog.");
                    break;

                case 3:
                    // Print Catalog
                    library.PrintCatalog();
                    break;

                case 4:
                    // Exit
                    Console.WriteLine("Exiting the program...");
                    return;

                default:
                    Console.WriteLine("Invalid option. Please choose a valid option.");
                    break;
            }
        }

    }
}
