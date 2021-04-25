import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { APP_CONFIG, IAppConfig } from '../../../settings/app-config';
import { ComponentDataService } from '../component-data.service';
import { Passport } from '../../../contracts/login-data';
import { ActivatedRoute } from '@angular/router';

@Injectable()
export class PassportDataService extends ComponentDataService<Passport> {

  constructor(
    private http: HttpClient,
    @Inject(APP_CONFIG) private config: IAppConfig, activatedRoute: ActivatedRoute) {
    super('api/v1/passport', http, config, activatedRoute);
  }

  setDataById(componentData: Passport, id: string) {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    const options = { headers: headers };

    return this.client.post<any>(`${this.uriPath}/${id}`, componentData, options);
  }
}
