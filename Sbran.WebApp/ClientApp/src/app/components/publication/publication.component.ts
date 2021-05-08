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

  @Input() title: string;

  profileId: string;
  employeeId: string;

  publications = [];
  publicationListUrl: string;
  publicationFormUrl: string;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private publicationService: PublicationDataService) { }

  ngOnInit(): void {
    this.profileId = this.activatedRoute.snapshot.paramMap.get('profileId');
    this.employeeId = this.activatedRoute.snapshot.paramMap.get('employeeId');

    let urlSegmentArray = this.activatedRoute.snapshot.url;
    this.publicationListUrl = urlSegmentArray.join('/');
    this.publicationFormUrl = `${urlSegmentArray.slice(0, urlSegmentArray.length - 1).join('/')}`;

    this.refreshPublicationTable();
  }

  // обновить таблицу с публикациями
  private refreshPublicationTable(): void {

    console.info(`>>> начинаем получение списка публикаций`);
    this.publicationService.getAll(this.employeeId).subscribe(
      _publications => {
        console.info(`>>> список публикаций успешно получен:`);
        console.table(_publications);

        _publications.forEach((publication: Publication, idx: number, arr: Publication[]) => {
          this.publications.push(publication);
        });

        // obsolete
        // this.publications = JSON.parse(JSON.stringify(e));
      },
      _error => {
        // JSON.stringify(JSON.parse(JSON.stringify(_error)), null, 2)
        // http://jsfiddle.net/KJQ9K/554/
        console.error(`>>> во время получения списка публикаций проищошла ошибка: ${JSON.stringify(_error)}`);
      });
  }

  // перейти на форму добавления новой публикации
  public add(): void {

    console.info(`>>> нажата кнопка Новая для создания новой публикации`);
    this.router.navigate([this.publicationFormUrl]).then(
      _success => {
        console.info(`>>> открыта форма для создания новой публикации`);
      },
      _error => {
        console.error(`>>> открытие формы для созданий новой публикации завершилось ошибкой:`);
        console.error(`>>> ${JSON.stringify(_error)}`);
      }
    );
  }

  // перейти на форму редактирования существующей публикации
  public edit(publication: Publication): void {

    console.info(`>>> нажата кнопка Редактировать для редактирования публикации`);
    this.router.navigate([this.publicationFormUrl, `${publication.id}`]).then(
      _success => {
        console.info(`>>> форма редактирования публикации успешно открыта`);
      },
      _error => {
        console.error(`>>> открытие формы редактирования публикации завершилось ошибкой: ${_error}`);
      }
    );
  }
}
