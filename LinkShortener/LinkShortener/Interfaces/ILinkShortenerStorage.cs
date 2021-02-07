using LinkShortener.DataTransferObjects;

namespace LinkShortener.Interfaces
{
	public interface ILinkShortenerStorage
	{
		UserUri Create(UserUri uri);

		IList<UserUri> GetAll();

		IList<UserUri> GetByCreator(string creator);

		UserUri GetByToken(string token);

		UserUri GetById(string id);

		void Update(UserUri uri);

		void Delete(string id);

		void DeleteAll();
	}
}
