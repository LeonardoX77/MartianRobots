namespace MartialRobotWebApi.Model
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class MartialRobotsDTO
    {
        public string[] OutputResult { get; set; }
        public MartialRobotDTO[] Robots { get; set; }

    }

    public class MartialRobotDTO
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Orientation { get; set; }
        public bool IsLost { get; set; }

    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}