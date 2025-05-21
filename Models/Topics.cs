using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

public class Topics
{
    // Se Topics for um subdocumento embutido em Tool, você pode não precisar de BsonId aqui,
    // mas BsonRepresentation(BsonType.String) ainda é uma boa prática para o Guid.
    [BsonId] // Indica que esta propriedade é o campo _id no MongoDB (se Topics for um documento raiz)
    [BsonRepresentation(BsonType.String)] // Serializa o Guid como uma string no MongoDB
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Text { get; set; }
}