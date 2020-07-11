using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BethanysPieShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BethanysPieShop.Controllers
{
	[Authorize]
	public class OrderController : Controller
	{
		private readonly IOrderRepository _orderRepository;
		private readonly ShoppingCart _shoppingCart;

		public OrderController(IOrderRepository orderRepository, ShoppingCart shoppingCart)
		{
			this._orderRepository = orderRepository;
			this._shoppingCart = shoppingCart;
		}

		// GET: /<controller>/
		public IActionResult Checkout()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Checkout(Order order)
		{
			var items = this._shoppingCart.GetCartItems();
			this._shoppingCart.Items = items;

			if(this._shoppingCart.Items.Count == 0)
			{
				ModelState.AddModelError("", "Your cart is empty. add some pies first.");
			}

			if(ModelState.IsValid)
			{
				this._orderRepository.CreateOrder(order);
				this._shoppingCart.Clear();
				return RedirectToAction("CheckoutComplete");
			}

			return View(order);
		}

		public IActionResult CheckoutComplete()
		{
			ViewBag.Message = "Thanks for your order. You'll soon enjoy our delicious pies!";
			return View();
		}
	}
}
