namespace Domain.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public List<string> PastEmployers { get; set; }
        public List<string> Education { get; set; }
        public Dictionary<string, string> Preferences { get; set; }
        public byte[] ProfilePicture { get; set; } 
        public string Biography { get; set; }

        public static List<Person> GeneratePersons(int quantity)
        {
            var persons = new List<Person>();
            Random random = new Random();

            for (int i = 0; i < quantity; i++)
            {
                var person = new Person
                {
                    Id = i + 1,
                    FirstName = $"FirstName{i}",
                    LastName = $"LastName{i}",
                    BirthDate = new DateTime(random.Next(1950, 2000), random.Next(1, 12), random.Next(1, 28)),
                    Email = $"person{i}@example.com",
                    PhoneNumber = $"{random.Next(100000000, 999999999)}",
                    Address = $"Address {i}",
                    City = "CityName",
                    State = "StateName",
                    ZipCode = $"{random.Next(10000, 99999)}",
                    Country = "CountryName",
                    PastEmployers = new List<string> { "Company A", "Company B", "Company C" },
                    Education = new List<string> { "High School", "College", "Masters" },
                    Preferences = new Dictionary<string, string> { { "Preference1", "Value1" }, { "Preference2", "Value2" } },
                    ProfilePicture = new byte[30 * 30], 
                    Biography = new string('a', 300)
                };

                persons.Add(person);
            }

            return persons;
        }
    }
}
