using System.Collections.Generic;

namespace BethanysPieShop.Models
{
	public interface ICategoryRepository
	{
		IEnumerable<Category> Categories { get; }
	}
}