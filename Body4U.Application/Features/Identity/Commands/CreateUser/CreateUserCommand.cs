namespace Body4U.Application.Features.Identity.Commands.CreateUser
{
    using Body4U.Application.Common;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public class CreateUserCommand : IRequest<Result<IUser>>
    {
        public CreateUserCommand(
            string email,
            string phoneNumber,
            string firstName,
            string lastName,
            int age,
            int gender,
            IFormFile profilePicture,
            string password)
        {
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Age = age;
            this.Gender = gender;
            this.ProfilePicture = profilePicture;
            this.Password = password;
        }

        public string Email { get; }

        public string PhoneNumber { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public int Age { get; }

        public int Gender { get; }

        //TODO: Не съм сигурен на 100% дали е ок, да е от тип IFormFile
        public IFormFile ProfilePicture { get; }

        public string Password { get; }

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<IUser>>
        {
            private readonly IIdentityService identityService;

            public CreateUserCommandHandler(IIdentityService identityService)
                => this.identityService = identityService;

            public async Task<Result<IUser>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var result = await this.identityService.Register(request);

                if (!result.Succeeded)
                {
                    return result.Errors;
                }

                return result;
            }
        }
    }
}
