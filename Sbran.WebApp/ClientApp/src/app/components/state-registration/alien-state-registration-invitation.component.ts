import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { StateRegistration } from '../../contracts/login-data';
import { InvitationDataService } from '../../services/component-providers/invitation/invitation-data.service';
import { StateRegistrationComponent } from './state-registration.component';

@Component({
  selector: 'app-alien-state-registration-invitation',
  templateUrl: './state-registration.component.html',
  styleUrls: ['./state-registration.component.scss'],
  providers: [InvitationDataService]
})
export class AlienStateRegistrationInvitationComponent extends StateRegistrationComponent {

  private invitationId: string;

  constructor(private activatedRoute: ActivatedRoute, private invitationDataService: InvitationDataService) {
    super();
    this.stateRegistration = new StateRegistration();
  }

  ngOnInit(): void {
    this.isNewForm = this.scope === "invitation";
    this.invitationId = this.activatedRoute.snapshot.paramMap.get('invitationId');
    console.log("current scope: " + this.scope);
  }

  protected CompleteSaveOperation(): void {
    this.invitationDataService.editAlienStateRegistrationByInvitationId(this.invitationId, this.stateRegistration).subscribe(
      operationResult => {
        console.log(`operationResult: ` + operationResult);
      },
      operationError => {
        console.error(`operationError: ` + operationError);
      }
    );
  }


}
