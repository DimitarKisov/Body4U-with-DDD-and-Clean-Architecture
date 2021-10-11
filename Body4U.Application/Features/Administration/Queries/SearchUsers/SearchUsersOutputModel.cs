namespace Body4U.Application.Features.Administration.Queries.SearchUsers
{
    using System.Collections.Generic;

    public class SearchUsersOutputModel
    {
        public SearchUsersOutputModel(
            IEnumerable<UserOutputModel> users,
            int totalRecords)
        {
            this.Users = users;
            this.TotalRecords = totalRecords;
        }

        public IEnumerable<UserOutputModel> Users { get; }

        public int TotalRecords { get; }
    }
}
