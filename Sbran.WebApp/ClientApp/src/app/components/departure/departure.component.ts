import { OnInit, Input, Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Departure, DepartureStatus } from '../../contracts/login-data';
import { AuthService } from '../../services/auth.service';
import { DepartureDataService } from '../../services/component-providers/departure/departure-data.service';

@Component({
  selector: 'app-departure',
  templateUrl: './departure.component.html',
  styleUrls: ['./departure.component.scss'],
  providers: [DepartureDataService, AuthService]
})
export class DepartureComponent implements OnInit {

  profileId: string;
  employeeId: string;
  tableMode: boolean = true;
  forManager: boolean = false;

  @Input() title: string;
  departures = [];
  departure: Departure;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private authService: AuthService,
    private departureDataService: DepartureDataService) {
    this.departure = new Departure();
    this.forManager = authService.isManager;
  }

  ngOnInit(): void {
    this.profileId = this.activatedRoute.snapshot.paramMap.get('profileId');
    this.employeeId = this.activatedRoute.snapshot.paramMap.get('employeeId');

    this.getAll();
  }

  getAll(): void {
    this.departureDataService.get(this.employeeId).subscribe(e => {
      console.log(e);
      this.departures = JSON.parse(JSON.stringify(e));
      console.log(this.departures);
    })
  }

  edit(p: Departure) {
    this.departure = p;
    this.tableMode = false;
  }

  public agree(id: string) {
    this.departureDataService.agree(id).subscribe(e => {
      console.log("agree");
      this.departure.departureStatus = DepartureStatus.Agreement;
      //this.departures = JSON.parse(JSON.stringify(e));
    })
  }

  save() {
    this.departure.dateStart = this.formatDate(this.departure.dateStart);
    this.departure.dateEnd = this.formatDate(this.departure.dateEnd);
    console.log(this.departure);
    if (this.departure.id == null) {
      this.departureDataService.add(this.departure)
        //.subscribe((data: Departure) => this.departures.push(data));
        .subscribe((data: any) => {
          console.log("test:")
          console.log(data);
          this.departures.push(data);
        });
    } else {
      this.departureDataService.update(this.departure.id, this.departure)
        //.subscribe(data => this.getAll());
        .subscribe((data: any) => {
          console.log("test:")
          console.log(data);
          this.getAll();
        });
    }
    this.cancel();
  }

  cancel() {
    this.departure = new Departure();
    this.departure.employeeId = this.employeeId;
    this.tableMode = true;
  }

  add() {
    this.cancel();
    this.tableMode = false;
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

