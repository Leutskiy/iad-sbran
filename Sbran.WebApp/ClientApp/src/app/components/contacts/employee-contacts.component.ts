import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Contact } from '../../contracts/login-data';
import { EmployeeDataService } from '../../services/component-providers/employee/employee-data.service';
import { ContactsComponent } from './contacts.component';

@Component({
  selector: 'app-employee-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.scss'],
  providers: [EmployeeDataService]
})
export class EmployeeContactsComponent extends ContactsComponent {

  private employeeId: string;

  constructor(private activatedRoute: ActivatedRoute, private employeeDataService: EmployeeDataService) {
    super();
    this.contact = new Contact();
  }

  ngOnInit(): void {
    this.isNewForm = this.scope === "invitation";
    this.employeeId = this.activatedRoute.snapshot.paramMap.get('employeeId');
    console.log("current scope: " + this.scope);
  }

  // TODO: переделать метод сохранения джоба по идентификатору для сотрудника
  protected CompleteSaveOperation(): void {
    this.employeeDataService.editEmployeeContactById(this.employeeId, this.contact).subscribe(
      operationResult => {
        console.log(`operationResult: ` + operationResult);
      },
      operationError => {
        console.error(`operationError: ` + operationError);
      }
    );

  }

}
