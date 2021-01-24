import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Organization } from '../../contracts/login-data';
import { InvitationDataService } from '../../services/component-providers/invitation/invitation-data.service';
import { OrganizationComponent } from './organization.component';

@Component({
  selector: 'app-alien-organization-invitation',
  templateUrl: './organization.component.html',
  styleUrls: ['./organization.component.scss'],
  providers: [InvitationDataService]
})
export class AlienOrganizationInvitationComponent extends OrganizationComponent {

  private invitationId: string;

  constructor(
      private activatedRoute: ActivatedRoute,
      private invitationDataService: InvitationDataService) {
    super();
    this.organization = new Organization();
  }

  ngOnInit(): void {
    this.isNewForm = this.scope === "invitation";
    this.invitationId = this.activatedRoute.snapshot.paramMap.get('invitationId');
    console.log(this.isNewForm);
    console.log(this.scope);
  }

  protected CompleteSaveOperation(): void {
    this.invitationDataService.editAlienOrganizationByInvitationId(this.invitationId, this.organization).subscribe(
      operationResult => {
        console.log(`operationResult: ` + operationResult);
      },
      operationError => {
        console.error(`operationError: ` + operationError);
      }
    );
  }
}
