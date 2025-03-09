// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Net;
using System.Xml.Linq;
using Category.Application.Commands;
using Category.Application.Queries;
using Category.Application.Responses;
using Category.Core.Specs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Category.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(IMediator mediator, ILogger<CategoryController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpGet]
        [Route("GetAllCategories")]
        [ProducesResponseType(typeof(IList<CategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<CategoryResponse>>> GetAllCategories([FromQuery]CategorySpecParams categorySpecParams)
        {
            try
            {
                var query = new GetAllCategoriesQuery(categorySpecParams);
                var result = await _mediator.Send(query);
                return Ok(result);
            }catch(Exception ex)
            {
                _logger.LogError(ex, "An Exception has occured: {Exception}");
                throw;
            }
        }
        [HttpGet]
        [Route("[action]/{id}", Name = "GetCategoryByID")]
        [ProducesResponseType(typeof(IList<CategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<CategoryResponse>>> GetCategoryByID(string id)
        {
            try
            {
                var query = new GetCategoryByIdQuery(id);
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An Exception has occured: {Exception}");
                throw;
            }
        }
        [HttpGet]
        [Route("[action]/{name}", Name = "GetCategoryByName")]
        [ProducesResponseType(typeof(IList<CategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<CategoryResponse>>> GetCategoryByName(string name)
        {
            try
            {
                var query = new GetCategoryByNameQuery(name);
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An Exception has occured: {Exception}");
                throw;
            }
        }
        [HttpPost]
        [Route("CreateCategory")]
        [ProducesResponseType(typeof(CategoryResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CategoryResponse>> CreateCategory([FromBody] CreateCategoryCommand categoryCommand)
        {
            try
            {
                var result = await _mediator.Send(categoryCommand);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An Exception has occured: {Exception}");
                throw;
            }
        }
        [HttpPut]
        [Route("UpdateCategory")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CategoryResponse>> UpdateCategory([FromBody] UpdateCategoryCommand categoryCommand)
        {
            try
            {
                var result = await _mediator.Send(categoryCommand);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An Exception has occured: {Exception}");
                throw;
            }
        }
        [HttpDelete]
        [Route("{id}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CategoryResponse>> DeleteCategory(string id)
        {
            try
            {
                var query = new DeleteCategoryByIdQuery(id);
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An Exception has occured: {Exception}");
                throw;
            }
        }
    }
}
