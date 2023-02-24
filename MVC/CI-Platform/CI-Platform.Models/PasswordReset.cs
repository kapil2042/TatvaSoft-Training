using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.Models;

[Keyless]
[Table("password_reset")]
public partial class PasswordReset
{
    [Column("email")]
    [StringLength(191)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [Column("token")]
    [StringLength(191)]
    [Unicode(false)]
    public string Token { get; set; } = null!;

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }
}
