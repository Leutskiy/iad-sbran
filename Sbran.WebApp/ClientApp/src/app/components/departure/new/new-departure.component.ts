import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DateHelper } from '../../../common/helpers/DateHelper';
import { Departure, DepartureStatus } from '../../../contracts/login-data';
import { DepartureDataService } from '../../../services/component-providers/departure/departure-data.service';

// TODO: назвать просто form-departure.component
@Component({
  selector: 'app-new-departure',
  templateUrl: './new-departure.component.html',
  styleUrls: ['./new-departure.component.scss'],
  providers: [DepartureDataService, DateHelper]
})
export class NewDepartureComponent implements OnInit {

  isNew: boolean;
  profileId: string;
  employeeId: string;

  reportId: string | null = null;

  departure: Departure | null = null;
  departureId: string | null = null;

  departureListUrl: string;
  departureBaseUrl: string;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private dateHelper: DateHelper,
    private departureService: DepartureDataService) { }

  ngOnInit(): void {
    // получаем параметры из адресной строки браузера
    this.profileId = this.activatedRoute.snapshot.paramMap.get('profileId');
    this.employeeId = this.activatedRoute.snapshot.paramMap.get('employeeId');
    this.departureId = this.activatedRoute.snapshot.paramMap.get('departureId');

    // NOTE! departureId может быть ''(""), null, undefined, если не задана, но надо только null
    // NOTE! просто, если поменяется сигнатура метода get, то может сломаться!
    // NOTE! this.departureId === null, по факту
    this.isNew = !this.departureId; 

    // фломируем набор url адресов
    let baseUrlSegmentArray = this.activatedRoute.snapshot.url.slice(0, 5);
    this.departureBaseUrl = `${baseUrlSegmentArray.join('/')}`;
    this.departureListUrl = `${this.departureBaseUrl}/list`;

    if (!this.isNew) {
      console.info(`>>> перешли на форму редактирования, так как departureId равен ${this.departureId};`);
      this.fillForm();
    }
    else {
      console.info(`>>> перешли на форму создания нового приглашения, так как departureId равен ${this.departureId};`);
      this.departure = new Departure();
    }
  }

  // обработчик нажатия кнопки Согласовать
  public agree(departureId: string): void {
    this.departureService.agree(departureId).subscribe(
      success => {
        console.info(`>>> выезд c departureId равным ${departureId} был успешно согласован;`);
        this.departure.departureStatus = DepartureStatus.Agreement;
      },
      error => {
        console.error(`>>> согласование выезда c departureId равным ${departureId} завершилось ошибкой ${JSON.stringify(error)}`);
      });
  }

  // обработчик нажатия кнопки Сохранить
  public save(): void {
    // TODO: костыль
    this.departure.employeeId = this.employeeId;

    if (this.isNew) {
      this.add(this.departure);
    } else {
      this.update(this.departure);
    }
  }

  // обработчик нажатия кнопки Отменить
  public cancel(): void {
    this.backward();
  }

  // перейти на предыдущую страницу
  public backward(): void {
    this.router.navigate([this.departureListUrl]);
  }

  // TODO: надо полноценное DTO, так как id не передается при добавлении!
  // TODO: И еще нельзя менять формат дату у компонента!
  // TODO: Идентификатор сотрудника не должен быть в домене!
  private add(departure: Departure): void {
    // формируем новый выезд для добавления
    var newDepartureDto = departure;
    newDepartureDto.dateStart = this.dateHelper.formatDateForBack(this.departure.dateStart);
    newDepartureDto.dateEnd = this.dateHelper.formatDateForBack(this.departure.dateEnd);

    this.departureService.add(newDepartureDto).subscribe(
      success => {
        console.info(`>>> выезд успешно добавлен для departureId равным ${this.departureId};`);
        // переход назад здесь, но это надо исправить по-нормальному
        this.backward();
      },
      error => {
        console.error(`>>> добавление выезда с идентификаторм ${this.departureId} завершилось ошибкой;`);
      }
    );
  }

  // TODO: надо полноценное DTO!
  private update(departure: Departure): void {
    // форматируем даты
    let updatedDeparture = departure;
    updatedDeparture.dateStart = this.dateHelper.formatDateForBack(departure.dateStart);
    updatedDeparture.dateEnd = this.dateHelper.formatDateForBack(departure.dateEnd);

    this.departureService.update(departure.id, updatedDeparture).subscribe(
      success => {
        console.info(`>>> выезд успешно обновлен для departureId равным ${this.departureId};`);
        // переход назад здесь, но это надо исправить по-нормальному
        this.backward();
      },
      error => {
        console.error(`>>> обновление выезда с идентификаторм ${this.departureId} завершилось ошибкой;`);
      }
    );
  }

  // заполнить форму с выездом по идентификатору
  private fillForm(): void {
    this.departureService.getDepartureById(this.departureId).subscribe(
      (_departure: Departure) => {
        // если задана дата "с", то форматируем
        if (_departure.dateStart) {
          let dateStart = new Date(_departure.dateStart);
          _departure.dateStart = dateStart;
        }

        // если задана дата "по", то форматируем
        if (_departure.dateEnd) {
          let dateEnd = new Date(_departure.dateEnd);
          _departure.dateEnd = this.dateHelper.formatDateForFront(dateEnd);
        }

        this.departure = _departure;
      },
      error => {
        console.log(`${error}`);
      }
    );
  }
}
