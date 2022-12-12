using Sample.Api.Models;
using Sample.Api.Models.Meeting;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Mapster;
using Sample.Application.Contract.Meeting.Command;
using System.Collections.Generic;
using Sample.Application.Contract.Meeting.Query;
using Sample.Application.Contract.Meeting;

namespace Sample.Api.Controllers
{
    [ApiController]
    [Route("meeting")]
    public class MeetingController : BaseController
    {
        private readonly ILogger<MeetingController> _logger;

        private readonly IMediator _mediator;
        public MeetingController(IMediator mediator, ILogger<MeetingController> logger) : base(logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Create Meeting entity
        /// </summary>
        /// <param name="request"></param>
        /// <response code="201" >Entity created</response>
        /// <response code="400">Entity has missing/invalid values</response>
        [HttpPost]
        public async Task<ActionResult<MeetingViewModel>> CreateAsync([FromBody] CreateMeetingRequest request)
        {
            var command = request.Adapt<CreateMeetingCommand>();
            var meeting = await _mediator.Send(command);

            return Created(meeting.Adapt<MeetingViewModel>());
        }

        /// <summary>
        /// Get Meeting list by host msisdn
        /// </summary>
        /// <param name="msisdn"></param>
        /// <param name="request"></param>
        /// <response code="200">Successfully get entity</response>
        /// <response code="400">Entity has missing/invalid values</response>
        /// <response code="404">Entity not found</response>
        [HttpGet("{msisdn:required}/meetings")]
        public async Task<ActionResult<IEnumerable<MeetingViewModel>>> GetMeetingListAsync([FromRoute] long msisdn, [FromQuery] GetListRequest request)
        {
            var query = new GetHostMeetingsListQuery(msisdn, request.Offset, request.Count);

            var meetings = await _mediator.Send(query, HttpContext.RequestAborted);

            return List<MeetingResponseDto, MeetingViewModel>(meetings);
        }

        /// <summary>
        /// book Meeting entity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <response code="200" >Entity booked</response>
        /// <response code="400">Entity has missing/invalid values</response>
        [HttpPut("{id:guid}/booking")]
        public async Task<ActionResult> BookMeetingAsync([FromRoute] Guid id)
        {
            var bookCommand = new MarkAsBookedCommand(id);
            await _mediator.Send(bookCommand);

            return Ok();
        }
    }
}
