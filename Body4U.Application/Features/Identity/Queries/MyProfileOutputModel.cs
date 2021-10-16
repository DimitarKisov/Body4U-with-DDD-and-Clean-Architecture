namespace Body4U.Application.Features.Identity.Queries
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
            int gender,
            int? trainerId)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.ProfilePicture = profilePicture;
            this.Age = age;
            this.PhoneNumber = phoneNumber;
            this.Gender = gender;
            this.TrainerId = trainerId;
        }

        public string Id { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string Email { get; }

        public string? ProfilePicture { get; }

        public int Age { get; set; }

        public string PhoneNumber { get; }

        public int Gender { get; }

        public int? TrainerId { get; }
    }
}
