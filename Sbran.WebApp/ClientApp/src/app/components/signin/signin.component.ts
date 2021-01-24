import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.scss']
})
export class SigninComponent {

  errorMessage: string;

  constructor(
    private router: Router,
    private authService: AuthService) {
  }

  login(login: string, password: string) {
    this.authService.login(login, password).subscribe(
      accountDetailsResult => {
        this.router.navigateByUrl(`/profile/${accountDetailsResult.profileId}/employee/${accountDetailsResult.employeeId}`);
      },
      error => {
        console.log(error);
        this.errorMessage = error.error_description;
      }
    );
  }
}
