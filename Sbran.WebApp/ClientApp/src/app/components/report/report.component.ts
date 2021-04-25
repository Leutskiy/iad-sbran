import { OnInit, Input, Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Appendix, ListOfScientist, Report, ReportType } from '../../contracts/login-data';
import { AuthService } from '../../services/auth.service';
import { ReportDataService } from '../../services/component-providers/report/report-data.service';

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
  isNew: boolean = true;
  createScientistflag: boolean = false;
  createFileflag: boolean = false;
  @Input() title: string;
  @Input() report: Report;
  scientist: ListOfScientist;
  appendix: Appendix;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private authService: AuthService,
    private reportDataService: ReportDataService) {
    this.report = new Report();
    this.appendix = new Appendix();
    this.scientist = new ListOfScientist();
  }

  ngOnInit(): void {
    this.departureId = this.activatedRoute.snapshot.paramMap.get('departureId');
    this.invitationId = this.activatedRoute.snapshot.paramMap.get('invitationId');
    this.reportId = this.activatedRoute.snapshot.paramMap.get('reportId');
    this.profileId = this.activatedRoute.snapshot.paramMap.get('profileId');
    this.employeeId = this.activatedRoute.snapshot.paramMap.get('employeeId');

    if (this.reportId != null) {
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
    this.reportDataService.get(this.reportId).subscribe(report => {
      this.report.id = report.id;
      this.report.mainPart = report.mainPart;
      this.report.findings = report.findings;
      this.report.suggestion = report.suggestion;
      this.report.foreignInterest = report.foreignInterest;
      this.report.reportType = report.reportType;
      this.report.parentId = report.parentId;
      this.report.status = report.status;
      this.report.appendix = report.appendix;
      this.report.listOfScientists = report.listOfScientists;
    })
  }

  createFile(): void {
    this.isNew = false;
    this.createFileflag = true;
    this.appendix = new Appendix();
  }

  createScientistTrue(): void {
    this.isNew = false;
    this.createScientistflag = true;
    this.scientist = new ListOfScientist();
    this.scientist.type = true;
  }

  createScientistFalse(): void {
    this.isNew = false;
    this.createScientistflag = true;
    this.scientist = new ListOfScientist();
    this.scientist.type = false;
  }

  cancel(): void {
    this.isNew = true;
    this.createScientistflag = false;
    this.createFileflag = false;
    this.appendix = new Appendix();
    this.scientist = new ListOfScientist();
  }

  save() {
    console.log(this.report);
    if (this.report.id == null) {
      this.reportDataService.add(this.report)
        .subscribe((data: Report) => this.report = JSON.parse(JSON.stringify(data)));
    } else {
      this.reportDataService.update(this.report.id, this.report)
        .subscribe(data => this.get());
    }
  }

  agree() {
    console.log(this.report);
    this.reportDataService.agree(this.report.id)
      .subscribe(data => this.get());
  }

  saveFile() {
    console.log(this.report.appendix);
    console.log(this.appendix);
    if (this.report.appendix === null) {
      this.report.appendix = [];
    }
    this.report.appendix.push(this.appendix);
    console.log(this.report.appendix);
    this.cancel();
  }

  saveScientist() {
    console.log(this.report.listOfScientists);
    console.log(this.scientist);
    if (this.report.listOfScientists === null) {
      this.report.listOfScientists = [];
    }
    this.report.listOfScientists.push(this.scientist);
    console.log(this.report.listOfScientists);
    this.cancel();
  }

  deleteFile(index: number) {
    console.log(index);
    this.report.appendix.splice(index, 1);
  }

  deleteScientist(index: number) {
    console.log(index);
    this.report.listOfScientists.splice(index, 1);
  }

  public fileChange(event) {
    let fileList: FileList = event.target.files;
    if (fileList.length > 0 && fileList.length < 2) {
      //console.log("sendFile");
      let me = this;
      let file: File = fileList[0];
      let reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = function () {
        var b64: string = typeof reader.result === 'string' ? reader.result : Buffer.from(reader.result).toString();
        b64 = b64.substr(b64.indexOf(',') + 1);
        //console.log(b64);
        me.appendix.fileBinary = b64;
        me.appendix.fileName = file.name;
      }
    }
  }
}

