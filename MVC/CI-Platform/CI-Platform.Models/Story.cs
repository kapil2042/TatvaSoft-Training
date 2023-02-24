﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.Models;

[Table("story")]
public partial class Story
{
    [Key]
    [Column("story_id")]
    public long StoryId { get; set; }

    [Column("user_id")]
    public long UserId { get; set; }

    [Column("mission_id")]
    public long MissionId { get; set; }

    [Column("title")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Title { get; set; }

    [Column("description", TypeName = "text")]
    public string? Description { get; set; }

    [Column("status")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Status { get; set; }

    [Column("published_at", TypeName = "datetime")]
    public DateTime? PublishedAt { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at", TypeName = "datetime")]
    public DateTime? DeletedAt { get; set; }

    [ForeignKey("MissionId")]
    [InverseProperty("Stories")]
    public virtual Mission Mission { get; set; } = null!;

    [InverseProperty("Story")]
    public virtual ICollection<StoryInvite> StoryInvites { get; } = new List<StoryInvite>();

    [InverseProperty("Story")]
    public virtual ICollection<StoryMedium> StoryMedia { get; } = new List<StoryMedium>();

    [ForeignKey("UserId")]
    [InverseProperty("Stories")]
    public virtual User User { get; set; } = null!;
}
