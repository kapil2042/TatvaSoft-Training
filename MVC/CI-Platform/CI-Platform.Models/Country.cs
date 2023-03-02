using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.Models;

[Table("country")]
public partial class Country
{
    [Key]
    [Column("country_id")]
    public long CountryId { get; set; }

    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    [Column("ISO")]
    [StringLength(16)]
    public string? Iso { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at", TypeName = "datetime")]
    public DateTime? DeletedAt { get; set; }

    [InverseProperty("Country")]
    public virtual ICollection<City> Cities { get; } = new List<City>();

    [InverseProperty("Country")]
    public virtual ICollection<Mission> Missions { get; } = new List<Mission>();

    [InverseProperty("Country")]
    public virtual ICollection<User> Users { get; } = new List<User>();
}
