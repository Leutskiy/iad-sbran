import { OnInit, Input } from '@angular/core';
import { Passport } from '../../contracts/login-data';

// базовый класс для компонент, представляющих паспорт
export class PassportComponent implements OnInit {

  @Input() scope: string;
  @Input() title: string;
  @Input() passport: Passport;

  public isNew: boolean = false;
  public editable: boolean = false;
  public viewMode: boolean = false;

  constructor() {
    this.passport = new Passport();
  }

  // TODO: переделать определения того, что форма заполнения приглашения с нуля
  ngOnInit(): void {
    this.isNew = this.scope === "invitation";
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

    this.passport.birthDate = this.formatDate(this.passport.birthDate);
    this.passport.issueDate = this.formatDate(this.passport.issueDate);

    this.CompleteSaveOperation()
  }

  public clearGender(): void {
    this.passport.gender = null;
  }

  protected CompleteSaveOperation(): void { };

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

  private reset() {
    this.ngOnInit();
  }

}
