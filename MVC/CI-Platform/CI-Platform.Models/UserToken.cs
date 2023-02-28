using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.Models;

[Table("user_token")]
public partial class UserToken
{
    [Key]
    public long Id { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [Column("UserToken")]
    [StringLength(50)]
    [Unicode(false)]
    public string UserToken1 { get; set; } = null!;

    [Column("Generated_at", TypeName = "datetime")]
    public DateTime GeneratedAt { get; set; }

    public int Used { get; set; }
}
