using Microsoft.AspNetCore.Mvc;
using SportiveOrder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportiveOrder.ViewComponents
{
    public class Category : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;
        public Category(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public IViewComponentResult Invoke()
        {
            return View(_categoryRepository.GetEntities());
        }

    }
}
