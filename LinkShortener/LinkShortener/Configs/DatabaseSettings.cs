namespace LinkShortener.Configs
{
	public interface IDatabaseSettings
	{
		public string ConnectionString { get; set; }
		public string DatabaseName { get; set; }
		public string CollectionName { get; set; }
	}

	public sealed class DatabaseSettings : IDatabaseSettings
	{
		public string ConnectionString { get; set; }
		public string DatabaseName { get; set; }
		public string CollectionName { get; set; }
	}
}
