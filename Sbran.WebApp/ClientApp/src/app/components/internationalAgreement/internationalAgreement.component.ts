import { OnInit, Input, Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DateHelper } from '../../common/helpers/DateHelper';
import { InternationalAgreement } from '../../contracts/login-data';
import { AuthService } from '../../services/auth.service';
import { InternationalAgreementDataService } from '../../services/component-providers/internationalAgreement/internationalAgreement-data.service';

@Component({
  selector: 'app-internationalAgreement',
  templateUrl: './internationalAgreement.component.html',
  styleUrls: ['./internationalAgreement.component.scss'],
  providers: [InternationalAgreementDataService, AuthService, DateHelper]
})
export class InternationalAgreementComponent implements OnInit {

  isNew: boolean;

  profileId: string;
  employeeId: string;
  tableMode: boolean = true;

  @Input() title: string;
  internationalAgreements = [];
  internationalAgreement: InternationalAgreement;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private dateHelper: DateHelper,
    private authService: AuthService,
    private internationalAgreementDataService: InternationalAgreementDataService) {
    this.internationalAgreement = new InternationalAgreement();
  }

  ngOnInit(): void {
    this.profileId = this.activatedRoute.snapshot.paramMap.get('profileId');
    this.employeeId = this.activatedRoute.snapshot.paramMap.get('employeeId');

    this.isNew = false;

    this.refreshInternationalAgreementTable();
  }

  refreshInternationalAgreementTable(): void {
    this.internationalAgreementDataService.get(this.employeeId).subscribe(
      _internationalAgreements => {
        _internationalAgreements.forEach((internationalAgreement: InternationalAgreement) => {
          if (internationalAgreement.dateOfEntry) {
            internationalAgreement.dateOfEntry = this.dateHelper.formatDateForFront(new Date(internationalAgreement.dateOfEntry));
            this.internationalAgreements.push(internationalAgreement);
          }
      });
    })
  }

  edit(internationalAgreement: InternationalAgreement) {
    this.internationalAgreement = new InternationalAgreement();
    this.internationalAgreement.id = internationalAgreement.id;
    this.internationalAgreement.employeeId = internationalAgreement.employeeId;
    this.internationalAgreement.dateOfEntry = internationalAgreement.dateOfEntry;
    this.internationalAgreement.placeOfSigning = internationalAgreement.placeOfSigning;
    this.internationalAgreement.textOfTheAgreement = internationalAgreement.textOfTheAgreement;
    this.internationalAgreement.theFirstPartyToTheAgreement = internationalAgreement.theFirstPartyToTheAgreement;
    this.internationalAgreement.theNameOfTheAgreement = internationalAgreement.theNameOfTheAgreement;
    this.internationalAgreement.theSecondPartyToTheAgreement = internationalAgreement.theSecondPartyToTheAgreement;

    this.tableMode = false;
    this.isNew = false;
  }

  save() {

    this.internationalAgreement.dateOfEntry = this.dateHelper.formatDateForBack(this.internationalAgreement.dateOfEntry);
    if (this.internationalAgreement.id == null) {
      this.internationalAgreementDataService.add(this.internationalAgreement).subscribe(
        (_internationalAgreement: InternationalAgreement) => {
          _internationalAgreement.dateOfEntry = this.dateHelper.formatDateForFront(new Date(_internationalAgreement.dateOfEntry));
          this.internationalAgreements.push(_internationalAgreement);
        });
    } else {
      this.internationalAgreementDataService.update(this.internationalAgreement.id, this.internationalAgreement)
        .subscribe(data => this.refreshInternationalAgreementTable());
    }

    this.cancel();
  }


  cancel() {
    this.internationalAgreement = new InternationalAgreement();
    this.internationalAgreement.employeeId = this.employeeId;
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
