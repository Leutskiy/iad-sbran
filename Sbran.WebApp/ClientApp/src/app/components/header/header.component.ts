import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import * as signalR from '@microsoft/signalr';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  width: number;

  logoAlt: string = "СИГМА";
  logoTitle: string = "Cибирское отделение РАН ";
  logoRelativePath: string = "assets/images/sum.svg";
  private hubConnection: signalR.HubConnection;
  accessToken: string;
  profileId: string;
  employeeId: string;

  showToMainPageButton: boolean;

  constructor(
    private router: Router,
    public authService: AuthService) {
  }

  ngOnInit(): void {

    this.width = 0;

    this.showToMainPageButton = true;

    this.authService.profileId.subscribe(e => {
      this.profileId = e;
    });
    this.authService.employeeId.subscribe(e => {
      this.employeeId = e;
    });

    this.accessToken = this.authService.getToken();
    this.hubConnection = new signalR.HubConnectionBuilder()
      //конфигурации на сервере 
      .configureLogging(signalR.LogLevel.Error)
      .withUrl("https://localhost:5001/chatsocket", {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
        accessTokenFactory: () => this.accessToken
      })
      .build();
  }

  onLogout() {
    this.authService.logout();
    this.router.navigate(['/']);

    // начинаем соединение с хабом
    this.hubConnection.stop();
  }

  onGoToPanelInfo() {
    this.router.navigate(['panel/info']);
  }

  openNav(): any {
    this.width === 0 ? this.width = 250 : this.width = 0;
    //let style = document.getElementById("mySidenav").style;
    //style.width === "0px" ? style.width = "250px" : style.width = "0px";
  }

  closeNav(): any {
    this.width = 0;
    //document.getElementById("mySidenav").style.width = "0px";
  }
}
