using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayerProject.API.DTOs;
using NLayerProject.API.Filters;
using NLayerProject.Core.Models;
using NLayerProject.Core.Services;

namespace NLayerProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var products = await _productService.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetEntityAsync(id);

            return Ok(_mapper.Map<ProductDto>(product));
        }

        [HttpPost]
       // [ValidationFilter]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var product = await _productService.AddAsync(_mapper.Map<Product>(productDto));

            return Created(string.Empty,_mapper.Map<ProductDto>(product));
        }
        [HttpPut]
        public IActionResult Update(ProductDto productDto)
        {

          /*  if (string.IsNullOrEmpty(productDto.Id.ToString())|| productDto.Id<=0)
            {
                throw new Exception("Id gereklidir");
            } Tavsiye edilmez.
          */
            _productService.Update(_mapper.Map<Product>(productDto));

            return NoContent();

        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(NotFoundFilter))]
        public IActionResult Remove(int id)
        {
            var product = _productService.GetEntityAsync(id).Result;

            _productService.Remove(product);

            return NoContent();
        }

        [HttpGet("{id}/category")]
        [ServiceFilter(typeof(NotFoundFilter))]
        public async Task<IActionResult> GetWithCategoryById(int id )
        {
            var productWithCategory =await  _productService.GetWithCategoryByIdAsync(id);

            return Ok(_mapper.Map<ProductWithCategoryDto>(productWithCategory));
        }


    }
}
