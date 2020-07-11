using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Models
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly AppDbContext _dbContext;

		public CategoryRepository(AppDbContext dbContext)
		{
			this._dbContext = dbContext;
		}
		public IEnumerable<Category> Categories
		{
			get
			{
				return this._dbContext.Categories;
			}
		}
	}
}
