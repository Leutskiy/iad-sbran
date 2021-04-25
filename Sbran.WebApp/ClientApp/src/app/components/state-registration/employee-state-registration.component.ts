import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { StateRegistration } from '../../contracts/login-data';
import { EmployeeDataService } from '../../services/component-providers/employee/employee-data.service';
import { StateRegistrationComponent } from './state-registration.component';

@Component({
  selector: 'app-employee-state-registration',
  templateUrl: './state-registration.component.html',
  styleUrls: ['./state-registration.component.scss'],
  providers: [EmployeeDataService]
})
export class EmployeeStateRegistrationComponent extends StateRegistrationComponent {

  private employeeId: string;

  constructor(private activatedRoute: ActivatedRoute, private employeeDataService: EmployeeDataService) {
    super();
    this.stateRegistration = new StateRegistration();
  }

  ngOnInit(): void {
    this.isNewForm = this.scope === "invitation";
    this.employeeId = this.activatedRoute.snapshot.paramMap.get('employeeId');
    console.log("current scope: " + this.scope);
  }

  protected CompleteSaveOperation(): void {
    this.employeeDataService.editEmployeeStateRegistrationById(this.employeeId, this.stateRegistration).subscribe(
      operationResult => {
        console.log(`operationResult: ` + operationResult);
      },
      operationError => {
        console.error(`operationError: ` + operationError);
      }
    );
  }


}
