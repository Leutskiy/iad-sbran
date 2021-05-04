import { OnInit, Input, Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Departure } from '../../contracts/login-data';
import { AuthService } from '../../services/auth.service';
import { DepartureDataService } from '../../services/component-providers/departure/departure-data.service';

@Component({
  selector: 'app-departure',
  templateUrl: './departure.component.html',
  styleUrls: ['./departure.component.scss'],
  providers: [DepartureDataService, AuthService]
})
export class DepartureComponent implements OnInit {

  @Input() title: string;

  tableMode: boolean = false;
  forManager: boolean = false;

  profileId: string;
  employeeId: string;
  departureId: string;
  departures = [];

  constructor(
    private router: Router,
    private authService: AuthService,
    private activatedRoute: ActivatedRoute,
    private departureService: DepartureDataService) { }

  ngOnInit(): void {
    this.forManager = this.authService.isManager;

    this.profileId = this.activatedRoute.snapshot.paramMap.get('profileId');
    this.employeeId = this.activatedRoute.snapshot.paramMap.get('employeeId');
    
    this.fillAll();
  }

  private fillAll(): void {
    this.departureService.get(this.employeeId).subscribe(
      result => {
        this.departures = JSON.parse(JSON.stringify(result));
      },
      error => {
        console.log(`${error}`);
      });
  }

  public edit(event, departure: Departure, index): void {
    this.router.navigate([this.activatedRoute.snapshot.url.join('/'),`${departure.id}`]);
  }

  public add(): void {
    this.router.navigate([this.activatedRoute.snapshot.url.join('/'), 'new']);
  }
}
