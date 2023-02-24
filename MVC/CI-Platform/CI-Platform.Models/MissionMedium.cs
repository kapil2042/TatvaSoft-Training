using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.Models;

[Table("mission_media")]
public partial class MissionMedium
{
    [Key]
    [Column("mission_media_id")]
    public long MissionMediaId { get; set; }

    [Column("mission_id")]
    public long MissionId { get; set; }

    [Column("media_name")]
    [StringLength(64)]
    [Unicode(false)]
    public string? MediaName { get; set; }

    [Column("media_type")]
    [StringLength(4)]
    [Unicode(false)]
    public string? MediaType { get; set; }

    [Column("media_path")]
    [StringLength(255)]
    [Unicode(false)]
    public string? MediaPath { get; set; }

    [Column("media_default")]
    public int? MediaDefault { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at", TypeName = "datetime")]
    public DateTime? DeletedAt { get; set; }

    [ForeignKey("MissionId")]
    [InverseProperty("MissionMedia")]
    public virtual Mission Mission { get; set; } = null!;
}
