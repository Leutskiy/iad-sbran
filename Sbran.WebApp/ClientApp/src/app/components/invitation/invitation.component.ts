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

    this.fillTable();
  }

  public new(): void {
    let url = `profile/${this.profileId}/employee/${this.employeeId}/invitation/new/form`;
    console.log(`after new button click redirect to: ` + url);

    this.router.navigate([url]);
  }

  public edit(event, invitation, index) {
    console.log(invitation);
    let url = `profile/${this.profileId}/employee/${this.employeeId}/invitation/${invitation.id}/edit/form`;
    console.log(`after edit button click for invitation ${invitation.id} redirect to: ` + url);
    console.log(`invitation was clicked: ` + invitation);

    this.router.navigate([url]);
  }



  // заполнить таблицу приглашениями сотрудника
  private fillTable(): void {
    this.invitationService.getInvitationsByEmployeeId(this.employeeId).subscribe(
      queryInvitationListResult => {
        console.log(`queryInvitationListResult: ` + queryInvitationListResult);
        //this.invitations = JSON.parse(JSON.stringify(queryInvitationListResult));

        queryInvitationListResult.filter((val: any, index, array) => val.visitDetail !== null).forEach((val: any, index, array) =>
        {

          console.log(val);
          val.visitDetail.arrivalDate = new Date(val.visitDetail.arrivalDate);
          val.visitDetail.departureDate = new Date(val.visitDetail.departureDate);;

          this.invitations.push(val);
        });

        ////queryInvitationListResult.visitDetail.arrivalDate = new Date(queryInvitationListResult.visitDetail.arrivalDate);
        ////queryInvitationListResult.visitDetail.departureDate = new Date(queryInvitationListResult.visitDetail.departureDate);;

        //this.invitations = queryInvitationListResult;
      },
      queryInvitationListError => {
        console.error(`queryInvitationListError: ` + queryInvitationListError);
      }
    );
  }
}
