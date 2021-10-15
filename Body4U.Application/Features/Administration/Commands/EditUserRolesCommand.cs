namespace Body4U.Application.Features.Administration.Commands
{
    using Body4U.Application.Common;
    using Body4U.Application.Features.Identity;
    using Body4U.Application.Features.Trainers;
    using Body4U.Domain.Factories.Trainers;
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class EditUserRolesCommand : IRequest<Result>
    {
        public EditUserRolesCommand(string email, IEnumerable<string> rolesIds)
        {
            this.Email = email;
            this.RolesIds = rolesIds;
        }

        public string Email { get; }

        public IEnumerable<string> RolesIds { get; }

        public class EditUserRolesCommandHandler : IRequestHandler<EditUserRolesCommand, Result>
        {
            private readonly IIdentityService identityService;
            private readonly IIdentityRepository identityRepository;
            private readonly ITrainerFactory trainerFactory;
            private readonly ITrainerRepository trainerRepository;

            public EditUserRolesCommandHandler(
                IIdentityService identityService,
                IIdentityRepository identityRepository,
                ITrainerFactory trainerFactory,
                ITrainerRepository trainerRepository)
            {
                this.identityService = identityService;
                this.identityRepository = identityRepository;
                this.trainerFactory = trainerFactory;
                this.trainerRepository = trainerRepository;
            }

            public async Task<Result> Handle(EditUserRolesCommand request, CancellationToken cancellationToken)
            {
                var result = await this.identityService.EditUserRoles(request, cancellationToken);
                if (!result.Succeeded)
                {
                    return Result<EditUserRolesOutputModel>.Failure(result.Errors);
                }

                var errors = new List<string>();

                foreach (var userId in result.Data.UsersIdsForCreate)
                {
                    var trainer = this.trainerFactory.Build();
                    var user = await this.identityRepository.Find(userId, cancellationToken);
                    user.BecomeTrainer(trainer);
                    await this.trainerRepository.Save(trainer);
                }

                foreach (var userId in result.Data.UsersIdsForDelete)
                {
                    var user = await this.identityRepository.Find(userId, cancellationToken);
                    var deleteResult = await this.trainerRepository.Delete(user, cancellationToken);
                    if (!deleteResult.Succeeded)
                    {
                        errors.AddRange(deleteResult.Errors);
                    }
                }

                return Result.Success;
            }
        }
    }
}
