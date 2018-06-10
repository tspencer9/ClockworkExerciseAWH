using System;
using Microsoft.AspNetCore.Mvc;
using Clockwork.API.Models;
using System.Collections.Generic;

namespace Clockwork.API.Controllers
{
    [Route("api/[controller]")]
    public class CurrentTimeController : Controller
    {
        // GET api/currenttime
        [HttpGet]
        public IActionResult Get()
        {
            var utcTime = DateTime.UtcNow;
            var serverTime = DateTime.Now;
            var ip = this.HttpContext.Connection.RemoteIpAddress.ToString();

            var returnVal = new CurrentTimeQuery
            {
                UTCTime = utcTime,
                ClientIp = ip,
                Time = serverTime
            };

            using (var db = new ClockworkContext())
            {
                db.CurrentTimeQueries.Add(returnVal);
                var count = db.SaveChanges();
                Console.WriteLine("{0} records saved to database", count);

                Console.WriteLine();
                foreach (var CurrentTimeQuery in db.CurrentTimeQueries)
                {
                    Console.WriteLine(" - {0}", CurrentTimeQuery.UTCTime);
                }
            }

            return Ok(returnVal);
        }
    }

    [Route("api/[controller]")]
    public class AllTimesController : Controller
    {
        // GET api/alltimes
        [HttpGet]
        public IActionResult Get()
        {
            List<CurrentTimeQuery> times = new List<CurrentTimeQuery>();

            using (var db = new ClockworkContext())
            {
                foreach (var record in db.CurrentTimeQueries)
                {
                    times.Add(
                        new CurrentTimeQuery
                        {
                            CurrentTimeQueryId = record.CurrentTimeQueryId,
                            ClientIp = record.ClientIp,
                            Time = record.Time,
                            UTCTime = record.UTCTime
                        });
                }
            }

            // times.Reverse();

            return Ok(times);
        }
    }
}
