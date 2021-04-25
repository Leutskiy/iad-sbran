import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Passport } from '../../contracts/login-data';
import { EmployeeDataService } from '../../services/component-providers/employee/employee-data.service';
import { PassportComponent } from './passport.component';

// для профиля сотрудника
@Component({
  selector: 'app-employee-passport',
  templateUrl: './passport.component.html',
  styleUrls: ['./passport.component.scss'],
  providers: [EmployeeDataService]
})
export class EmployeePassportComponent extends PassportComponent {

  private employeeId: string;

  constructor(
      private activatedRoute: ActivatedRoute,
      private employeeDataService: EmployeeDataService) {
    super();
    this.passport = new Passport();
  }

  ngOnInit(): void {
    this.isNew = this.scope === "invitation";
    this.employeeId = this.activatedRoute.snapshot.paramMap.get('employeeId');
    console.log(this.scope);
  }

  // TODO: переделать метод сохранения паспорта по идентификатору для сотрудника
  protected CompleteSaveOperation(): void {
    this.employeeDataService.editEmployeePassportById(this.employeeId, this.passport).subscribe(
      operationResult => {
        console.log(`operationResult: ` + operationResult);
      },
      operationError => {
        console.error(`operationError: ` + operationError);
      }
    );
  }
}
