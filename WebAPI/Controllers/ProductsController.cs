using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Repository;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductsController(MyStorePRN231Context context,IProductRepository repository)
        {
            _productRepository = repository;
        }

        // GET: Products
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public  ActionResult<IEnumerable<Product>> GetProducts()
        {
            var list = _productRepository.GetAll();
            if (list == null)
            {
                return NotFound();
            }

            return Ok(list); 
        }

       
        

        // GET: api/Product/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Product> GetProduct(int id)
        {
            // if (_context.Products == null)
            // {
            //     return NotFound();
            // }
            var product = _productRepository.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            try
            {
                _productRepository.Update(product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_productRepository.GetProductById(id) == null)
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/Product
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Product> PostProduct(Product product)
        {
            // if (_context.Products == null)
            // {
            //     return Problem("Entity set 'FStoreDBContext.Products'  is null.");
            // }
            try
            {
                _productRepository.Add(product);
            }
            catch (DbUpdateException)
            {
                if (_productRepository.GetProductById(product.ProductId) != null)
                {
                    return Conflict();
                }

                throw;
            }

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            // if (_context.Products == null)
            // {
            //     return NotFound();
            // }
            try
            {
                var product = _productRepository.GetProductById(id);
                if (product == null)
                {
                    return NotFound();
                }

                _productRepository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }

        }

        // private bool ProductExists(int id)
        // {
        //     return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        // }
    }
}
