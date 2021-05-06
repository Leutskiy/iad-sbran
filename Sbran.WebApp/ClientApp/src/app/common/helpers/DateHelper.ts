export class DateHelper {

  public formatDateForFront(dateSource: Date | string | null): string | null {

    if (dateSource instanceof Date) {
      return this.parseDate(dateSource);
    } else if (dateSource) {
      return this.parseDate(new Date(dateSource));
    }

    return null;
  }

  // Используем для форматирования перед передачей на сервер
  // TODO: Сделать стандартизацию формата даты (подходящий ISO)
  // TODO: Отформатировать дату (привести к правильному формату)
  public formatDateForBack(dateSource: Date | string | null): Date | null {

    if (dateSource instanceof Date) {
      return dateSource;
    } else if (dateSource) {
      return new Date(this.reformatDate(dateSource));
    }

    return null;
  }

  private reformatDate(dateString: string | null): string | null {
    if (dateString) {
      let date = dateString.split(".");
      return date[2] + "-" + date[1] + "-" + date[0];
    }

    return null;
  }

  private parseDate(dateSource: Date | null): string | null {

    let date = {
      day: dateSource.getDate(),
      month: dateSource.getMonth() + 1,
      year: dateSource.getFullYear()
    };

    let dateDay = `${date.day}`;
    let dateYear = `${date.year}`;
    let dateMonth = `${date.month}`;

    dateDay = date.day < 10 ? 0 + dateDay : dateDay;
    dateMonth = date.month < 10 ? 0 + dateMonth : dateMonth;

    return `${dateDay}.${dateMonth}.${dateYear}`;
  }
}
