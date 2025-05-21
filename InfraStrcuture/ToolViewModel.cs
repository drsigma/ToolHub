namespace ToolHub.InfraStrcuture
{
    public class ToolViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Topics> Topics { get; set; } = new();
    }
}
