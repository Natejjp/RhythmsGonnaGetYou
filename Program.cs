using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RhythmsGonnaGetYou
{
    class RhythmsGonnaGetYouContext : DbContext
    {
        public DbSet<Band> Bands { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("server=localhost;database=RhythmsGonnaGetYou");
        }
    }
    class Band
    {
        public string Name { get; set; }
        public string CountryOfOrigin { get; set; }
        public int NumberOfMembers { get; set; }
        public string Website { get; set; }
        public string Style { get; set; }
        public Boolean IsSigned { get; set; }
        public string ContactName { get; set; }
        public string ContactPhoneNumber { get; set; }

    }

    class Album
    {
        public string Title { get; set; }
        public Boolean IsExplicit { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int BandId { get; set; }
        public Band Bands { get; set; }

    }

    class Song
    {
        public int TrackNumber { get; set; }
        public string Title { get; set; }
        public string Duration { get; set; }
        public int AlbumId { get; set; }
        public Album Albums { get; set; }

    }

    class Program
    {
        static string PromptForString(string prompt)
        {
            Console.Write(prompt);
            var userInput = Console.ReadLine().ToUpper();
            return userInput;
        }

        static int PromptForInteger(string prompt)
        {
            Console.Write(prompt);
            int userInput;
            var isThisGoodInput = Int32.TryParse(Console.ReadLine().ToUpper(), out userInput);
            if (isThisGoodInput)
            {
                return userInput;
            }
            else
            {
                Console.WriteLine("This is not valid input. Using 0 ");
                return 0;
            }

        }
        static void Main(string[] args)
        {
            var context = new RhythmsGonnaGetYouContext();

            Console.WriteLine("Welcome to the Musical Google!");
            Console.WriteLine("What would you like to do? Interact with (B)and database, (A)lbum database, or (S)ong database");
            var choice = Console.ReadLine().ToUpper();
            var keepGoing = true;

            while (keepGoing)
            {

                if (choice == "B")
                {
                    Console.WriteLine("Would you like to (V1)iew all bands, (V2)iew all signed bands, (V3)iew all unsigned bands, or (A)dd a band?");
                    var bandOptions = Console.ReadLine().ToUpper();
                    if (bandOptions == "V1")
                    {
                        foreach (var band in context.Bands)
                        {
                            Console.WriteLine($"{band.Name}");
                        }
                    }
                    else if (bandOptions == "V2")
                    {
                        var signedBands = context.Bands.Where(s => s.IsSigned == true);
                        foreach (var band in signedBands)
                        {
                            Console.WriteLine($"{band.Name} are signed! ");
                            var pineapple = PromptForString("Would you like to unsign any bands? y/n ");
                            if (pineapple == "y")
                            {
                                var mango = PromptForString("What band would you like to unsign? ");
                                var foundBand = context.Bands.FirstOrDefault(band => band.Name == mango);
                                if (mango == null)
                                {
                                    Console.WriteLine("No band by that name is signed! ");
                                }
                                else
                            }
                            else if (pineapple == "n")
                            {

                            }

                        }
                    }
                    else if (bandOptions == "V3")
                    {
                        var unSignedBands = context.Bands.Where(s => s.IsSigned == false);
                        foreach (var band in unSignedBands)
                        {
                            Console.WriteLine($"{band.Name} are not signed! ");
                            var pineapple = PromptForString("Would you like to sign any bands? y/n");
                            if (pineapple == "y")
                            {

                            }
                            else if (pineapple == "n")
                            {

                            }
                        }
                    }
                    else if (bandOptions == "A")
                    {
                        var newBand = new Band();
                        newBand.Name = PromptForString("What is the name of the band? ");
                        newBand.CountryOfOrigin = PromptForString("Where is this band from? ");
                        newBand.NumberOfMembers = PromptForInteger("How many members are in this band? ");
                        newBand.Website = PromptForString("What is the website for this band? ")
                        newBand.Style = PromptForString("What is this bands style? ");
                        newBand.ContactName = PromptForString("What is this name of contact? ");
                        newBand.ContactPhoneNumber = PromptForString("What is the contact phone number? ");

                        context.Bands.Add(newBand);
                        context.SaveChanges();
                    }

                }

                else if (choice == "A")
                {
                    Console.WriteLine("Would you like to (V1)iew all albums ordered by release date, (V2)iew albums by band, or (A)dd album to band? ");
                    var albumOptions = Console.ReadLine().ToUpper();
                    if (albumOptions == "V1")
                    {
                        var releaseDate = context.Albums.Include(album => album.Bands).OrderBy(album => album.ReleaseDate);
                        foreach (var pineapple in releaseDate)
                        {

                            Console.WriteLine($"{album.Title} by {album.Bands.Name} was released on {album.ReleaseDate}");

                        }
                    }
                    else if (albumOptions == "V2")
                    {
                        var viewAlbumByBand = PromptForString("What band's discography are you looking for?");
                        var foundBand = context.Bands.FirstOrDefault(band => band.Name == viewAlbumByBand);
                        if (viewAlbumByBand == null)
                        {
                            Console.WriteLine("No band by that name dummy!");
                        }
                        else
                        {
                            var bandAlbums = context.Albums.Where(album => album.Bands.Name == viewAlbumByBand);
                            foreach (var pineapple in bandAlbums)
                            {
                                Console.WriteLine($"{album.Title}");
                            }
                        }
                    }
                    else if (albumOptions == "A")
                    {
                        var newAlbum = new Album();
                        newAlbum.Title = PromptForString("What is the track number of the new song? ");
                        newAlbum.IsExplicit =
                        newAlbum.ReleaseDate =
                        newAlbum.BandId =

                        context.Albums.Add(newAlbum);
                        context.SaveChanges();
                    }
                }

                else if (choice == "S")
                {
                    Console.WriteLine("Would you like to (A)dd a song to an album?");
                    var songOptions = Console.ReadLine().ToUpper();
                    if (songOptions == "A")
                    {
                        var newSong = new Song();
                        newSong.TrackNumber = PromptForInteger("What is the track number of the new song? ");
                        newSong.Title = PromptForString("What is the title of the new song? ");
                        newSong.Duration = PromptForString("What is the duration of the song? ");
                        newSong.AlbumId = PromptForInteger("What is the album ID number? ");

                        context.Songs.Add(newSong);
                        context.SaveChanges();
                    }
                }

                else if (choice == "Q")
                {
                    keepGoing = false;
                }
            }

        }
    }
}

