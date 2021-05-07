import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Appendix, ListOfScientist, Report } from '../../../contracts/login-data';
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
  public get(reportId: string): Observable<Report> {
    let url = `${this.baseAddress}${this.uriReportPath}/${reportId}`;
    console.log(`get all reports for ${reportId} by url: ` + url);
    return this.http.get<Report>(url, this.options);
  }
  // получить отчет
  public agree(reportId: string): Observable<any> {
    let url = `${this.baseAddress}${this.uriReportPath}/${reportId}/agree`;
    console.log(`agree ${reportId} by url: ` + url);
    return this.http.get<any>(url, this.options);
  }

  // создать новый отчет
  public add(report: Report): Observable<any> {
    let url = `${this.baseAddress}${this.uriReportPath}`;
    console.log("report data for new instance: " + report);
    return this.http.post<any>(url, report, this.options);
  }

  // создать новый файл
  public addFile(appendix: Appendix): Observable<any> {
    let url = `${this.baseAddress}${this.uriReportPath}/file`;
    console.log("appendix data for new instance: " + appendix);
    return this.http.post<any>(url, appendix, this.options);
  }

  // создать нового ученого
  public addScientist(scientist: ListOfScientist): Observable<any> {
    let url = `${this.baseAddress}${this.uriReportPath}/scientist`;
    console.log("scientist data for new instance: " + scientist);
    return this.http.post<any>(url, scientist, this.options);
  }

  // создать нового ученого
  public deleteFile(appendix: Appendix): Observable<any> {
    let url = `${this.baseAddress}${this.uriReportPath}/file/${appendix.id}`;
    return this.http.delete<any>(url, this.options);
  }

  // создать нового ученого
  public deleteScientist(scientist: ListOfScientist): Observable<any> {
    let url = `${this.baseAddress}${this.uriReportPath}/scientist/${scientist.id}`;
    return this.http.delete<any>(url, this.options);
  }

  // обновить отчет
  public update(reportId: string, report: Report): Observable<any> {
    let url = `${this.baseAddress}${this.uriReportPath}/${reportId}`;
    console.log(`update report for ${reportId} by url: ` + url);
    console.log("report data for new instance: " + report);

    return this.http.post<any>(url, report, this.options);
  }

}
