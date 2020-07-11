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
	public class HomeController : Controller
	{
		private readonly IPieRepository _pieRepository;

		public HomeController(IPieRepository pieRepository)
		{
			this._pieRepository = pieRepository;
		}

		// GET: /<controller>/
		public IActionResult Index()
		{
			var model = new HomeViewModel
			{
				PiesOfTheWeek = this._pieRepository.PiesOfTheWeek
			};

			return View(model);
		}
	}
}
