import { OnInit, Input } from '@angular/core';
import { Organization } from '../../contracts/login-data';

export class OrganizationComponent implements OnInit {

  @Input() scope: string;
  @Input() title: string;
  @Input() organization: Organization;

  editable: boolean = false;
  viewMode: boolean = false;
  isNewForm: boolean = false;

  constructor() {
    this.organization = new Organization();
  }

  ngOnInit(): void {
    this.viewMode = false;
    this.editable = false;

    this.isNewForm = this.scope === "invitation";
  }

  openForm() {
    this.viewMode = !this.viewMode;

    if (!this.viewMode) {
      this.reset();
    }
  }

  editForm() {
    this.editable = !this.editable;
  }

  saveForm() {
    this.editable = !this.editable;

    this.CompleteSaveOperation();
  }

  private reset() {
    this.ngOnInit();
  }

  protected CompleteSaveOperation(): void { };
}
