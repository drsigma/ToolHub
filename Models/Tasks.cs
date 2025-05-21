using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

public class Tasks
{
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime? DateStart { get; set; }
    public DateTime? DateFinish { get; set; }
}
