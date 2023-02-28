using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.Models;

[Table("token")]
public partial class Token
{
    [Key]
    public long Id { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [Column("Token")]
    [StringLength(30)]
    public string Token1 { get; set; } = null!;

    [Column("Generated_at", TypeName = "datetime")]
    public DateTime GeneratedAt { get; set; }

    [Column("used")]
    public int Used { get; set; }
}
