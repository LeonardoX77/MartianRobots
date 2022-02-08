import { Component, Input, OnInit } from '@angular/core';
import { RobotOrientationEnum } from './robot.enum';

@Component({
  selector: 'app-robot',
  templateUrl: './robot.component.html',
  styleUrls: ['./robot.component.scss']
})
export class RobotComponent implements OnInit {

  @Input() orientation!: RobotOrientationEnum;

  constructor() { }

  ngOnInit(): void {
  }

}
