namespace Body4U.Application.Features.Administration.Queries.Common
{
    public class RolesOutputModel
    {
        public RolesOutputModel(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public string Id { get; }

        public string Name { get; }
    }
}
