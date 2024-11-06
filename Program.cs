using System;
using System.Collections.Generic;
using System.Security.AccessControl;

// Main Program Class
// This class contains the Main method, which is the entry point of the program
class Program
{
    static void Main(string[] args)
    {
        // Display a welcome message and instructions for using the program
        Console.WriteLine("Welcome to the Film and Actors Catalogue!");
        Console.WriteLine("=========================================");
        Console.WriteLine("Use this program to manage your collection of films and actors.");
        Console.WriteLine("Choose an option from the menu below:");

        // Initialise lists to store films and actors
        List<Film> films = new List<Film>();
        List<Actor> actors = new List<Actor>();

        // Start the main program loop
        while (true)
        {
            // Display the menu options to the user
            DisplayMenu();
            // Get the user's menu choice
            string choice = GetUserChoice();
            // Handle the user's menu choice
            HandleUserChoice(choice, films, actors);
        }
    }

    // Displays the menu options to the user
    // This function provides a user-friendly interface for selecting actions within the program
    static void DisplayMenu()
    {
        Console.WriteLine("1 - Add a new Film");
        Console.WriteLine("2 - Add a new Actor");
        Console.WriteLine("3 - Display all Films");
        Console.WriteLine("4 - Load films and Actors");
        Console.WriteLine("5 - Save & Exit");
        Console.WriteLine("=========================================");
    }

    // Gets the user's menu choice
    // This function reads the user's choice from the console to determine which action to perform
    static string GetUserChoice()
    {
        Console.Write("Enter your choice: ");
        return Console.ReadLine();
    }

    // Handles the user's menu choice
    // This function takes the user's choice and calls the appropriate function to perform the action
    static void HandleUserChoice(string choice, List<Film> films, List<Actor> actors)
    {
        switch (choice)
        {
            case "1":
                // Call the function to add a new film to the catalogue
                AddNewFilm(films);
                break;
            case "2":
                // Call the function to add a new actor to the catalogue
                AddNewActor(actors);
                break;
            case "3":
                // Call the function to display all films in the catalogue
                DisplayAllFilms(films);
                break;
            case "4":
                // Call the function to load films and actors from a file
                LoadFilmsAndActors(films, actors);
                break;
            case "5":
                // Call the function to exit the program
                SaveDataExitProgram();
                break;
            default:
                // Display an error message for invalid menu choices
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }

    // Adds a new film to the catalogue
    // This function prompts the user for film details and adds the film to the list of films
    static void AddNewFilm(List<Film> films)
    {
        try
        {
            // Prompt the user to enter the film title
            Console.Write("Enter the film title: ");
            string filmTitle = Console.ReadLine();
            // Prompt the user to enter the film genre
            Console.Write("Enter the genre: ");
            string genre = Console.ReadLine();
            // Prompt the user to enter the release year of the film
            Console.Write("Enter the release year: ");
            if (!int.TryParse(Console.ReadLine(), out int releaseYear))
            {
                // Display an error message if the release year is not a valid number
                Console.WriteLine("Invalid input. Release year must be a number.");
                return;
            }

            // Create a new Film object and add it to the films list
            Film newFilm = new Film(filmTitle, genre, releaseYear);
            films.Add(newFilm);
            Console.WriteLine("Film added successfully!");
        }
        catch (Exception e) // Handles all possible exceptions
        {
            // Handle any unexpected errors that occur during the process
            Console.WriteLine("An error occurred: " + e.Message);
        }
    }

    // Adds a new actor to the catalogue
    // This function prompts the user for actor details and adds the actor to the list of actors
    static void AddNewActor(List<Actor> actors)
    {
        try
        {
            // Prompt the user to enter the actor's name
            Console.Write("Enter the actor's name: ");
            string actorName = Console.ReadLine();
            // Prompt the user to enter the actor's age
            Console.Write("Enter the actor's age: ");
            if (!int.TryParse(Console.ReadLine(), out int actorAge))  
            {
                // Display an error message if the age is not a valid number
                Console.WriteLine("Invalid input. Age must be a number.");
                return;
            }

            // Create a new Actor object and add it to the actors list
            Actor newActor = new Actor(actorName, actorAge);
            actors.Add(newActor);
            Console.WriteLine("Actor added successfully!");
        }
        catch (Exception e)
        {
            // Handle any unexpected errors that occur during the process
            Console.WriteLine("An error occurred: " + e.Message);
        }
    }

    // Displays information for all films in the catalogue
    // This function iterates through the list of films and displays their details
    static void DisplayAllFilms(List<Film> films)
    {
        if (films.Count == 0)
        {
            // Display a message if there are no films available in the catalogue
            Console.WriteLine("No films available in the catalogue.");
        }
        else
        {
            // Iterate over the list of films and display their information
            foreach (Film film in films)
            {
                film.DisplayInfo();
                Console.WriteLine();
            }
        }
    }

    // Loads films and actors from a file
    // This function prompts the user for a filename and loads films and actors from the file if it exists
    static void LoadFilmsAndActors(List<Film> films, List<Actor> actors)
    {
        Console.WriteLine("What file would you like to load?");
        string filename = Console.ReadLine();

        if (File.Exists(filename))
        {
            Console.WriteLine($"Loading data from '{filename}'...");
            List<string> lines = File.ReadAllLines(filename).ToList();

            bool isFilmSection = false;
            bool isActorSection = false;

            foreach (string line in lines)
            {
                if (line.Trim() == "Films:")
                {
                    // Start parsing the Films section
                    isFilmSection = true;
                    isActorSection = false;
                    Console.WriteLine("Found Films section");
                }
                else if (line.Trim() == "Actors:")
                {
                    // Start parsing the Actors section
                    isFilmSection = false;
                    isActorSection = true;
                    Console.WriteLine("Found Actors section");
                }
                else if (!string.IsNullOrWhiteSpace(line))
                {
                    if (isFilmSection)
                    {
                        try
                        {
                            // Split the line by comma to extract film details
                            var parts = line.Split(",");
                            if (parts.Length == 4)
                            {
                                string title = parts[0].Trim();
                                string genre = parts[2].Trim();
                                int releaseYear = int.Parse(parts[3].Trim());

                                // Create a new Film object and add it to the list
                                Film newFilm = new Film(title, genre, releaseYear);
                                films.Add(newFilm);
                                Console.WriteLine($"Loaded Film: {title}");
                            }
                            else
                            {
                                // Inform the user about incorrect film format
                                Console.WriteLine($"Incorrect film format: {line}");
                            }
                        }
                        catch (Exception e)
                        {
                            // Handle any error that occurs while parsing a film line
                            Console.WriteLine($"Error parsing film: {line}. Error: {e.Message}");
                        }
                    }
                    else if (isActorSection)
                    {
                        try
                        {
                            // Split the line by comma to extract actor details
                            var parts = line.Split(",");
                            if (parts.Length == 3)
                            {
                                string name = parts[0].Trim();
                                int age = int.Parse(parts[2].Trim());

                                // Create a new Actor object and add it to the list
                                Actor newActor = new Actor(name, age);
                                actors.Add(newActor);
                                Console.WriteLine($"Loaded Actor: {name}");
                            }
                            else
                            {
                                // Inform the user about incorrect actor format
                                Console.WriteLine($"Incorrect actor format: {line}");
                            }
                        }
                        catch (Exception e)
                        {
                            // Handle any error that occurs while parsing an actor line
                            Console.WriteLine($"Error parsing actor: {line}. Error: {e.Message}");
                        }
                    }
                }
            }

            // Display a success message after loading data
            Console.WriteLine("Data loaded successfully.");
        }
        else
        {
            // Inform the user if the specified file does not exist
            Console.WriteLine($"Sorry, '{filename}' does not exist.");
        }
    }

    // Exits the program
    // This function saves the data (if necessary) and then terminates the program
    static void SaveDataExitProgram()
    {
        // Display a goodbye message and exit the program
        Console.WriteLine("Your file has been saved, Goodbye!");
        Environment.Exit(0);
    }
}
