namespace UserManagement.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public DateTime BirthDate { get; set; }

        public string BirthPlace { get; set; }

        public string City { get; set; }
        public User()
        {

        }

        public User(int id, string name, string password, string lastName, string firstName, DateTime birthDate, string birthPlace, string city)
        {
            Id = id;
            Name = name;
            Password = password;
            LastName = lastName;
            FirstName = firstName;
            BirthDate = birthDate;
            BirthPlace = birthPlace;
            City = city;
        }
    }
}
