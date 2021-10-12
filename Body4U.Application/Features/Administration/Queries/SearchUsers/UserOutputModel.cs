namespace Body4U.Application.Features.Administration.Queries.SearchUsers
{
    using Body4U.Application.Features.Administration.Queries.Common;
    using System.Collections.Generic;

    public class UserOutputModel
    {
        public UserOutputModel(
            string id,
            string firstName,
            string lastName,
            string email, 
            string phoneNumber,
            IEnumerable<RolesOutputModel> roles)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.PhoneNumber = phoneNumber;

            this.Roles = roles;
        }

        public string Id { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string Email { get; }

        public string PhoneNumber { get; }

        public IEnumerable<RolesOutputModel> Roles { get; }
    }
}
