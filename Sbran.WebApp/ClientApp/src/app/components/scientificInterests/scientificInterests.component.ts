import { OnInit, Input, Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ScientificInterests } from '../../contracts/login-data';
import { AuthService } from '../../services/auth.service';
import { ScientificInterestsDataService } from '../../services/component-providers/scientificInterests/scientificInterests-data.service';

@Component({
  selector: 'app-scientificInterests',
  templateUrl: './scientificInterests.component.html',
  styleUrls: ['./scientificInterests.component.scss'],
  providers: [ScientificInterestsDataService, AuthService]
})
export class ScientificInterestsComponent implements OnInit {

  profileId: string;
  employeeId: string;
  tableMode: boolean = true;

  @Input() title: string;
  scientificInterestss = [];
  scientificInterests: ScientificInterests;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private authService: AuthService,
    private scientificInterestsDataService: ScientificInterestsDataService) {
    this.scientificInterests = new ScientificInterests();
  }

  ngOnInit(): void {
    this.profileId = this.activatedRoute.snapshot.paramMap.get('profileId');
    this.employeeId = this.activatedRoute.snapshot.paramMap.get('employeeId');

    this.getAll();
  }

  getAll(): void {
    this.scientificInterestsDataService.get(this.employeeId).subscribe(e => {
      this.scientificInterestss = JSON.parse(JSON.stringify(e));
    })
  }

  edit(p: ScientificInterests) {
    this.scientificInterests = p;
    this.tableMode = false;
  }

  save() {
    console.log(this.scientificInterests);
    if (this.scientificInterests.id == null) {
      this.scientificInterestsDataService.add(this.scientificInterests)
        .subscribe((data: ScientificInterests) => this.scientificInterestss.push(data));
    } else {
      this.scientificInterestsDataService.update(this.scientificInterests.id, this.scientificInterests)
        .subscribe(data => this.getAll());
    }
    this.cancel();
  }


  cancel() {
    this.scientificInterests = new ScientificInterests();
    this.scientificInterests.employeeId = this.employeeId;
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

