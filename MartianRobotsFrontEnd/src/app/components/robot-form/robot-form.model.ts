import { Robot } from "../robot/robot.model";

export interface RobotSequece {
  positions?: string;
  instructions?: string;
}

export interface RobotFormData {
  gridXBoundary: number;
  gridYBoundary: number;
  sequences: RobotSequece[];
}

export interface RobotResponse {
  outputResult: string[];
  robots: Robot[]
}
