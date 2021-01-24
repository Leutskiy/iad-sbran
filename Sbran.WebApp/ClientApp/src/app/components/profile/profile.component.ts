import { Component, OnInit } from '@angular/core';
import { ActivatedRoute} from '@angular/router';
import { UserInfo } from '../../contracts/login-data';
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

  profileId: string;
  employeeId: string;
  userInfo: UserInfo;

  constructor(
    private activatedRoute: ActivatedRoute,
    private profileDataService: ProfileDataService) {
    this.userInfo = new UserInfo();
  }

  ngOnInit(): void {
    this.profileId = this.activatedRoute.snapshot.paramMap.get('profileId');
    this.employeeId = this.activatedRoute.snapshot.paramMap.get('employeeId');

    this.getProfileData(this.profileId, this.employeeId);
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
          userInfoResult.mobilePhoneNumber
        );
      });
  }

  private _parse(data: string): string[] | null {
    if (data) {
      var separatedData = data.split(",");
      return separatedData.map(function(partData) { return partData.trim();  });
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
