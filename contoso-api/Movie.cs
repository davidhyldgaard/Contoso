using System;

namespace ContosoApi
{
    public class Movie
    {
        private DateTime dateTime;

        public Movie()
        {
        }

        public Movie(string name, string director, string cast, string description, DateTime dateTime, string genre)
        {
            Name = name;
            Director = director;
            Cast = cast;
            Description = description;
            this.dateTime = dateTime;
            Genre = genre;
        }

        public DateTime ReleaseDate { get; set; }

        public string Director { get; set; }

        public string Name { get; set; }

        public string Cast { get; set; }

        public string Genre { get; set; }

        public string Description { get; set; }
    }
}
