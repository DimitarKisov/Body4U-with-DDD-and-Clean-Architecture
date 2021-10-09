namespace Body4U.Application.Features.Identity.Commands.ForgotPassword
{
    public class ForgotPasswordOutputModel
    {
        public ForgotPasswordOutputModel(string email, string userId, string token)
        {
            this.Email = email;
            this.UserId = userId;
            this.Token = token;
        }

        public string Email { get; }

        public string UserId { get; }

        public string Token { get; }
    }
}
