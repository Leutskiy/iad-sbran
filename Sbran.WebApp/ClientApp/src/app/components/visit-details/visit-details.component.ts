import { OnInit, Input, Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { VisitDetail } from '../../contracts/login-data';
import { InvitationDataService } from '../../services/component-providers/invitation/invitation-data.service';

@Component({
  selector: 'app-visit-details-invitation',
  templateUrl: './visit-details.component.html',
  styleUrls: ['./visit-details.component.scss']
})
export class VisitDetailsInvitationComponent implements OnInit {

  private invitationId: string;

  @Input() scope: string;
  @Input() title: string;
  @Input() visitDetails: VisitDetail;

  editable: boolean = false;
  viewMode: boolean = false;
  isNewForm: boolean = false;

  constructor(
    private activatedRoute: ActivatedRoute,
    private invitationDataService: InvitationDataService) {
    this.visitDetails = new VisitDetail();
  }

  ngOnInit(): void {
    this.isNewForm = this.scope === "invitation";
    this.invitationId = this.activatedRoute.snapshot.paramMap.get('invitationId');
    console.log(this.scope);
  }

  public viewForm() {
    this.viewMode = !this.viewMode;
    if (!this.viewMode) {
      this.reset();
    }
  }

  public editForm() {
    this.editable = !this.editable;
  }

  public saveForm() {
    this.editable = !this.editable;

    this.visitDetails.arrivalDate = this.formatDate(this.visitDetails.arrivalDate);
    this.visitDetails.departureDate = this.formatDate(this.visitDetails.departureDate);

    this.CompleteSaveOperation();
  }

  private reset() {
    this.ngOnInit();
  }

  // TODO: Сделать стандартизацию формата даты (подходящий ISO)
  // отформатировать дату (привести к правильному формату)
  private formatDate(model: Date | string | null): Date | null {
    if (model instanceof Date) {
      return model;
    }
    else if (model) {
      return new Date(this.parse(model));
    } else {
      return null;
    }
  }

  private parse(value: string): string {
    if (value) {
      let date = value.split(".");
      return date[2] + "-" + date[1] + "-" + date[0];
    }

    return null;
  }

  private CompleteSaveOperation(): void {
    this.invitationDataService.editVisitDetailsByInvitationId(this.invitationId, this.visitDetails).subscribe(
      operationResult => {
        console.log(`operationResult: ` + operationResult);
      },
      operationError => {
        console.error(`operationError: ` + operationError);
      }
    );
  };
}

