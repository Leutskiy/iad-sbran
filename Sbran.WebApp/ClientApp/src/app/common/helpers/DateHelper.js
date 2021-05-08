"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.DateHelper = void 0;
var DateHelper = /** @class */ (function () {
    function DateHelper() {
    }
    DateHelper.prototype.formatDateForFront = function (dateSource) {
        if (dateSource instanceof Date) {
            return this.parseDate(dateSource);
        }
        else if (dateSource) {
            return this.parseDate(new Date(dateSource));
        }
        return null;
    };
    DateHelper.prototype.formatDateForChat = function (dateSource) {
        if (dateSource instanceof Date) {
            return this.parseDate(dateSource) + " " + this.parseTime(dateSource);
        }
        else if (dateSource) {
            var dateSourceAsDate = new Date(dateSource);
            return this.parseDate(dateSourceAsDate) + " " + this.parseTime(dateSourceAsDate);
            ;
        }
        return null;
    };
    // Используем для форматирования перед передачей на сервер
    // TODO: Сделать стандартизацию формата даты (подходящий ISO)
    // TODO: Отформатировать дату (привести к правильному формату)
    DateHelper.prototype.formatDateForBack = function (dateSource) {
        if (dateSource instanceof Date) {
            return dateSource;
        }
        else if (dateSource) {
            return new Date(this.reformatDate(dateSource));
        }
        return null;
    };
    DateHelper.prototype.reformatDate = function (dateString) {
        if (dateString) {
            var date = dateString.split(".");
            return date[2] + "-" + date[1] + "-" + date[0];
        }
        return null;
    };
    DateHelper.prototype.parseDate = function (dateSource) {
        var date = {
            day: dateSource.getDate(),
            month: dateSource.getMonth() + 1,
            year: dateSource.getFullYear()
        };
        var dateDay = "" + date.day;
        var dateYear = "" + date.year;
        var dateMonth = "" + date.month;
        dateDay = date.day < 10 ? 0 + dateDay : dateDay;
        dateMonth = date.month < 10 ? 0 + dateMonth : dateMonth;
        return dateDay + "." + dateMonth + "." + dateYear;
    };
    DateHelper.prototype.parseTime = function (dateSource) {
        return ("0" + dateSource.getHours()).slice(-2) + ":" +
            ("0" + dateSource.getMinutes()).slice(-2) + ":" +
            ("0" + dateSource.getSeconds()).slice(-2);
    };
    return DateHelper;
}());
exports.DateHelper = DateHelper;
//# sourceMappingURL=DateHelper.js.map