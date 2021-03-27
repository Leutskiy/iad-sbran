import { OnInit, Input, Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Report, ReportType } from '../../contracts/login-data';
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
  @Input() title: string;
  @Input() report: Report;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private authService: AuthService,
    private reportDataService: ReportDataService) {
    this.report = new Report();
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
      this.report.description = report.description;
      this.report.fileBinary = report.fileBinary;
      this.report.fileName = report.fileName;
      this.report.reportType = report.reportType;
      this.report.parentId = report.parentId;
      this.report.parentId = report.parentId;
      this.report.appendixId = report.appendixId;
    })
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
        me.report.fileBinary = b64;
        me.report.fileName = file.name;
      }
    }

  }
}

