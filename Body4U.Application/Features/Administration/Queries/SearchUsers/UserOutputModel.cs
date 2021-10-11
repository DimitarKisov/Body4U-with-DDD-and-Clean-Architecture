namespace Body4U.Application.Features.Administration.Queries.SearchUsers
{
    public class UserOutputModel
    {
        public UserOutputModel(
            string id,
            string firstName,
            string lastName,
            string email, string
            phoneNumber)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
        }

        public string Id { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string Email { get; }

        public string PhoneNumber { get; }
    }
}
