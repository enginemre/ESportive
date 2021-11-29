using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SportiveOrder.Interfaces;
using SportiveOrder.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportiveOrder.Models;

namespace SportiveOrder.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly  IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public ProductController(IProductRepository rep,ICategoryRepository categoryRepository)
        {
            _productRepository = rep;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            var productList = _productRepository.GetEntities();
            foreach (var cat in _categoryRepository.GetEntities())
            {
                foreach (var pro in productList)
                {
                    if(pro.CategoryId == cat.CategoryId)
                    {
                        pro.Category = new Category
                        {
                            CategoryId = pro.CategoryId,
                            CategoryName = cat.CategoryName,

                        };
                    }
                }
            }
          
            

            // Mevcut Ürünler listeleniyor...
            return View(productList);
        }
        public IActionResult Edit(int id)
        {
            var product = _productRepository.GetEntity(id);
            CRUDProduct updateProduct = new CRUDProduct
            {
                Id = product.ProductId,
                Name = product.ProductName,
                Barcode = product.Barcode,
                Size = product.Size,
                SalePrice = product.SalePrice,
                Stock = product.Stock,
                CategoryId = product.CategoryId,
                KDV = product.KDV,
                Color = product.Color,
                ProductGroup = product.ProductGroup,
                StockCode = product.StockCode,

            };
            // Kategori listesi hazılanıyor.
            List<SelectListItem> categoryList = new List<SelectListItem>();
            foreach (var item in _categoryRepository.GetEntities())
            {
                categoryList.Add(new SelectListItem { Value = item.CategoryId.ToString(), Text = item.CategoryName });
            }
            ViewBag.categoryList = categoryList;
            return View(updateProduct);
        }
        [HttpPost]
        public IActionResult Edit(CRUDProduct product)
        {
            // Ürün güncelleniyor.
            var orgProduct   = _productRepository.GetEntity(product.Id);
            if (ModelState.IsValid)
            {
                Product newProduct = new Product();
                orgProduct.Barcode = product.Barcode;
                orgProduct.ProductName = product.Name;
                orgProduct.Size = product.Size;
                orgProduct.Stock = product.Stock;
                orgProduct.StockCode = product.StockCode;
                orgProduct.Color = product.Color;
                orgProduct.KDV = product.KDV;
                orgProduct.SalePrice = product.SalePrice;
                orgProduct.PurchasePrice = product.PurchasePrice;
                orgProduct.CategoryId = product.CategoryId;
                orgProduct.ProductGroup = product.ProductGroup;
                orgProduct.ProductId = product.Id;
                // Ürün güncelleniyor.
                _productRepository.Update(orgProduct);
                return RedirectToAction("Index", "Product", new { area = "Admin" });
            }
            // Kategori listesi hazılanıyor.
            List<SelectListItem> categoryList = new List<SelectListItem>();
            foreach (var item in _categoryRepository.GetEntities())
            {
                categoryList.Add(new SelectListItem { Value = item.CategoryId.ToString(), Text = item.CategoryName });
            }
            ViewBag.categoryList = categoryList;

            return View(product);
        }
        public IActionResult Create()
        {
            // Kategori listesi hazılanıyor.
            List<SelectListItem> categoryList = new List<SelectListItem>();
            foreach (var item in _categoryRepository.GetEntities())
            {
                categoryList.Add(new SelectListItem { Value = item.CategoryId.ToString(), Text = item.CategoryName });
            }

            
            ViewBag.categoryList = categoryList;
            return View(new CRUDProduct());

        }
        
        [HttpPost]
        public IActionResult Create(CRUDProduct Cproduct)
        {
            // Yeni ürün oluştuluyor.
            CRUDProduct product =Cproduct;
            if (ModelState.IsValid)
            {
                Product newProduct = new Product();
                newProduct.Barcode = product.Barcode;
                newProduct.ProductName = product.Name;
                newProduct.Size = product.Size;
                newProduct.Stock = product.Stock;
                newProduct.StockCode = product.StockCode;
                newProduct.Color = product.Color;
                newProduct.KDV = product.KDV;
                newProduct.SalePrice = product.SalePrice;
                newProduct.PurchasePrice = product.PurchasePrice;
                newProduct.CategoryId = product.CategoryId;
                newProduct.ProductGroup = product.ProductGroup;

                _productRepository.Add(newProduct);
                return RedirectToAction("Index", "Product", new { area = "Admin" });
            }

            return View(Cproduct);
        }

        public IActionResult Delete(int id)
        {
            // Ürün siliniyor.
            _productRepository.Delete(new Product { ProductId = id });
            return RedirectToAction("Index", "Product", new { area = "Admin" });
        }

    }
}
