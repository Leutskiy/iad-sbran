import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SigninComponent } from './components/signin/signin.component';
import { SingupComponent } from './components/singup/singup.component';
import { ProfileComponent } from './components/profile/profile.component';
import { MainComponent } from './components/main/main.component';
import { NotFoundPageComponent } from './components/pages/not-found-page/not-found-page.component';
import { InvitationComponent } from './components/invitation/invitation.component';
import { NewInvitationFormComponent } from './components/invitation/new-form/new-invitation-form.component';
import { AuthGuard } from './auth/auth.guard';
import { RedirectToProfileGuard } from './auth/redirect-to-profile.guard';
import { ChatComponent } from './components/chat/chat.component';
import { DepartureComponent } from './components/departure/departure.component';
import { ConsularOfficeComponent } from './components/consularOffice/consularOffice.component';
import { InternationalAgreementComponent } from './components/internationalAgreement/internationalAgreement.component';
import { MembershipComponent } from './components/membership/membership.component';
import { PublicationComponent } from './components/publication/publication.component';
import { ScientificInterestsComponent } from './components/scientificInterests/scientificInterests.component';
import { ReportComponent } from './components/report/report.component';
import { NewDepartureComponent } from './components/departure/new/new-departure.component';
import { PublicationItemComponent } from './components/publication/form/publication-item.component';
import { PanelInfoComponent } from './components/panel-info/panel-info.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    path: 'logout',
    redirectTo: 'login',
    pathMatch: 'full'
  },

  // вход/выход
  { path: 'login', component: SigninComponent, canActivate: [RedirectToProfileGuard] },
  { path: 'singup', component: SingupComponent },

  // информационная панель
  { path: 'panel/info', component: PanelInfoComponent, canActivate: [AuthGuard] },

  // чат
  { path: 'profile/:profileId/employee/:employeeId/chat', component: ChatComponent, canActivate: [AuthGuard] },

  { path: 'profile/:profileId/employee/:employeeId', component: ProfileComponent, canActivate: [AuthGuard] },
  { path: 'profile/:profileId/employee/:employeeId/information', component: MainComponent, canActivate: [AuthGuard] },


  { path: 'profile/:profileId/employee/:employeeId/consularOffice/:employeeId', component: ConsularOfficeComponent, canActivate: [AuthGuard] },
  { path: 'profile/:profileId/employee/:employeeId/scientificInterests/:employeeId', component: ScientificInterestsComponent, canActivate: [AuthGuard] },
  { path: 'profile/:profileId/employee/:employeeId/internationalAgreement/:employeeId', component: InternationalAgreementComponent, canActivate: [AuthGuard] },
  { path: 'profile/:profileId/employee/:employeeId/membership/:employeeId/:type', component: MembershipComponent, canActivate: [AuthGuard] },
  { path: 'profile/:profileId/employee/:employeeId/membership/:employeeId/:type', component: MembershipComponent, canActivate: [AuthGuard] },

  // публикации
  { path: 'profile/:profileId/employee/:employeeId/publication/list', component: PublicationComponent, canActivate: [AuthGuard] },
  { path: 'profile/:profileId/employee/:employeeId/publication', component: PublicationItemComponent, canActivate: [AuthGuard] },
  { path: 'profile/:profileId/employee/:employeeId/publication/:publicationId', component: PublicationItemComponent, canActivate: [AuthGuard] },

  // приглашения
  { path: 'profile/:profileId/employee/:employeeId/invitation/list', component: InvitationComponent, canActivate: [AuthGuard] },
  { path: 'profile/:profileId/employee/:employeeId/invitation', component: NewInvitationFormComponent, canActivate: [AuthGuard] },
  { path: 'profile/:profileId/employee/:employeeId/invitation/:invitationId', component: NewInvitationFormComponent, canActivate: [AuthGuard] },

  // отчет по пригшалшению
  { path: 'profile/:profileId/employee/:employeeId/invitation/:invitationId/report/:reportId', component: ReportComponent, canActivate: [AuthGuard] },
  { path: 'profile/:profileId/employee/:employeeId/invitation/:invitationId/report', component: ReportComponent, canActivate: [AuthGuard] },

  // выезды
  { path: 'profile/:profileId/employee/:employeeId/departure/list', component: DepartureComponent, canActivate: [AuthGuard] },
  { path: 'profile/:profileId/employee/:employeeId/departure/:departureId', component: NewDepartureComponent, canActivate: [AuthGuard] },
  { path: 'profile/:profileId/employee/:employeeId/departure', component: NewDepartureComponent, canActivate: [AuthGuard] },

  // отчет по выезду
  { path: 'profile/:profileId/employee/:employeeId/departure/:departureId/report/:reportId', component: ReportComponent, canActivate: [AuthGuard] },
  { path: 'profile/:profileId/employee/:employeeId/departure/:departureId/report', component: ReportComponent, canActivate: [AuthGuard] },

  // страница не найдена
  { path: '**', component: NotFoundPageComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [AuthGuard, RedirectToProfileGuard]
})
export class AppRoutingModule { }
