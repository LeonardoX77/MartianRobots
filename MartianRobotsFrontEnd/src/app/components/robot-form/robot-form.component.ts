import { Component, EventEmitter, OnDestroy, OnInit, Output } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Subscription } from 'rxjs';
import { RobotFormEnum } from './robot-form.enum';
import { RobotForm } from './robot-form.form';
import { RobotFormData, RobotResponse } from './robot-form.model';

export const defaultRobotFormData: RobotFormData = {
  gridXBoundary: 5,
  gridYBoundary: 3,
  sequences: [{
    positions: '1 1 E',
    instructions: 'RFRFRFRF'
  }]
};


@Component({
  selector: 'app-robot-form',
  templateUrl: './robot-form.component.html',
  styleUrls: ['./robot-form.component.scss']
})
export class RobotFormComponent implements OnInit, OnDestroy {
  @Output() onSubmit: EventEmitter<RobotFormData> = new EventEmitter<RobotFormData>();
  @Output() onChange: EventEmitter<RobotFormData> = new EventEmitter<RobotFormData>();
  private subscriptions: Subscription = new Subscription();

  robotFormEnum = RobotFormEnum;
  robotForm!: RobotForm;
  result!: RobotResponse;
  error!: boolean;

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.robotForm = new RobotForm(this.formBuilder, defaultRobotFormData);
    this.subscriptions.add(
      this.robotForm.onChanges().subscribe(data => {
        this.onChange.emit(data);
      })
    );
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  submit(): void {
    const data: RobotFormData = {
      ...this.robotForm.form.value,
      sequences: this.robotForm.sequences,
      xxxx: 'xxxx'
    };
    this.onSubmit.emit(data);
    this.robotForm.resetForm();
  }

  addRobot(): void {
    this.robotForm.addRobot();
  }
}
