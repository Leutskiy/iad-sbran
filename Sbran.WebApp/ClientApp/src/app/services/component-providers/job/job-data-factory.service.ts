import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { APP_CONFIG, IAppConfig } from '../../../settings/app-config';
import { ComponentDataService } from '../component-data.service';
import { JobDataService } from '../job/job-data.service';
import { Job } from '../../../contracts/login-data';
import { ActivatedRoute } from '@angular/router';

@Injectable()
export class JobDataServiceFactory {

  profileRelativeUrl: string = 'api/v1/employee';
  invitationRelativeUrl: string = 'api/v1/invitation';
  client: HttpClient;
  appConfig: IAppConfig;

  constructor(
    private httpClient: HttpClient,
    @Inject(APP_CONFIG) private applicationConfig: IAppConfig,
    private activatedRoute: ActivatedRoute) {
    this.client = httpClient;
    this.appConfig = applicationConfig;
  }

  create(scope: string): ComponentDataService<Job> {
    if (scope == "profile") {
      console.log(scope);

      return new JobDataService(this.profileRelativeUrl, this.client, this.appConfig, this.activatedRoute);
    }
    else if (scope == "invitation") {
      console.log(scope);

      return new JobDataService(this.invitationRelativeUrl, this.client, this.appConfig, this.activatedRoute);
    }

    throw new Error("Not supported component scope");
  }
}
