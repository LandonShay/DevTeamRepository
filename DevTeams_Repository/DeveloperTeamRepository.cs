using DevTeams_POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeams_Repository
{
    public class DeveloperTeamRepository
    {
        private readonly List<DevTeam> _devTeamContext = new List<DevTeam>();
        private DeveloperRepository _devRepo;
        private int _count;

        public DeveloperTeamRepository(DeveloperRepository devRepo)
        {
            _devRepo = devRepo;
        }
        public bool AddTeamToDirectory(DevTeam devTeam)
        {
            if (devTeam == null)
            {
                return false;
            }
            else
            {
                _count++;
                devTeam.TeamID = _count;
                _devTeamContext.Add(devTeam);
                return true;
            }
        }

        public List<DevTeam> GetAllTeams()
        {
            return _devTeamContext;
        }

        public DevTeam GetDevTeamByID(int teamID)
        {
            foreach (DevTeam devTeam in _devTeamContext)
            {
                if (devTeam.TeamID == teamID)
                {
                    return devTeam;
                }
            }
            return null;
        }

        public bool UpdateExistingDevTeam(int id, DevTeam devTeam)
        {
            DevTeam oldDevTeam = GetDevTeamByID(id);

            if (oldDevTeam != null)
            {
                oldDevTeam.TeamID = devTeam.TeamID;
                oldDevTeam.TeamName = devTeam.TeamName;
                oldDevTeam.Developers = devTeam.Developers;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddDeveloperToTeam(int id, int teamId)
        {
            DevTeam devTeam = GetDevTeamByID(teamId);
            if (devTeam == null)
            {
                return false;
            }
            Developer developer = _devRepo.GetOneDeveloper(id);
            if (developer == null)
            {
                return false;
            }
            devTeam.Developers.Add(developer);
            return true;
        }

        public bool RemoveDeveloperFromTeam(int id, int teamId)
        {
            DevTeam devTeam = GetDevTeamByID(teamId);
            if (devTeam == null)
            {
                return false;
            }
            Developer developer = _devRepo.GetOneDeveloper(id);
            if (developer == null)
            {
                return false;
            }
            devTeam.Developers.Remove(developer);
            return true;
        }

        public bool AddMultipleDevsToTeam(int teamId, List<Developer> developers)
        {
            DevTeam devTeam = GetDevTeamByID(teamId);
            if (devTeam == null)
            {
                return false;
            }
            devTeam.Developers.AddRange(developers);
            return true;
        }

        public bool DeleteDevTeam(DevTeam devTeam)
        {
            bool deleteDevTeam = _devTeamContext.Remove(devTeam);
            return deleteDevTeam;
        }
    }
}
