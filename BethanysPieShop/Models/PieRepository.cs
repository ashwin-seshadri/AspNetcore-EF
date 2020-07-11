using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Models
{
	public class PieRepository : IPieRepository
	{
		private readonly AppDbContext _dbContext;

		public PieRepository(AppDbContext dbContext)
		{
			this._dbContext = dbContext;
		}
		public IEnumerable<Pie> Pies
		{
			get
			{
				return this._dbContext.Pies.Include(p => p.Category);
			}
		}

		public IEnumerable<Pie> PiesOfTheWeek
		{
			get
			{
				return this._dbContext.Pies.Include(p => p.Category).Where(p => p.IsPieOfTheWeek);
			}
		}

		public Pie GetPieById(int pieId)
		{
			return this._dbContext.Pies.Include(p => p.Category).FirstOrDefault(p => p.Id == pieId);
		}
	}
}
