import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { APP_CONFIG, IAppConfig } from '../../../settings/app-config';
import { ComponentDataService } from '../component-data.service';
import { ScientificInfo } from '../../../contracts/login-data';
import { ActivatedRoute } from '@angular/router';

@Injectable()
export class ScientificDataService extends ComponentDataService<ScientificInfo> {

  constructor(
    private http: HttpClient,
    @Inject(APP_CONFIG) private config: IAppConfig, activatedRoute: ActivatedRoute) {
    super('api/v1/employee', http, config, activatedRoute);
  }

  setDataById(componentData: ScientificInfo, id: string) {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    const options = { headers: headers };

    return this.client.post<any>(`${this.uriPath}/${id}/scientific`, componentData, options);
  }
}
