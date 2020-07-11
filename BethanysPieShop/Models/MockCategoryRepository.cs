using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Models
{
	public class MockCategoryRepository : ICategoryRepository
	{
		public IEnumerable<Category> Categories =>
			new List<Category>
			{
				new Category { Id = 1, Name = "Fruit Pies", Description = "All-fruity pies" },
				new Category { Id = 2, Name = "Cheese Cakes", Description = "Oh-so cheesy" },
				new Category { Id = 3, Name = "Seasonal Pies", Description = "It's that time of the year" }
			};
	}
}
