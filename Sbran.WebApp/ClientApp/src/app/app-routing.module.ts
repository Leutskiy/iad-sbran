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
  { path: 'login', component: SigninComponent, canActivate: [RedirectToProfileGuard] },
  { path: 'singup', component: SingupComponent },
  { path: 'profile/:profileId/employee/:employeeId/departure/:employeeId', component: DepartureComponent, canActivate: [AuthGuard] },
  { path: 'profile/:profileId/employee/:employeeId/consularOffice/:employeeId', component: ConsularOfficeComponent, canActivate: [AuthGuard] },
  { path: 'profile/:profileId/employee/:employeeId/internationalAgreement/:employeeId', component: InternationalAgreementComponent, canActivate: [AuthGuard] },
  { path: 'profile/:profileId/employee/:employeeId/membership/:employeeId/:type', component: MembershipComponent, canActivate: [AuthGuard] },
  { path: 'profile/:profileId/employee/:employeeId/membership/:employeeId/:type', component: MembershipComponent, canActivate: [AuthGuard] },
  { path: 'profile/:profileId/employee/:employeeId/publish/:employeeId', component: PublicationComponent, canActivate: [AuthGuard] },
  { path: 'profile/:profileId/employee/:employeeId/scientificInterests/:employeeId', component: ScientificInterestsComponent, canActivate: [AuthGuard] },
  { path: 'chat/:profileId', component: ChatComponent, canActivate: [AuthGuard] },
  { path: 'profile/:profileId/employee/:employeeId', component: ProfileComponent, canActivate: [AuthGuard] },
  { path: 'profile/:profileId/employee/:employeeId/information', component: MainComponent, canActivate: [AuthGuard] },
  { path: 'profile/:profileId/employee/:employeeId/invitation', component: InvitationComponent, canActivate: [AuthGuard] },
  { path: 'profile/:profileId/employee/:employeeId/invitation/new/form', component: NewInvitationFormComponent, canActivate: [AuthGuard] },
  { path: 'profile/:profileId/employee/:employeeId/invitation/:invitationId/edit/form', component: NewInvitationFormComponent, canActivate: [AuthGuard] },
  { path: 'profile/:profileId/employee/:employeeId/departure/:employeeId/report/:departureId/create/:reportId/departure', component: ReportComponent, canActivate: [AuthGuard] },
  { path: 'profile/:profileId/employee/:employeeId/departure/:employeeId/report/:departureId/create/departure', component: ReportComponent, canActivate: [AuthGuard] },
  { path: 'profile/:profileId/employee/:employeeId/report/:invitationId/create/:reportId/invitation', component: ReportComponent, canActivate: [AuthGuard] },
  { path: 'profile/:profileId/employee/:employeeId/report/:invitationId/create/invitation', component: ReportComponent, canActivate: [AuthGuard] },
  { path: '**', component: NotFoundPageComponent },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [AuthGuard, RedirectToProfileGuard]
})
export class AppRoutingModule { }
