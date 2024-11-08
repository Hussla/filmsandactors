using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;

// Main Program Class
class Program
{
    static void Main(string[] args)
    {
        // Display a welcome message and instructions for using the program
        Console.WriteLine("Welcome to the Film and Actors Catalogue!");
        Console.WriteLine("=========================================");
        Console.WriteLine("Use this program to manage your collection of films and actors.");
        Console.WriteLine("Choose an option from the menu below:");

        // Initialise dictionary to store films and actors
        Dictionary<string, Film> films = new Dictionary<string, Film>();
        Dictionary<string, Actor> actors = new Dictionary<string, Actor>();

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
    static void DisplayMenu()
    {
        Console.WriteLine("1 - Add a new Film");
        Console.WriteLine("2 - Add a new Actor");
        Console.WriteLine("3 - Display all Films");
        Console.WriteLine("4 - Display Films by Genre");
        Console.WriteLine("5 - Load films and Actors");
        Console.WriteLine("6 - Search Films or Actors");
        Console.WriteLine("7 - Update Film or Actor Information");
        Console.WriteLine("8 - Display Films by Actor");
        Console.WriteLine("9 - Save & Exit");
        Console.WriteLine("=========================================");
    }

    // Gets the user's menu choice
    static string GetUserChoice()
    {
        Console.Write("Enter your choice: ");
        return Console.ReadLine();
    }

    // Handles the user's menu choice
    static void HandleUserChoice(string choice, Dictionary<string, Film> films, Dictionary<string, Actor> actors)
    {
        switch (choice)
        {
            case "1":
                AddNewFilm(films); // Call function to add a new film
                break;
            case "2":
                AddNewActor(actors); // Call function to add a new actor
                break;
            case "3":
                DisplayAllFilms(films); // Call function to display all films
                break;
            case "4":
                DisplayFilmsByGenre(films); // Call function to display films by genre
                break;
            case "5":
                LoadFilmsAndActors(films, actors); // Call function to load films and actors
                break;
            case "6":
                SearchFilmsOrActors(films, actors); // Call function to search films or actors
                break;
            case "7":
                UpdateFilmOrActorInformation(films, actors); // Call function to update film or actor information
                break;
            case "8":
                DisplayFilmsByActor(films, actors); // Call function to display films by actor
                break;
            case "9":
                SaveDataToFile(films, actors); // Save data before exiting
                SaveDataExitProgram();
                break;
            default:
                Console.WriteLine("Invalid choice. Please try again."); // Handle invalid menu choices
                break;
        }
    }

    // Adds a new film to the catalogue
    static void AddNewFilm(Dictionary<string, Film> films)
    {
        try
        {
            Console.Write("Enter the film title: ");
            string filmTitle = Console.ReadLine();

            // Check if the film title already exists to avoid overwriting
            if (films.ContainsKey(filmTitle))
            {
                Console.WriteLine("A film with this title already exists. Please enter a different title.");
                return;
            }

            Console.Write("Enter the genre: ");
            string genre = Console.ReadLine();

            Console.Write("Enter the release year: ");
            if (!int.TryParse(Console.ReadLine(), out int releaseYear))
            {
                Console.WriteLine("Invalid input. Release year must be a number.");
                return;
            }

            // Create a new Film object and add it to the films dictionary
            Film newFilm = new Film(filmTitle, genre, releaseYear);
            films[filmTitle] = newFilm;
            Console.WriteLine("Film added successfully!");
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred: " + e.Message); // Handle unexpected errors
        }
    }

    // Adds a new actor to the catalogue
    static void AddNewActor(Dictionary<string, Actor> actors)
    {
        try
        {
            Console.Write("Enter the actor's name: ");
            string actorName = Console.ReadLine();

            // Check if the actor's name already exists to avoid overwriting
            if (actors.ContainsKey(actorName))
            {
                Console.WriteLine("An actor with this name already exists. Please enter a different name.");
                return;
            }

            Console.Write("Enter the actor's age: ");
            if (!int.TryParse(Console.ReadLine(), out int actorAge))
            {
                Console.WriteLine("Invalid input. Age must be a number.");
                return;
            }

            // Create a new Actor object and add it to the actors dictionary
            Actor newActor = new Actor(actorName, actorAge);
            actors[actorName] = newActor;
            Console.WriteLine("Actor added successfully!");
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred: " + e.Message); // Handle unexpected errors
        }
    }

    // Displays information for all films in the catalogue
    static void DisplayAllFilms(Dictionary<string, Film> films)
    {
        if (films.Count == 0)
        {
            Console.WriteLine("No films available in the catalogue.");
        }
        else
        {
            foreach (Film film in films.Values)
            {
                film.DisplayInfo(); // Call DisplayInfo method of Film class to show film details
                Console.WriteLine();
            }
        }
    }

    // Displays films by genre using LINQ
    static void DisplayFilmsByGenre(Dictionary<string, Film> films)
    {
        Console.Write("Enter the genre to filter by: ");
        string genre = Console.ReadLine();

        // Use LINQ to filter films by the specified genre
        var filteredFilms = films.Values.Where(f => f.GetGenre().Equals(genre, StringComparison.OrdinalIgnoreCase)).ToList();

        if (filteredFilms.Count == 0)
        {
            Console.WriteLine($"No films found in the genre '{genre}'.");
        }
        else
        {
            Console.WriteLine($"Films in the genre '{genre}':");
            foreach (Film film in filteredFilms)
            {
                film.DisplayInfo();
                Console.WriteLine();
            }
        }
    }

    // Loads films and actors from a file
    static void LoadFilmsAndActors(Dictionary<string, Film> films, Dictionary<string, Actor> actors)
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
                            if (parts.Length >= 3)
                            {
                                string title = parts[0].Trim();
                                string genre = parts[1].Trim();
                                if (int.TryParse(parts[2].Trim(), out int releaseYear))
                                {
                                    // Create a new Film object and add it to the list
                                    Film newFilm = new Film(title, genre, releaseYear);
                                    films[title] = newFilm;
                                    Console.WriteLine($"Loaded Film: {title}");
                                }
                                else
                                {
                                    Console.WriteLine($"Incorrect film format: {line}");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Incorrect film format: {line}");
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Error parsing film: {line}. Error: {e.Message}");
                        }
                    }
                    else if (isActorSection)
                    {
                        try
                        {
                            // Split the line by comma to extract actor details
                            var parts = line.Split(",");
                            if (parts.Length >= 2)
                            {
                                string name = parts[0].Trim();
                                if (int.TryParse(parts[1].Trim(), out int age))
                                {
                                    // Create a new Actor object and add it to the list
                                    Actor newActor = new Actor(name, age);
                                    actors[name] = newActor;
                                    Console.WriteLine($"Loaded Actor: {name}");
                                }
                                else
                                {
                                    Console.WriteLine($"Incorrect actor format: {line}");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Incorrect actor format: {line}");
                            }
                        }
                        catch (Exception e)
                        {
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

    // Saves films and actors to a file
    static void SaveDataToFile(Dictionary<string, Film> films, Dictionary<string, Actor> actors)
    {
        Console.WriteLine("Enter the filename to save data to:");
        string filename = Console.ReadLine();

        try
        {
            List<string> lines = new List<string>();

            // Write Films section
            lines.Add("Films:");
            foreach (Film film in films.Values)
            {
                lines.Add($"{film.GetTitle()}, {film.GetGenre()}, {film.GetReleaseYear()}");
            }

            // Write Actors section
            lines.Add("Actors:");
            foreach (Actor actor in actors.Values)
            {
                lines.Add($"{actor.GetName()}, {actor.GetAge()}");
            }

            // Write all lines to the specified file
            File.WriteAllLines(filename, lines);

            Console.WriteLine("Data saved successfully.");
        }
        catch (Exception e)
        {
            // Handle any unexpected errors that occur during the save process
            Console.WriteLine("An error occurred while saving data: " + e.Message);
        }
    }

    // Exits the program
    static void SaveDataExitProgram()
    {
        // Display a goodbye message and exit the program
        Console.WriteLine("Your file has been saved, Goodbye!");
        Environment.Exit(0);
    }

    // Search functionality to find films or actors using LINQ
    static void SearchFilmsOrActors(Dictionary<string, Film> films, Dictionary<string, Actor> actors)
    {
        Console.WriteLine("Enter search term (film title or actor name):");
        string searchTerm = Console.ReadLine().ToLower();

        // Search for films that match the search term
        var matchingFilms = films.Values.Where(f => f.GetTitle().ToLower().Contains(searchTerm)).ToList();
        if (matchingFilms.Count > 0)
        {
            Console.WriteLine("Matching Films:");
            foreach (var film in matchingFilms)
            {
                film.DisplayInfo();
            }
        }
        else
        {
            Console.WriteLine("No matching films found.");
        }

        // Search for actors that match the search term
        var matchingActors = actors.Values.Where(a => a.GetName().ToLower().Contains(searchTerm)).ToList();
        if (matchingActors.Count > 0)
        {
            Console.WriteLine("Matching Actors:");
            foreach (var actor in matchingActors)
            {
                actor.DisplayInfo();
            }
        }
        else
        {
            Console.WriteLine("No matching actors found.");
        }
    }

    // Update functionality to modify film or actor information
    static void UpdateFilmOrActorInformation(Dictionary<string, Film> films, Dictionary<string, Actor> actors)
    {
        Console.WriteLine("Do you want to update a Film or an Actor? (Enter 'Film' or 'Actor'):");
        string choice = Console.ReadLine().ToLower();

        if (choice == "film")
        {
            Console.Write("Enter the title of the film to update: ");
            string filmTitle = Console.ReadLine();

            if (films.ContainsKey(filmTitle))
            {
                Console.Write("Enter new genre: ");
                string newGenre = Console.ReadLine();

                Console.Write("Enter new release year: ");
                if (!int.TryParse(Console.ReadLine(), out int newReleaseYear))
                {
                    Console.WriteLine("Invalid input. Release year must be a number.");
                    return;
                }

                // Update film details
                films[filmTitle].SetGenre(newGenre);
                films[filmTitle].SetReleaseYear(newReleaseYear);
                Console.WriteLine("Film updated successfully!");
            }
            else
            {
                Console.WriteLine("Film not found.");
            }
        }
        else if (choice == "actor")
        {
            Console.Write("Enter the name of the actor to update: ");
            string actorName = Console.ReadLine();

            if (actors.ContainsKey(actorName))
            {
                Console.Write("Enter new age: ");
                if (!int.TryParse(Console.ReadLine(), out int newAge))
                {
                    Console.WriteLine("Invalid input. Age must be a number.");
                    return;
                }

                // Update actor details
                actors[actorName].SetAge(newAge);
                Console.WriteLine("Actor updated successfully!");
            }
            else
            {
                Console.WriteLine("Actor not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid choice. Please enter either 'Film' or 'Actor'.");
        }
    }

    // Displays films by actor
    static void DisplayFilmsByActor(Dictionary<string, Film> films, Dictionary<string, Actor> actors)
    {
        Console.Write("Enter the actor's name to find their films: ");
        string actorName = Console.ReadLine();

        if (actors.ContainsKey(actorName))
        {
            // Use LINQ to find films featuring the specified actor
            var filmsWithActor = films.Values.Where(f => f.GetActors().Any(a => a.GetName().Equals(actorName, StringComparison.OrdinalIgnoreCase))).ToList();

            if (filmsWithActor.Count > 0)
            {
                Console.WriteLine($"Films featuring {actorName}:");
                foreach (var film in filmsWithActor)
                {
                    film.DisplayInfo();
                }
            }
            else
            {
                Console.WriteLine($"No films found featuring {actorName}.");
            }
        }
        else
        {
            Console.WriteLine($"Actor '{actorName}' not found.");
        }
    }
}




// Explanation of Topics Included So Far
/*
    1. Console IO and Variables:
        - The program makes extensive use of Console.WriteLine() and Console.ReadLine() for user interaction.
        - Variables are used to store user input and data like films and actors.

    2. Operators and Selection:
        - The program uses selection structures like `switch` statements in `HandleUserChoice` to decide which action to perform based on the user's input.
        - This approach provides clear, structured decision-making, making the code easier to understand and maintain.
        - An alternative could be using `if-else` statements, but `switch` is more suitable here for handling multiple distinct options.
        - Improvement: A dictionary with function pointers could be used to reduce verbosity and improve scalability.

    3. Functions, Return, and Parameters:
        - The program is modularized using functions such as `AddNewFilm`, `AddNewActor`, etc.
        - Functions take parameters, return values, and perform specific tasks to maintain separation of concerns.
        - This makes the program more organized and easier to test.
        - Alternatives include having all the logic in the main method, which would make the program much less readable.
        - Improvement: Functions could return more descriptive results to make error handling more consistent and detailed.

    4. Collections and foreach Loop:
        - The program makes use of `Dictionary<string, Film>` and `Dictionary<string, Actor>` to store collections of films and actors
          instead of lists for storing films and actors, which will improve lookup performance and ensure unique entries for each film and actor.
        - The `foreach` loop is used to iterate over these dictionaries when displaying or saving data, which enhances readability and simplicity when accessing all elements.
        - An alternative would be using arrays, but dictionaries are more suitable for fast lookups and managing unique keys for each film and actor.
        - Improvement: Consider using LINQ for more advanced data querying and manipulation.

    5. Class Member Functions:
        - The `Film` and `Actor` classes have member functions such as `GetTitle`, `GetName`, and `AddActor` to encapsulate their behavior.
        - These member functions allow access to private data members and perform operations related to each class, ensuring data encapsulation.
        - Direct access to variables could be used, but using member functions provides better control over how data is accessed and modified.
        - Improvement: Properties with getters and setters could be used for more fine-grained control over data access.

    6. File IO:
        - The program allows loading and saving of films and actors data using text files.
        - Functions like `LoadFilmsAndActors` and `SaveDataToFile` are responsible for reading and writing data to files, making the data persistent across program runs.
        - Alternatives include using a database or JSON/XML for structured data storage, which would offer more flexibility and scalability.
        - Improvement: Implementing error recovery to handle partial data loads or invalid file formats more gracefully.
*/

