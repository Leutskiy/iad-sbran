import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  logoAlt: string = "СИГМА";
  logoTitle: string = "Cибирское отделение РАН ";
  logoRelativePath: string = "assets/images/sum.svg";

  profileId: Observable<string>;
  employeeId: Observable<string>;

  constructor(
    public authService: AuthService,
    private router: Router) {
  }

  ngOnInit(): void {
    this.profileId = this.authService.profileId;
    this.employeeId = this.authService.employeeId;
  }

  onLogout() {
    this.authService.logout();
    this.router.navigate(['/']);
  }
}
