import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Job } from '../../contracts/login-data';
import { EmployeeDataService } from '../../services/component-providers/employee/employee-data.service';
import { JobDataServiceFactory } from '../../services/component-providers/job/job-data-factory.service';
import { JobComponent } from './job.component';

@Component({
  selector: 'app-employee-job',
  templateUrl: './job.component.html',
  styleUrls: ['./job.component.scss'],
  providers: [JobDataServiceFactory]
})
export class EmployeeJobComponent extends JobComponent {

  private employeeId: string;

  constructor(private activatedRoute: ActivatedRoute, private employeeDataService: EmployeeDataService) {
    super();
    this.job = new Job();
  }

  ngOnInit(): void {
    this.isNewForm = this.scope === "invitation";
    this.employeeId = this.activatedRoute.snapshot.paramMap.get('employeeId');
    console.log("current scope: " + this.scope);
  }

  // TODO: переделать метод сохранения джоба по идентификатору для сотрудника
  protected CompleteSaveOperation(): void {
    this.employeeDataService.editEmployeeJobById(this.employeeId, this.job).subscribe(
      operationResult => {
        console.log(`operationResult: ` + operationResult);
      },
      operationError => {
        console.error(`operationError: ` + operationError);
      }
    );
  }
}
