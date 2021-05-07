import { OnInit, Input, Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Appendix, ListOfScientist, Report, ReportType } from '../../contracts/login-data';
import { AuthService } from '../../services/auth.service';
import { ReportDataService } from '../../services/component-providers/report/report-data.service';
import * as uuid from 'uuid';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.scss'],
  providers: [ReportDataService, AuthService]
})
export class ReportComponent implements OnInit {

  profileId: string;
  employeeId: string;
  departureId: string;
  invitationId: string;
  reportId: string;
  isMainReportForm: boolean = true;
  createScientistflag: boolean = false;
  createFileflag: boolean = false;
  forManager: boolean = false;
  @Input() title: string;
  @Input() report: Report;
  scientist: ListOfScientist;
  appendix: Appendix;
  isNew: boolean;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private authService: AuthService,
    private reportDataService: ReportDataService) {
    this.report = new Report();
    this.appendix = new Appendix();
    this.scientist = new ListOfScientist();
    this.forManager = authService.isManager;
  }

  ngOnInit(): void {
    this.profileId = this.activatedRoute.snapshot.paramMap.get('profileId');
    this.employeeId = this.activatedRoute.snapshot.paramMap.get('employeeId');
    this.departureId = this.activatedRoute.snapshot.paramMap.get('departureId');
    this.invitationId = this.activatedRoute.snapshot.paramMap.get('invitationId');
    this.reportId = this.activatedRoute.snapshot.paramMap.get('reportId');

    this.isNew = !this.reportId

    if (!this.isNew) {
      this.get();
    }

    if (this.departureId != null) {
      this.report.reportType = ReportType.Departure;
      this.report.parentId = this.departureId;
    }

    if (this.invitationId != null) {
      this.report.reportType = ReportType.Invition;
      this.report.parentId = this.invitationId;
    }
  }

  get(): void {
    this.reportDataService.get(this.reportId).subscribe((report: Report) => {
      this.report.id = report.id;
      this.report.mainPart = report.mainPart;
      this.report.findings = report.findings;
      this.report.suggestion = report.suggestion;
      this.report.foreignInterest = report.foreignInterest;
      this.report.status = report.status;
      this.report.reportType = report.reportType;
      this.report.parentId = report.parentId;
      this.report.appendix = report.appendix;
      this.report.listOfScientists = report.listOfScientists;
    })
  }

  createFile(): void {
    this.isMainReportForm = false;
    this.createFileflag = true;
    this.appendix = new Appendix();
  }

  createScientist(type: boolean): void {
    this.isMainReportForm = false;
    this.createScientistflag = true;
    this.scientist = new ListOfScientist();
    this.scientist.id = uuid.v4();
    this.scientist.type = type;
  }

  cancel(): void {
    this.isMainReportForm = true;
    this.createScientistflag = false;
    this.createFileflag = false;
    this.appendix = new Appendix();
    this.scientist = new ListOfScientist();
  }

  save() {
    if (this.report.id == null) {
      this.reportDataService.add(this.report)
        .subscribe((data: Report) => {
          this.report = JSON.parse(JSON.stringify(data));
          this.router.navigate([this.activatedRoute.snapshot.url.join("/"), `${this.report.id}`]);
        });
    } else {
      this.reportDataService.update(this.report.id, this.report)
        .subscribe(data => this.get());
    }
  }

  agree() {
    this.reportDataService.agree(this.report.id)
      .subscribe(data => this.get());
  }

  public saveFile(): void {
    if (!this.report.appendix) {
      this.report.appendix = [];
    }

    this.report.appendix.push(this.appendix);
    this.cancel();
  }

  saveScientist() {
    this.report.listOfScientists.push(this.scientist);
    this.cancel();
  }

  public getScientistsAsTheSameIssueResolvers(): ListOfScientist[] {
    return this.report.listOfScientists.filter((val, idx, arr) => { return val.type; });
  }

  public getScientistsAsResearchParticipants(): ListOfScientist[] {
    return this.report.listOfScientists.filter((val, idx, arr) => { return !val.type; });
  }

  deleteFile(index: number) {
    this.report.appendix.splice(index, 1);
  }

  public deleteScientist(item: ListOfScientist): void {
    this.report.listOfScientists.forEach((value, index) => {
      if (value.id == item.id) {
        delete this.report.listOfScientists[index];
      }
    });
  }

  public fileChange(event) {
    let fileList: FileList = event.target.files;

    if (fileList.length > 0 && fileList.length < 2) {
      let me = this;
      let file: File = fileList[0];
      let reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = function () {
        var b64: string = typeof reader.result === 'string' ? reader.result : Buffer.from(reader.result).toString();
        b64 = b64.substr(b64.indexOf(',') + 1);
        me.appendix.fileBinary = b64;
        me.appendix.fileName = file.name;
      }
    }
  }
}
