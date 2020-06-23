using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TG = MissilePuppy.TitleGenerator;

namespace MissilePuppy.TitleGeneratorAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")] //makes the outputs JSON. I would just like it to default to JSON but be overwriteable by the client.
    //will have to figure that out later
    public class TitleGeneratorController : ControllerBase
    {
        private static TG.TitleGenerator generator = new TG.TitleGenerator();
        private readonly ILogger<TitleGeneratorController> _logger;

        public TitleGeneratorController(ILogger<TitleGeneratorController> logger)
        {
            _logger = logger;
        }
        //Get a Strike Force name
        [HttpGet] // "/TitleGenerator"
        public ActionResult Get()
        {
            return Ok(generator.GenerateStrikeForce());
        }
        //Get multiple Strike Force names
        [HttpGet("{id}")] // "/TitleGenerator/3"
        public ActionResult Get(int? id)
        {
            List<string> ls = new List<string>();
            for (int i = 0; i < id; i++)
            {
                ls.Add(generator.GenerateStrikeForce());
            }
            return Ok(ls.ToArray());
        }
        //Get a traditional media title
        [HttpGet("title")] // "/TitleGenerator/title"
        public ActionResult GetTitle()
        {
            return Ok(generator.GenerateTitle());
        }
        //Get multiple title names
        [HttpGet("title/{id}")] // "/TitleGenerator/title/3"
        public ActionResult GetTitle(int? id)
        {
            List<string> ls = new List<string>();
            for (int i = 0; i < id; i++)
            {
                ls.Add(generator.GenerateTitle());
            }
            return Ok(ls.ToArray());
        }

        //Get a title by input template
        [HttpGet("ByTemplate/{template}")] // "/TitleGenerator/bytemplate"
        public ActionResult GetByTemplate(string template)
        {
            return Ok(generator.GenerateTitleFromTemplate(template));
        }
        //Get multiple titles by input template
        [HttpGet("ByTemplate/{template}/{id}")] // "/TitleGenerator/bytemplate"
        public ActionResult GetByTemplate(string template, int? id)
        {
            List<string> ls = new List<string>();
            for (int i = 0; i < id; i++)
            {
                ls.Add(generator.GenerateTitleFromTemplate(template));
            }
            return Ok(ls.ToArray());
        }
    }
}
