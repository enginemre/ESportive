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
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryRepository  _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public List<Category> Get()
        {
            return _categoryRepository.GetEntities();
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public Category Get(int id)
        {
            return _categoryRepository.GetEntity(id);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public Response Post([FromBody] Category category)
        {
            try
            {
                _categoryRepository.Add(category);
                return new Response { Type = "S", Message = "İşlem Başarılı" };

            }
            catch(Exception e)
            {
                return new Response { Type = "E", Message = e.Message };
            }
            
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public Response Put(int id, [FromBody] Category category)
        {
            var orgCategory = _categoryRepository.GetEntity(id);
            if (orgCategory != null)
            {
                _categoryRepository.Update(category);
                return new Response { Message = "İşlem Başarılı", Type = "S" };
            }
            else
            {
                return new Response { Message = "Kullanıcı Bulunamadı", Type = "E" };
            }
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public Response Delete(int id)
        {
            var category = _categoryRepository.GetEntity(id);
            if (category != null)
            {
                _categoryRepository.Delete(category);
                return new Response { Message = "Başarıyla Silindi", Type = "S" };
            }
            else
            {
                return new Response { Message = "Kullanıcı silme işlemi başarısız", Type = "E" };
            }
        }
    }
}
