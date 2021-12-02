using Microsoft.AspNetCore.Mvc;
using SportiveOrder.Entity;
using SportiveOrder.Interfaces;
using SportiveOrder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SportiveOrder.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public List<Product> Get()
        {
            var products = _productRepository.GetEntities();
            return products;
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            var product = _productRepository.GetEntity(id);
            return product;
        }

        // POST api/<ProductController>
        [HttpPost]
        public Response Post([FromBody] Product value)
        {
            var newProduct = value;
            _productRepository.Add(value);
            return new Response { Message ="İşlem Başarılı", Type = "S"};
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public Response Put(int id, [FromBody] Product value)
        {
            var orgProduct = _productRepository.GetEntity(id);
            if(orgProduct != null)
            {
                _productRepository.Update(value);
                return new Response { Message = "İşlem Başarılı", Type = "S" };
            }
            else
            {
                return new Response { Message = "Kullanıcı Bulunamadı", Type = "E"};
            }
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public Response Delete(int id)
        {
            var product = _productRepository.GetEntity(id);
            if(product != null)
            {
                _productRepository.Delete(product);
                return new Response { Message = "Başarıyla Silindi", Type = "S" };
            }
            else
            {
                return new Response { Message = "Kullanıcı silme işlemi başarısız", Type = "E" };
            }
            
        }
    }
}
