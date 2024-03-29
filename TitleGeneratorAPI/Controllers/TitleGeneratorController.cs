﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TG = MissilePuppy.TitleGenerator;

namespace MissilePuppy.TitleGeneratorAPI.Controllers
{
    /// <summary>
    /// Title Generator Controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")] //makes the outputs JSON. I would just like it to default to JSON but be overwriteable by the client.
    //will have to figure that out later
    public class TitleGeneratorController : ControllerBase
    {
        //Instance of the TitleGenerator Class
        private static TG.TitleGenerator generator = new TG.TitleGenerator();
        //Instance of the logging class
        private readonly ILogger<TitleGeneratorController> _logger;
        //Maximum number of returns we want end users to be able to request
        private readonly int MaxReturn = 100;

        /// <summary>
        /// creates an instance of the TitleGenerator controller
        /// </summary>
        /// <param name="logger"></param>
        public TitleGeneratorController(ILogger<TitleGeneratorController> logger)
        {
            _logger = logger;
        }

        //Get a Title
        /// <summary>
        /// Get a randomly generated Title
        /// </summary>
        /// <returns></returns>
        [HttpGet] // "/TitleGenerator"
        public ActionResult Get()
        {
            return Ok(generator.GenerateTitle());
        }

        /// <summary>
        /// Get {num} number of randomly generated Titles
        /// </summary>
        /// <param name="num" example="5"></param>
        /// <returns></returns>
        [HttpGet("{num}")] // "/TitleGenerator/3"
        public ActionResult Get(int? num)
        {
            if (num > MaxReturn) { num = MaxReturn; }

            List<string> ls = new List<string>();
            for (int i = 0; i < num; i++)
            {
                ls.Add(generator.GenerateTitle());
            }
            return Ok(ls.ToArray());
        }

        /// <summary>
        /// Get a random "Strike Force" Title
        /// </summary>
        /// <returns></returns>
        [HttpGet("strikeforce")] // "/TitleGenerator/strikeforce"
        public ActionResult GetStrikeForce()
        {
            return Ok(generator.GenerateStrikeForce());
        }

        /// <summary>
        /// get {num} number of random "Strike Force" titles
        /// </summary>
        /// <param name="num" example="5"></param>
        /// <returns></returns>
        [HttpGet("strikeforce/{num}")] // "/TitleGenerator/strikeforce/3"
        public ActionResult GetStrikeForce(int? num)
        {
            if (num > MaxReturn) { num = MaxReturn; }

            List<string> ls = new List<string>();
            for (int i = 0; i < num; i++)
            {
                ls.Add(generator.GenerateStrikeForce());
            }
            return Ok(ls.ToArray());
        }

        /// <summary>
        /// Get a title by input template
        /// </summary>
        /// <param name="template" example="Noun is just a bad version of Adjective Noun"></param>
        /// <returns></returns>
        [HttpGet("ByTemplate/{template}")] // "/TitleGenerator/bytemplate/Adjective Noun Verb"
        public ActionResult GetByTemplate(string template)
        {
            return Ok(generator.GenerateTitleFromTemplate(template));
        }

        /// <summary>
        /// Get {num} number of titles by input template
        /// </summary>
        /// <param name="template" example="I love Adjective Noun"></param>
        /// <param name="num" example="5"></param>
        /// <returns></returns>
        [HttpGet("ByTemplate/{template}/{num}")] // "/TitleGenerator/bytemplate/Adjective Noun Verb/3"
        public ActionResult GetByTemplate(string template, int? num)
        {
            if (num > MaxReturn) { num = MaxReturn; }

            List<string> ls = new List<string>();
            for (int i = 0; i < num; i++)
            {
                ls.Add(generator.GenerateTitleFromTemplate(template));
            }
            return Ok(ls.ToArray());
        }
    }
}
