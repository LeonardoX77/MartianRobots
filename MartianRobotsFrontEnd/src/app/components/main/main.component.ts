import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Subscription } from 'rxjs';
import { RobotService } from 'src/app/services/robot.service';
import { GridComponent } from '../grid/grid.component';
import { GridData } from '../grid/grid.component.model';
import { defaultRobotFormData } from '../robot-form/robot-form.component';
import { RobotFormData, RobotResponse } from '../robot-form/robot-form.model';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit, OnDestroy {
  @ViewChild('grid') private gridComponent!: GridComponent;

  gridData!: GridData;
  loading!: boolean;
  error!: boolean;
  finished!: boolean;
  outputResult!: string[];

  private subscriptions: Subscription = new Subscription();
  constructor(private robotService: RobotService) { }

  ngOnInit(): void {
    this.gridData = {
      xBound: defaultRobotFormData.gridXBoundary,
      yBound: defaultRobotFormData.gridYBoundary,
      displayCellNumbers: true
    };
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  setGridData(robotFormData: RobotFormData, displayCellNumbers: boolean = false, submitForm: boolean = false): void {
    this.gridData = {
      xBound: +robotFormData.gridXBoundary,
      yBound: +robotFormData.gridYBoundary,
      displayCellNumbers
    };

    if (submitForm) {
      this.submit(robotFormData);
    }
  }

  submit(robotFormData: RobotFormData): void {
    this.loading = true;

    this.subscriptions.add(
      this.robotService.getRobotFinalGridPosition(robotFormData).subscribe({
        next: (result) => this.processResult(result),
        error: (error) => {this.loading = false; this.error = true}
      })
    );
  }

  processResult(result: RobotResponse): void {
    this.loading = false;
    this.finished = true;
    this.outputResult = result.outputResult

    result.robots.forEach(robot => {
      this.gridComponent.addRobot(robot, robot.positionX, robot.positionY);
    });

  }
}
