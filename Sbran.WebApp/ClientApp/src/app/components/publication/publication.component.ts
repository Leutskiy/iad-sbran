import { OnInit, Input, Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Publication } from '../../contracts/login-data';
import { AuthService } from '../../services/auth.service';
import { PublicationDataService } from '../../services/component-providers/publication/publication-data.service';

@Component({
  selector: 'app-publication',
  templateUrl: './publication.component.html',
  styleUrls: ['./publication.component.scss'],
  providers: [PublicationDataService, AuthService]
})
export class PublicationComponent implements OnInit {

  profileId: string;
  employeeId: string;
  tableMode: boolean = true;

  @Input() title: string;
  publications = [];
  publication: Publication;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private authService: AuthService,
    private publicationDataService: PublicationDataService) {
    this.publication = new Publication();
  }

  ngOnInit(): void {
    this.profileId = this.activatedRoute.snapshot.paramMap.get('profileId');
    this.employeeId = this.activatedRoute.snapshot.paramMap.get('employeeId');

    this.getAll();
  }

  getAll(): void {
    this.publicationDataService.get(this.employeeId).subscribe(e => {
      this.publications = JSON.parse(JSON.stringify(e));
    })
  }

  edit(p: Publication) {
    this.publication = p;
    this.tableMode = false;
  }

  save() {
    console.log(this.publication);
    if (this.publication.id == null) {
      this.publicationDataService.add(this.publication)
        .subscribe((data: Publication) => this.publications.push(data));
    } else {
      this.publicationDataService.update(this.publication.id, this.publication)
        .subscribe(data => this.getAll());
    }
    this.cancel();
  }


  cancel() {
    this.publication = new Publication();
    this.publication.employeeId = this.employeeId;
    this.tableMode = true;
  }

  add() {
    this.cancel();
    this.tableMode = false;
  }
}

