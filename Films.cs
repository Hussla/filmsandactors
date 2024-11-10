
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

// Class representing a Film
public class Film
{
    // Member Variables
    private string title; // Stores the title of the film
    private string genre; // Stores the genre of the film (e.g., Action, Drama)
    private int releaseYear; // Stores the release year of the film
    private List<int> ratings; // Stores user ratings for the film
    private List<Actor> actors; // Stores the list of actors who have appeared in the film

    // Constructor
    // Initialises a new instance of the Film class with the specified title, genre, and release year
    public Film(string title, string genre, int releaseYear)
    {
        this.title = title;
        this.genre = genre;
        this.releaseYear = releaseYear;
        this.ratings = new List<int>();
        this.actors = new List<Actor>();
    }

    // Member Functions
    // Returns the title of the film
    public string GetTitle() => title;

    // Returns the genre of the film
    public string GetGenre() => genre;

    // Sets a new genre for the film
    public void SetGenre(string genre) => this.genre = genre;

    // Returns the release year of the film
    public int GetReleaseYear() => releaseYear;

    // Sets a new release year for the film
    public void SetReleaseYear(int releaseYear) => this.releaseYear = releaseYear;

    // Adds an actor to the film's cast
    public void AddActor(Actor actor) => actors.Add(actor);

    // Returns the list of actors in the film
    public List<Actor> GetActors() => actors;

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
        Console.WriteLine("Average Rating: " + GetAverageRating());
    }

    // Adds a rating to the film
    public void AddRating(int rating)
    {
        if (rating >= 1 && rating <= 5)
        {
            ratings.Add(rating);
        }
        else
        {
            throw new ArgumentException("Rating must be between 1 and 5.");
        }
    }

    // Calculates and returns the average rating of the film
    public double GetAverageRating()
    {
        if (ratings.Count == 0)
        {
            return 0.0; // No ratings available
        }
        else
        {
            return ratings.Average();
        }
    }

    // Unit Tests
    public static void RunTests()
    {
        Film testFilm = new Film("Test Film", "Action", 2021);

        // Test GetTitle
        Debug.Assert(testFilm.GetTitle() == "Test Film", "Error: GetTitle failed.");

        // Test GetGenre
        Debug.Assert(testFilm.GetGenre() == "Action", "Error: GetGenre failed.");

        // Test SetGenre
        testFilm.SetGenre("Drama");
        Debug.Assert(testFilm.GetGenre() == "Drama", "Error: SetGenre failed.");

        // Test GetReleaseYear
        Debug.Assert(testFilm.GetReleaseYear() == 2021, "Error: GetReleaseYear failed.");

        // Test SetReleaseYear
        testFilm.SetReleaseYear(2022);
        Debug.Assert(testFilm.GetReleaseYear() == 2022, "Error: SetReleaseYear failed.");

        // Test AddRating and GetAverageRating
        testFilm.AddRating(4);
        testFilm.AddRating(5);
        Debug.Assert(testFilm.GetAverageRating() == 4.5, "Error: GetAverageRating failed.");

        // Test AddActor
        Actor testActor = new Actor("Test Actor", 35);
        testFilm.AddActor(testActor);
        Debug.Assert(testFilm.GetActors().Count == 1, "Error: AddActor failed.");
        Debug.Assert(testFilm.GetActors()[0] == testActor, "Error: GetActors failed.");
    }
}
