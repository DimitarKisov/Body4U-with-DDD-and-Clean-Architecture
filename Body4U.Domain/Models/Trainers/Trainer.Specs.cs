namespace Body4U.Domain.Models.Trainers
{
    using Body4U.Domain.Exceptions;
    using FluentAssertions;
    using System;
    using Xunit;

    using static Body4U.Domain.Models.ModelDatas.Trainer;

    public class TrainerSpecs
    {
        [Fact]
        public void ValidTrainerShouldNotThrowException()
        {
            Action act = () => TrainerCreator();

            act.Should().NotThrow<InvalidTrainerException>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(BioLessThanMinRequirement)]
        [InlineData(BioMoreThanMaxRequirement)]
        public void InvalidTrainerBioShouldThrowException(string bio)
        {
            Action act = () => new Trainer(bio, ValidShortBio, ValidFacebookUrl, ValidInstragramUrl, ValidYoutubeChannelUrl);

            act.Should().Throw<InvalidTrainerException>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(ShortBioLessThanMinRequirement)]
        [InlineData(ShortBioMoreThanMaxRequirement)]
        public void InvalidTrainerShortBioShouldThrowException(string shortBio)
        {
            Action act = () => new Trainer(ValidBio, shortBio, ValidFacebookUrl, ValidInstragramUrl, ValidYoutubeChannelUrl);

            act.Should().Throw<InvalidTrainerException>();
        }

        [Fact]
        public void InvalidFacebookUrlShouldThrowException()
        {
            Action act = () => new Trainer(ValidBio, ValidShortBio, InvalidSocialAccountUrl, ValidInstragramUrl, ValidYoutubeChannelUrl);

            act.Should().Throw<InvalidTrainerException>();
        }

        [Fact]
        public void InvalidInstagramUrlShouldThroxException()
        {
            Action act = () => new Trainer(ValidBio, ValidShortBio, ValidFacebookUrl, InvalidSocialAccountUrl, ValidYoutubeChannelUrl);

            act.Should().Throw<InvalidTrainerException>();
        }

        [Fact]
        public void InvalidYoutubeChannelUrlShouldThrowException()
        {
            Action act = () => new Trainer(ValidBio, ValidShortBio, ValidFacebookUrl, ValidInstragramUrl, InvalidSocialAccountUrl);

            act.Should().Throw<InvalidTrainerException>();
        }

        [Fact]
        public void UpdateBioShouldWorkCorrectly()
        {
            var trainer = TrainerCreator();

            var currentBio = trainer.Bio;

            trainer.UpdateBio(ValidBio + "some new bio", "someGuid");

            var result = currentBio == trainer.Bio;

            result.Should().BeFalse();
        }

        [Fact]
        public void UpdateShortBioShouldWorkCorrectly()
        {
            var trainer = TrainerCreator();

            var currentShortBio = trainer.ShortBio;

            trainer.UpdateShortBio(ValidShortBio + "new", "someGuid");

            var result = currentShortBio == trainer.ShortBio;

            result.Should().BeFalse();
        }

        [Fact]
        public void UpdateFacebookUrlShouldWorkCorrectly()
        {
            var trainer = TrainerCreator();

            var currentFacebookUrl = trainer.FacebookUrl;

            trainer.UpdateFacebookUrl(ValidFacebookUrl + "new", "someGuid");

            var result = currentFacebookUrl == trainer.FacebookUrl;

            result.Should().BeFalse();
        }

        [Fact]
        public void UpdateInstagramUrlShouldWorkCorrectly()
        {
            var trainer = TrainerCreator();

            var currentInstagramUrl = trainer.InstagramUrl;

            trainer.UpdateInstagramUrl(ValidInstragramUrl + "new", "someGuid");

            var result = currentInstagramUrl == trainer.InstagramUrl;

            result.Should().BeFalse();
        }

        [Fact]
        public void UpdateYoutubeChannelUrlShouldWorkCorrectly()
        {
            var trainer = TrainerCreator();

            var currentYoutubeChannelUrl = trainer.YoutubeChannelUrl;

            trainer.UpdateYoutubeChannelUrl(ValidYoutubeChannelUrl + "new", "someGuid");

            var result = currentYoutubeChannelUrl == trainer.YoutubeChannelUrl;

            result.Should().BeFalse();
        }

        [Fact]
        public void ChangeVisibilityShouldWorkCorrectly()
        {
            var trainer = TrainerCreator();

            trainer.ChangeVisibility();

            trainer.IsReadyToVisualize.Should().BeTrue();
        }

        [Fact]
        public void ChangeOpportunityToWriteShouldWorkCorrectly()
        {
            var trainer = TrainerCreator();

            trainer.ChangeOpportunityToWrite();

            trainer.IsReadyToWrite.Should().BeTrue();
        }

        public Trainer TrainerCreator() => new Trainer(ValidBio, ValidShortBio, ValidFacebookUrl, ValidInstragramUrl, ValidYoutubeChannelUrl);
    }
}
