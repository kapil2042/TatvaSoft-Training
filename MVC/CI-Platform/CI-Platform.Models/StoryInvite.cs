using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.Models;

[Table("story_invite")]
public partial class StoryInvite
{
    [Key]
    [Column("story_invite_id")]
    public long StoryInviteId { get; set; }

    [Column("story_id")]
    public long StoryId { get; set; }

    [Column("from_user_id")]
    public long FromUserId { get; set; }

    [Column("to_user_id")]
    public long ToUserId { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at", TypeName = "datetime")]
    public DateTime? DeletedAt { get; set; }

    [ForeignKey("FromUserId")]
    [InverseProperty("StoryInviteFromUsers")]
    public virtual User FromUser { get; set; } = null!;

    [ForeignKey("StoryId")]
    [InverseProperty("StoryInvites")]
    public virtual Story Story { get; set; } = null!;

    [ForeignKey("ToUserId")]
    [InverseProperty("StoryInviteToUsers")]
    public virtual User ToUser { get; set; } = null!;
}
