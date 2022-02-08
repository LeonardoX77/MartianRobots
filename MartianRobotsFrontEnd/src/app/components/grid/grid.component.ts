import { Component, Input, OnChanges, OnInit, SimpleChanges, Type } from '@angular/core';
import { GridCell } from './grid-cell/grid-cell.model';
import { RobotOrientationEnum } from '../robot/robot.enum';
import { Robot } from '../robot/robot.model';
import { GridData } from './grid.component.model';

type GridRow = GridCell[];

@Component({
  selector: 'app-grid',
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.scss']
})
export class GridComponent implements OnInit, OnChanges {

  @Input() data!: GridData;
  grid!: GridRow[];

  robotOrientationEnum = RobotOrientationEnum;

  constructor() { }

  ngOnInit(): void {
    this.grid = this.buildGridRows();
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.grid = this.buildGridRows();
  }

  private buildGridRows(): GridRow[] {
    // NOTE: In order to make the grid rows and display cells in x-y axis from left to right and bottom to top we need to reverse the array.
    const grid: GridRow[] = [];
    if (this.data) {
      let gridRow: GridRow = [];

      let index = 0;
      // for (let x = 0; x <= this.data.xBound; x++) {
      //   for (let y = 0; y <= this.data.yBound; y++) {
      //     if (index > 0 && index % (this.data.xBound + 1) === 0) {
      //       grid.push(gridRow);
      //       gridRow = [];
      //     }
      //     gridRow.push({ cellIndex: index, x, y, displayCellNumber: this.data.displayCellNumbers });
      //     index++;
      //   }
      // }
      for (let y = 0; y <= this.data.yBound; y++) {
        for (let x = 0; x <= this.data.xBound; x++) {
          if (index > 0 && index % (this.data.xBound + 1) === 0) {
            grid.push(gridRow);
            gridRow = [];
          }
          gridRow.push({ cellIndex: index, x: x, y: y, displayCellNumber: this.data.displayCellNumbers });
          index++;
        }
      }

      if (gridRow.length > 0) {
        grid.push(gridRow);
      }

      return grid.reverse();
    } else {
      return grid;
    }
  }

  addRobot(robot: Robot, x: number, y: number): void {
    const row = this.grid.find(row => row.find(cell => cell.x === x && cell.y === y));
    if (row) {
      const cell = row.find(cell => cell.x === x && cell.y === y);
      if (cell) {
        cell.robot = {...robot, orientation: this.convertRobotOrientation(robot.orientation)};
      }
    }
  }
  convertRobotOrientation(orientation: any): RobotOrientationEnum {
    var res: RobotOrientationEnum = RobotOrientationEnum.North;

    switch (orientation) {
      case 0:
        return RobotOrientationEnum.North;
      case 1:
        return RobotOrientationEnum.East;
      case 2:
        return RobotOrientationEnum.South;
      case 3:
        return RobotOrientationEnum.West;
    }

    return res;
  }
}
