using System.Linq.Expressions;
using System.Net;

using Microsoft.AspNetCore.Mvc;

using AutoMapper;

using AW.Common.Interfaces.Services;
using AW.Domain.Dto.Response;
using AW.Domain.Entities;
using AW.Api.Responses;
using AW.Domain.Dto.Request.Create;
using AW.Domain.Dto.Request.Update;
using AW.Common.Exceptions;
using AW.Common.Helpers;
using Microsoft.AspNetCore.Authorization;
using AW.Domain.Dto.QueryFilters;
using AW.Domain.Interfaces.Services;
using AW.Common.Functions;

namespace AW.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize]
public class CraftController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICraftService _service;
    private readonly TokenHelper _tokenHelper;

    public CraftController(IMapper mapper, ICraftService service, TokenHelper tokenHelper)
    {
        this._mapper = mapper;
        this._service = service;
        this._tokenHelper = tokenHelper;
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<CraftResponseDto>))]
    public async Task<IActionResult> CreateCraft([FromBody] CraftCreateRequestDto requestDto)
    {
        // try
        // {
        //     var entity = _mapper.Map<Craft>(requestDto);
        //     entity.CreatedBy = _tokenHelper.GetUserName();
        //     await _service.CreateCraft(entity, requestDto.CategoryIds!, requestDto.TechniqueTypeIds!);

        //     var result = _mapper.Map<CraftResponseDto>(entity);
        //     var response = new ApiResponse<CraftResponseDto>(result);
        //     return Ok(response);
        // }
        // catch (Exception ex)
        // {

        //     throw new LogicBusinessException(ex);
        // }
        var entity = _mapper.Map<Craft>(requestDto);
        entity.CreatedBy = _tokenHelper.GetUserName();
        await _service.CreateCraft(entity, requestDto.CategoryIds!, requestDto.TechniqueTypeIds!);

        var result = _mapper.Map<CraftResponseDto>(entity);
        var response = new ApiResponse<CraftResponseDto>(result);
        return Ok(response);
    }

}