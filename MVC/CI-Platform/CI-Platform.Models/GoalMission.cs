using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.Models;

[Table("goal_mission")]
public partial class GoalMission
{
    [Key]
    [Column("goal_mission_id")]
    public long GoalMissionId { get; set; }

    [Column("mission_id")]
    public long MissionId { get; set; }

    [Column("goal_objective_text")]
    [StringLength(255)]
    [Unicode(false)]
    public string? GoalObjectiveText { get; set; }

    [Column("goal_value")]
    public int GoalValue { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at", TypeName = "datetime")]
    public DateTime? DeletedAt { get; set; }

    [ForeignKey("MissionId")]
    [InverseProperty("GoalMissions")]
    public virtual Mission Mission { get; set; } = null!;
}
