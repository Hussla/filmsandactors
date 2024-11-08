/// Class representing an Actor
public class Actor
{
    // Member Variables
    private string name; // Stores the name of the actor
    private int age; // Stores the age of the actor
    private List<string> films; // Stores the list of films the actor has appeared in

    // Constructor
    // Initialises a new instance of the Actor class with the specified name and age
    public Actor(string name, int age)
    {
        this.name = name;
        this.age = age;
        this.films = new List<string>();
    }

    // Member Functions
    // Returns the name of the actor
    public string GetName()
    {
        return name;
    }

    // Sets a new name for the actor
    public void SetName(string name)
    {
        this.name = name;
    }

    // Returns the age of the actor
    public int GetAge()
    {
        return age;
    }

    // Sets a new age for the actor
    public void SetAge(int age)
    {
        this.age = age;
    }

    // Adds a film to the actor's filmography
    public void AddFilm(string film)
    {
        films.Add(film);
    }

    // Returns a list of films the actor has appeared in
    public List<string> GetFilms()
    {
        return films;
    }

    // Displays detailed information about the actor, including their name, age, and filmography
    public void DisplayInfo()
    {
        Console.WriteLine("Name: " + name);
        Console.WriteLine("Age: " + age);
        Console.WriteLine("Films:");
        foreach (string film in films)
        {
            Console.WriteLine("- " + film);
        }
    }
}

