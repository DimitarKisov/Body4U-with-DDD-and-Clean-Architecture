namespace Body4U.Domain.Models.Trainers
{
    using Body4U.Domain.Common;
    using Body4U.Domain.Exceptions;

    public class TrainerImage : Entity<int>
    {
        internal TrainerImage(byte[] image)
        {
            this.Validate(image);

            this.Image = image;
        }

        public byte[] Image { get; set; }

        private void Validate(byte[] image)
        {
            //TODO: Тъй като преди това е IFormFile и там си има проверки, не съм сигурен на дали ще ни е нужна тази проверка
            Guard.AgaintsEmptyFile<InvalidTrainerImageException>(image, nameof(this.Image));
        }
    }
}
