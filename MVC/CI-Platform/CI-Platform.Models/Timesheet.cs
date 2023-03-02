using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.Models;

[Table("timesheet")]
public partial class Timesheet
{
    [Key]
    [Column("timesheet_id")]
    public long TimesheetId { get; set; }

    [Column("user_id")]
    public long UserId { get; set; }

    [Column("mission_id")]
    public long MissionId { get; set; }

    [Column("time")]
    public TimeSpan? Time { get; set; }

    [Column("action")]
    public int? Action { get; set; }

    [Column("date_volunteered", TypeName = "datetime")]
    public DateTime DateVolunteered { get; set; }

    [Column("notes", TypeName = "text")]
    public string? Notes { get; set; }

    [Column("status")]
    [StringLength(20)]
    public string Status { get; set; } = null!;

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at", TypeName = "datetime")]
    public DateTime? DeletedAt { get; set; }

    [ForeignKey("MissionId")]
    [InverseProperty("Timesheets")]
    public virtual Mission Mission { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Timesheets")]
    public virtual User User { get; set; } = null!;
}
