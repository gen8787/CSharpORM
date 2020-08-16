using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsORM.Models;

namespace SportsORM.Controllers
{
    public class HomeController : Controller
    {

        private static Context _context;

        public HomeController(Context DBContext)
        {
            _context = DBContext;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.BaseballLeagues = _context.Leagues
                .Where(l => l.Sport.Contains("Baseball"))
                .ToList();
            return View();
        }

        [HttpGet("level_1")]
        public IActionResult Level1()
        {
//1...all womens' leagues
            ViewBag.WomenLeagues = _context.Leagues
                .Where(l => l.Name.Contains("Women"))
                .ToList();
//2...all leagues where sport is any type of hockey
            ViewBag.AllHockey = _context.Leagues
                .Where(l => l.Sport.Contains("Hockey"))
                .ToList();
//3...all leagues where sport is something OTHER THAN football
            ViewBag.NoFootball = _context.Leagues
                .Where(l => l.Sport != "Football")
                .ToList();
//4...all leagues that call themselves "conferences"
            ViewBag.AllConf = _context.Leagues
                .Where(l => l.Name.Contains("Conference"))
                .ToList();
//5...all leagues in the Atlantic region
            ViewBag.AllAtlantic = _context.Leagues
                .Where(l => l.Name.Contains("Atlantic"))
                .ToList();
//6...all teams based in Dallas
            ViewBag.AllDallas = _context.Teams
                .Where(l => l.Location == ("Dallas"))
                .ToList();
//7...all teams named the Raptors
            ViewBag.AllRaptors = _context.Teams
                .Where(l => l.TeamName == ("Raptors"))
                .ToList();
//8...all teams whose location includes "City"
            ViewBag.AllCity = _context.Teams
                .Where(l => l.Location.Contains("City"));
                // .ToList();
//9...all teams whose names begin with "T"
            ViewBag.AllT = _context.Teams
                .Where(l => l.TeamName.StartsWith("T"));
//10...all teams, ordered alphabetically by location
            IEnumerable<Team> AllTeamsByLoc = _context.Teams.OrderBy(n => n.Location);
            ViewBag.AllTeamsByLoc = AllTeamsByLoc;
//11...all teams, ordered by team name in reverse alphabetical order
            IEnumerable<Team> AllTeamsRev = _context.Teams.OrderByDescending(n => n.TeamName);
            ViewBag.AllTeamsRev = AllTeamsRev;
//12...every player with last name "Cooper"
            IEnumerable<Player> AllCooper = _context.Players
            .OrderBy(fn => fn.FirstName)
            .Where(pn => pn.LastName.Contains("Cooper"));
            ViewBag.AllCooper = AllCooper;
//13...every player with first name "Joshua"
            IEnumerable<Player> AllJoshua = _context.Players
            .OrderBy(fn => fn.LastName)
            .Where(pn => pn.FirstName.Contains("Joshua"));
            ViewBag.AllJoshua = AllJoshua;
//14...every player with last name "Cooper" EXCEPT those with "Joshua" as the first name
            IEnumerable<Player> AllCoopNoJosh = _context.Players
            .Where(pn => pn.LastName.Contains("Cooper") && pn.FirstName != "Joshua");
            ViewBag.AllCoopNoJosh = AllCoopNoJosh;
//15...all players with first name "Alexander" OR first name "Wyatt"
            IEnumerable<Player> AllWyAl = _context.Players
            .Where(pn => pn.FirstName.Contains("Alexander") || pn.FirstName.Contains("Wyatt"))
            .OrderBy(fn => fn.FirstName)
            .ThenBy(fn => fn.LastName);
            ViewBag.AllWyAl = AllWyAl;
            return View("Level1");
        }

        [HttpGet("level_2")]
        public IActionResult Level2()
        {
//1...all teams in the Atlantic Soccer Conference
            List<League> teamsInAtSocConf = _context.Leagues
            .Include(team => team.Teams)
            .Where(x => x.Name.Contains("Atlantic Soccor Conference"))
            .ToList();
            ViewBag.teamsInAtSocConf = teamsInAtSocConf;
//2...all (current) players on the Boston Penguins (Hint: Boston is the Location, Penguins is the TeamName)

//3...all (current) players in the International Collegiate Baseball Conference

//4...all (current) players in the American Conference of Amateur Football with last name "Lopez"

//5...all football players

//6...all teams with a (current) player named "Sophia"

//7...all leagues with a (current) player named "Sophia"

//8...everyone with the last name "Flores" who DOESN'T (currently) play for the Washington Roughriders




            return View("Level2");
        }

        [HttpGet("level_3")]
        public IActionResult Level3()
        {
            return View();
        }

    }
}