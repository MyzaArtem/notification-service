using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Dtos;
using NotificationService.Models;
using NotificationService.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NotificationService.Controllers
{
    [Route("notifications/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _service;
        private readonly IMapper _mapper;

        public NotificationController(INotificationService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("foruser/{userId}", Name = "GetAllNotificationsForUser")]
        public async Task<ActionResult<IEnumerable<NotificationReadDto>>> GetAllNotificationsForUser(int userId)
        {
            try
            {
                var notifications = await _service.GetAllNotificationsForUserAsync(userId);
                if (notifications == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<IEnumerable<NotificationReadDto>>(notifications));
            }
            catch (ArgumentException ex)
            {
                return BadRequest("User ID is invalid: " + ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "GetNotificationById")]
        public async Task<ActionResult<NotificationReadDto>> GetNotificationById(int id)
        {
            try
            {
                var notification = await _service.GetNotificationByIdAsync(id);
                if (notification == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<NotificationReadDto>(notification));
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Concurrency error occurred");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<NotificationReadDto>> CreateNotifications(NotificationCreateDto notificationCreateDto)
        {
            try
            {
                var notificationModel = _mapper.Map<Notification>(notificationCreateDto);
                await _service.CreateNotificationAsync(notificationModel);

                var notificationReadDto = _mapper.Map<NotificationReadDto>(notificationModel);

                return CreatedAtRoute(nameof(GetNotificationById), new { id = notificationReadDto.Id }, notificationReadDto);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest("Notification is null: " + ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNotification(int id, NotificationUpdateDto notificationUpdateDto)
        {
            try
            {
                var notificationModel = _mapper.Map<Notification>(notificationUpdateDto);
                await _service.UpdateNotificationAsync(notificationModel);

                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Concurrency error occurred");
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest("Notification is null: " + ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            try
            {
                var notification = await _service.GetNotificationByIdAsync(id);

                await _service.DeleteNotificationAsync(notification);
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Concurrency error occurred");
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest("Notification is null: " + ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
