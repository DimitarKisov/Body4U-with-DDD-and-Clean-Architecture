namespace Body4U.Application.Features.Administration.Commands
{
    using System.Collections.Generic;

    public class EditUserRolesOutputModel
    {
        public EditUserRolesOutputModel(
            IEnumerable<string> usersIdsForCreate,
            IEnumerable<string> usersIdsForDelete)
        {
            this.UsersIdsForCreate = usersIdsForCreate;
            this.UsersIdsForDelete = usersIdsForDelete;
        }

        public IEnumerable<string> UsersIdsForCreate { get; }

        public IEnumerable<string> UsersIdsForDelete { get; }
    }
}
