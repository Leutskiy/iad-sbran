import { OnInit, Input, Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ConsularOffice } from '../../contracts/login-data';
import { AuthService } from '../../services/auth.service';
import { ConsularOfficeDataService } from '../../services/component-providers/consularOffice/consularOffice-data.service';

@Component({
  selector: 'app-consularOffice',
  templateUrl: './consularOffice.component.html',
  styleUrls: ['./consularOffice.component.scss'],
  providers: [ConsularOfficeDataService, AuthService]
})
export class ConsularOfficeComponent implements OnInit {

  isNew: boolean;

  profileId: string;
  employeeId: string;
  tableMode: boolean = true;

  @Input() title: string;
  consularOffices = [];
  consularOffice: ConsularOffice;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private authService: AuthService,
    private consularOfficeDataService: ConsularOfficeDataService) {
    this.consularOffice = new ConsularOffice();
  }

  ngOnInit(): void {
    this.profileId = this.activatedRoute.snapshot.paramMap.get('profileId');
    this.employeeId = this.activatedRoute.snapshot.paramMap.get('employeeId');

    this.isNew = false;

    this.refreshConsularOfficeTable();
  }

  refreshConsularOfficeTable(): void {
    this.consularOfficeDataService.get(this.employeeId).subscribe(e => {
      this.consularOffices = JSON.parse(JSON.stringify(e));
    })
  }

  edit(consularOffice: ConsularOffice) {
    this.consularOffice = new ConsularOffice();
    this.consularOffice.id = consularOffice.id;
    this.consularOffice.employeeId = consularOffice.employeeId;
    this.consularOffice.countryOfLocation = consularOffice.countryOfLocation;
    this.consularOffice.cityOfLocation = consularOffice.cityOfLocation;
    this.consularOffice.nameOfTheConsularPost = consularOffice.nameOfTheConsularPost;

    this.tableMode = false;
    this.isNew = false;
  }

  save() {
    if (this.consularOffice.id == null) {
      this.consularOfficeDataService.add(this.consularOffice)
        .subscribe((data: ConsularOffice) => this.consularOffices.push(data));
    } else {
      this.consularOfficeDataService.update(this.consularOffice.id, this.consularOffice)
        .subscribe(data => this.refreshConsularOfficeTable());
    }

    this.cancel();
  }

  cancel() {
    this.consularOffice = new ConsularOffice();
    this.consularOffice.employeeId = this.employeeId;
    this.tableMode = true;
    this.isNew = false;
  }

  backward() {
    this.cancel();
  }

  add() {
    this.cancel();
    this.tableMode = false;
    this.isNew = true;
  }
}

