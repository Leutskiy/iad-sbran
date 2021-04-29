import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { mergeMap, map } from 'rxjs/operators';
import { APP_CONFIG, IAppConfig } from '../settings/app-config';
import { News, Vote, VoteList } from '../contracts/login-data';

@Injectable()
export class AuthService {

  authSessionKey: string = "auth";
  userSessionKey: string = "user";
  isManager: boolean = false;

  accountDetailService: string;
  authorizationService: string;

  profileId$: BehaviorSubject<string> = new BehaviorSubject<string>("");
  employeeId$: BehaviorSubject<string> = new BehaviorSubject<string>("");
  expiresAt: number;
  authenticated: boolean;

  private readonly options = { headers: new HttpHeaders().set('Content-Type', 'application/json') };

  constructor(
    @Inject(APP_CONFIG) private config: IAppConfig,
    private http: HttpClient) {
    this.authorizationService = `${this.config.icsApiEndpoint}token`;
    this.accountDetailService = `${this.config.icsApiEndpoint}api/account/details`;

    this._initAuthData();
    this._initUserData();
  }

  public login(login: string, password: string): Observable<any> {

    let queryParams = `grant_type=${this.config.authGrantType}&username=${login}&password=${password}`;

    const headers = new HttpHeaders().set('Content-Type', 'application/json'); // x-www-form-urlencoded
    const options = { headers: headers };

    let data = {
      userName: login,
      password: password
    }

    var profileEmployeeInfo = this.http.post(this.authorizationService, data, options) // queryParams
      .pipe(mergeMap(authData => {
        this._initAuthData(authData);
        return this.http.get(this.accountDetailService).pipe(map(profileEmployeeResult => {
          this._initUserData(profileEmployeeResult);
          return profileEmployeeResult;
        }));
      }));

    return profileEmployeeInfo;
  }

  public logout() {
    this.expiresAt = 0;
    this.authenticated = false;

    localStorage.removeItem(this.authSessionKey);
  }

  private _initAuthData(authData: any = null): void {
    if (authData) {
      // даем права на UI-контролы только админам и руководителям
      this.isManager = authData.role === "InstituteDirector" || authData.role === "Admin";
      this.expiresAt = new Date(authData.expires_in).getTime() * 1000 + Date.now();
      this.authenticated = !!authData;

      localStorage.setItem(this.authSessionKey, JSON.stringify({
        role: authData.role,
        accessToken: authData.access_token,
        expiresAt: this.expiresAt
      }));
    }
    else {
      let authDataAsString = localStorage.getItem(this.authSessionKey);
      let authDataFromStorage = JSON.parse(authDataAsString);

      if (authDataFromStorage) {
        this.isManager = authDataFromStorage.role === "InstituteDirector" || authDataFromStorage.role === "Admin";
        this.expiresAt = authDataFromStorage.expiresAt;
        this.authenticated = !!authDataFromStorage;
      }
    }
  }

  private _initUserData(profileEmployeeData: any = null): void {
    if (profileEmployeeData) {
      let profileId = profileEmployeeData.profileId;
      let employeeId = profileEmployeeData.employeeId;

      localStorage.setItem(this.userSessionKey, JSON.stringify({
        profileId: profileId,
        employeeId: employeeId,
      }));

      this.profileId$.next(profileId);
      this.employeeId$.next(employeeId);
    }
    else {
      let profileEmployeeDataAsString = localStorage.getItem(this.userSessionKey);
      let profileEmployeeDataFromStorage = JSON.parse(profileEmployeeDataAsString);

      if (profileEmployeeDataFromStorage) {
        this.profileId$.next(profileEmployeeDataFromStorage.profileId);
        this.employeeId$.next(profileEmployeeDataFromStorage.employeeId);
      }
    }
  }

  public getToken(): string {
    let access_token = null;
    let authDataAsString = localStorage.getItem(this.authSessionKey);
    let authData = JSON.parse(authDataAsString);

    if (authData) {
      access_token = authData.accessToken;
    }

    return access_token;
  }

  public get isLoggedIn(): boolean {
    // Check if current date is before token
    // expiration and user is signed in locally
    return Date.now() < this.expiresAt && this.authenticated;
  }

  public get profileId(): Observable<string> {
    return this.profileId$.asObservable();
  }

  get() {
    return this.http.get<any>(`api/v2/account/login`);
  }

  sendNews(news: News) {
    console.log("add new: " + news);
    return this.http.post<any>(`api/v2/account/addnews`, news, this.options);
  }

  sendVote(vote: Vote) {
    console.log("add vote: " + vote);
    return this.http.post<any>(`api/v2/account/addvote`, vote, this.options);
  }

  sendVoteList(id: string) {
    return this.http.post<any>(`api/v2/account/sendVoteList/${id}`, this.options);
  }

  public get employeeId(): Observable<string> {
    return this.employeeId$.asObservable();
  }
}
