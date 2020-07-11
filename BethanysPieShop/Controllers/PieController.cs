using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BethanysPieShop.Controllers
{
	public class PieController : Controller
	{
		private readonly ICategoryRepository _categoryRepository;
		private readonly IPieRepository _pieRepository;

		public PieController(ICategoryRepository categoryRepository, IPieRepository pieRepository)
		{
			if (categoryRepository is null)
			{
				throw new ArgumentNullException(nameof(categoryRepository));
			}

			if (pieRepository is null)
			{
				throw new ArgumentNullException(nameof(pieRepository));
			}

			this._categoryRepository = categoryRepository;
			this._pieRepository = pieRepository;
		}

		// GET: /<controller>/
		public IActionResult Index(string category)
		{
			IEnumerable<Pie> pies;
			string currentCategory;

			if(string.IsNullOrWhiteSpace(category))
			{
				pies = this._pieRepository.Pies.OrderBy(p => p.Id);
				currentCategory = "All pies";
			}
			else
			{
				pies = this._pieRepository.Pies.Where(p => p.Category.Name == category).OrderBy(p => p.Id);
				currentCategory = this._categoryRepository.Categories.FirstOrDefault(c => c.Name == category).Name;
			}
			var model = new PiesIndexViewModel { Pies = pies, CurrentCategory = currentCategory };
			return View(model);
		}

		public IActionResult Details(int id)
		{
			var model = this._pieRepository.GetPieById(id);
			if(model == null)
			{
				return NotFound();
			}

			return View(model);
		}
	}
}
