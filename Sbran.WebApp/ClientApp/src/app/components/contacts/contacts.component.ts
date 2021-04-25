import { OnInit, Input } from '@angular/core';
import { Contact } from '../../contracts/login-data';

export class ContactsComponent implements OnInit {

  @Input() scope: string;
  @Input() title: string;
  @Input() contact: Contact;

  isNewForm: boolean = false;
  editable: boolean = false;
  viewMode: boolean = false;

  constructor() {
    this.contact = new Contact();
  }

  ngOnInit(): void {
    this.isNewForm = this.scope === "invitation";
  }

  public openForm() {
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

    this.CompleteSaveOperation();
  }

  protected CompleteSaveOperation(): void { };

  private reset() {
    this.ngOnInit();
  }

}
