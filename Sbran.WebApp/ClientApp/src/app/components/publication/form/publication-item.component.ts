import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Publication } from '../../../contracts/login-data';
import { PublicationDataService } from '../../../services/component-providers/publication/publication-data.service';

// TODO: назвать просто publication-form.component
@Component({
  selector: 'app-publication-item',
  templateUrl: './publication-item.component.html',
  styleUrls: ['./publication-item.component.css'],
  providers: [PublicationDataService]
})
export class PublicationItemComponent implements OnInit {

  isNew: boolean;
  profileId: string;
  employeeId: string;
  publicationId: string;
  publication: Publication;

  publicationListUrl: string;
  publicationBaseUrl: string;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private publicationService: PublicationDataService) { }

  ngOnInit(): void {
    // получаем параметры из адресной строки браузера
    this.profileId = this.activatedRoute.snapshot.paramMap.get('profileId');
    this.employeeId = this.activatedRoute.snapshot.paramMap.get('employeeId');
    this.publicationId = this.activatedRoute.snapshot.paramMap.get('publicationId');

    this.isNew = !this.publicationId;

    // фломируем набор url адресов
    let baseUrlSegmentArray = this.activatedRoute.snapshot.url.slice(0, 5);
    this.publicationBaseUrl = `${baseUrlSegmentArray.join('/')}`;
    this.publicationListUrl = `${this.publicationBaseUrl}/list`;

    this.publication = new Publication();
    if (!this.isNew) {
      console.info(`>>> перешли на форму редактирования публикации с идентификатором равным ${this.publicationId}`);
      this.fillForm();
    }
    else {
      console.info(`>>> перешли на форму создания новой публикации`);
    }
  }

  // заполнить форму с выездом по идентификатору
  private fillForm(): void {
    this.publicationService.getPublicationById(this.publicationId).subscribe(
      (_publication: Publication) => {
        console.log(`>>> данные для публикации с ${this.publicationId} успешно получены`);
        this.publication = _publication;
      },
      _error => {
        console.log(`>>> получение данных для публикации с ${this.publicationId} завершилось ошибкой: ${_error}`);
      }
    );
  }

  public save(): void {

    console.info(`>>> нажата кнопка Сохранить для сохранения публикации`);
    this.publication.employeeId = this.employeeId; // TODO: костыль
    this.isNew ? this.add(this.publication) : this.update(this.publication);
  }

  // TODO: надо полноценное DTO, так как id не передается при добавлении!
  // TODO: И еще нельзя менять формат дату у компонента!
  // TODO: Идентификатор сотрудника не должен быть в домене!
  private add(publication: Publication): void {
    // формируем новую публикацию для добавления/обновления
    let newPublicationDto = publication;
    this.publicationService.add(newPublicationDto).subscribe(
      _success => {
        console.info(`>>> публикация c ${this.publicationId} успешно добавлена`);
        // переход назад здесь, но это надо исправить по-нормальному
        this.backward();
      },
      _error => {
        console.error(`>>> добавление публикации с идентификаторм ${this.publicationId} завершилось ошибкой`);
      }
    );
  }

  // TODO: надо полноценное DTO!
  private update(publication: Publication): void {

    console.info(`>>> начинаем обновление публикации`);
    let updatedPublicationDto = publication;
    this.publicationService.update(publication.id, updatedPublicationDto).subscribe(
      _success => {
        console.info(`>>> публикация с ${this.publicationId} успешно обновлена`);
        // переход назад здесь, но это надо исправить по-нормальному
        this.backward();
      },
      _error => {
        console.error(`>>> обновление публикации с идентификаторм ${this.publicationId} завершилось ошибкой`);
      }
    );
  }

  // обработчик нажатия кнопки Отменить
  public cancel(): void {
    this.backward();
  }

  // перейти на предыдущую страницу
  public backward(): void {
    this.router.navigate([this.publicationListUrl]);
  }
}
