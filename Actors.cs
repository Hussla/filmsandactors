// Class representing an Actor
    public class Actor
    {
        // Member Variables
        // Stores the name of the actor
        private string name;
        // Stores the age of the actor
        private int age;
        // Stores the list of films the actor has appeared in
        private List<string> films;

        // Constructor
        // Initializes a new instance of the Actor class with the specified name and age
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

        // Returns the age of the actor
        public int GetAge()
        {
            return age;
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
    }
