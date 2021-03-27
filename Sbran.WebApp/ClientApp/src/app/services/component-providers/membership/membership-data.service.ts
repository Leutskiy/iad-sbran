import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Membership } from '../../../contracts/login-data';
import { APP_CONFIG, IAppConfig } from '../../../settings/app-config';

@Injectable()
export class MembershipDataService {

  private readonly options = { headers: new HttpHeaders().set('Content-Type', 'application/json') };

  private baseAddress: string;
  private readonly uriRelativePath: string = "api/v1";
  private readonly uriMembershipPath: string = `${this.uriRelativePath}/Membership`;

  constructor(private http: HttpClient, @Inject(APP_CONFIG) private config: IAppConfig) {
    this.baseAddress = `${config.icsApiEndpoint}`;
  }

  // получить все выезды сотрудника
  public get(employeeId: string): Observable<[]> {
    let url = `${this.baseAddress}${this.uriMembershipPath}/${employeeId}`;
    console.log(`get all memberships for ${employeeId} by url: ` + url);
    return this.http.get<[]>(url, this.options);
  }

  // создать нового выезда
  public add(membership: Membership): Observable<any> {
    let url = `${this.baseAddress}${this.uriMembershipPath}`;
    console.log("membership data for new instance: " + membership);
    return this.http.post<any>(url, membership, this.options);
  }

  // создать новое приглашение для конкретного сотрудника
  public update(membershipId: string, membership: Membership): Observable<any> {
    let url = `${this.baseAddress}${this.uriMembershipPath}/${membershipId}`;
    console.log(`update membership for ${membershipId} by url: ` + url);
    console.log("membership data for new instance: " + membership);

    return this.http.post<any>(url, membership, this.options);
  }

}
