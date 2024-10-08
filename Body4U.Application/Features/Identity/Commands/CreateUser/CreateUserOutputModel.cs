﻿namespace Body4U.Application.Features.Identity.Commands.CreateUser
{
    public class CreateUserOutputModel
    {
        public CreateUserOutputModel(string email, string userId, string token)
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
