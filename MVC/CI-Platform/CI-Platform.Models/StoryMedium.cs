using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.Models;

[Table("story_media")]
public partial class StoryMedium
{
    [Key]
    [Column("story_media_id")]
    public long StoryMediaId { get; set; }

    [Column("story_id")]
    public long StoryId { get; set; }

    [Column("media_type")]
    [StringLength(4)]
    public string MediaType { get; set; } = null!;

    [Column("media_path")]
    [StringLength(255)]
    public string MediaPath { get; set; } = null!;

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at", TypeName = "datetime")]
    public DateTime? DeletedAt { get; set; }

    [ForeignKey("StoryId")]
    [InverseProperty("StoryMedia")]
    public virtual Story Story { get; set; } = null!;
}
