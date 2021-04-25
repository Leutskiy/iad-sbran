import { OnInit, Input, Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Membership } from '../../contracts/login-data';
import { AuthService } from '../../services/auth.service';
import { MembershipDataService } from '../../services/component-providers/membership/membership-data.service';

@Component({
  selector: 'app-membership',
  templateUrl: './membership.component.html',
  styleUrls: ['./membership.component.scss'],
  providers: [MembershipDataService, AuthService]
})
export class MembershipComponent implements OnInit {

  profileId: string;
  employeeId: string;
  tableMode: boolean = true;

  @Input() title: string;
  memberships = [];
  membership: Membership;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private authService: AuthService,
    private membershipDataService: MembershipDataService) {
    this.membership = new Membership();
  }

  ngOnInit(): void {
    this.profileId = this.activatedRoute.snapshot.paramMap.get('profileId');
    this.employeeId = this.activatedRoute.snapshot.paramMap.get('employeeId');

    this.getAll();
  }

  getAll(): void {
    this.membershipDataService.get(this.employeeId).subscribe(e => {
      this.memberships = JSON.parse(JSON.stringify(e));
    })
  }

  edit(p: Membership) {
    this.membership = p;
    this.tableMode = false;
  }

  save() {
    console.log(this.membership);
    if (this.membership.id == null) {
      this.membershipDataService.add(this.membership)
        .subscribe((data: Membership) => this.memberships.push(data));
    } else {
      this.membershipDataService.update(this.membership.id, this.membership)
        .subscribe(data => this.getAll());
    }
    this.cancel();
  }


  cancel() {
    this.membership = new Membership();
    this.membership.employeeId = this.employeeId;
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

