using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.Models;

[Table("mission_rating")]
public partial class MissionRating
{
    [Key]
    [Column("mission_rating_id")]
    public long MissionRatingId { get; set; }

    [Column("user_id")]
    public long UserId { get; set; }

    [Column("mission_id")]
    public long MissionId { get; set; }

    [Column("rating")]
    public int? Rating { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at", TypeName = "datetime")]
    public DateTime? DeletedAt { get; set; }

    [ForeignKey("MissionId")]
    [InverseProperty("MissionRatings")]
    public virtual Mission Mission { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("MissionRatings")]
    public virtual User User { get; set; } = null!;
}
