﻿using System.ComponentModel.DataAnnotations;

namespace EventReservation.Models.DTO.Event;

public class EventDto
{
    public int Id { get; set; }
    [Required] [MaxLength(50)] public string? Name { get; set; }
    [Required] [MaxLength(500)] public string? Description { get; set; }
    [Required] public DateTime StartTime { get; set; }
    [Required] public DateTime EndTime { get; set; }
    [Required] [MaxLength(50)] public string? Location { get; set; }
    [Required] [MaxLength(50)] public string? EventEmail { get; set; }
    [Required] public bool IsOverLappingAllowed { get; set; }
    [Required] public string? CoordinatorName { get; set; }
    [Required] public string? CoordinatorSurname { get; set; }
    [Required] public string? CoordinatorPhone { get; set; }
}