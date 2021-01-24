import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { IAppConfig } from '../../settings/app-config';

export abstract class ComponentDataService<TComponentData>  {

  protected uriPath: string;
  client: HttpClient;

  constructor(uriRelativePath: string, httpClient: HttpClient, config: IAppConfig, activatedRoute: ActivatedRoute) {
    this.uriPath = `${config.icsApiEndpoint}${uriRelativePath}`;
    this.client = httpClient;
  }

  getData() {
    return this.client.get<any>(this.uriPath);
  }

  setData(componentData: TComponentData) {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    const options = { headers: headers };

    // ругался на any
    return this.client.post(this.uriPath, componentData, options);
  }

  setDataById(componentData: TComponentData, id: string) {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    const options = { headers: headers };

    return this.client.post<any>(`${this.uriPath}/${id}/job`, componentData, options);
  }
}
