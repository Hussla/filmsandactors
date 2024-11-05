using System;
using System.Collections.Generic;

    // Main Program Class
    // This class contains the Main method, which is the entry point of the program
    class Program
    {
        static void Main(string[] args)
        {
            // Display a welcome message and instructions for using the program
            Console.WriteLine("Welcome to the Film and Actors Catalogue!");
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
        static void DisplayMenu()
        {
            Console.WriteLine("1 - Add a new Film");
            Console.WriteLine("2 - Add a new Actor");
            Console.WriteLine("3 - Display all Films");
            Console.WriteLine("4 - Exit");
        }

        // Gets the user's menu choice
        static string GetUserChoice()
        {
            Console.Write("Enter your choice: ");
            return Console.ReadLine();
        }

        // Handles the user's menu choice
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

        // Exits the program
        static void SaveDataExitProgram()
        {
            // Display a goodbye message and exit the program
            Console.WriteLine("Your file has been saved, Goodbye!");
            Environment.Exit(0);
        }
    }
