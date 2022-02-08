import { OnDestroy } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms"
import { Observable, Subject } from "rxjs";
import { CustomFormValidators } from "src/app/shared/forms/form.validators";
import { RobotFormEnum } from "./robot-form.enum";
import { RobotFormData, RobotSequece } from "./robot-form.model";

export class RobotForm {
  form!: FormGroup
  sequences: RobotSequece[] = [];
  eventChange: Subject<RobotFormData> = new Subject();

  constructor(private formBuilder: FormBuilder, data: RobotFormData) {
    this.buildForm(data);
  }

  private buildForm(data: RobotFormData) {
    let defaultSequence: RobotSequece = {};
    if (data.sequences?.length > 0) {
      defaultSequence.instructions = data.sequences[0].instructions;
      defaultSequence.positions = data.sequences[0].positions;
    }

    this.form = this.formBuilder.group({
      [RobotFormEnum.GridXBoundary]: [data.gridXBoundary, [Validators.required, CustomFormValidators.inRange(0, 50)]],
      [RobotFormEnum.GridYBoundary]: [data.gridYBoundary, [Validators.required, CustomFormValidators.inRange(0, 50)]],
      [RobotFormEnum.Instructions]: [defaultSequence.instructions, Validators.required],
      [RobotFormEnum.Positions]: [defaultSequence.positions, Validators.required],
    });

    // valueChanges
    this.form.get(RobotFormEnum.GridXBoundary)?.valueChanges.subscribe(value => {
      this.eventChange.next({...this.form.value, sequences: [], gridXBoundary: value});
    });
    this.form.get(RobotFormEnum.GridYBoundary)?.valueChanges.subscribe(value => {
      this.eventChange.next({...this.form.value, sequences: [], gridYBoundary: value});
    });

  }

  addRobot() {
    if (this.form.get(RobotFormEnum.Instructions)?.valid && this.form.get(RobotFormEnum.Positions)?.valid) {
      const sequence: RobotSequece = {
        instructions: this.form.get(RobotFormEnum.Instructions)?.value,
        positions: this.form.get(RobotFormEnum.Positions)?.value,
      };
      this.sequences.push(sequence);
    }

    this.resetForm();
  }

  resetForm() {
    const data: RobotFormData = {
      gridXBoundary: this.form.get(RobotFormEnum.GridXBoundary)?.value,
      gridYBoundary: this.form.get(RobotFormEnum.GridYBoundary)?.value,
      sequences: []
    };
    this.buildForm(data);
  }

  onChanges(): Observable<RobotFormData> {
    return this.eventChange.asObservable();
  }
}
