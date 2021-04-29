import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import * as signalR from '@microsoft/signalr';
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
  private hubConnection: signalR.HubConnection;
  accessToken: string;
  profileId: string;
  employeeId: string;

  constructor(
    public authService: AuthService,
    private router: Router) {
  }

  ngOnInit(): void {
    this.authService.profileId.subscribe(e => {
      this.profileId = e;
    });
    this.authService.employeeId.subscribe(e => {
      this.employeeId = e;
    });

    this.accessToken = this.authService.getToken();
    this.hubConnection = new signalR.HubConnectionBuilder()
      //конфигурации на сервере 
      //.withUrl("https://localhost:5001/chatsocket", {  })
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

  openNav(): any {
    document.getElementById("mySidenav").style.width = "250px";
    // document.getElementById("main").style.marginLeft = "250px";
  }
  closeNav(): any {
    document.getElementById("mySidenav").style.width = "0";
    // document.getElementById("main").style.marginLeft = "0";
  }

}
