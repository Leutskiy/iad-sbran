import { InjectionToken } from "@angular/core";
import { environment } from "../../environments/environment";

export const APP_CONFIG = new InjectionToken<IAppConfig>("APP_CONFIG");

export interface IAppConfig {
  icsApiEndpoint: string;
  authGrantType: string;
}

export const AppConfig: IAppConfig = {
    icsApiEndpoint: environment.apiUrl,
    authGrantType: "password"
};
