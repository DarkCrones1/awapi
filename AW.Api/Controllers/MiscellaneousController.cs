using System.Net;

using Microsoft.AspNetCore.Mvc;

using AW.Common.Dtos.Response;
using AW.Domain.Interfaces.Services;
using AW.Api.Responses;

namespace AW.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class MiscellaneousController : ControllerBase
{
    private readonly IMiscellaneousService _service;

    public MiscellaneousController(IMiscellaneousService service)
    {
        this._service = service;
    }

    [HttpGet]
    [Route("CartStatus")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ResponseCache(Duration = 300)]
    public async Task<IActionResult> GetCartStatus()
    {
        var lstItem = await _service.GetCartStatus();
        var response = new ApiResponse<IEnumerable<EnumValueResponseDto>>(lstItem);
        return Ok(response);
    }

    [HttpGet]
    [Route("CraftmanStatus")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ResponseCache(Duration = 300)]
    public async Task<IActionResult> GetCraftmanStatus()
    {
        var lstItem = await _service.GetCraftmanStatus();
        var response = new ApiResponse<IEnumerable<EnumValueResponseDto>>(lstItem);
        return Ok(response);
    }

    [HttpGet]
    [Route("Gender")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ResponseCache(Duration = 300)]
    public async Task<IActionResult> GetGender()
    {
        var lstItem = await _service.GetGender();
        var response = new ApiResponse<IEnumerable<EnumValueResponseDto>>(lstItem);
        return Ok(response);
    }

    [HttpGet]
    [Route("TicketStatus")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ResponseCache(Duration = 300)]
    public async Task<IActionResult> GetTicketStatus()
    {
        var lstItem = await _service.GetTicketStatus();
        var response = new ApiResponse<IEnumerable<EnumValueResponseDto>>(lstItem);
        return Ok(response);
    }

    [HttpGet]
    [Route("UserAccountType")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ResponseCache(Duration = 300)]
    public async Task<IActionResult> GetUserAccountType()
    {
        var lstItem = await _service.GetUserAccountType();
        var response = new ApiResponse<IEnumerable<EnumValueResponseDto>>(lstItem);
        return Ok(response);
    }
}