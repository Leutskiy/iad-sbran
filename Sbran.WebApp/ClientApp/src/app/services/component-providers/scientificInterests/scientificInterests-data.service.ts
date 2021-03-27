import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ScientificInterests } from '../../../contracts/login-data';
import { APP_CONFIG, IAppConfig } from '../../../settings/app-config';

@Injectable()
export class ScientificInterestsDataService {

  private readonly options = { headers: new HttpHeaders().set('Content-Type', 'application/json') };

  private baseAddress: string;
  private readonly uriRelativePath: string = "api/v1";
  private readonly uriScientificInterestsPath: string = `${this.uriRelativePath}/ScientificInterests`;

  constructor(private http: HttpClient, @Inject(APP_CONFIG) private config: IAppConfig) {
    this.baseAddress = `${config.icsApiEndpoint}`;
  }

  // получить все выезды сотрудника
  public get(employeeId: string): Observable<[]> {
    let url = `${this.baseAddress}${this.uriScientificInterestsPath}/${employeeId}`;
    console.log(`get all scientificInterestss for ${employeeId} by url: ` + url);
    return this.http.get<[]>(url, this.options);
  }

  // создать нового выезда
  public add(scientificInterests: ScientificInterests): Observable<any> {
    let url = `${this.baseAddress}${this.uriScientificInterestsPath}`;
    console.log("scientificInterests data for new instance: " + scientificInterests);
    return this.http.post<any>(url, scientificInterests, this.options);
  }

  // создать новое приглашение для конкретного сотрудника
  public update(scientificInterestsId: string, scientificInterests: ScientificInterests): Observable<any> {
    let url = `${this.baseAddress}${this.uriScientificInterestsPath}/${scientificInterestsId}`;
    console.log(`update scientificInterests for ${scientificInterestsId} by url: ` + url);
    console.log("scientificInterests data for new instance: " + scientificInterests);

    return this.http.post<any>(url, scientificInterests, this.options);
  }

}
