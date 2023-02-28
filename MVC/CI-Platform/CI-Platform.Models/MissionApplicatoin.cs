using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.Models;

[Table("mission_applicatoin")]
public partial class MissionApplicatoin
{
    [Key]
    [Column("mission_application_id")]
    public long MissionApplicationId { get; set; }

    [Column("mission_id")]
    public long MissionId { get; set; }

    [Column("user_id")]
    public long UserId { get; set; }

    [Column("applied_at", TypeName = "datetime")]
    public DateTime? AppliedAt { get; set; }

    [Column("approval_status")]
    [StringLength(10)]
    [Unicode(false)]
    public string? ApprovalStatus { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at", TypeName = "datetime")]
    public DateTime? DeletedAt { get; set; }

    [ForeignKey("MissionId")]
    [InverseProperty("MissionApplicatoins")]
    public virtual Mission Mission { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("MissionApplicatoins")]
    public virtual User User { get; set; } = null!;
}
