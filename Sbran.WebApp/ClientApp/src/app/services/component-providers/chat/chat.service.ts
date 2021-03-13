import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { APP_CONFIG, IAppConfig } from '../../../settings/app-config';
import { ComponentDataService } from '../component-data.service';
import { Profile, Message } from '../../../contracts/login-data';
import { ActivatedRoute } from '@angular/router';

@Injectable()
export class ChatService extends ComponentDataService<Message> {

  constructor(
    private http: HttpClient,
    @Inject(APP_CONFIG) private config: IAppConfig, activatedRoute: ActivatedRoute) {
    super('api/v1/chat', http, config, activatedRoute);
  }

  getDataById(profileId: string) {
    return this.http.get<any>(`${this.uriPath}/${profileId}/myrooms`);
  }

  getDataByMyChats(chatRoomId: string, userId: string) {
    return this.http.get<any>(`${this.uriPath}/${chatRoomId}/${userId}`);
  }

  getDataByMyChatsForName(profileId: string, userName: string) {
    return this.http.get<any>(`${this.uriPath}/${profileId}/allUsers/${userName}`);
  }

  setDataByIdd(account: string, message: string, id: string, profileId: string) {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    const options = { headers: headers };

    let messagePost = {
      account: account,
      chatRoomId: id,
      message: message
    }

    return this.client.post<any>(`${this.uriPath}/${id}/send/${profileId}`, messagePost, options);
  }
}
