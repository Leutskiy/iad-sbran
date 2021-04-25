import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Publication } from '../../../contracts/login-data';
import { APP_CONFIG, IAppConfig } from '../../../settings/app-config';

@Injectable()
export class PublicationDataService {

  private readonly options = { headers: new HttpHeaders().set('Content-Type', 'application/json') };

  private baseAddress: string;
  private readonly uriRelativePath: string = "api/v1";
  private readonly uriPublicationPath: string = `${this.uriRelativePath}/Publication`;

  constructor(private http: HttpClient, @Inject(APP_CONFIG) private config: IAppConfig) {
    this.baseAddress = `${config.icsApiEndpoint}`;
  }

  // получить все выезды сотрудника
  public get(employeeId: string): Observable<[]> {
    let url = `${this.baseAddress}${this.uriPublicationPath}/${employeeId}`;
    console.log(`get all publications for ${employeeId} by url: ` + url);
    return this.http.get<[]>(url, this.options);
  }

  // создать нового выезда
  public add(publication: Publication): Observable<any> {
    let url = `${this.baseAddress}${this.uriPublicationPath}`;
    console.log("publication data for new instance: " + publication);
    return this.http.post<any>(url, publication, this.options);
  }

  // создать новое приглашение для конкретного сотрудника
  public update(publicationId: string, publication: Publication): Observable<any> {
    let url = `${this.baseAddress}${this.uriPublicationPath}/${publicationId}`;
    console.log(`update publication for ${publicationId} by url: ` + url);
    console.log("publication data for new instance: " + publication);

    return this.http.post<any>(url, publication, this.options);
  }

}
