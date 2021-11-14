using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using MonsterAdvancer.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace MonsterAdvancer.Controllers
{
    public class AdvancerController : Controller
    {
        private readonly ILogger<AdvancerController> _logger;
        private Dictionary<string, JObject> _monsterDictionary;

        public AdvancerController(ILogger<AdvancerController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _monsterDictionary = GetMonsterList();
            ViewData["MonsterList"] = CreateDropDownList(_monsterDictionary);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        private Dictionary<string, JObject> GetMonsterList()
        {
            using (StreamReader file = System.IO.File.OpenText(@"convertjson.json"))
            {
                return JsonConvert.DeserializeObject<Dictionary<string, JObject>>(file.ReadToEnd(), new MonsterArrayConverter());
            }
        }

        private List<SelectListItem> CreateDropDownList(Dictionary<string, JObject> monsters)
        {
            return monsters.Keys.Select(n => new SelectListItem
            {
                Value = n.ToString(),
                Text = n.ToString()
            }).ToList();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
