namespace Body4U.Application.Features.Identity.Commands.CreateUser
{
    public class CreateUserOutputModel
    {
        public CreateUserOutputModel(string email, string userId, string token, string confirmationLink)
        {
            this.Email = email;
            this.UserId = userId;
            this.Token = token;
            this.ConfirmationLink = confirmationLink;
        }

        public string Email { get; }

        public string UserId { get; }

        public string Token { get; private set; }

        public string ConfirmationLink { get; private set; }

        public void UpdateConfirmationLink(string confirmationLink)
            => this.ConfirmationLink = confirmationLink;
    }
}
