using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace LinkShortener.DataTransferObjects
{
  public sealed class EntryUri
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string FullUri { get; set; }
    public string ShortUri { get; set; }
    public string Token { get; set; }
    public int ClickCounter { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Creator { get; set; }
  }
}
