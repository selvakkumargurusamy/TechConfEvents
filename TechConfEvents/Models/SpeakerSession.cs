using System;
using System.Collections.Generic;

namespace TechConfEvents.Models;

public partial class SpeakerSession
{
    public Guid SpeakerSessionId { get; set; }

    public Guid EventId { get; set; }

    public DateTime SessionDate { get; set; }

    public virtual Event Event { get; set; } = null!;
}
