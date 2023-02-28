using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.Models;

[Table("mission_skill")]
public partial class MissionSkill
{
    [Key]
    [Column("mission_skill_id")]
    public long MissionSkillId { get; set; }

    [Column("skill_id")]
    public int SkillId { get; set; }

    [Column("mission_id")]
    public long MissionId { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at", TypeName = "datetime")]
    public DateTime? DeletedAt { get; set; }

    [ForeignKey("MissionId")]
    [InverseProperty("MissionSkills")]
    public virtual Mission Mission { get; set; } = null!;

    [ForeignKey("SkillId")]
    [InverseProperty("MissionSkills")]
    public virtual Skill Skill { get; set; } = null!;
}
