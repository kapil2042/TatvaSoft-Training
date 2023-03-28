using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.Models;

[Table("mission")]
public partial class Mission
{
    [Key]
    [Column("mission_id")]
    public long MissionId { get; set; }

    [Column("theme_id")]
    public long ThemeId { get; set; }

    [Column("city_id")]
    public long CityId { get; set; }

    [Column("country_id")]
    public long CountryId { get; set; }

    [Column("title")]
    [StringLength(128)]
    public string Title { get; set; } = null!;

    [Column("short_description", TypeName = "text")]
    public string? ShortDescription { get; set; }

    [Column("description", TypeName = "text")]
    public string? Description { get; set; }

    [Column("total_seat")]
    public int TotalSeat { get; set; }

    [Column("start_date", TypeName = "datetime")]
    public DateTime? StartDate { get; set; }

    [Column("end_date", TypeName = "datetime")]
    public DateTime? EndDate { get; set; }

    [Column("mission_type")]
    [StringLength(10)]
    public string MissionType { get; set; } = null!;

    [Column("status")]
    public int? Status { get; set; }

    [Column("organization_name")]
    [StringLength(255)]
    public string? OrganizationName { get; set; }

    [Column("organization_details", TypeName = "text")]
    public string? OrganizationDetails { get; set; }

    [Column("availability")]
    [StringLength(10)]
    public string? Availability { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at", TypeName = "datetime")]
    public DateTime? DeletedAt { get; set; }

    [ForeignKey("CityId")]
    [InverseProperty("Missions")]
    public virtual City City { get; set; } = null!;

    [InverseProperty("Mission")]
    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    [ForeignKey("CountryId")]
    [InverseProperty("Missions")]
    public virtual Country Country { get; set; } = null!;

    [InverseProperty("Mission")]
    public virtual ICollection<FavoriteMission> FavoriteMissions { get; } = new List<FavoriteMission>();

    [InverseProperty("Mission")]
    public virtual ICollection<GoalMission> GoalMissions { get; } = new List<GoalMission>();

    [InverseProperty("Mission")]
    public virtual ICollection<MissionApplicatoin> MissionApplicatoins { get; } = new List<MissionApplicatoin>();

    [InverseProperty("Mission")]
    public virtual ICollection<MissionDocument> MissionDocuments { get; } = new List<MissionDocument>();

    [InverseProperty("Mission")]
    public virtual ICollection<MissionInvite> MissionInvites { get; } = new List<MissionInvite>();

    [InverseProperty("Mission")]
    public virtual ICollection<MissionMedium> MissionMedia { get; } = new List<MissionMedium>();

    [InverseProperty("Mission")]
    public virtual ICollection<MissionRating> MissionRatings { get; } = new List<MissionRating>();

    [InverseProperty("Mission")]
    public virtual ICollection<MissionSkill> MissionSkills { get; } = new List<MissionSkill>();

    [InverseProperty("Mission")]
    public virtual ICollection<Story> Stories { get; } = new List<Story>();

    [ForeignKey("ThemeId")]
    [InverseProperty("Missions")]
    public virtual MissionTheme Theme { get; set; } = null!;

    [InverseProperty("Mission")]
    public virtual ICollection<Timesheet> Timesheets { get; } = new List<Timesheet>();
}
