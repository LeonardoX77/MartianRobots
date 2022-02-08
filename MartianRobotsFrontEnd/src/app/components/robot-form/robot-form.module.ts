import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RobotFormComponent } from './robot-form.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [ RobotFormComponent ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ],
  exports: [ RobotFormComponent ]
})
export class RobotFormModule { }
