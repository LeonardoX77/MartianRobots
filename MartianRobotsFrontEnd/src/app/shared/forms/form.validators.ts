import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export class CustomFormValidators {
    public static inRange(low: number, upper: number): ValidatorFn {
    return (control: AbstractControl): ValidationErrors => {
        if (control.value !== null && control.value !== '') {
            if (control.value >= low && control.value <= upper) return null!;
            else
                return {
                    ['outOfRange']: { value: control.value, acceptedRange: '[' + low + ', ' + upper + ']' }
                };
        } else {
            return null!; // no error
        }
    };
  }
}
