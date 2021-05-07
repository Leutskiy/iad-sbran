import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { InvitationDataService } from '../../services/component-providers/invitation/invitation-data.service';

// TODO: логирование происходит только в консоль!
// TODO: переназвать как invitation-table.component и папку сделать table
@Component({
  selector: 'app-invitation',
  templateUrl: './invitation.component.html',
  styleUrls: ['./invitation.component.scss'],
  providers: [InvitationDataService]
})
export class InvitationComponent implements OnInit {

  profileId: string;
  employeeId: string;

  invitations = [];

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private invitationService: InvitationDataService) { }

  ngOnInit(): void {
    this.profileId = this.activatedRoute.snapshot.paramMap.get('profileId');
    this.employeeId = this.activatedRoute.snapshot.paramMap.get('employeeId');

    this.refreshInvitationTable();
  }

  public new(): void {
    let url = `profile/${this.profileId}/employee/${this.employeeId}/invitation`;
    console.log(`after new button click redirect to: ` + url);

    this.router.navigate([url]);
  }

  public edit(event, invitation, index) {
    let url = `profile/${this.profileId}/employee/${this.employeeId}/invitation/${invitation.id}`;
    console.log(`after the Edit button is clicked for invitation ${invitation.id} it should redirect to: ${url}`);

    this.router.navigate([url]);
  }

  private refreshInvitationTable(): void {
    this.invitationService.getInvitationsByEmployeeId(this.employeeId).subscribe(
      invitations => {
        invitations.filter((val: any, index, array) => val.visitDetail !== null).forEach((val: any, index, array) =>
        {
          val.visitDetail.arrivalDate = new Date(val.visitDetail.arrivalDate);
          val.visitDetail.departureDate = new Date(val.visitDetail.departureDate);;

          this.invitations.push(val);
        });
      },
      error => {
        console.error(`error: ${error}`);
      }
    );
  }
}
