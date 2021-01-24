import { Component, OnInit, Input } from '@angular/core';
import { Profile, Employee } from '../../contracts/login-data';
import { EmployeeDataService } from '../../services/component-providers/employee/employee-data.service';
import { ProfileDataService } from '../../services/component-providers/profile/profile-data.service';

@Component({
  selector: 'app-person-details',
  templateUrl: './person-details.component.html',
  styleUrls: ['./person-details.component.scss'],
  providers: [ProfileDataService, EmployeeDataService]
})
export class PersonDetailsComponent implements OnInit {

  @Input() scope: string;
  @Input() title: string;
  @Input() profile: Profile;
  @Input() employee: Employee;

  editable: boolean = false;
  viewMode: boolean = false;
  isNewForm: boolean = false;

  constructor(
    private profileDataService: ProfileDataService,
    private employeeDataService: EmployeeDataService) {
      this.profile = new Profile();
      this.employee = new Employee();
  }

  ngOnInit(): void {
    this.isNewForm = this.scope === "invitation";
  }

  public viewForm() {
    this.viewMode = !this.viewMode;

    if (!this.viewMode) {
      this.reset();
    }
  }

  public editForm() {
    this.editable = !this.editable;
  }

  public saveForm() {
    this.editable = !this.editable;

    this.profileDataService.setDataById(this.profile, this.profile.id).subscribe(
      response => {
        console.log(response);
      },
      error => {
        console.log(error);
      }
    );

    this.employeeDataService.editEmployeeScientificInfoById(this.employee.id, this.employee.scientificInfo).subscribe(
      operationResult => {
        console.log("operationResult: " + operationResult);
      },
      operationError => {
        console.log("operationError: " + operationError);
      }
    );
  }

  private reset() {
    this.ngOnInit();
  }
}
