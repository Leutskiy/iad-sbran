import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { APP_CONFIG, IAppConfig } from '../../../settings/app-config';
import { Contact, Employee, Job, Organization, Passport, ScientificInfo, StateRegistration } from '../../../contracts/login-data';
import { Observable } from 'rxjs';

@Injectable()
export class EmployeeDataService {

  private readonly options = { headers: new HttpHeaders().set('Content-Type', 'application/json') };

  private readonly employeeSegmentPath: string = "api/v1/employee";
  private urlEmployeePath: string;

  constructor(
    private http: HttpClient,
    @Inject(APP_CONFIG) private config: IAppConfig) {
    this.urlEmployeePath = `${config.icsApiEndpoint}${this.employeeSegmentPath}`;
  }

  public getData(): Observable<any> {
    return this.http.get<any>(this.urlEmployeePath);
  }

  public getDataById(id: string): Observable<any> {
    return this.http.get<any>(`${this.urlEmployeePath}/${id}`);
  }

  // TODO: доделать возможность полного обновления за один запрос
  public editEmployee(employee: Employee): Observable<any> {
    let url = `${this.urlEmployeePath}/${employee.id}`;

    return this.http.put<any>(url, employee, this.options);
  }

  public editEmployeeJobById(employeeId: string, job: Job): Observable<any> {
    let url = `${this.urlEmployeePath}/${employeeId}/job`;
    console.log(`update job for ${employeeId} by url: ${url}`)

    return this.http.put<any>(url, job, this.options);
  }

  public editEmployeeContactById(employeeId: string, contact: Contact): Observable<any> {
    let url = `${this.urlEmployeePath}/${employeeId}/contact`;
    console.log(`update contact for ${employeeId} by url: ${url}`)

    return this.http.put<any>(url, contact, this.options);
  }

  public editEmployeePassportById(employeeId: string, passport: Passport): Observable<any> {
    let url = `${this.urlEmployeePath}/${employeeId}/passport`;
    console.log(`update passport for ${employeeId} by url: ${url}`)

    return this.http.put<any>(url, passport, this.options);
  }

  public editEmployeeOrganizationById(employeeId: string, organization: Organization): Observable<any> {
    let url = `${this.urlEmployeePath}/${employeeId}/organization`;
    console.log(`update organization for ${employeeId} by url: ${url}`)

    return this.http.put<any>(url, organization, this.options);
  }

  public editEmployeeScientificInfoById(employeeId: string, scientificInfo: ScientificInfo): Observable<any> {
    let url = `${this.urlEmployeePath}/${employeeId}/scientificinfo`;
    console.log(`update scientific info for ${employeeId} by url: ${url}`)

    return this.http.put<any>(url, scientificInfo, this.options);
  }

  public editEmployeeStateRegistrationById(employeeId: string, stateRegistration: StateRegistration): Observable<any> {
    let url = `${this.urlEmployeePath}/${employeeId}/stateregistration`;
    console.log(`update state registration for ${employeeId} by url: ${url}`)

    return this.http.put<any>(url, stateRegistration, this.options);
  }
}
