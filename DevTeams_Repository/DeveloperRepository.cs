using DevTeams_POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeams_Repository
{
    public class DeveloperRepository
    {
        private readonly List<Developer> _developerContext = new List<Developer>();
        private int _count;

        public bool AddDeveloperToDirectory(Developer developer)
        {
            if (developer == null)
            {
                return false;
            }
            else
            {
                _count++;
                developer.ID = _count;
                _developerContext.Add(developer);
                return true;
            }
        }

        public List<Developer> GetAllDevelopers()
        {
            return _developerContext;
        }

        public Developer GetOneDeveloper(int id)
        {
            foreach (Developer developer in _developerContext)
            {
                if (developer.ID == id)
                {
                    return developer;
                }
            }
            return null;
        }

        public bool UpdateExistingDeveloper(int id, Developer developer)
        {
            Developer oldDeveloper = GetOneDeveloper(id);

            if (oldDeveloper != null)
            {
                oldDeveloper.ID = developer.ID;
                oldDeveloper.FirstName = developer.FirstName;
                oldDeveloper.LastName = developer.LastName;
                oldDeveloper.HasPluralSight = developer.HasPluralSight;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteDeveloper(Developer developer)
        {
            bool deleteDev = _developerContext.Remove(developer);
            return deleteDev;
        }

        public List<Developer> CheckDeveloperPluralSight()
        {
            foreach (Developer developer in _developerContext)
            {
                if (developer.HasPluralSight != true)
                {
                    Console.WriteLine("\n" +
                        $"{developer.FullName} does not have a Pluralsight license.");
                }
                else
                {
                    
                }
            }
            return null;
        }
    }
}
