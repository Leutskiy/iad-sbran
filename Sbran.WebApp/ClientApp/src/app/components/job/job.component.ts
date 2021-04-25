import { OnInit, Input } from '@angular/core';
import { Job } from '../../contracts/login-data';

export class JobComponent implements OnInit {

  @Input() scope: string;
  @Input() title: string;

  editable: boolean = false;
  viewMode: boolean = false;
  isNewForm: boolean = false;

  @Input() job: Job;

  constructor() {
    this.job = new Job();
  }

  ngOnInit(): void {
    this.isNewForm = this.scope === "invitation";
  }

  public openForm(): void {
    this.viewMode = !this.viewMode;

    if (!this.viewMode) {
      this.reset();
    }
  }

  public editForm(): void {
    this.editable = !this.editable;
  }

  public saveForm(): void {
    this.editable = !this.editable;

    this.CompleteSaveOperation();
  }

  protected CompleteSaveOperation(): void { };

  private reset(): void {
    this.ngOnInit();
  }

}
