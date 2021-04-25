import { HttpClient } from '@angular/common/http';
import { IAppConfig } from '../../../settings/app-config';
import { ComponentDataService } from '../component-data.service';
import { Job } from '../../../contracts/login-data';
import { ActivatedRoute } from '@angular/router';

export class JobDataService extends ComponentDataService<Job> {

  constructor(
    private relativeUrl: string,
    private httpClient: HttpClient,
    private appConfig: IAppConfig,
    private activatedRoute: ActivatedRoute) {
    super(relativeUrl, httpClient, appConfig, activatedRoute);
  }
}

