using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeams_POCOs
{
    // Plain Old C# Object
    public class DevTeam
    {
        public DevTeam()
        {

        }
        public DevTeam(List<Developer> developers, string teamName, int teamId)
        {
            Developers = developers;
            TeamName = teamName;
            TeamID = teamId;
        }
        // Teams need to contain their Team members (Developers) and their Team Name, and Team ID.
        public List<Developer> Developers { get; set; } = new List<Developer>();
        public string TeamName { get; set; }

        // unique identifier
        public int TeamID { get; set; }
    }
}
