// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Net;
using System.Xml.Linq;
using Category.Application.Commands;
using Category.Application.Handlers;
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
        [Route("GetCategories")]
        [ProducesResponseType(typeof(IList<CategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<CategoryResponse>>> GetCategories([FromQuery]CategorySpecParams categorySpecParams)
        {
            try
            {
                var query = new GetCategoriesQuery(categorySpecParams);
                var result = await _mediator.Send(query);
                return Ok(result);
            }catch(Exception ex)
            {
                _logger.LogError(ex, "An Exception has occured: {Exception}");
                throw;
            }
        }
        [HttpGet]
        [Route("GetAllCategories")]
        [ProducesResponseType(typeof(IList<CategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<CategoryResponse>>> GetAllCategories()
        {
            try
            {
                var query = new GetAllCategoriesQuery();
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
        [Route("GetCategoriesCount")]
        [ProducesResponseType(typeof(IList<CategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<CategoryResponse>>> GetCategoriesCount()
        {
            try
            {
                var query = new GetCategoriesCountQuery();
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
        [Route("GetAllActiveCategoriesCount")]
        [ProducesResponseType(typeof(IList<CategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<CategoryResponse>>> GetAllActiveCategoriesCount()
        {
            try
            {
                var query = new GetAllActiveCategoriesCountQuery();
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
        [Route("GetAllInactiveCategoriesCount")]
        [ProducesResponseType(typeof(IList<CategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<CategoryResponse>>> GetAllInactiveCategoriesCount()
        {
            try
            {
                var query = new GetAllInactiveCategoriesCountQuery();
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
        [Route("GetAllActiveCategories")]
        [ProducesResponseType(typeof(IList<CategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<CategoryResponse>>> GetAllActiveCategories()
        {
            try
            {
                var query = new GetAllActiveCategoriesQuery();
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
        [Route("GetAllInactiveCategories")]
        [ProducesResponseType(typeof(IList<CategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<CategoryResponse>>> GetAllInactiveCategories()
        {
            try
            {
                var query = new GetAllInactiveCategoriesQuery();
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An Exception has occured: {Exception}");
                throw;
            }
        }
        [HttpPut]
        [Route("RestoreCategory/{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<CategoryResponse>>> RestoreCategory(string id)
        {
            try
            {
                var query = new RestoreCategoryCommand(id);
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
        [HttpGet]
        [Route("[action]/{name}", Name = "GetCategoryByNameInactive")]
        [ProducesResponseType(typeof(CategoryResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CategoryResponse>> GetCategoryByNameInactive(string name)
        {
            try
            {
                var query = new GetCategoryByNameInactiveQuery(name);
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
        [Route("DeleteCategory/{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CategoryResponse>> DeleteCategory(string id)
        {
            try
            {
                var query = new DeleteCategoryByIdCommand(id);
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
