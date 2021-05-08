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

  // получить все публикации
  public getAll(employeeId: string): Observable<Publication[]> {
    let url = `${this.baseAddress}${this.uriPublicationPath}/${employeeId}/all`;
    console.log(`get all publications for ${employeeId} by url: ` + url);
    return this.http.get<[]>(url, this.options);
  }

  // создать новой публикации
  public add(publication: Publication): Observable<Publication> {
    let url = `${this.baseAddress}${this.uriPublicationPath}`;
    console.log("publication data for new instance: " + publication);
    return this.http.post<Publication>(url, publication, this.options);
  }

  // создать новую публикацию для конкретного сотрудника
  public update(publicationId: string, publication: Publication): Observable<Publication> {
    let url = `${this.baseAddress}${this.uriPublicationPath}/${publicationId}`;
    console.log(`update publication for ${publicationId} by url: ` + url);
    console.log("publication data for new instance: " + publication);

    return this.http.post<Publication>(url, publication, this.options);
  }

  // получить данные для формы публикации
  public getPublicationById(publicationId: string): Observable<Publication> {
    let url = `${this.baseAddress}${this.uriPublicationPath}/${publicationId}`;
    console.log(`>>> запрос публикации с ${publicationId}: ${url}`);

    return this.http.get<Publication>(url, this.options);
  }
}
