namespace Body4U.Infrastructure.Identity
{
    using Microsoft.AspNetCore.Identity;
    using System;

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
            : this(null!)
        {
        }

        public ApplicationRole(string name)
            : base(name)
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
