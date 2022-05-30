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
using WebAPI.DTO;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductsController(IProductRepository repository)
        {
            _productRepository = repository;
        }

        // GET: Products
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public  ActionResult<IEnumerable<ProductDTO>> GetProducts()
        {
            var list = _productRepository.GetAll().Select(product => product.AsProductDTO() );
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
        public ActionResult<ProductDTO> GetProduct(int id)
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

            return Ok(product.AsProductDTO());
        }

        // PUT: api/Product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutProduct(int id, UpdateProductDTO productInput)
        {
            if (id != productInput.ProductId)
            {
                return BadRequest();
            }
            var product = new Product
            {
                CategoryId = productInput.CategoryId,
                ProductId = productInput.ProductId,
                ProductName = productInput.ProductName,
                UnitPrice = productInput.UnitPrice,
                UnitsInStock = productInput.UnitsInStock,
            };
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
        public ActionResult<ProductDTO> PostProduct(CreateProductDTO productInput)
        {
            // if (_context.Products == null)
            // {
            //     return Problem("Entity set 'FStoreDBContext.Products'  is null.");
            // }
            var product = new Product
            {
                CategoryId = productInput.CategoryId,
                ProductId = productInput.ProductId,
                ProductName = productInput.ProductName,
                UnitPrice = productInput.UnitPrice,
                UnitsInStock = productInput.UnitsInStock,
            };
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

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product.AsProductDTO());
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
