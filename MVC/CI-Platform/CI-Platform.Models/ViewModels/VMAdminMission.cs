using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Models.ViewModels
{
    public class VMAdminMission
    {
        [Key]
        public long MissionId { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Please Select Theme")]
        public long ThemeId { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Please Select City")]
        public long CityId { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Please Select Country")]
        public long CountryId { get; set; }

        [StringLength(128)]
        public string Title { get; set; } = null!;

        [MaxLength(255, ErrorMessage = "Short Description Must Between 1 to 255 Character")]
        public string ShortDescription { get; set; } = null!;

        [MaxLength(40000, ErrorMessage = "Description Must Between 1 to 40000 Character")]
        public string Description { get; set; } = null!;

        [Range(1, int.MaxValue, ErrorMessage = "Please Enter Total Seat")]
        public int TotalSeat { get; set; }

        [Required(ErrorMessage = "Please Select Start Date")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "Please Select End Date")]
        public DateTime? EndDate { get; set; }

        [StringLength(10)]
        [Required(ErrorMessage = "Please Select Mission Type")]
        public string MissionType { get; set; } = null!;

        public int Status { get; set; }

        [StringLength(255)]
        public string OrganizationName { get; set; } = null!;

        [MaxLength(40000, ErrorMessage = "Short Description Must Between 1 to 40000 Character")]
        public string OrganizationDetails { get; set; } = null!;

        [StringLength(10)]
        [Required(ErrorMessage = "Please Select Availability")]
        public string Availability { get; set; } = null!;

        public List<Country> country { get; set; } = null!;

        public List<MissionTheme> themes { get; set; } = null!;

        public List<Skill> skills { get; set; } = null!;

        public List<MissionSkill> MissionSkills { get; set; } = null!;

        [Range(1, int.MaxValue, ErrorMessage = "Please Enter Goal Value")]
        public int GoalValue { get; set; }

        public string GoalObjectiveText { get; set; } = null!;

        public List<MissionDocument>? mDocuments { get; set; }

        public List<MissionMedium>? missionMedia { get; set; }
    }
}
