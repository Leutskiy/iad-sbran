import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { RegistrationData } from '../contracts/login-data';
import { APP_CONFIG, IAppConfig } from '../settings/app-config';

@Injectable()
export class SingupService {

  registrationService: string;

  constructor(
    @Inject(APP_CONFIG) private config: IAppConfig,
    private http: HttpClient) {
      this.registrationService = `${this.config.icsApiEndpoint}api/account/register`;
  }

  public register(regFormData: RegistrationData) : any {

      const headers = new HttpHeaders().set('Content-Type', 'application/json');
      const options = { headers: headers };

      let data = {
        userName: regFormData.login,
        password: regFormData.password,
        confirmPassword: regFormData.confirmPassword
      }

      return this.http.post(this.registrationService, data, options);
  }
}
