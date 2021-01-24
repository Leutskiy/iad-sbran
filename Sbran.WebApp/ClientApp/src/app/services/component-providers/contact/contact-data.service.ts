import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { APP_CONFIG, IAppConfig } from '../../../settings/app-config';
import { ComponentDataService } from '../component-data.service';
import { Contact } from '../../../contracts/login-data';
import { ActivatedRoute } from '@angular/router';

@Injectable()
export class ContactDataService extends ComponentDataService<Contact> {

  constructor(
    private http: HttpClient,
    @Inject(APP_CONFIG) private config: IAppConfig, activatedRoute: ActivatedRoute) {
    super('api/v1/contact', http, config, activatedRoute);
  }

  setDataById(componentData: Contact, id: string) {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    const options = { headers: headers };

    return this.client.post<any>(`${this.uriPath}/${id}`, componentData, options);
  }
}
