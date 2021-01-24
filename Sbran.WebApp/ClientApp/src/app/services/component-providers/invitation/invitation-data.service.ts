import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Contact, Job, NewInvitationDto, Organization, Passport, StateRegistration, VisitDetail } from '../../../contracts/login-data';
import { APP_CONFIG, IAppConfig } from '../../../settings/app-config';

@Injectable()
export class InvitationDataService  {

  private readonly options = { headers: new HttpHeaders().set('Content-Type', 'application/json') };

  private baseAddress: string;
  private readonly uriRelativePath: string = "api/v1";
  private readonly uriInvitationPath: string = `${this.uriRelativePath}/invitation/`;
  private readonly uriEmployeePath: string = `${this.uriRelativePath}/employee/`;

  constructor(private http: HttpClient, @Inject(APP_CONFIG) private config: IAppConfig) {
    this.baseAddress = `${config.icsApiEndpoint}`;
  }

  // отредактировать данные формы деталей визита приглашения
  public editVisitDetailsByInvitationId(invitationId: string, visitDetails: VisitDetail): Observable<any> {
    let url = `${this.baseAddress}${this.uriInvitationPath}${invitationId}/visitdetails`;
    console.log(`edit visit details for invitation ${invitationId} by url: ` + url);
    console.log("visit details: " + visitDetails);

    return this.http.put<any>(url, visitDetails, this.options);
  }

  // получить все приглашения конкретного сотрудника
  public getInvitationsByEmployeeId(employeeId: string): Observable<[]> {
    let url = `${this.baseAddress}${this.uriEmployeePath}${employeeId}/invitations`;
    console.log(`get invitations for ${employeeId} by url: ` + url);

    return this.http.get<[]>(url, this.options);
  }

  // создать новое приглашение для конкретного сотрудника
  public newInvitationForEmployee(employeeId: string, employeeInvitation: NewInvitationDto): Observable<any> {
    let url = `${this.baseAddress}${this.uriEmployeePath}${employeeId}/invitation`;
    console.log(`create invitation for ${employeeId} by url: ` + url);
    console.log("invitation data for new instance: " + employeeInvitation);

    return this.http.post<any>(url, employeeInvitation, this.options);
  }

  // получить данные для формы приглашения
  public getInvitationById(invitationId: string): Observable<any> {
    let url = `${this.baseAddress}${this.uriInvitationPath}${invitationId}`;
    console.log(`get invitation for ${invitationId} by url: ` + url);

    return this.http.get<any>(url, this.options);
  }

  //# редактирование различных форм с данными у приглашения #//

  // отредактировать данные формы паспорта приглашаемого иностранца
  public editAlienPassportByInvitationId(invitationId: string, passport: Passport): Observable<any>  {
    let url = `${this.baseAddress}${this.uriInvitationPath}${invitationId}/alien/passport`;
    console.log(`edit alien passport for invitation ${invitationId} by url: ` + url);
    console.log("alien passport: " + passport);

    return this.http.put<any>(url, passport, this.options);
  }

  // отредактировать данные формы контактов приглашаемого иностранца
  public editAlienContactByInvitationId(invitationId: string, contact: Contact): Observable<any> {
    let url = `${this.baseAddress}${this.uriInvitationPath}${invitationId}/alien/contact`;
    console.log(`edit alien contact for invitation ${invitationId} by url: ` + url);
    console.log("alien contact: " + contact);

    return this.http.put<any>(url, contact, this.options);
  }

  // отредактировать данные формы организации приглашаемого иностранца
  public editAlienOrganizationByInvitationId(invitationId: string, organization: Organization): Observable<any> {
    let url = `${this.baseAddress}${this.uriInvitationPath}${invitationId}/alien/organization`;
    console.log(`edit alien organization for invitation ${invitationId} by url: ` + url);
    console.log("alien organization: " + organization);

    return this.http.put<any>(url, organization, this.options);
  }

  // отредактировать данные формы государственной регистрации приглашаемого иностранца
  public editAlienStateRegistrationByInvitationId(invitationId: string, stateRegistration: StateRegistration): Observable<any> {
    let url = `${this.baseAddress}${this.uriInvitationPath}${invitationId}/alien/stateregistration`;
    console.log(`edit alien state registration for invitation ${invitationId} by url: ` + url);
    console.log("alien state registration: " + stateRegistration);

    return this.http.put<any>(url, stateRegistration, this.options);
  }

  // отредактировать данные формы государственной регистрации приглашаемого иностранца
  public editAlienJobByInvitationId(invitationId: string, stateRegistration: Job): Observable<any> {
    let url = `${this.baseAddress}${this.uriInvitationPath}${invitationId}/alien/job`;
    console.log(`edit alien job for invitation ${invitationId} by url: ` + url);
    console.log("alien job: " + stateRegistration);

    return this.http.put<any>(url, stateRegistration, this.options);
  }
}
