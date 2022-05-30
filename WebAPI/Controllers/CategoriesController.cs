using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Repository;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesController(MyStorePRN231Context context,ICategoryRepository repository)
        {
            _categoryRepository = repository;
        }

        // GET: Categories
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Category>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Category>> GetCategorys()
        {
            var list = _categoryRepository.GetAll();
            if (list == null)
            {
                return NotFound();
            }

            return Ok(list);
        }
     

        // GET: api/Category/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Category))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Category> GetCategory(int id)
        {
            // if (_context.Categorys == null)
            // {
            //     return NotFound();
            // }
            var Category = _categoryRepository.GetCategoryById(id);

            if (Category == null)
            {
                return NotFound();
            }

            return Ok(Category);
        }

        // PUT: api/Category/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutCategory(int id, Category Category)
        {
            if (id != Category.CategoryId)
            {
                return BadRequest();
            }

            try
            {
                _categoryRepository.Update(Category);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_categoryRepository.GetCategoryById(id) == null)
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/Category
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Category> PostCategory(Category Category)
        {
            // if (_context.Categorys == null)
            // {
            //     return Problem("Entity set 'FStoreDBContext.Categorys'  is null.");
            // }
            try
            {
                _categoryRepository.Add(Category);
            }
            catch (DbUpdateException)
            {
                if (_categoryRepository.GetCategoryById(Category.CategoryId) != null)
                {
                    return Conflict();
                }

                throw;
            }

            return CreatedAtAction("GetCategory", new { id = Category.CategoryId }, Category);
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            // if (_context.Categorys == null)
            // {
            //     return NotFound();
            // }
            try
            {
                var Category = _categoryRepository.GetCategoryById(id);
                if (Category == null)
                {
                    return NotFound();
                }

                _categoryRepository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }

        }

        // private bool CategoryExists(int id)
        // {
        //     return (_context.Categorys?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        // }
    }
}

