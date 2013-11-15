﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;

namespace SportsStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IProductRepository  _repository;

        public NavController(IProductRepository repo)
        {
            _repository = repo;
        }

        public PartialViewResult Menu(String category = null)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<String> categories = _repository.Products
                                                .Select(x => x.Category)
                                                .Distinct()
                                                .OrderBy(x => x);
            return PartialView(categories);
        }
    }
}
