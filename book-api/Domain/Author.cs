namespace book_api.Domain
{
    public class Author
    {
        public string FirstName { get; }
        public string LastName { get; }

        public Author(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name cannot be empty.");
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be empty.");

            FirstName = firstName;
            LastName = lastName;
        }
    }
}