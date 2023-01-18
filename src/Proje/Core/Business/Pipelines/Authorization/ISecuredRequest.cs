namespace Core.Business.Pipelines.Authorization
{
    public interface ISecuredRequest
    {
        public string[] Roles { get; }
    }
}
