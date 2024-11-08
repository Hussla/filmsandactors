
// Class representing a Film
public class Film
{
    // Member Variables
    private string title; // Stores the title of the film
    private string genre; // Stores the genre of the film (e.g., Action, Drama)
    private int releaseYear; // Stores the release year of the film
    private List<Actor> actors; // Stores the list of actors who have appeared in the film

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

    // Returns the genre of the film
    public string GetGenre()
    {
        return genre;
    }

    // Sets a new genre for the film
    public void SetGenre(string genre)
    {
        this.genre = genre;
    }

    // Returns the release year of the film
    public int GetReleaseYear()
    {
        return releaseYear;
    }

    // Sets a new release year for the film
    public void SetReleaseYear(int releaseYear)
    {
        this.releaseYear = releaseYear;
    }

    // Adds an actor to the film's cast
    public void AddActor(Actor actor)
    {
        actors.Add(actor);
    }

    // Returns the list of actors in the film
    public List<Actor> GetActors()
    {
        return actors;
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
            Console.WriteLine("- " + actor.GetName());
        }
    }
}




