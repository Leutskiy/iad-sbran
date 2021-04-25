import { OnInit, Input } from '@angular/core';
import { StateRegistration } from '../../contracts/login-data';

export class StateRegistrationComponent implements OnInit {

  @Input() scope: string;
  @Input() title: string;
  @Input() stateRegistration: StateRegistration;

  editable: boolean = false;
  viewMode: boolean = false;
  isNewForm: boolean = false;

  constructor() {
    this.stateRegistration = new StateRegistration();
  }

  ngOnInit(): void {
    this.isNewForm = this.scope === "invitation";
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

    this.CompleteSaveOperation();
  }

  protected CompleteSaveOperation(): void {
    console.log("invoking CompleteSaveOperation method in the super class");
  };

  private reset() {
    this.ngOnInit();
  }

}
