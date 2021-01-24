import { InjectionToken } from "@angular/core";

export const APP_CONFIG = new InjectionToken<IAppConfig>("APP_CONFIG");

export interface IAppConfig {
  icsApiEndpoint: string;
  authGrantType: string;
}

export const AppConfig: IAppConfig = {
    icsApiEndpoint: "http://localhost:8991/",
    /*icsApiEndpoint: "http://ras.techdir.ru:4200/",*/
    authGrantType: "password"
};
