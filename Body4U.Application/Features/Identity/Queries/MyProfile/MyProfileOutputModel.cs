namespace Body4U.Application.Features.Identity.Queries.MyProfile
{
    public class MyProfileOutputModel
    {
        public MyProfileOutputModel(
            string id,
            string firstName,
            string lastName,
            string email,
            string? profilePicture,
            int age,
            string phoneNumber,
            int gender)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.ProfilePicture = profilePicture;
            this.Age = age;
            this.PhoneNumber = phoneNumber;
            this.Gender = gender;
        }

        public string Id { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string Email { get; }

        public string? ProfilePicture { get; }

        public int Age { get; set; }

        public string PhoneNumber { get; }

        public int Gender { get; }
    }
}
