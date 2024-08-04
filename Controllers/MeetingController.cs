using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_webapi_ef.Data;
using dotnet_webapi_ef.DTOs.Meeting;
using dotnet_webapi_ef.Mappers;
using dotnet_webapi_ef.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_webapi_ef.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MeetingController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public MeetingController(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GettAll()
        {
            var meeting = _context.Meetings;
            return Ok(meeting);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var meeting = _context.Meetings.Find(id);
            if (meeting == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(meeting.ToMeetingDTO());
            }
        }

        [HttpPost]
        public IActionResult InsertMeeting([FromBody] MeetingDTO meetingDTO)
        {
            Meeting meeting = meetingDTO.ToMeeting();
            _context.Meetings.Add(meeting);
            int affect = _context.SaveChanges();

            if (affect > 0)
            {
                return CreatedAtAction(nameof(GetById), new { id = meeting.Idx }, meeting.ToMeetingDTO());
            }
            return StatusCode(400);
        }

        //Delete
        [HttpDelete("{id}")]
        public IActionResult DeleteMeeting([FromRoute] int id)
        {
            Meeting meeting = _context.Meetings.Find(id);
            if (meeting != null)
            {
                _context.Meetings.Remove(meeting);
                int affect = _context.SaveChanges();
                if (affect > 0)
                {
                    return Ok();
                }
            }
            return StatusCode(400);
        }

        //Update
        [HttpPut("{id}")]
        public IActionResult UpdateMeeting([FromRoute] int id,[FromBody] MeetingDTO meetingDTO){
            Meeting meeting=_context.Meetings.Find(id);
            if (meeting!=null)
            {
                //convert MeetingDTO to Model
                //meeting=MeetingMapper.ToMeeting(meetingDTO);
                meeting.Detail=meetingDTO.Detail;
                meeting.Meetingdatetime=meetingDTO.Meetingdatetime;
                meeting.Latitude=meetingDTO.Latitude;
                meeting.Longitude=meetingDTO.Longitude;
                //update
                _context.Meetings.Update(meeting);
                int affect = _context.SaveChanges();
                if (affect>0)
                {
                    //When update success Show resault By convert Model to DTO
                    return Accepted(meeting.ToMeetingDTO());
                }
            }
            return StatusCode(400);
        }
    }
}