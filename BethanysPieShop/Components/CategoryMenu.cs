using BethanysPieShop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Components
{
	public class CategoryMenu : ViewComponent
	{
		private readonly ICategoryRepository _categoryRepository;

		public CategoryMenu(ICategoryRepository categoryRepository)
		{
			this._categoryRepository = categoryRepository;
		}

		public IViewComponentResult Invoke()
		{
			var categories = this._categoryRepository.Categories.OrderBy(c => c.Name);
			return View(categories);
		}
	}
}
