namespace Body4U.Application.Features.Administration.Queries.SearchUsers
{
    public class UserOutputModel
    {
        public UserOutputModel()
        {

        }
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

        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
