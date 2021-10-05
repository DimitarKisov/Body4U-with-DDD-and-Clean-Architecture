namespace Body4U.Application.Features.Identity.Commands.EditUser
{
    using Body4U.Application.Common;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using System.Threading;
    using System.Threading.Tasks;

    using static Body4U.Application.Common.GlobalConstants.Account;

    public class EditUserCommand : IRequest<Result>
    {
        public EditUserCommand(
            string id,
            string phoneNumber,
            string firstName,
            string lastName,
            int age,
            int gender,
            IFormFile profilePicture)
        {
            this.Id = id;
            this.PhoneNumber = phoneNumber;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Age = age;
            this.Gender = gender;
            this.ProfilePicture = profilePicture;
        }

        public string Id { get; }

        public string PhoneNumber { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public int Age { get; }

        public int Gender { get; }

        public IFormFile ProfilePicture { get; }

        public class EditUserCommandHandler : IRequestHandler<EditUserCommand, Result>
        {
            private readonly IIdentityRepository identityRepository;
            private readonly IIdentityService identityService;

            public EditUserCommandHandler(IIdentityRepository identityRepository, IIdentityService identityService)
            {
                this.identityRepository = identityRepository;
                this.identityService = identityService;
            }

            public async Task<Result> Handle(EditUserCommand request, CancellationToken cancellationToken)
            {
                var user = await this.identityRepository.Find(request.Id, cancellationToken);

                if (user == null)
                {
                    return Result.Failure(string.Format(WrongId, request.Id));
                }

                var result = await this.identityService.EditMyProfile(request, user, cancellationToken);

                if (result.Succeeded)
                {
                    await this.identityRepository.Save(result.Data, cancellationToken);
                    return Result.Success;
                }

                return Result.Failure(result.Errors);
            }
        }
    }
}
