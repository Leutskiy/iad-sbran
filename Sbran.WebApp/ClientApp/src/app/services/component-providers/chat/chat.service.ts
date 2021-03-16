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

  getFile(fileId: string) {
    return this.http.get<any>(`${this.uriPath}/getFile/${fileId}`, {});
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

  sendFile(account: string, profileId: string, chatRoomId: string, event, fileName: string) {

    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    const options = { headers: headers };

    //let dataArray = new Uint8Array(event);
    //const STRING_CHAR = dataArray.reduce((data, byte) => {
    //  return data + String.fromCharCode(byte);
    //}, '');

    //let base64String = btoa(STRING_CHAR);

    let file = {
      fileBinary: event,
      profileId: profileId,
      chatRoomId: chatRoomId,
      fileName: fileName,
      account: account
    }

    console.log(file);
    return this.client.post<any>(`${this.uriPath}/SendFile`, file, options);
  }


}
