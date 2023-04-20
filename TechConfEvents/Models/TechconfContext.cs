using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TechConfEvents.Models;

public partial class TechConfContext : DbContext
{
    public TechConfContext()
    {
    }

    public TechConfContext(DbContextOptions<TechConfContext> options)
        : base(options)
    {
       
    }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Speaker> Speakers { get; set; }

    public virtual DbSet<SpeakerSession> SpeakerSessions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=TechConf;Integrated Security = true;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK_Event");

            entity.Property(e => e.EventId).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.EventEndDate).HasColumnType("date");
            entity.Property(e => e.EventStartDate).HasColumnType("date");
            entity.Property(e => e.EventTitle).HasMaxLength(100);
            entity.Property(e => e.LinkForDetails).HasMaxLength(200);
            entity.Property(e => e.Venue).HasMaxLength(200);
            entity.Property(e => e.Website).HasMaxLength(200);

            entity.HasOne(d => d.Speaker).WithMany(p => p.Events)
                .HasForeignKey(d => d.SpeakerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Event_Speaker");
        });

        modelBuilder.Entity<Speaker>(entity =>
        {
            entity.HasKey(e => e.SpeakerId).HasName("PK_Speaker");

            entity.Property(e => e.SpeakerId).ValueGeneratedNever();
            entity.Property(e => e.Biography).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.SocialLinks).HasMaxLength(100);
        });

        modelBuilder.Entity<SpeakerSession>(entity =>
        {
            entity.Property(e => e.SpeakerSessionId).ValueGeneratedNever();
            entity.Property(e => e.SessionDate).HasColumnType("date");

            entity.HasOne(d => d.Event).WithMany(p => p.SpeakerSessions)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SpeakerSessions_Events1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
