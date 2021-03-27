import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Report } from '../../../contracts/login-data';
import { APP_CONFIG, IAppConfig } from '../../../settings/app-config';

@Injectable()
export class ReportDataService {

  private readonly options = { headers: new HttpHeaders().set('Content-Type', 'application/json') };

  private baseAddress: string;
  private readonly uriRelativePath: string = "api/v1";
  private readonly uriReportPath: string = `${this.uriRelativePath}/Report`;

  constructor(private http: HttpClient, @Inject(APP_CONFIG) private config: IAppConfig) {
    this.baseAddress = `${config.icsApiEndpoint}`;
  }

  // получить отчет
  public get(employeeId: string): Observable<any> {
    let url = `${this.baseAddress}${this.uriReportPath}/${employeeId}`;
    console.log(`get all reports for ${employeeId} by url: ` + url);
    return this.http.get<any>(url, this.options);
  }

  // создать новый отчет
  public add(report: Report): Observable<any> {
    let url = `${this.baseAddress}${this.uriReportPath}`;
    console.log("report data for new instance: " + report);
    return this.http.post<any>(url, report, this.options);
  }

  // обновить отчет
  public update(reportId: string, report: Report): Observable<any> {
    let url = `${this.baseAddress}${this.uriReportPath}/${reportId}`;
    console.log(`update report for ${reportId} by url: ` + url);
    console.log("report data for new instance: " + report);

    return this.http.post<any>(url, report, this.options);
  }

}
