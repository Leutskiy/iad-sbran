import { Component, OnInit } from '@angular/core';
import { ActivatedRoute} from '@angular/router';
import { Employee, Profile } from '../../contracts/login-data';
import { EmployeeDataService } from '../../services/component-providers/employee/employee-data.service';
import { ProfileDataService } from '../../services/component-providers/profile/profile-data.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
  providers: [EmployeeDataService, ProfileDataService]
})
export class MainComponent implements OnInit {

  scope: string = "profile";

  employeePersonDetailsTitle: string = "Профиль";
  employeePasportDetailsTitle: string = "Паспорт";
  employeeContactDetailsTitle: string = "Контакты";
  employeeOrganizationTitle: string = "Организация";
  employeeWorkPlaceTitle: string = "Работа";
  employeeStateRegistrationTitle: string = "Государственная регистрация";

  profile: Profile;
  employee: Employee;

  constructor(
      private activatedRoute: ActivatedRoute,
      private profileDataService: ProfileDataService,
      private employeeDataService: EmployeeDataService) {

    this.profile = new Profile();
    this.employee = new Employee();

    this.profile.id = this.activatedRoute.snapshot.paramMap.get('profileId');
    this.employee.id = this.activatedRoute.snapshot.paramMap.get('employeeId');
  }

  ngOnInit(): void {

    // TODO: вынести в отдельный метод
    this.employeeDataService.getDataById(this.employee.id).subscribe(
      employeeResult => {

        if (employeeResult) {
          this.employee.init(
            employeeResult.id,
            employeeResult.academicDegree,
            employeeResult.academicRank,
            employeeResult.education,
            employeeResult.workPlace,
            employeeResult.position);
        }

        if (employeeResult && employeeResult.passport) {
          this.employee.passport.init(
            employeeResult.passport.id,
            employeeResult.passport.nameRus,
            employeeResult.passport.nameEng,
            employeeResult.passport.surnameRus,
            employeeResult.passport.surnameEng,
            employeeResult.passport.patronymicNameRus,
            employeeResult.passport.patronymicNameEng,
            employeeResult.passport.gender,
            this.formatterFromModel(new Date(employeeResult.passport.birthDate)),
            employeeResult.passport.birthPlace,
            employeeResult.passport.birthCountry,
            employeeResult.passport.residence,
            employeeResult.passport.citizenship,
            employeeResult.passport.residenceRegion,
            employeeResult.passport.residenceCountry,
            this.formatterFromModel(new Date(employeeResult.passport.issueDate)),
            employeeResult.passport.issuePlace,
            employeeResult.passport.departmentCode,
            employeeResult.passport.identityDocument);
        }

        if (employeeResult && employeeResult.contact) {
          this.employee.contact.init(
            employeeResult.contact.id,
            employeeResult.contact.email,
            employeeResult.contact.postcode,
            employeeResult.contact.homePhoneNumber,
            employeeResult.contact.workPhoneNumber,
            employeeResult.contact.mobilePhoneNumber);
        }

        if (employeeResult && employeeResult.organization) {
          this.employee.organization.init(
            employeeResult.organization.id,
            employeeResult.organization.name,
            employeeResult.organization.shortName,
            employeeResult.organization.legalAddress,
            employeeResult.organization.scientificActivity,
            employeeResult.organization.stateRegistration.id,
            employeeResult.organization.stateRegistration.inn,
            employeeResult.organization.stateRegistration.ogrnip);
        }

        if (employeeResult && employeeResult.stateRegistration) {
          this.employee.stateRegistration.init(
            employeeResult.stateRegistration.id,
            employeeResult.stateRegistration.inn,
            employeeResult.stateRegistration.ogrnip);
        }
      },
      error => {
        console.log(error);
      }
    );

    this.profileDataService.getDataById(this.profile.id, this.employee.id).subscribe(
      userInfoResult => {
        this.profile.init(
          userInfoResult.profile.id,
          userInfoResult.profile.userId,
          this._base64ToArrayBuffer(userInfoResult.profile.avatar),
          userInfoResult.profile.webPages);
      },
      error => {
        console.log(error);
      }
    );
  }

  private _base64ToArrayBuffer(base64): any[] {
    let byteArray = [];
    let binary_string = window.atob(base64);
    let length = binary_string.length;
    //let bytes = new Uint8Array(length);

    for (let index = 0; index < length; index++) {
        byteArray.push(binary_string.charCodeAt(index));
    }

    return byteArray;
  }

  private formatterFromModel(model: Date | null): string | null {
    if (model) {
      let date = {
        day : model.getDate(),
        month : model.getMonth() + 1,
        year : model.getFullYear()
      };

      let dateDay = `${date.day}`;
      let dateYear = `${date.year}`;
      let dateMonth = `${date.month}`;

      if (date.day < 10) {
        dateDay = 0 + dateDay;
      }

      if (date.month < 10) {
        dateMonth = 0 + dateMonth;
      }

      return dateDay + "." + dateMonth + "." + dateYear;
    }

    return null;
  }

}
