﻿using Cafe.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.ViewComponents
{
	public class Yorumlar :ViewComponent
	{
		private readonly ApplicationDbContext _db;
		public Yorumlar(ApplicationDbContext db)
		{
			_db= db;
		}
		public IViewComponentResult Invoke()
		{
			var yorum=_db.Blogs.Where(i=>i.Onay).ToList();
			return View(yorum);
		}
	}
}
