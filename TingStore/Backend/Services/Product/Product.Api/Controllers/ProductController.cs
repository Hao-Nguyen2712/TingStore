// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Net;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Product.Application.Commands;
using Product.Application.Queries;
using Product.Application.Responses;
using Product.Application.Services.ImageCloud;
using Product.Core.Models;
using Product.Core.Specs;
using Product.Infrastructure.DbContext;

namespace Product.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductController> _logger;
        private readonly ImageManagementProductServices _imageManagementProductServices;

        public ProductController(IMediator mediator, ILogger<ProductController> logger , ImageManagementProductServices imageManagementProductServices)
        {
            _mediator = mediator;
            _logger = logger;
            _imageManagementProductServices = imageManagementProductServices;
        }
        [HttpGet]
        [Route("GetAllProducts")]
        [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductSpecParams productSpecParams)
        {
            try
            {
                var query = new GetAllProductQuery(productSpecParams);
                var result = await this._mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An Exception has occured: {Exception}");
                throw;
            }
        }

        [HttpGet]
        [Route("[action]/{brand}", Name = "GetProductByBrand")]
        [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProductByBrand(string brand)
        {
            var query = new GetProductByBrandQuery(brand);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("[action]/{name}", Name = "GetProductByName")]
        [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProductByName(string name)
        {
            var query = new GetProductByNameQuery(name);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("[action]/{id}", Name = "GetProductById")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProductById(string id)
        {
            var query = new GetProductByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [Route("CreateProduct")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductCommand createProductCommand)
        {
            var result = await _mediator.Send(createProductCommand);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateProduct")]
        [ProducesResponseType(typeof(UpdateProductCommand), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand updateProductCommand)
        {
            var result = await _mediator.Send(updateProductCommand);
            return Ok(result);
        }

        [HttpDelete]
        [Route("[action]/{id}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var query = new DeleteProductCommand(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [Route("AddImageProduct")]
        public async Task<IActionResult> AddImageProduct(string id, bool isPrimary, List<IFormFile> files)
        {
            var fileIds = new List<bool>();
            foreach (var file in files)
            {
                if (file.Length == 0)
                {
                    return BadRequest("File is empty");
                }
                using (var stream = file.OpenReadStream())
                {
                    AddImageProductCommand addImageProductCommand = new AddImageProductCommand{
                        id = id,
                        imageFile = file,
                        isPrimary = isPrimary,
                    };
                    var result = await _mediator.Send(addImageProductCommand);
                    fileIds.Add(result);
                }
            }
            return Ok(fileIds);
        }
    }
}
