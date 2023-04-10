using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.Models;

[Table("mission_theme")]
public partial class MissionTheme
{
    [Key]
    [Column("mission_theme_id")]
    public long MissionThemeId { get; set; }

    [Column("title")]
    [StringLength(255)]
    [Required(ErrorMessage ="Theme Title is required")]
    public string? Title { get; set; }

    [Column("status")]
    [Required(ErrorMessage ="Status is required")]
    public byte? Status { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at", TypeName = "datetime")]
    public DateTime? DeletedAt { get; set; }

    [InverseProperty("Theme")]
    public virtual ICollection<Mission> Missions { get; } = new List<Mission>();
}
