import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Departure, DepartureStatus } from '../../../contracts/login-data';
import { DepartureDataService } from '../../../services/component-providers/departure/departure-data.service';

@Component({
  selector: 'app-new-departure',
  templateUrl: './new-departure.component.html',
  styleUrls: ['./new-departure.component.css'],
  providers: [DepartureDataService]
})
export class NewDepartureComponent implements OnInit {

  profileId: string;
  employeeId: string;
  departureId: string;
  departure: Departure;
  isNew: boolean = false;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private departureService: DepartureDataService) { }

  ngOnInit(): void {

    this.profileId = this.activatedRoute.snapshot.paramMap.get('profileId');
    this.employeeId = this.activatedRoute.snapshot.paramMap.get('employeeId');
    this.departureId = this.activatedRoute.snapshot.paramMap.get('departureId');

    this.isNew = !!!this.departureId;

    if (!this.isNew) {
      console.log("so case new then to make filling")
      this.fillDepartureById(this.departureId);
    }
  }

  private fillDepartureById(departureId: string): void {
    this.departureService.getDepartureById(departureId).subscribe(
      queryDepartureResult => {
        // если задана дата, то форматируем
        if (!!queryDepartureResult.dateStart) {
          let dateStart = new Date(queryDepartureResult.dateStart);
          queryDepartureResult.dateStart = dateStart;
        }

        // если задана дата, то форматируем
        if (!!queryDepartureResult.dateEnd) {
          let dateEnd = new Date(queryDepartureResult.dateEnd);
          queryDepartureResult.dateEnd = this.formatDateToDatePickerString(dateEnd);
        }

        this.departure = queryDepartureResult;
      },
      queryInvitationError => {
        console.log(`queryInvitationError: ` + queryInvitationError);
      }
    );
  }


  public agree(id: string) {
    this.departureService.agree(id).subscribe(() => {
      console.log("agree");
      this.departure.departureStatus = DepartureStatus.Agreement;
    })
  }

  public save(): void {

    var newDeparture = this.departure;

    newDeparture.dateStart = this.formatDate(this.departure.dateStart);
    newDeparture.dateEnd = this.formatDate(this.departure.dateEnd);

    this.departureService.update(this.departureId, newDeparture).subscribe((data: any) => { });
    this.cancel();
  }

  cancel() {
    this.router.navigate([this.activatedRoute.snapshot.url.slice(0, this.activatedRoute.snapshot.url.length - 1).join('/')]);
  }

  // TODO: вынести в helper для форматирования дат
  private formatDateToDatePickerString(model: Date | null): string | null {
    if (model) {
      let date = {
        day: model.getDate(),
        month: model.getMonth() + 1,
        year: model.getFullYear()
      };

      let dateDay = `${date.day}`;
      let dateYear = `${date.year}`;
      let dateMonth = `${date.month}`;

      if (date.day < 10) {
        dateDay = 0 + dateDay;
      }

      if (date.month < 10) {
        dateMonth = 0 + dateMonth;
      }

      return dateDay + "." + dateMonth + "." + dateYear;
    }

    return null;
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
}
