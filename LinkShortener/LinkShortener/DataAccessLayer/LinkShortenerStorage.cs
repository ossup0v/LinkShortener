using LinkShortener.Configs;
using LinkShortener.DataTransferObjects;
using LinkShortener.Interfaces;

namespace LinkShortener.DataAccessLayer
{
	public sealed class LinkShortenerStorage : ILinkShortenerStorage
	{
		private readonly IMongoCollection<EntryUri> _uries;

		public LinkShortenerStorage(IDatabaseSettings settings)
		{
			var client = new MongoClient(settings.ConnectionString);
			var database = client.GetDatabase(settings.DatabaseName);
			_uries = database.GetCollection<EntryUri>(settings.CollectionName);
		}

		public UserUri Create(UserUri uri)
		{
			var entry = uri.ToEntryUri();
			_uries.InsertOne(entry);
			return entry.ToUserUri();
		}

		public IList<UserUri> GetAll() => _uries.Find(o => true)
																			 .ToList()
																			 .Select(o=>o.ToUserUri())
																			 .ToList();

		public IList<UserUri> GetByCreator(string creator)
					=> _uries.Find(o => o.Creator == creator)
							.ToList()
							.Select(o => o.ToUserUri())
							.ToList();

		public UserUri GetByToken(string token)
					=> _uries.Find(o => o.Token == token)
							.SingleOrDefault()
							.ToUserUri();

		public UserUri GetById(string id)
					=> _uries.Find(o => o.Id == id)
							.SingleOrDefault()
							.ToUserUri();

		public void Update(UserUri uri)
			=> _uries.ReplaceOne(o => o.Id == uri.Id, uri.ToEntryUri());

		public void Delete(string id)
			=> _uries.DeleteOne(o => o.Id == id);
	
		public void DeleteAll()
			=> _uries.DeleteMany(o => true);
	}
}
