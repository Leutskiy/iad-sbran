import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Departure } from '../../../contracts/login-data';
import { APP_CONFIG, IAppConfig } from '../../../settings/app-config';

@Injectable()
export class DepartureDataService {

  private readonly options = { headers: new HttpHeaders().set('Content-Type', 'application/json') };

  private baseAddress: string;
  private readonly uriRelativePath: string = "api/v1";
  private readonly uriDeparturePath: string = `${this.uriRelativePath}/Departure`;

  constructor(private http: HttpClient, @Inject(APP_CONFIG) private config: IAppConfig) {
    this.baseAddress = `${config.icsApiEndpoint}`;
  }

  // получить все выезды сотрудника
  public get(employeeId: string): Observable<[]> {
    let url = `${this.baseAddress}${this.uriDeparturePath}/all/${employeeId}`;
    console.log(`get all departures for ${employeeId} by url: ` + url);
    return this.http.get<[]>(url, this.options);
  }

  // получить данные для формы приглашения
  public getDepartureById(departureId: string): Observable<any> {
    let url = `${this.baseAddress}${this.uriDeparturePath}/${departureId}`;
    console.log(`get departure for ${departureId} by url: ` + url);

    return this.http.get<any>(url, this.options);
  }

  // получить все выезды сотрудника
  public agree(id: string): Observable<[]> {
    let url = `${this.baseAddress}${this.uriDeparturePath}/${id}/agree`;
    console.log(`agree departure for ${id} by url: ` + url);
    return this.http.get<[]>(url, this.options);
  }

  // создать нового выезда
  public add(departure: Departure): Observable<any> {
    let url = `${this.baseAddress}${this.uriDeparturePath}`;
    console.log("departure data for new instance: " + departure);
    return this.http.post<any>(url, departure, this.options);
  }

  // создать новое приглашение для конкретного сотрудника
  public update(departureId: string, departure: Departure): Observable<any> {
    let url = `${this.baseAddress}${this.uriDeparturePath}/${departureId}`;
    console.log(`update departure for ${departureId} by url: ` + url);
    console.log("departure data for new instance: " + departure);

    return this.http.post<any>(url, departure, this.options);
  }

}
