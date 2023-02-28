using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.Models;

[Table("mission_invite")]
public partial class MissionInvite
{
    [Key]
    [Column("mission_invite_id")]
    public long MissionInviteId { get; set; }

    [Column("mission_id")]
    public long MissionId { get; set; }

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
    [InverseProperty("MissionInviteFromUsers")]
    public virtual User FromUser { get; set; } = null!;

    [ForeignKey("MissionId")]
    [InverseProperty("MissionInvites")]
    public virtual Mission Mission { get; set; } = null!;

    [ForeignKey("ToUserId")]
    [InverseProperty("MissionInviteToUsers")]
    public virtual User ToUser { get; set; } = null!;
}
