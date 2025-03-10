// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Category.Application.Queries;
using Category.Application.Responses;
using Category.Core.Specs;
using System.Net;
using Category.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Category.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ICategoryContext _categoryContext;

        public ValuesController(ICategoryContext categoryContext)
        {
            _categoryContext = categoryContext;
        }

        [HttpGet]
        public async Task<ActionResult> testgetAlll()
        {
            var categoryList = await _categoryContext.Categories.Find(_ => true).ToListAsync();
            return Ok(categoryList);

        }
    }
}
