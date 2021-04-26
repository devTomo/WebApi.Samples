using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Samples.Application.Interfaces;
using WebApi.Samples.Core.Entities;

namespace WebApi.Samples.Controllers
{
  [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public ProductController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _unitOfWork.Products.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _unitOfWork.Products.GetByIdAsync(id);
            if (data == null)
                return NotFound();
            return Ok(data);
        }

        /// <summary>
        /// Add produkt
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/Product/Add
        ///     {
        ///         "productID": 0,
        ///         "name": "test",
        ///         "productNumber": "TEST",
        ///         "makeFlag": true,
        ///         "finishedGoodsFlag": true,
        ///         "color": "Black",
        ///         "safetyStockLevel": 0,
        ///         "reorderPoint": 0,
        ///         "standardCost": 0,
        ///         "listPrice": 0,
        ///         "size": "test",
        ///         "sizeUnitMeasureCode": "test",
        ///         "weightUnitMeasureCode": "test",
        ///         "weight": 0,
        ///         "daysToManufacture": 0,
        ///         "productLine": "test",
        ///         "class": "test",
        ///         "style": "test",
        ///         "productSubcategoryID": 0,
        ///         "productModelID": 0,
        ///         "sellStartDate": "2021-04-13",
        ///         "sellEndDate": "2021-04-13",
        ///         "discontinuedDate": "2021-04-13",
        ///         "rowguid": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "modifiedDate": "2021-04-13",
        ///     }
        /// </remarks>
        /// <response code="201">Return added product</response>
        /// <response code="400">If send NULL</response>
        /// <response code="500">Error</response>
        // POST: api/Product/Add
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public async Task<IActionResult> Add(Product product)
        {
            var data = await _unitOfWork.Products.AddAsync(product);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _unitOfWork.Products.DeleteAsync(id);
            return Ok(data);
        }
        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            var data = await _unitOfWork.Products.UpdateAsync(product);
            return Ok(data);
        }
    }
}