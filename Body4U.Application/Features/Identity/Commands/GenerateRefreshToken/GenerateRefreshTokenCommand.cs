namespace Body4U.Application.Features.Identity.Commands.GenerateRefreshToken
{
    using Body4U.Application.Common;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class GenerateRefreshTokenCommand : IRequest<Result<GenerateRefreshTokenOutputModel>>
    {
        public class GenerateRefreshTokenCommandHandler : IRequestHandler<GenerateRefreshTokenCommand, Result<GenerateRefreshTokenOutputModel>>
        {
            private readonly IJwtTokenGeneratorService jwtTokenGeneratorService;

            public GenerateRefreshTokenCommandHandler(IJwtTokenGeneratorService jwtTokenGeneratorService)
                => this.jwtTokenGeneratorService = jwtTokenGeneratorService;

            public async Task<Result<GenerateRefreshTokenOutputModel>> Handle(GenerateRefreshTokenCommand request, CancellationToken cancellationToken)
                => await this.jwtTokenGeneratorService.GenerateRefreshToken();
        }
    }
}
