import { Component, Input, OnInit } from '@angular/core';
import { RobotOrientationEnum } from '../../robot/robot.enum';
import { GridCell } from './grid-cell.model';

@Component({
  selector: 'app-grid-cell',
  templateUrl: './grid-cell.component.html',
  styleUrls: ['./grid-cell.component.scss']
})
export class GridCellComponent implements OnInit {

  robotOrientationEnum = RobotOrientationEnum;
  @Input() cell!: GridCell;

  constructor() { }

  ngOnInit(): void {
  }

}
