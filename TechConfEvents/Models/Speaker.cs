using System;
using System.Collections.Generic;

namespace TechConfEvents.Models;

public partial class Speaker
{
    public Guid SpeakerId { get; set; }

    public string Name { get; set; } = null!;

    public string? Biography { get; set; }

    public string? SocialLinks { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
