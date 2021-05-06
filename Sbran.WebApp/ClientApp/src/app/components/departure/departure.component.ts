import { OnInit, Input, Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DateHelper } from '../../common/helpers/DateHelper';
import { Departure } from '../../contracts/login-data';
import { AuthService } from '../../services/auth.service';
import { DepartureDataService } from '../../services/component-providers/departure/departure-data.service';

@Component({
  selector: 'app-departure',
  templateUrl: './departure.component.html',
  styleUrls: ['./departure.component.scss'],
  providers: [DepartureDataService, AuthService, DateHelper]
})
export class DepartureComponent implements OnInit {

  @Input() title: string;

  profileId: string;
  employeeId: string;
  departures = [];

  forManager: boolean = false;

  departureListUrl: string;
  departureFormUrl: string;

  constructor(
    private dateHelper: DateHelper,
    private router: Router,
    private authService: AuthService,
    private activatedRoute: ActivatedRoute,
    private departureService: DepartureDataService) { }

  ngOnInit(): void {
    this.forManager = this.authService.isManager;

    this.profileId = this.activatedRoute.snapshot.paramMap.get('profileId');
    this.employeeId = this.activatedRoute.snapshot.paramMap.get('employeeId');

    let urlSegmentArray = this.activatedRoute.snapshot.url;
    this.departureListUrl = urlSegmentArray.join('/');
    this.departureFormUrl = `${urlSegmentArray.slice(0, urlSegmentArray.length - 1).join('/')}`;
    
    this.refreshDepartureTable();
  }

  // создать новый выезд
  public new(): void {
    this.router.navigate([this.departureFormUrl]);
  }

  // отредактировать существующий выезд
  public edit(event, departure: Departure, index): void {
    this.router.navigate([this.departureFormUrl,`${departure.id}`]);
  }

  // обновить список в таблице выездов
  private refreshDepartureTable(): void {
    console.info(`>>> начинаем получение списка выездов`);
    this.departureService.get(this.employeeId).subscribe(
      departures => {
        console.info(`>>> список выездов успешно получен:`);
        console.table(departures);
        departures.filter((val: any, index, array) => val.visitDetail !== null).forEach((val: any, index, array) => {
          val.dateStart = this.dateHelper.formatDateForFront(val.dateStart);
          val.dateEnd = this.dateHelper.formatDateForFront(val.dateEnd);

          this.departures.push(val);
        });
      },
      error => {
        console.error(`${error}`);
      });
  }
}
