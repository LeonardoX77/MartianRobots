using MartialRobotWebApi.Model;
using MartianRobots;
using Microsoft.AspNetCore.Mvc;

namespace MartialRobotWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MartianRobotController : ControllerBase
    {
        private readonly ILogger<MartianRobotController> _logger;

        public MartianRobotController(ILogger<MartianRobotController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetRobotFinalGridPosition")]
        public MartialRobotsDTO Get([FromQuery]string[] sequence)
        {
            /*
             NOTE: If we follow the REST principles in designing our APIs, we will have automatically idempotent REST APIs for GET, PUT, 
                   DELETE, HEAD, OPTIONS, and TRACE methods. Only POST APIs will not be idempotent.
                   When we invoke the same POST request N times, we will have N new resources on the server. So, POST is not idempotent.
                   That's why following best practices I decided to create a GET method instead. 
             */

            _logger.LogInformation($"Incoming request: ");
            var robots = Input.GetRobots(sequence);
            var commandStation = new CommandStation(robots);
            for (int i = 0; i < robots.Count; i++)
            {
                commandStation.ExecuteCommandSequence(i);
            }
            var robotReport = Output.GetRobotOutput(robots);

            return new MartialRobotsDTO() { 
                OutputResult = robotReport, 
                Robots = robots.Select(r => new MartialRobotDTO {
                    PositionX = r.PositionX,
                    PositionY = r.PositionY,
                    Orientation = (int) r.Orientation,
                    IsLost = r.IsLost
                })
                .ToArray() };

        }
    }
}