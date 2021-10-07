namespace Body4U.Infrastructure.Identity.Models
{
    using Body4U.Application.Features.Identity;
    using Body4U.Domain.Exceptions;
    using Body4U.Domain.Models.Trainers;
    using Body4U.Infrastructure.Identity.Common;
    using Body4U.Infrastructure.Identity.Exceptions;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;

    using static Body4U.Domain.Models.ModelContants.User;

    public class ApplicationUser : IdentityUser, IUser
    {
        internal ApplicationUser(
            string email,
            string phoneNumber,
            string firstName,
            string lastName,
            int age,
            byte[]? profilePicture,
            Gender gender)
            : base(email)
        {
            this.Validate(email, phoneNumber, firstName, lastName, age, gender);

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
            byte[]? profilePicture
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

        #region State mutation methods
        public void BecomeTrainer(Trainer trainer)
        {
            if (this.Trainer != null)
            {
                throw new InvalidTrainerException($"User '{this.UserName}' is already a trainer.");
            }

            this.Trainer = trainer;
        }

        public ApplicationUser UpdatePhoneNumber(string phoneNumber)
        {
            this.ValidatePhoneNumber(phoneNumber);

            this.PhoneNumber = phoneNumber;
            return this;
        }

        public ApplicationUser UpdateFirstName(string firstName)
        {
            this.ValidateFirstName(firstName);

            this.FirstName = firstName;
            return this;
        }

        public ApplicationUser UpdateLastName(string lastName)
        {
            this.ValidateLastName(lastName);

            this.LastName = lastName;
            return this;
        }

        public ApplicationUser UpdateAge(int age)
        {
            this.ValidateAge(age);

            this.Age = age;
            return this;
        }

        public ApplicationUser UpdateGender(Gender gender)
        {
            this.ValidateGender(gender);

            this.Gender = gender;
            return this;
        }

        public ApplicationUser UpdateProfilePicture(byte[] profilePicture)
        {
            this.ValidateProfilePicture(profilePicture);

            this.ProfilePicture = profilePicture;
            return this;
        }
        #endregion

        #region Validations
        private void Validate(string email, string phoneNumber, string firstName, string lastName, int age, Gender gender)
        {
            this.ValidateEmail(email);
            this.ValidatePhoneNumber(phoneNumber);
            this.ValidateFirstName(firstName);
            this.ValidateLastName(lastName);
            this.ValidateAge(age);
            this.ValidateGender(gender);
        }

        private void ValidateFirstName(string firstName)
        {
            Guard.AgainstEmptyString<InvalidApplicationUserException>(firstName, nameof(this.FirstName));

            Guard.ForStringLength<InvalidApplicationUserException>(firstName, MinFirstNameLength, MaxFirstNameLength, nameof(this.FirstName));
        }

        private void ValidateLastName(string lastName)
        {
            Guard.AgainstEmptyString<InvalidApplicationUserException>(lastName, nameof(this.FirstName));

            Guard.ForStringLength<InvalidApplicationUserException>(lastName, MinLastNameLength, MaxLastNameLength, nameof(this.LastName));
        }

        private void ValidateEmail(string email)
            => Guard.ForRegexExpression<InvalidApplicationUserException>(email, EmailRegex, nameof(this.Email));

        private void ValidatePhoneNumber(string phoneNumber)
           => Guard.ForRegexExpression<InvalidApplicationUserException>(phoneNumber, PhoneNumberRegex, nameof(this.PhoneNumber));

        private void ValidateAge(int age)
            => Guard.AgainstOutOfRange<InvalidApplicationUserException>(age, MinAge, MaxAge, nameof(this.Age));

        private void ValidateGender(Gender gender)
           => Guard.ForValidGender<InvalidApplicationUserException>(gender, nameof(this.Gender));

        private void ValidateProfilePicture(byte[] profilePicture)
            => Guard.AgaintsEmptyFile<InvalidApplicationUserException>(profilePicture, nameof(this.ProfilePicture));
        #endregion
    }
}
