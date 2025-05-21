using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

public class Tool
{
    [BsonId] // Indica que esta propriedade é o campo _id no MongoDB
    [BsonRepresentation(BsonType.String)] // Serializa o Guid como uma string no MongoDB
    public Guid Id { get; set; }

    public string Name { get; set; }

    public List<Topics> Topics { get; set; } = new(); // Inicializa para evitar nulls
}
