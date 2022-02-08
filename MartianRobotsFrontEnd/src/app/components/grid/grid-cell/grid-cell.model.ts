import { Robot } from "../../robot/robot.model";

export interface GridCell {
  robot?: Robot;
  cellIndex?: number;
  x?: number;
  y?: number;
  displayCellNumber?: boolean;
}
