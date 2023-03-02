using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.Models;

[Table("skill")]
public partial class Skill
{
    [Key]
    [Column("skill_id")]
    public int SkillId { get; set; }

    [Column("skill_name")]
    [StringLength(64)]
    public string SkillName { get; set; } = null!;

    [Column("status")]
    public byte? Status { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at", TypeName = "datetime")]
    public DateTime? DeletedAt { get; set; }

    [InverseProperty("Skill")]
    public virtual ICollection<MissionSkill> MissionSkills { get; } = new List<MissionSkill>();

    [InverseProperty("Skill")]
    public virtual ICollection<UserSkill> UserSkills { get; } = new List<UserSkill>();
}
