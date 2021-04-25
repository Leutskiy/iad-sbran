import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Job } from '../../contracts/login-data';
import { InvitationDataService } from '../../services/component-providers/invitation/invitation-data.service';
import { JobComponent } from './job.component';

@Component({
  selector: 'app-alien-job-invitation',
  templateUrl: './job.component.html',
  styleUrls: ['./job.component.scss'],
  providers: [InvitationDataService]
})
export class AlienJobInvitationComponent extends JobComponent {

  private invitationId: string;

  constructor(private activatedRoute: ActivatedRoute, private invitationDataService: InvitationDataService) {
    super();
    this.job = new Job();
  }

  ngOnInit(): void {
    this.isNewForm = this.scope === "invitation";
    this.invitationId = this.activatedRoute.snapshot.paramMap.get('invitationId');
    console.log("current scope: " + this.scope);
  }

  protected CompleteSaveOperation(): void {
    this.invitationDataService.editAlienJobByInvitationId(this.invitationId, this.job).subscribe(
      operationResult => {
        console.log(`operationResult: ` + operationResult);
      },
      operationError => {
        console.error(`operationError: ` + operationError);
      }
    );
  }
}
