import { OnInit, Input, Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { InternationalAgreement } from '../../contracts/login-data';
import { AuthService } from '../../services/auth.service';
import { InternationalAgreementDataService } from '../../services/component-providers/internationalAgreement/internationalAgreement-data.service';

@Component({
  selector: 'app-internationalAgreement',
  templateUrl: './internationalAgreement.component.html',
  styleUrls: ['./internationalAgreement.component.scss'],
  providers: [InternationalAgreementDataService, AuthService]
})
export class InternationalAgreementComponent implements OnInit {

  profileId: string;
  employeeId: string;
  tableMode: boolean = true;

  @Input() title: string;
  internationalAgreements = [];
  internationalAgreement: InternationalAgreement;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private authService: AuthService,
    private internationalAgreementDataService: InternationalAgreementDataService) {
    this.internationalAgreement = new InternationalAgreement();
  }

  ngOnInit(): void {
    this.profileId = this.activatedRoute.snapshot.paramMap.get('profileId');
    this.employeeId = this.activatedRoute.snapshot.paramMap.get('employeeId');

    this.getAll();
  }

  getAll(): void {
    this.internationalAgreementDataService.get(this.employeeId).subscribe(e => {
      this.internationalAgreements = JSON.parse(JSON.stringify(e));
    })
  }

  edit(p: InternationalAgreement) {
    this.internationalAgreement = p;
    this.tableMode = false;
  }

  save() {
    console.log(this.internationalAgreement);
    if (this.internationalAgreement.id == null) {
      this.internationalAgreementDataService.add(this.internationalAgreement)
        .subscribe((data: InternationalAgreement) => this.internationalAgreements.push(data));
    } else {
      this.internationalAgreementDataService.update(this.internationalAgreement.id, this.internationalAgreement)
        .subscribe(data => this.getAll());
    }
    this.cancel();
  }


  cancel() {
    this.internationalAgreement = new InternationalAgreement();
    this.internationalAgreement.employeeId = this.employeeId;
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

