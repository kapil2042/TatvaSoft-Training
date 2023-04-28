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
    [MaxLength(16)]
    public string? FirstName { get; set; }

    [Column("last_name")]
    [MaxLength(16)]
    public string? LastName { get; set; }

    [Column("email")]
    [StringLength(128)]
    public string Email { get; set; } = null!;

    [Column("password")]
    [MaxLength(255)]
    public string Password { get; set; } = null!;

    [Column("phone_number")]
    [MaxLength(15)]
    public string PhoneNumber { get; set; } = null!;

    [Column("avatar")]
    [MaxLength(2048)]
    public string? Avatar { get; set; }

    [Column("why_i_volunteer", TypeName = "text")]
    [MaxLength(1000, ErrorMessage = "This has not containe more than 1000 character")]
    public string? WhyIVolunteer { get; set; }

    [Column("employee_id")]
    [StringLength(16)]
    public string? EmployeeId { get; set; }

    [Column("manager_details", TypeName = "text")]
    public string? ManagerDetails { get; set; }

    [Column("department")]
    [MaxLength(16)]
    public string? Department { get; set; }

    [Column("availability")]
    [StringLength(10)]
    public string? Availability { get; set; }

    [Column("city_id")]
    public long CityId { get; set; }

    [Column("country_id")]
    public long CountryId { get; set; }

    [Column("profile_text", TypeName = "text")]
    [MaxLength(1000,ErrorMessage ="Profile Text is not containe more than 1000 character")]
    public string? ProfileText { get; set; }

    [Column("linked_in_url")]
    [StringLength(255)]
    [RegularExpression(@"https:\/\/www\.linkedin\.com\/in\/[a-zA-Z0-9-]+\/?", ErrorMessage = "Write correct linked url!")]
    public string? LinkedInUrl { get; set; }

    [Column("title")]
    [StringLength(255)]
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
