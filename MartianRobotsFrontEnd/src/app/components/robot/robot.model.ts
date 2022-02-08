import { CommandEnum, RobotOrientationEnum } from "./robot.enum";

export interface Robot {
  positionX: number;
  positionY: number;
  orientation: RobotOrientationEnum;
  isLost: boolean;
  sequences: CommandEnum[];
}
