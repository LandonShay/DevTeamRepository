using DevTeams_POCOs;
using DevTeams_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeams.UI
{
    public class Program_UI
    {
        private readonly DeveloperRepository _devRepo;
        private readonly DeveloperTeamRepository _devTeamRepo;

        public Program_UI()
        {
            _devRepo = new DeveloperRepository();
            _devTeamRepo = new DeveloperTeamRepository(_devRepo);
        }

        public void Run()
        {
            Seed();
            RunApplication();
        }

        private void RunApplication()
        {

            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("Welcome to Dev Teams\n" +
                    "1. Add A Developer\n" +
                    "2. View All Existing Developers\n" +
                    "3. View An Existing Developer\n" +
                    "4. Update An Existing Developer\n" +
                    "5. Delete An Existing Developer\n" +
                    "6. Add Team\n" +
                    "7. View All Teams\n" +
                    "8. View One Team\n" +
                    "9. Add One Developer To Team\n" +
                    "10. Add Multiple Developers To Team\n" +
                    "11. Update Team\n" +
                    "12. Delete Team\n" +
                    "13. All Developers That Do Not Have a Pluralsight License");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        AddADeveloper();
                        break;
                    case "2":
                        ViewAllExistingDevelopers();
                        break;
                    case "3":
                        ViewAnExistingDeveloper();
                        break;
                    case "4":
                        UpdateAnExistingDeveloper();
                        break;
                    case "5":
                        DeleteAnExistingDeveloper();
                        break;
                    case "6":
                        AddTeamToDirectory();
                        break;
                    case "7":
                        GetAllTeams();
                        break;
                    case "8":
                        GetDevTeamByID();
                        break;
                    case "9":
                        AddDeveloperToTeam();
                        break;
                    case "10":
                        AddMultipleDevsToTeam();
                        break;
                    case "11":
                        UpdateExistingDevTeam();
                        break;
                    case "12":
                        DeleteDevTeam();
                        break;
                    case "13":
                        CheckDeveloperPluralSight();
                        break;
                    default:
                        Console.WriteLine("Invalid Selection.");
                        WaitForKey();
                        break;
                }
            }
        }

        private void CheckDeveloperPluralSight()
        {
            Console.Clear();
            _devRepo.CheckDeveloperPluralSight();
            WaitForKey();
        }

        private void AddADeveloper()
        {
            Console.Clear();
            Developer developer = new Developer();

            Console.WriteLine("Please enter Developer's first name");
            string devFirstName = Console.ReadLine();
            Console.WriteLine("Please enter Developer's last name");
            string devLastName = Console.ReadLine();
            bool keepLooping = true;

            while (keepLooping == true)
            {
                Console.Clear();
                Console.WriteLine($"Does {devFirstName} {devLastName} have a Pluralsight license? Please enter Yes or No.");
                string pluralSight = Console.ReadLine();
                if (pluralSight == "Yes")
                {
                    developer.HasPluralSight = true;
                    keepLooping = false;
                    continue;
                }
                else if (pluralSight == "No")
                {
                    developer.HasPluralSight = false;
                    keepLooping = false;
                    continue;
                }
                else
                {
                    Console.WriteLine("That is not a valid response. Please try again.");
                    WaitForKey();
                }
            }
            developer.FirstName = devFirstName;
            developer.LastName = devLastName;

            _devRepo.AddDeveloperToDirectory(developer);
        }
        private void AddTeamToDirectory()
        {
            Console.Clear();
            DevTeam devTeam = new DevTeam();

            Console.WriteLine("Please enter the dev team's name");
            devTeam.TeamName = Console.ReadLine();

            _devTeamRepo.AddTeamToDirectory(devTeam);
        }
        private void ViewAllExistingDevelopers()
        {
            Console.Clear();
            Console.WriteLine("Developers in database: ");
            List<Developer> dev = _devRepo.GetAllDevelopers();
            foreach (Developer person in dev)
            {
                DisplayDeveloperList(person);

            }
            WaitForKey();


        }
        private void GetAllTeams()
        {
            Console.Clear();
            Console.WriteLine("Dev Teams in Database: ");
            List<DevTeam> teams = _devTeamRepo.GetAllTeams();
            foreach (DevTeam team in teams)
            {
                DisplayDevTeamList(team);
            }
            WaitForKey();
        }
        private void ViewAnExistingDeveloper()
        {
            Console.Clear();
            Console.WriteLine("Please enter Developer ID: ");
            string person = Console.ReadLine();
            int result = Int32.Parse(person);
            Developer developer = _devRepo.GetOneDeveloper(result);
            if (developer == null)
            {
                Console.WriteLine("There is no Developer with that ID. Please try again.");
            }
            else
            {
                DisplayDeveloperList(developer);
            }
            WaitForKey();
        }
        private void UpdateAnExistingDeveloper()
        {
            Console.Clear();
            Console.WriteLine("Please enter the ID of the Developer you wish to edit: ");
            string person = Console.ReadLine();
            int result = Int32.Parse(person);
            Developer developer = _devRepo.GetOneDeveloper(result);
            bool keepLooping = true;
            while (keepLooping == true)
            {
                Console.Clear();
                DisplayDeveloperList(developer);
                Console.WriteLine("What do you wish to edit? Please enter corresponding number.\n" +
                    "1. First Name\n" +
                    "2. Last Name\n" +
                    "3. Pluralsight Status\n" +
                    "4. ID\n" +
                    "5. Done Editing");
                string response = Console.ReadLine();
                switch (response)
                {
                    case "1":
                        Console.WriteLine("Please enter new first name.");
                        developer.FirstName = Console.ReadLine();
                        break;
                    case "2":
                        Console.WriteLine("Please enter a new last name.");
                        developer.LastName = Console.ReadLine();
                        break;
                    case "3":
                        if (developer.HasPluralSight == true)
                        {
                            developer.HasPluralSight = false;
                        }
                        else
                        {
                            developer.HasPluralSight = true;
                        }
                        break;
                    case "4":
                        Console.WriteLine("Please enter new Developer ID.");
                        developer.ID = int.Parse(Console.ReadLine());
                        break;
                    case "5":
                        keepLooping = false;
                        break;
                }
            }
        }
        private void GetDevTeamByID()
        {
            Console.Clear();
            Console.WriteLine("Please enter team ID: ");
            string teamID = Console.ReadLine();
            int result = Int32.Parse(teamID);
            DevTeam devTeam = _devTeamRepo.GetDevTeamByID(result);

        }
        private void AddDeveloperToTeam()
        {
            Console.Clear();
            Console.WriteLine("Please enter the ID of the team you wish to add developer to.");
            string result = Console.ReadLine();
            int teamID = Int32.Parse(result);
            DevTeam devTeam = _devTeamRepo.GetDevTeamByID(teamID);
            Console.WriteLine($"Please enter the ID of the Developer you wish to add to {devTeam.TeamName}");
            string result2 = Console.ReadLine();
            int devID = Int32.Parse(result2);
            Developer dev = _devRepo.GetOneDeveloper(devID);
            _devTeamRepo.AddDeveloperToTeam(devID, teamID);
        }
        private void AddMultipleDevsToTeam()
        {
            Console.Clear();
            Console.WriteLine("Please enter the ID of the team you wish to add developers to.");
            string result = Console.ReadLine();
            int teamID = Int32.Parse(result);
            DevTeam devTeam = _devTeamRepo.GetDevTeamByID(teamID);
            Console.WriteLine($"Please enter the ID of the Developer you wish to add to {devTeam.TeamName}");
            string result2 = Console.ReadLine();
            int devID = Int32.Parse(result2);
            Developer dev = _devRepo.GetOneDeveloper(devID);
            _devTeamRepo.AddDeveloperToTeam(devID, teamID);
            bool keepLooping = true;
            while (keepLooping == true)
            {
                Console.Clear();
                DisplayDevTeamList(devTeam);
                Console.WriteLine("Add another developer? Please enter Yes or No.");
                string answer = Console.ReadLine();
                if (answer == "Yes")
                {
                    Console.WriteLine($"Please enter the ID of the Developer you wish to add to {devTeam.TeamName}");
                    string result3 = Console.ReadLine();
                    int devID2 = Int32.Parse(result3);
                    Developer dev2 = _devRepo.GetOneDeveloper(devID2);
                    _devTeamRepo.AddDeveloperToTeam(devID2, teamID);
                }
                else if (answer == "No")
                {
                    keepLooping = false;
                }
                else
                {
                    Console.WriteLine("That is not a valid answer. Please try again.");
                    WaitForKey();
                }
            }
        }
        private void UpdateExistingDevTeam()
        {
            Console.Clear();
            Console.WriteLine("Please enter the ID of the team you wish to edit: ");
            string devTeamID = Console.ReadLine();
            int result = Int32.Parse(devTeamID);
            DevTeam devTeam = _devTeamRepo.GetDevTeamByID(result);
            bool keepLooping = true;
            while (keepLooping == true)
            {
                Console.Clear();
                DisplayDevTeamList(devTeam);
                Console.WriteLine("");
                Console.WriteLine("What do you wish to edit?\n" +
                    "1. Team Name\n" +
                    "2. Team ID\n" +
                    "3. Add Developer to Team\n" +
                    "4. Remove Developer From Team\n" +
                    "5. Exit");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Console.WriteLine($"Please enter new name for {devTeam.TeamName}");
                        devTeam.TeamName = Console.ReadLine();
                        break;
                    case "2":
                        Console.WriteLine($"Please enter new ID for {devTeam.TeamName}");
                        devTeam.TeamID = int.Parse(Console.ReadLine());
                        break;
                    case "3":
                        AddDeveloperToTeam();
                        break;
                    case "4":
                        Console.WriteLine("Which developer do you want to remove? Enter their ID: ");
                        string result2 = Console.ReadLine();
                        int devID = Int32.Parse(result2);
                        Developer dev = _devRepo.GetOneDeveloper(devID);
                        _devTeamRepo.RemoveDeveloperFromTeam(devID, result);
                        break;
                    case "5":
                        keepLooping = false;
                        break;
                }
            }
        }
        private void DeleteAnExistingDeveloper()
        {
            Console.Clear();
            Console.WriteLine("Please enter the ID of the Developer you wish to delete.");
            string person = Console.ReadLine();
            int result = Int32.Parse(person);
            Developer developer = _devRepo.GetOneDeveloper(result);
            if (developer == null)
            {
                Console.WriteLine("There is no Developer with that ID. Please try again.");
            }
            else
            {
                bool keepLooping = true;
                while (keepLooping == true)
                {

                    DisplayDeveloperList(developer);
                    Console.WriteLine("Do you wish to delete this Developer? Please enter Yes or No");
                    string answer = Console.ReadLine();
                    if (answer == "Yes")
                    {
                        _devRepo.DeleteDeveloper(developer);
                        keepLooping = false;
                    }
                    else if (answer == "No")
                    {
                        keepLooping = false;
                    }
                    else
                    {
                        Console.WriteLine("That is not a valid response. Please try again.");
                        WaitForKey();
                    }
                }
            }
        }
        private void DeleteDevTeam()
        {
            Console.Clear();
            Console.WriteLine("Please enter the ID of the team you wish to delete.");
            string devTeamDelete = Console.ReadLine();
            int result = Int32.Parse(devTeamDelete);
            DevTeam devTeam = _devTeamRepo.GetDevTeamByID(result);
            if (devTeam == null)
            {
                Console.WriteLine("There is no team with that ID. Please try again.");
            }
            else
            {
                bool keepLooping = true;
                while (keepLooping == true)
                {

                    DisplayDevTeamList(devTeam);
                    Console.WriteLine("Do you wish to delete this team? Please enter Yes or No");
                    string answer = Console.ReadLine();
                    if (answer == "Yes")
                    {
                        _devTeamRepo.DeleteDevTeam(devTeam);
                        keepLooping = false;
                    }
                    else if (answer == "No")
                    {
                        keepLooping = false;
                    }
                    else
                    {
                        Console.WriteLine("That is not a valid response. Please try again.");
                        WaitForKey();
                    }
                }
            }
        }
        private void DisplayDeveloperList(Developer developer)
        {
            Console.WriteLine($"\n" +
                $"{developer.FullName} (Dev ID: {developer.ID})\n" +
                $"\n" +
                $"Has Pluralsight: {developer.HasPluralSight}\n" +
                $"\n" +
                $"===================");
        }
        private void DisplayDevTeamList(DevTeam devTeam)
        {
            {
                Console.WriteLine($"\n" +
                    $"{devTeam.TeamName} (Dev Team ID: {devTeam.TeamID})");
                foreach (var dev in devTeam.Developers)
                {
                    Console.WriteLine($"\n" +
                        $"{dev.FullName} (Developer ID: {dev.ID})\n");
                }
            }
        }
        private void WaitForKey()
        {
            Console.ReadKey();
        }

        private void Seed()
        {
            Developer developer = new Developer();
            developer.FirstName = "Landon";
            developer.LastName = "Shay";
            developer.HasPluralSight = false;
            _devRepo.AddDeveloperToDirectory(developer);

            Developer developer2 = new Developer();
            developer2.FirstName = "Fake";
            developer2.LastName = "Person";
            developer2.HasPluralSight = true;
            _devRepo.AddDeveloperToDirectory(developer2);

            DevTeam devTeamOne = new DevTeam();
            devTeamOne.TeamName = "Team One";
            _devTeamRepo.AddTeamToDirectory(devTeamOne);

            DevTeam devTeamTwo = new DevTeam();
            devTeamTwo.TeamName = "Team Two";
            _devTeamRepo.AddTeamToDirectory(devTeamTwo);
        }
    }
}
