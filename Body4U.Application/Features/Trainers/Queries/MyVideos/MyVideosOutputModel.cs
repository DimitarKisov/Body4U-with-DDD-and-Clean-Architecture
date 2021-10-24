namespace Body4U.Application.Features.Trainers.Queries.MyVideos
{
    public class MyVideosOutputModel
    {
        public MyVideosOutputModel(
            int id,
            string videoUrl)
        {
            this.Id = id;
            this.VideoUrl = videoUrl;
        }

        public int Id { get; }

        public string VideoUrl { get; }
    }
}
