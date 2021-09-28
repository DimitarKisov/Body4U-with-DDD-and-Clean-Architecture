namespace Body4U.Domain.Common
{
    using Body4U.Domain.Models.Trainers;
    using FluentAssertions;
    using Xunit;

    public class EntitySpecs
    {
        [Fact]
        public void EntitiesWithEqualIdsShouldBeEqual()
        {
            var first = new TrainerVideo("https://www.youtube.com/watch?v=wpU9fPso8h0").SetId(1);
            var second = new TrainerVideo("https://www.youtube.com/watch?v=wpU9fPso8h0").SetId(1);

            var result = first == second;

            result.Should().BeTrue();
        }
    }

    internal static class EntityExtensions
    {
        public static Entity<T> SetId<T>(this Entity<T> entity, int id)
            where T : struct
        {
            entity
                .GetType()
                .BaseType!
                .GetProperty(nameof(Entity<T>.Id))!
                .GetSetMethod(true)!
                .Invoke(entity, new object[] { id });

            return entity;
        }
    }
}
