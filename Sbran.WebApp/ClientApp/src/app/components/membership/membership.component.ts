import { OnInit, Input, Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DateHelper } from '../../common/helpers/DateHelper';
import { Membership, MembershipType } from '../../contracts/login-data';
import { AuthService } from '../../services/auth.service';
import { MembershipDataService } from '../../services/component-providers/membership/membership-data.service';

@Component({
  selector: 'app-membership',
  templateUrl: './membership.component.html',
  styleUrls: ['./membership.component.scss'],
  providers: [MembershipDataService, AuthService, DateHelper]
})
export class MembershipComponent implements OnInit {

  isNew: boolean;

  profileId: string;
  employeeId: string;
  tableMode: boolean = true;
  type: number;

  @Input() title: string;
  memberships = [];
  membership: Membership;

  constructor(
    private router: Router,
    private dateHelper: DateHelper,
    private activatedRoute: ActivatedRoute,
    private authService: AuthService,
    private membershipDataService: MembershipDataService) {
    this.membership = new Membership();
  }

  ngOnInit(): void {
    this.profileId = this.activatedRoute.snapshot.paramMap.get('profileId');
    this.employeeId = this.activatedRoute.snapshot.paramMap.get('employeeId');
    this.type = +this.activatedRoute.snapshot.paramMap.get('type');

    this.isNew = false;

    this.refreshMembershipTable();
  }

  refreshMembershipTable(): void {
    this.membershipDataService.get(this.employeeId, this.type).subscribe(
      _memberships => {
        var mappingType = this.type == 2 ? 0 : 1;
        _memberships.filter((membreship: Membership) => { return membreship.membershipType == mappingType; }).forEach((membership: Membership) => {
          if (membership.dateOfEntry) {
            membership.dateOfEntry = this.dateHelper.formatDateForFront(new Date(membership.dateOfEntry));
          }

          this.memberships.push(membership);
        });
      });
  }

  edit(membership: Membership) {
    this.membership = new Membership();
    this.membership.id = membership.id;
    this.membership.statusInTheOrganization = membership.statusInTheOrganization;
    this.membership.siteOfTheOrganization = membership.siteOfTheOrganization;
    this.membership.siteOfTheJournal = membership.siteOfTheJournal;
    this.membership.nameOfCompany = membership.nameOfCompany;
    this.membership.membershipType = membership.membershipType;
    this.membership.dateOfEntry = membership.dateOfEntry;
    this.membership.employeeId = membership.employeeId;

    this.tableMode = false;
    this.isNew = false;
  }

  save() {

    this.membership.dateOfEntry = this.dateHelper.formatDateForBack(this.membership.dateOfEntry);
    if (this.membership.id == null) {
      this.membershipDataService.add(this.membership)
        .subscribe((_membership: Membership) => {
          _membership.dateOfEntry = this.dateHelper.formatDateForFront(new Date(_membership.dateOfEntry));
          this.memberships.push(_membership);
        });
    } else {
      this.membershipDataService.update(this.membership.id, this.membership)
        .subscribe((_membership: Membership) => {
          _membership.dateOfEntry = this.dateHelper.formatDateForFront(new Date(_membership.dateOfEntry));
          this.memberships = this.memberships.filter((membershipValue: Membership) => { return membershipValue.id != _membership.id; });
          this.memberships.push(_membership);
        });
    }
    this.cancel();
  }


  cancel() {
    this.membership = new Membership();
    if (this.type == 2) {
      this.membership.membershipType = MembershipType.russian;
    }
    else {
      this.membership.membershipType = MembershipType.other;
    }

    this.membership.employeeId = this.employeeId;
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
