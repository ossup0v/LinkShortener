using LinkShortener.DataTransferObjects;

namespace LinkShortener.Interfaces
{
	public interface ILinkShortenerService
	{
		UserUri CreateShortLink(string fullUri, string creator, string localPort);
		
		UserUri GetUriAndIncreaseCounter(string token);
		
		IList<UserUri> GetAllUserUries(string creator);

		IList<UserUri> GetAllUries();
		
		void DeleteAll();
	}
}
