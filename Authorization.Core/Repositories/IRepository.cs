using System.Collections.Generic;

namespace Authorization.Core.Repositories
{
	public interface IRepository<T>
	{
		void Save(T thing);

		IEnumerable<T> Get(string nameFilter);

		void Delete(T thing);
	}
}
