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

    this.getAll();
  }

  getAll(): void {
    this.consularOfficeDataService.get(this.employeeId).subscribe(e => {
      this.consularOffices = JSON.parse(JSON.stringify(e));
    })
  }

  edit(p: ConsularOffice) {
    this.consularOffice = p;
    this.tableMode = false;
  }

  save() {
    console.log(this.consularOffice);
    if (this.consularOffice.id == null) {
      this.consularOfficeDataService.add(this.consularOffice)
        .subscribe((data: ConsularOffice) => this.consularOffices.push(data));
    } else {
      this.consularOfficeDataService.update(this.consularOffice.id, this.consularOffice)
        .subscribe(data => this.getAll());
    }
    this.cancel();
  }


  cancel() {
    this.consularOffice = new ConsularOffice();
    this.consularOffice.employeeId = this.employeeId;
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

