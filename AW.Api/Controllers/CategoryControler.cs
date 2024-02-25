using System.Linq.Expressions;
using System.Net;

using Microsoft.AspNetCore.Mvc;

using AutoMapper;

using AW.Common.Interfaces.Services;
using AW.Domain.Dto.Response;
using AW.Domain.Entities;
using AW.Api.Responses;
using AW.Domain.Dto.Request.Create;
// using AW.Domain.Dto.Request.Update;
using AW.Common.Exceptions;
using AW.Common.Helpers;
using Microsoft.AspNetCore.Authorization;
using AW.Domain.Dto.QueryFilters;
using AW.Domain.Interfaces.Services;
using AW.Common.Functions;

namespace Aw.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize]
public class CategoryControler : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICatalogBaseService<Category> _service;
    private readonly TokenHelper _tokenHelper;

    public CategoryControler(IMapper mapper, ICatalogBaseService<Category> service, TokenHelper tokenHelper)
    {
        this._mapper = mapper;
        this._service = service;
        this._tokenHelper = tokenHelper;
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<CategoryResponseDto>))]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateRequestDto requestDto)
    {
        try
        {
            var entity = _mapper.Map<Category>(requestDto);
            entity.CreatedBy = _tokenHelper.GetUserName();
            await _service.Create(entity);
            var dto = _mapper.Map<CategoryResponseDto>(entity);
            var response = new ApiResponse<CategoryResponseDto>(data: dto);
            return Ok(response);
        }
        catch (Exception ex)
        {
            throw new LogicBusinessException(ex);
        }
    }
}