import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { InternationalAgreement } from '../../../contracts/login-data';
import { APP_CONFIG, IAppConfig } from '../../../settings/app-config';

@Injectable()
export class InternationalAgreementDataService {

  private readonly options = { headers: new HttpHeaders().set('Content-Type', 'application/json') };

  private baseAddress: string;
  private readonly uriRelativePath: string = "api/v1";
  private readonly uriInternationalAgreementPath: string = `${this.uriRelativePath}/InternationalAgreement`;

  constructor(private http: HttpClient, @Inject(APP_CONFIG) private config: IAppConfig) {
    this.baseAddress = `${config.icsApiEndpoint}`;
  }

  // получить все выезды сотрудника
  public get(employeeId: string): Observable<[]> {
    let url = `${this.baseAddress}${this.uriInternationalAgreementPath}/${employeeId}`;
    console.log(`get all internationalAgreements for ${employeeId} by url: ` + url);
    return this.http.get<[]>(url, this.options);
  }

  // создать нового выезда
  public add(internationalAgreement: InternationalAgreement): Observable<any> {
    let url = `${this.baseAddress}${this.uriInternationalAgreementPath}`;
    console.log("internationalAgreement data for new instance: " + internationalAgreement);
    return this.http.post<any>(url, internationalAgreement, this.options);
  }

  // создать новое приглашение для конкретного сотрудника
  public update(internationalAgreementId: string, internationalAgreement: InternationalAgreement): Observable<any> {
    let url = `${this.baseAddress}${this.uriInternationalAgreementPath}/${internationalAgreementId}`;
    console.log(`update internationalAgreement for ${internationalAgreementId} by url: ` + url);
    console.log("internationalAgreement data for new instance: " + internationalAgreement);

    return this.http.post<any>(url, internationalAgreement, this.options);
  }

}
