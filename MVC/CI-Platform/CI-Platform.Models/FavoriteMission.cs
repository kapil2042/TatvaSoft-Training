using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.Models;

[Table("favorite_mission")]
public partial class FavoriteMission
{
    [Key]
    [Column("favourite_mission_id")]
    public long FavouriteMissionId { get; set; }

    [Column("user_id")]
    public long UserId { get; set; }

    [Column("mission_id")]
    public long MissionId { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at", TypeName = "datetime")]
    public DateTime? DeletedAt { get; set; }

    [ForeignKey("MissionId")]
    [InverseProperty("FavoriteMissions")]
    public virtual Mission Mission { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("FavoriteMissions")]
    public virtual User User { get; set; } = null!;
}
