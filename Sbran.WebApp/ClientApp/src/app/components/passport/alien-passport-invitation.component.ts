import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Passport } from '../../contracts/login-data';
import { InvitationDataService } from '../../services/component-providers/invitation/invitation-data.service';
import { PassportComponent } from './passport.component';

// для формы приглашения
@Component({
  selector: 'app-alien-passport-invitation',
  templateUrl: './passport.component.html',
  styleUrls: ['./passport.component.scss'],
  providers: [InvitationDataService]
})
export class AlienPassportInvitationComponent extends PassportComponent {

  private invitationId: string;

  constructor(
      private activatedRoute: ActivatedRoute,
      private invitationDataService: InvitationDataService) {
    super();
    this.passport = new Passport();
  }

  ngOnInit(): void {
    this.isNew = this.scope === "invitation";
    this.invitationId = this.activatedRoute.snapshot.paramMap.get('invitationId');
    console.log(this.isNew);
    console.log(this.scope);
  }

  protected CompleteSaveOperation(): void {
    this.invitationDataService.editAlienPassportByInvitationId(this.invitationId, this.passport).subscribe(
      operationResult => {
        console.log(`operationResult: ` + operationResult);
      },
      operationError => {
        console.error(`operationError: ` + operationError);
      }
    );
  }
}
