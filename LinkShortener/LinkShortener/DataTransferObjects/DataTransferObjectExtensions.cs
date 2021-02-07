namespace LinkShortener.DataTransferObjects
{
	internal static class DataTransferObjectExtensions
	{
		public static EntryUri ToEntryUri(this UserUri uri)
		{
			return new EntryUri
			{
				Id = uri.Id,
				Creator = uri.Creator,
				FullUri = uri.FullUri,
				ShortUri = uri.ShortUri,
				CreatedAt = uri.CreatedAt,
				ClickCounter = uri.ClickCounter,
				Token = uri.Token
			};
		}

		public static UserUri ToUserUri(this EntryUri uri)
		{
			return new UserUri
			{
				Id = uri.Id,
				Creator = uri.Creator,
				FullUri = uri.FullUri,
				ShortUri = uri.ShortUri,
				CreatedAt = uri.CreatedAt,
				ClickCounter = uri.ClickCounter,
				Token = uri.Token
			};
		}
	}
}
