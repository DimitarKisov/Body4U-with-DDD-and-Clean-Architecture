namespace Body4U.Infrastructure.Identity
{
    using Body4U.Application.Features.Identity;
    using Body4U.Domain.Common;
    using Body4U.Domain.Exceptions;
    using Body4U.Domain.Models.Trainers;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;

    public class ApplicationUser : IdentityUser, IUser
    {
        internal ApplicationUser(
            string email,
            string phoneNumber,
            string firstName,
            string lastName,
            int age,
            byte[] profilePicture,
            Gender gender)
            : base(email)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Age = age;
            this.ProfilePicture = profilePicture;
            this.Gender = gender;
            this.IsDisabled = false;
            this.CreatedOn = DateTime.Now;
            this.ModifiedOn = null;
            this.ModifiedBy = null;

            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
        }

        private ApplicationUser(
            string email,
            string phoneNumber,
            string firstName,
            string lastName,
            int age,
            byte[] profilePicture
            )
            : base(email)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Age = age;
            this.ProfilePicture = profilePicture;
            this.IsDisabled = false;
            this.CreatedOn = DateTime.Now;
            this.ModifiedOn = null;
            this.ModifiedBy = null;

            this.Gender = default!;

            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
        }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public int Age { get; private set; }

        public byte[]? ProfilePicture { get; private set; }
        
        public Gender Gender { get; private set; }

        public bool IsDisabled { get; private set; }

        public DateTime CreatedOn { get; private set; }

        public DateTime? ModifiedOn { get; private set; }

        public string? ModifiedBy { get; private set; }

        public Trainer? Trainer { get; private set; }

        public ICollection<IdentityUserRole<string>> Roles { get; set; }

        public ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public void BecomeTrainer(Trainer trainer)
        {
            if (this.Trainer != null)
            {
                throw new InvalidTrainerException($"User '{this.UserName}' is already a trainer.");
            }

            this.Trainer = trainer;
        }
    }
}
