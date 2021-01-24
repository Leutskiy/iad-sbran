import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Contact } from '../../contracts/login-data';
import { InvitationDataService } from '../../services/component-providers/invitation/invitation-data.service';
import { ContactsComponent } from './contacts.component';

@Component({
  selector: 'app-alien-contacts-invitation',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.scss'],
  providers: [InvitationDataService]
})
export class AlienContactsInvitationComponent extends ContactsComponent {

  private invitationId: string;

  constructor(private activatedRoute: ActivatedRoute, private invitationDataService: InvitationDataService) {
    super();
    this.contact = new Contact();
  }

  ngOnInit(): void {
    this.isNewForm = this.scope === "invitation";
    this.invitationId = this.activatedRoute.snapshot.paramMap.get('invitationId');
    console.log("current scope: " + this.scope);
  }

  // TODO: переделать метод сохранения джоба по идентификатору для сотрудника
  protected CompleteSaveOperation(): void {
    this.invitationDataService.editAlienContactByInvitationId(this.invitationId, this.contact).subscribe(
      operationResult => {
        console.log(`operationResult: ` + operationResult);
      },
      operationError => {
        console.error(`operationError: ` + operationError);
      }
    );
  }

}
