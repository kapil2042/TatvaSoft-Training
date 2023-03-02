using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.Models;

[Table("admin")]
public partial class Admin
{
    [Key]
    [Column("admin_id")]
    public long AdminId { get; set; }

    [Column("fisrt_name")]
    [StringLength(16)]
    public string? FisrtName { get; set; }

    [Column("last_name")]
    [StringLength(16)]
    public string? LastName { get; set; }

    [Column("email")]
    [StringLength(128)]
    public string Email { get; set; } = null!;

    [Column("password")]
    [StringLength(256)]
    public string Password { get; set; } = null!;

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at", TypeName = "datetime")]
    public DateTime? DeletedAt { get; set; }
}
