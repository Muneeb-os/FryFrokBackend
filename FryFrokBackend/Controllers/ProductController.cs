using FryFrokBackend.DataBase;
using FryFrokBackend.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FryFrokBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDBContext _dbContext;
        public ProductController(AppDBContext appDBContext)
        {
            _dbContext = appDBContext;
        }

        [HttpPost("AddProduct")]
        public async Task <IActionResult> AddProduct(ProductsDetail productsDetail)
        {
            if (productsDetail == null)
            {
                return BadRequest("Invalid Product Data.");
            }
            var Product = new ProductsDetail
            {
            Name=productsDetail.Name,
            Price=productsDetail.Price,
            status=productsDetail .status,
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
            };

            _dbContext.Add(Product);
            await _dbContext.SaveChangesAsync();
            return Ok("New Item Added Successfully");
        }

        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart([FromBody]AddCart addCart)
        {
            if (addCart == null)
            {
                return BadRequest("Product Not Found");
            }
            var Item = new AddCart
            {
             ProductName=addCart.ProductName,
             ProductPrice=addCart.ProductPrice,
             UserName=addCart.UserName,
             Phone=addCart.Phone,
             UserAddress=addCart.UserAddress,
             status=addCart.status,
             Bill=addCart.Bill,
             Created=DateTime.Now,
            };
            _dbContext.Add(Item);
            await _dbContext.SaveChangesAsync();
            return Ok("Item Added Successfully");
        }
        [HttpPut("UpdateStatus/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusDto statusDto)
        {
            var user = await _dbContext.Orders.FindAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            user.status = statusDto.Status; 
            _dbContext.Orders.Update(user);
            await _dbContext.SaveChangesAsync();

            return Ok("Status updated successfully.");
        }
        [HttpGet("Orders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var order = await _dbContext.Orders.ToListAsync();
            return Ok(order);
        }

    }
}
