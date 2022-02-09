using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Dttos;
using Shop.Models;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _ctx;
        private readonly IMapper _mapper;

        public ProductsController(DataContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           var product =  await _ctx.Products.ToListAsync();
           var productGet = _mapper.Map<List<ProductGetDto>>(product);
            return Ok(productGet);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto product)
        {
            var domainProduct = _mapper.Map<Product>(product);
            _ctx.Products.Update(domainProduct);
            await _ctx.SaveChangesAsync();
            return NoContent();

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _ctx.Products.FirstOrDefaultAsync(h => h.ProductId == id);

            var productGet = _mapper.Map<Product>(product);

            return Ok(productGet);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] ProductCreateDto update, int id)
        {
            var toUpdate = _mapper.Map<Product>(update);
            toUpdate.ProductId = id;
            _ctx.Products.Update(toUpdate);
            
            await _ctx.SaveChangesAsync();
  
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _ctx.Products.FirstOrDefaultAsync(h => h.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }
            _ctx.Products.Remove(product);
            await _ctx.SaveChangesAsync();
            return NoContent();
            
        }
        
    }
}
