using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportiveOrder.Entity;
using SportiveOrder.Interfaces;
using SportiveOrder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportiveOrder.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {

        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository rep)
        {
            _categoryRepository = rep;
        }
        public IActionResult Index()
        {
            // Mevcut kategoriler listeleniyor.
            return View(_categoryRepository.GetEntities());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CRUDCategory category)
        {
            
            if (ModelState.IsValid)
            {
                // Yeni kategori ekleniyor.
                _categoryRepository.Add(new Entity.Category
                {
                    CategoryName = category.CategoryName
                });
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Edit(int id)
        {
            // Kategori güncelleniyor.
            var orgCategory = _categoryRepository.GetEntity(id);
            CRUDCategory updateCategory = new CRUDCategory
            {
                CategoryId = orgCategory.CategoryId,
                CategoryName = orgCategory.CategoryName,
            };
            return View(updateCategory);
        }
        [HttpPost]
        public IActionResult Edit(CRUDCategory updateCategory)
        {
            // Kategori güncelleniyor.
            var orgCategory = _categoryRepository.GetEntity(updateCategory.CategoryId);
            Category category = new Category
            {
                CategoryId = orgCategory.CategoryId,
                CategoryName = orgCategory.CategoryName
            };
            if (ModelState.IsValid)
            {
                _categoryRepository.Update(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }
            
        public IActionResult Delete(int id)
        {
            // Kategori siliniyor.
            _categoryRepository.Delete(new Category
            {
                CategoryId = id
            });
            return RedirectToAction("Index");
        }

    }
}
