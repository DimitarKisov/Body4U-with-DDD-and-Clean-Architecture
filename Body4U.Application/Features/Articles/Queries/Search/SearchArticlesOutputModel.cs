namespace Body4U.Application.Features.Articles.Queries.Search
{
    using System.Collections.Generic;

    public class SearchArticlesOutputModel
    {
        public SearchArticlesOutputModel(
            IEnumerable<ArticleOutputModel> data,
            int totalRecords)
        {
            this.Data = data;
            this.TotalRecords = totalRecords;
        }

        public IEnumerable<ArticleOutputModel> Data { get; }

        public int TotalRecords { get; }
    }
}
