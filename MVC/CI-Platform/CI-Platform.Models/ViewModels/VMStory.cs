using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Models.ViewModels
{
    public class VMStory
    {
        public List<User> users { get; set; }
        public List<Country>? countries { get; set; }
        public List<City>? cities { get; set; }
        public List<MissionTheme>? themes { get; set; }
        public List<Skill>? skills { get; set; }
        public List<Story> stories { get; set; }
    }
}
