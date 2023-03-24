using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Models.ViewModels
{
    public class VMStoryDetails
    {
        public Story story { get; set; }
        public List<User> users { get; set; }
        public List<StoryMedium> storyMedia { get; set; }
    }
}
