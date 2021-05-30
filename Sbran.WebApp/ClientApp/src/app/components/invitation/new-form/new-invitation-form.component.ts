import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DateHelper } from '../../../common/helpers/DateHelper';
import { Invitation, InvitationStatus, Job, NewInvitationDto, Passport, ScientificInfo, VisitDetail } from '../../../contracts/login-data';
import { AuthService } from '../../../services/auth.service';
import { InvitationDataService } from '../../../services/component-providers/invitation/invitation-data.service';
import { AlienContactsInvitationComponent } from '../../contacts/alien-contacts-invitation.component';
import { AlienJobInvitationComponent } from '../../job/alien-job-invitation.component';
import { AlienOrganizationInvitationComponent } from '../../organization/alien-organization-invitation.component';
import { AlienPassportInvitationComponent } from '../../passport/alien-passport-invitation.component';
import { AlienStateRegistrationInvitationComponent } from '../../state-registration/alien-state-registration-invitation.component';
import { VisitDetailsInvitationComponent } from '../../visit-details/visit-details.component';

// TODO: переназвать как invitation-form.component и папку сделать form
@Component({
  selector: 'app-new-invitation-form',
  templateUrl: './new-invitation-form.component.html',
  styleUrls: ['./new-invitation-form.component.scss'],
  providers: [InvitationDataService, DateHelper]
})
export class NewInvitationFormComponent implements OnInit {

  @ViewChild(VisitDetailsInvitationComponent)
  private visitDetailsComponent: VisitDetailsInvitationComponent;

  @ViewChild(AlienJobInvitationComponent)
  private jobComponent: AlienJobInvitationComponent;

  @ViewChild(AlienPassportInvitationComponent)
  private passportComponent: AlienPassportInvitationComponent;

  @ViewChild(AlienContactsInvitationComponent)
  private contactDetailsComponent: AlienContactsInvitationComponent;

  @ViewChild(AlienOrganizationInvitationComponent)
  private organizationComponent: AlienOrganizationInvitationComponent;

  @ViewChild(AlienStateRegistrationInvitationComponent)
  private stateRegistrationComponent: AlienStateRegistrationInvitationComponent;

  scope: string = "invitation";
  isNew: boolean = false;
  forManager: boolean = false;

  profileId: string;
  employeeId: string;
  invitationId: string;

  job: Job;
  invitation: Invitation;

  visitDetailsTitle: string = "Детали визита";
  alienSkilDetailsTitle: string = "Профессионалные данные приглашенного";
  alienPasportDetailsTitle: string = "Паспортные данные приглашенного";
  alienContactDetailsTitle: string = "Контактные данные приглашенного";
  alienOrganizationTitle: string = "Организация приглашенного";
  alienWorkPlaceTitle: string = "Место работы приглашенного";
  alienStateRegistrationTitle: string = "Государственная регистрация приглашенного";

  constructor(
    private router: Router,
    private dateHelper: DateHelper,
    private authService: AuthService,
    private activatedRoute: ActivatedRoute,
    private invitationService: InvitationDataService) {
    this.job = new Job();
    this.invitation = new Invitation();
    this.forManager = authService.isManager;
  }

  ngOnInit(): void {
    this.profileId = this.activatedRoute.snapshot.paramMap.get('profileId');
    this.employeeId = this.activatedRoute.snapshot.paramMap.get('employeeId');
    this.invitationId = this.activatedRoute.snapshot.paramMap.get('invitationId');

    // TODO: надо упростить - вынести отдельно, проверку поменять и иные scope прописать
    this.isNew = !this.invitationId;
    this.scope = this.isNew ? "invitation" : "profile";

    if (!this.isNew) {
      console.log("make to fill")
      this.fill();
    }
  }

  public save(): void {
    this.saveNewPrint();

    var newInvitation = new NewInvitationDto();
    newInvitation.visitDetail = this.visitDetailsComponent.visitDetails;
    newInvitation.visitDetail.arrivalDate = this.dateHelper.formatDateForBack(this.visitDetailsComponent.visitDetails.arrivalDate);
    newInvitation.visitDetail.departureDate = this.dateHelper.formatDateForBack(this.visitDetailsComponent.visitDetails.departureDate);
    newInvitation.alien.alienJob = this.jobComponent.job;
    newInvitation.alien.alienPassport = this.passportComponent.passport;
    newInvitation.alien.alienPassport.birthDate = this.dateHelper.formatDateForBack(this.passportComponent.passport.birthDate);
    newInvitation.alien.alienPassport.issueDate = this.dateHelper.formatDateForBack(this.passportComponent.passport.issueDate);
    // TODO: доделать заполнение о научных достижениях приглашенного
    newInvitation.alien.alienScientificInfo = new ScientificInfo();
    newInvitation.alien.alienContact = this.contactDetailsComponent.contact;
    newInvitation.alien.alienOrganization = this.organizationComponent.organization;
    newInvitation.alien.alienStateRegistration = this.stateRegistrationComponent.stateRegistration;
    newInvitation.alien.alienWorkAddress = "";
    newInvitation.alien.alienStayAddress = "";

    // TODO: Назвать FromEmployee
    this.invitationService.newInvitationForEmployee(this.employeeId, newInvitation).subscribe(
      _invitationId => {
        console.info(`>>> перенаправление на созданное приглашение: ${_invitationId}`);
        this.router.navigate([this.router.url, _invitationId]);
      },
      _error => {
        console.error(`>>> ошибка при создании приглашения: ${_error}`);
      }
    );
  }

  public cancel(): void {
    console.info(">>> нажата кнопка Отменить")
    let url = `profile/${this.profileId}/employee/${this.employeeId}/invitation/list`;
    console.info(`>>> перенаправление на список приглашений: ${url}`);
    this.router.navigate([url]);
  }

  public backward(): void {
    console.info(">>> нажата кнопка Назад")
    let url = `profile/${this.profileId}/employee/${this.employeeId}/invitation/list`;
    console.info(`>>> перенаправление на список приглашений: ${url}`);
    this.router.navigate([url]);
  }

  public agree(invitationId: string): void {
    console.info(`>>> нажата кнопка Согласовать`);
    this.invitationService.agree(invitationId).subscribe(
      _success => {
        console.info(`>>> приглашение успешно согласовано`);
        this.invitation.invitationStatus = InvitationStatus.Agreement;
      },
      _error => {
        console.error(`>>> во время согласования приглашения произошла ошибка: ${_error}`);
      })
  }
  
  public report(invitationId: string): void {
    this.invitationService.report(invitationId).subscribe(
      _success => {
        console.info(`>>> приглашение успешно сформировано`);
      },
      _error => {
        console.error(`>>> во время формирования приглашения произошла ошибка: ${_error}`);
      });
  }

  private fill(): void {
    this.invitationService.getInvitationById(this.invitationId).subscribe(
      queryInvitationResult => {
        console.log(`queryInvitationResult: ` + queryInvitationResult);
        this.invitation = queryInvitationResult;

        queryInvitationResult.alien.passport = queryInvitationResult.alien.passport ?? new Passport();
        queryInvitationResult.visitDetail = queryInvitationResult.visitDetail ?? new VisitDetail();

        // если задана дата рождения, то форматируем
        if (!!queryInvitationResult.alien.passport.birthDate) {
          let birthDate = new Date(queryInvitationResult.alien.passport.birthDate);
          queryInvitationResult.alien.passport.birthDate = this.dateHelper.formatDateForFront(birthDate);
        }

        // если задана дата выдачи документа удостоверяющего личность, то форматируем
        if (!!queryInvitationResult.alien.passport.issueDate) {
          let issueDate = new Date(queryInvitationResult.alien.passport.issueDate);
          queryInvitationResult.alien.passport.issueDate = this.dateHelper.formatDateForFront(issueDate);
        }

        // если задана дата пребытия, то форматируем
        if (!!queryInvitationResult.visitDetail.arrivalDate) {
          let arrivalDate = new Date(queryInvitationResult.visitDetail.arrivalDate);
          queryInvitationResult.visitDetail.arrivalDate = this.dateHelper.formatDateForFront(arrivalDate);
        }

        // если задана дата отъезда, то форматируем
        if (!!queryInvitationResult.visitDetail.departureDate) {
          let departureDate = new Date(queryInvitationResult.visitDetail.departureDate);
          queryInvitationResult.visitDetail.departureDate = this.dateHelper.formatDateForFront(departureDate);
        }

        this.job.position = queryInvitationResult.alien.position;
        this.job.workPlace = queryInvitationResult.alien.workPlace;
      },
      queryInvitationError => {
        console.log(`queryInvitationError: ` + queryInvitationError);
      }
    );
  }

  // TODO: не на продакшн, только тест и разработка
  private saveNewPrint(): void {
    console.log("saving new invitation form");
    console.log(this.visitDetailsComponent.visitDetails);
    console.log(this.jobComponent.job);
    console.log(this.passportComponent.passport);
    console.log(this.contactDetailsComponent.contact);
    console.log(this.organizationComponent.organization);
    console.log(this.stateRegistrationComponent.stateRegistration);
    console.log("saved new invitation form");
  }
}
