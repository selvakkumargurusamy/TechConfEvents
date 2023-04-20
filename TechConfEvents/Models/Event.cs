using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechConfEvents.Models;

public partial class Event
{
    [Key]
    public Guid EventId { get; set; }

    public string EventTitle { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime EventStartDate { get; set; }

    public DateTime EventEndDate { get; set; }

    public bool IsOnline { get; set; }

    public string? Venue { get; set; }

    public string? Website { get; set; }

    public string? LinkForDetails { get; set; }
    [ForeignKey("FK_Event_Speaker")]
    public Guid SpeakerId { get; set; }

    public virtual Speaker Speaker { get; set; } = null!;

    public virtual ICollection<SpeakerSession> SpeakerSessions { get; set; } = new List<SpeakerSession>();
}
