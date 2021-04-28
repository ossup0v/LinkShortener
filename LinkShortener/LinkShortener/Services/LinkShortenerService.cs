using LinkShortener.DataTransferObjects;
using LinkShortener.Interfaces;
using System;
using System.Collections.Generic;

namespace LinkShortener.Services
{
	public sealed class LinkShortenerService : ILinkShortenerService
	{
		private readonly ILinkShortenerStorage _storage;

		public LinkShortenerService(ILinkShortenerStorage storage)
		{
			_storage = storage;
		}

		public UserUri CreateShortLink(string fullUri, string creator, string localPort)
		{
			var token = CreateToken();
			var result = new UserUri
			{
				Creator = creator, 
				FullUri = fullUri,
				ShortUri = $"{UserUri.ShortUriPrefix}{localPort}/q?token={token}",
				Token = token,
				ClickCounter = 0,
				CreatedAt = DateTime.UtcNow
			};

			_storage.Create(result);

			return result;
		}

		private string CreateToken()
		{
			var randomByteArray = Guid.NewGuid().ToByteArray();
			var token = Convert.ToBase64String(randomByteArray);
			return token;
		}

		public UserUri GetUriAndIncreaseCounter(string token)
		{
			lock (new object())
			{
				var uri = _storage.GetByToken(token);
				uri.ClickCounter++;
				_storage.Update(uri);
				return uri;
			}
		}

		public IList<UserUri> GetAllUserUries(string creator)
		{
			return _storage.GetByCreator(creator);
		}

		public IList<UserUri> GetAllUries()
		{
			return _storage.GetAll();
		}

		public void DeleteAll()
		{
			_storage.DeleteAll();
		}
	}
}
