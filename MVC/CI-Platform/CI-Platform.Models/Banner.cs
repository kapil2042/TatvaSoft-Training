using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.Models;

[Table("banner")]
public partial class Banner
{
    [Key]
    [Column("banner_id")]
    public long BannerId { get; set; }

    [Column("image")]
    [StringLength(512)]
    public string Image { get; set; } = null!;

    [Column("title")]
    [Required]
    [MaxLength(255)]
    public string Title { get; set; }

    [Column("text", TypeName = "text")]
    [MaxLength(2000)]
    [Required]
    public string? Text { get; set; }

    [Column("sort_order")]
    [Required]
    public int? SortOrder { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at", TypeName = "datetime")]
    public DateTime? DeletedAt { get; set; }
}
