namespace Body4U.Infrastructure.Identity
{
    public interface IJwtTokenGeneratorService
    {
        string GenerateToken(ApplicationUser user);
    }
}
