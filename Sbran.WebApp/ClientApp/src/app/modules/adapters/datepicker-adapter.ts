import {Component, Injectable, Input, Output, EventEmitter} from '@angular/core';
import {NgbCalendar, NgbDateAdapter, NgbDateParserFormatter, NgbDateStruct} from '@ng-bootstrap/ng-bootstrap';

/**
 * This Service handles how the date is represented in scripts i.e. ngModel.
 */
@Injectable()
export class CustomAdapter extends NgbDateAdapter<string> {

  readonly DELIMITER = '.';

  fromModel(value: Date | string | null): NgbDateStruct | null {

    let parsingDate;
    if (value instanceof Date) {
      parsingDate = value.toLocaleDateString("Ru-ru").split(this.DELIMITER);
    }
    else if (value) {
      parsingDate = value.split(this.DELIMITER);
    }

    if (parsingDate) {
      return {
        day : parseInt(parsingDate[0], 10),
        month : parseInt(parsingDate[1], 10),
        year : parseInt(parsingDate[2], 10)
      };
    }

    return null;
  }

  toModel(date: NgbDateStruct | null): string | null {

    if (!date) {
      return null;
    }

    let dateDay = `${date.day}`;
    let dateMonth = `${date.month}`;
    let dateYear = `${date.year}`;

    if (date.day < 10) {
      dateDay = 0 + dateDay;
    }

    if (date.month < 10) {
      dateMonth = 0 + dateMonth;
    }

    return dateDay + this.DELIMITER + dateMonth + this.DELIMITER + dateYear;
  }
}

/**
 * This Service handles how the date is rendered and parsed from keyboard i.e. in the bound input field.
 */
@Injectable()
export class CustomDateParserFormatter extends NgbDateParserFormatter {

  readonly DELIMITER = '.';

  parse(value: string): NgbDateStruct | null {
    if (value) {
      let date = value.split(this.DELIMITER);
      return {
        day : parseInt(date[0], 10),
        month : parseInt(date[1], 10),
        year : parseInt(date[2], 10)
      };
    }
    return null;
  }

  format(date: NgbDateStruct | null): string {

    if (!date) {
      return '';
    }

    let dateDay = `${date.day}`;
    let dateMonth = `${date.month}`;
    let dateYear = `${date.year}`;

    if (date.day < 10) {
      dateDay = 0 + dateDay;
    }

    if (date.month < 10) {
      dateMonth = 0 + dateMonth;
    }

    return dateDay + this.DELIMITER + dateMonth + this.DELIMITER + dateYear;
  }
}

@Component({
  selector: 'ngbd-datepicker-adapter',
  templateUrl: './datepicker-adapter.html',

  // NOTE: For this example we are only providing current component, but probably
  // NOTE: you will want to provide your main App Module
  providers: [
    {provide: NgbDateAdapter, useClass: CustomAdapter},
    {provide: NgbDateParserFormatter, useClass: CustomDateParserFormatter}
  ]
})
export class NgbdDatepickerAdapter {

  placeholder: string = "дд.мм.гггг";
  @Input() title: string;
  @Input('binding-label-input') identifier: string;
  @Input() disabled: boolean;
  @Input() date: string;
  @Output() dateChange = new EventEmitter<string>();

  constructor(private ngbCalendar: NgbCalendar, private dateAdapter: NgbDateAdapter<string>) {}

  onDateChange(model: string) {
      this.date = model;
      this.dateChange.emit(model);
  }

  get today() {
    return this.dateAdapter.toModel(this.ngbCalendar.getToday())!;
  }
}
