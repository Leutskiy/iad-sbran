import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SingupService } from '../../services/singup.service';

@Component({
  selector: 'app-singup',
  templateUrl: './singup.component.html',
  styleUrls: ['./singup.component.scss']
})
export class SingupComponent {

  errorMessage: string;

  constructor(
    private singupService: SingupService,
    private router: Router) {
  }

  public register(login: string, password: string, confirmPassword: string): void {
    let data = {
      login: login,
      password: password,
      confirmPassword: confirmPassword
    }

    this.singupService.register(data).subscribe(
      response => {
        console.log(response);
        this.router.navigateByUrl("/");
      },
      error => {
        console.log(error);
        this.errorMessage = error.error_description;
      }
    );
  }
}
