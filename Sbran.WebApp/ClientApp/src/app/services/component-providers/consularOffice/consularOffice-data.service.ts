import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ConsularOffice } from '../../../contracts/login-data';
import { APP_CONFIG, IAppConfig } from '../../../settings/app-config';

@Injectable()
export class ConsularOfficeDataService {

  private readonly options = { headers: new HttpHeaders().set('Content-Type', 'application/json') };

  private baseAddress: string;
  private readonly uriRelativePath: string = "api/v1";
  private readonly uriConsularOfficePath: string = `${this.uriRelativePath}/ConsularOffice`;

  constructor(private http: HttpClient, @Inject(APP_CONFIG) private config: IAppConfig) {
    this.baseAddress = `${config.icsApiEndpoint}`;
  }

  // получить все выезды сотрудника
  public get(employeeId: string): Observable<[]> {
    let url = `${this.baseAddress}${this.uriConsularOfficePath}/${employeeId}`;
    console.log(`get all consularOffices for ${employeeId} by url: ` + url);
    return this.http.get<[]>(url, this.options);
  }

  // создать нового выезда
  public add(consularOffice: ConsularOffice): Observable<any> {
    let url = `${this.baseAddress}${this.uriConsularOfficePath}`;
    console.log("consularOffice data for new instance: " + consularOffice);
    return this.http.post<any>(url, consularOffice, this.options);
  }

  // создать новое приглашение для конкретного сотрудника
  public update(consularOfficeId: string, consularOffice: ConsularOffice): Observable<any> {
    let url = `${this.baseAddress}${this.uriConsularOfficePath}/${consularOfficeId}`;
    console.log(`update consularOffice for ${consularOfficeId} by url: ` + url);
    console.log("consularOffice data for new instance: " + consularOffice);

    return this.http.post<any>(url, consularOffice, this.options);
  }

}
