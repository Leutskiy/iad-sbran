import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import * as signalR from '@microsoft/signalr';
import { UserInfo } from '../../contracts/login-data';
import { AuthService } from '../../services/auth.service';
import { ProfileDataService } from '../../services/component-providers/profile/profile-data.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
  providers: [ProfileDataService]
})
export class ProfileComponent implements OnInit {

  blobImagePrefix: string = "data:image/jpeg;base64,";
  defaultAvatarRelPath: string = "assets/images/avatar.jpg";

  private hubConnection: signalR.HubConnection;
  profileId: string;
  employeeId: string;
  accessToken: string;
  userInfo: UserInfo;

  constructor(
    private activatedRoute: ActivatedRoute,
    private profileDataService: ProfileDataService,
    private authService: AuthService) {
    this.userInfo = new UserInfo();
  }

  ngOnInit(): void {
    this.profileId = this.activatedRoute.snapshot.paramMap.get('profileId');
    this.employeeId = this.activatedRoute.snapshot.paramMap.get('employeeId');

    this.accessToken = this.authService.getToken();
    this.getProfileData(this.profileId, this.employeeId);
    this.hubConnection = new signalR.HubConnectionBuilder()
      //конфигурации на сервере 
      //.withUrl("https://localhost:5001/chatsocket", {  })
      .configureLogging(signalR.LogLevel.Error)
      .withUrl("https://localhost:44343/chatsocket", {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
        accessTokenFactory: () => this.accessToken
      })
      .build();
    // начинаем соединение с хабом
    this.hubConnection.start();
  }

  private getProfileData(profileId: string, employeeId: string): void {
    this.profileDataService.getDataById(profileId, employeeId).subscribe(userInfoResult => {
 
      this.userInfo.profile.init(
        userInfoResult.profile.id,
        userInfoResult.profile.userId,
        this._base64ToArrayBuffer(userInfoResult.profile.avatar),
        this._parse(userInfoResult.profile.webPages));

      this.userInfo.init(
        userInfoResult.fio,
        userInfoResult.fax,
        userInfoResult.profile.avatar,
        userInfoResult.academicDegree,
        userInfoResult.academicRank,
        userInfoResult.education,
        userInfoResult.shortName,
        userInfoResult.workPlace,
        userInfoResult.position,
        userInfoResult.email,
        userInfoResult.mobilePhoneNumber,
        userInfoResult.invitesCount,
        userInfoResult.departuresCount,
        userInfoResult.publishsCount,
        userInfoResult.membershipsCount,
        userInfoResult.scientificInterests,
        userInfoResult.consularOffices,
        userInfoResult.memberships,
      );
    });
  }

  private _parse(data: string): string[] | null {
    if (data) {
      var separatedData = data.split(",");
      return separatedData.map(function (partData) { return partData.trim(); });
    }

    return null;
  }

  private _base64ToArrayBuffer(base64): any[] {
    let byteArray = [];
    let binary_string = window.atob(base64);
    let length = binary_string.length;
    let bytes = new Uint8Array(length);

    for (let index = 0; index < length; index++) {
      byteArray.push(binary_string.charCodeAt(index));
    }

    return byteArray;
  }


}
