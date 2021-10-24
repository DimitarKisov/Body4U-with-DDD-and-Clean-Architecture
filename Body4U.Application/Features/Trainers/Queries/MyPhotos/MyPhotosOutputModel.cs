namespace Body4U.Application.Features.Trainers.Queries.MyPhotos
{
    public class MyPhotosOutputModel
    {
        public MyPhotosOutputModel(
            int id,
            string photo)
        {
            this.Id = id;
            this.Photo = photo;
        }

        public int Id { get; }

        public string Photo { get; }
    }
}
