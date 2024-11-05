// Class representing a Film
    public class Film
    {
        // Member Variables
        // Stores the title of the film
        private string title;
        // Stores the genre of the film (e.g., Action, Drama)
        private string genre;
        // Stores the release year of the film
        private int releaseYear;
        // Stores the list of actors who have appeared in the film
        private List<Actor> actors;

        // Constructor
        // Initialises a new instance of the Film class with the specified title, genre, and release year
        public Film(string title, string genre, int releaseYear)
        {
            this.title = title;
            this.genre = genre;
            this.releaseYear = releaseYear;
            this.actors = new List<Actor>();
        }

        // Member Functions
        // Returns the title of the film
        public string GetTitle()
        {
            return title;
        }

        // Adds an actor to the film's cast
        public void AddActor(Actor actor)
        {
            actors.Add(actor);
        }

        // Displays detailed information about the film, including its title, genre, release year, and cast
        public void DisplayInfo()
        {
            Console.WriteLine("Title: " + title);
            Console.WriteLine("Genre: " + genre);
            Console.WriteLine("Release Year: " + releaseYear);
            Console.WriteLine("Actors:");
            foreach (Actor actor in actors)
            {
                Console.WriteLine(" - " + actor.GetName());
            }
        }
    }
