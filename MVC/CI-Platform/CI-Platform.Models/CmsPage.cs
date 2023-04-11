using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.Models;

[Table("cms_page")]
public partial class CmsPage
{
    [Key]
    [Column("csm_page_id")]
    public long CsmPageId { get; set; }

    [Column("title")]
    [StringLength(255)]
    [Required]
    public string? Title { get; set; }

    [Column("description", TypeName = "text")]
    [Required]
    public string? Description { get; set; }

    [Column("slug")]
    [StringLength(255)]
    [Required]
    public string Slug { get; set; } = null!;

    [Column("status")]
    [Required]
    public int? Status { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at", TypeName = "datetime")]
    public DateTime? DeletedAt { get; set; }
}
