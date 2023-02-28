using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.Models;

[Table("users")]
public partial class User
{
    [Key]
    [Column("user_id")]
    public long UserId { get; set; }

    [Column("first_name")]
    [StringLength(16)]
    [Unicode(false)]
    public string? FirstName { get; set; }

    [Column("last_name")]
    [StringLength(16)]
    [Unicode(false)]
    public string? LastName { get; set; }

    [Column("email")]
    [StringLength(128)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [Column("password")]
    [StringLength(255)]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    [Column("phone_number")]
    [StringLength(15)]
    [Unicode(false)]
    public string PhoneNumber { get; set; } = null!;

    [Column("avatar")]
    [StringLength(2048)]
    [Unicode(false)]
    public string? Avatar { get; set; }

    [Column("why_i_volunteer", TypeName = "text")]
    public string? WhyIVolunteer { get; set; }

    [Column("employee_id")]
    [StringLength(16)]
    [Unicode(false)]
    public string? EmployeeId { get; set; }

    [Column("department")]
    [StringLength(16)]
    [Unicode(false)]
    public string? Department { get; set; }

    [Column("city_id")]
    public long CityId { get; set; }

    [Column("country_id")]
    public long CountryId { get; set; }

    [Column("profile_text", TypeName = "text")]
    public string? ProfileText { get; set; }

    [Column("linked_in_url")]
    [StringLength(255)]
    [Unicode(false)]
    public string? LinkedInUrl { get; set; }

    [Column("title")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Title { get; set; }

    [Column("status")]
    public int Status { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at", TypeName = "datetime")]
    public DateTime? DeletedAt { get; set; }

    [ForeignKey("CityId")]
    [InverseProperty("Users")]
    public virtual City City { get; set; } = null!;

    [InverseProperty("User")]
    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    [ForeignKey("CountryId")]
    [InverseProperty("Users")]
    public virtual Country Country { get; set; } = null!;

    [InverseProperty("User")]
    public virtual ICollection<FavoriteMission> FavoriteMissions { get; } = new List<FavoriteMission>();

    [InverseProperty("User")]
    public virtual ICollection<MissionApplicatoin> MissionApplicatoins { get; } = new List<MissionApplicatoin>();

    [InverseProperty("FromUser")]
    public virtual ICollection<MissionInvite> MissionInviteFromUsers { get; } = new List<MissionInvite>();

    [InverseProperty("ToUser")]
    public virtual ICollection<MissionInvite> MissionInviteToUsers { get; } = new List<MissionInvite>();

    [InverseProperty("User")]
    public virtual ICollection<MissionRating> MissionRatings { get; } = new List<MissionRating>();

    [InverseProperty("User")]
    public virtual ICollection<Story> Stories { get; } = new List<Story>();

    [InverseProperty("FromUser")]
    public virtual ICollection<StoryInvite> StoryInviteFromUsers { get; } = new List<StoryInvite>();

    [InverseProperty("ToUser")]
    public virtual ICollection<StoryInvite> StoryInviteToUsers { get; } = new List<StoryInvite>();

    [InverseProperty("User")]
    public virtual ICollection<Timesheet> Timesheets { get; } = new List<Timesheet>();

    [InverseProperty("User")]
    public virtual ICollection<UserSkill> UserSkills { get; } = new List<UserSkill>();
}
