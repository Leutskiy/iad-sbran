import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Organization } from '../../contracts/login-data';
import { EmployeeDataService } from '../../services/component-providers/employee/employee-data.service';
import { OrganizationDataService } from '../../services/component-providers/organization/organization-data.service';
import { OrganizationComponent } from './organization.component';

@Component({
  selector: 'app-employee-organization',
  templateUrl: './organization.component.html',
  styleUrls: ['./organization.component.scss'],
  providers: [OrganizationDataService]
})
export class EmployeeOrganizationComponent extends OrganizationComponent {

  private employeeId: string;

  constructor(
    private activatedRoute: ActivatedRoute,
    private employeeDataService: EmployeeDataService) {
    super();
    this.organization = new Organization();
  }

  ngOnInit(): void {
    this.isNewForm = this.scope === "invitation";
    this.employeeId = this.activatedRoute.snapshot.paramMap.get('employeeId');
    console.log(this.isNewForm);
    console.log(this.scope);
  }

  protected CompleteSaveOperation(): void {
    this.employeeDataService.editEmployeeOrganizationById(this.employeeId, this.organization).subscribe(
      operationResult => {
        console.log(`operationResult: ` + operationResult);
      },
      operationError => {
        console.error(`operationError: ` + operationError);
      }
    );
  }
}
