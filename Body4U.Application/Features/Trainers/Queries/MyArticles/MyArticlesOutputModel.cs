namespace Body4U.Application.Features.Trainers.Queries.MyArticles
{
    using System.Collections.Generic;

    public class MyArticlesOutputModel
    {
        public MyArticlesOutputModel(
            IEnumerable<MyArticleOutputModel> data,
            int totalRecords)
        {
            this.Data = data;
            this.TotalRecords = totalRecords;
        }

        public IEnumerable<MyArticleOutputModel> Data { get; }

        public int TotalRecords { get; }
    }
}
