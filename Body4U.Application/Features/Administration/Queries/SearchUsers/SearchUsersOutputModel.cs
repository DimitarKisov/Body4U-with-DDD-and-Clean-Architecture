namespace Body4U.Application.Features.Administration.Queries.SearchUsers
{
    using System.Collections.Generic;

    public class SearchUsersOutputModel
    {
        public SearchUsersOutputModel(
            IEnumerable<UserOutputModel> data,
            int totalRecords)
        {
            this.Data = data;
            this.TotalRecords = totalRecords;
        }

        public IEnumerable<UserOutputModel> Data { get; }

        public int TotalRecords { get; }
    }
}
