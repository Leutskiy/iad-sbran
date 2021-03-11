import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import * as signalR from '@microsoft/signalr';

declare var $: any;

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss'],
  providers: [AuthService]
})

export class ChatComponent implements OnInit {

  errorMessage: string;
  accessToken: string;
  private hubConnection: signalR.HubConnection;
  profileId: string;
  employeeId: string;
  receiver: string;
  chatDataJson: any;
  selectedUserid: string;

  constructor(
    private router: Router,
    private authService: AuthService) {

    this.chatDataJson = JSON.parse(`[{ 
    "userid": "alex@mail.ru",
      "image": "../../../assets/images/profilephotos/alex.jpg",
        "userfullname": "Алексей Богданов",
          "lastmessagedate": "11:30",
            "lastmessage": "Вы: Именно поэтому..."
},
{
  "userid": "alisa@mail.ru",
    "image": "../../../assets/images/profilephotos/alisa.jpg",
      "userfullname": "Алиса Буковская",
        "lastmessagedate": "Вчера",
          "lastmessage": "Вы: Отличная работа."
},
{
  "userid": "lora@mail.ru",
    "image": "../../../assets/images/profilephotos/lora.jpg",
      "userfullname": "Лора Кузова",
        "lastmessagedate": "Вчера",
          "lastmessage": "Встречу перенесли на 11-го."
},
{
  "userid": "nastya@mail.ru",
    "image": "../../../assets/images/profilephotos/nastya.jpg",
      "userfullname": "Настя Трофимова",
        "lastmessagedate": "03.02.2021",
          "lastmessage": "Я отправил вам отчёты"
},
{
  "userid": "tom@mail.ru",
    "image": "../../../assets/images/profilephotos/tom.jpg",
      "userfullname": "Рахаб Магамедов",
        "lastmessagedate": "25.01.2021",
          "lastmessage": "Презентация была отличной."
}]`);


  }

  ngOnInit(): void {

    let user = JSON.parse(localStorage.getItem('user'));
    this.profileId = user.profileId;
    this.employeeId = user.employeeId;

    this.accessToken = this.authService.getToken();

    this.hubConnection = new signalR.HubConnectionBuilder()
      .configureLogging(signalR.LogLevel.Debug)
      .withUrl("https://localhost:5001/chatsocket", {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
        accessTokenFactory: () => this.accessToken
      })
      .build();


    this.hubConnection.on("Receive", function (message, userName) {

      let image = "";
      if (userName == "alisa@mail.ru") image = "../../../assets/images/profilephotos/alisa.jpg";
      if (userName == "lora@mail.ru") image = "../../../assets/images/profilephotos/lora.jpg";
      if (userName == "tom@mail.ru") image = "../../../assets/images/profilephotos/tom.jpg";
      if (userName == "nastya@mail.ru") image = "../../../assets/images/profilephotos/nastya.jpg";
      if (userName == "alex@mail.ru") image = "../../../assets/images/profilephotos/alex.jpg";


      // создаем элемент <b> для имени пользователя
      let firstDiv = document.createElement("div");
      let img = document.createElement("img");
      let secondDiv = document.createElement("div");
      let thirdDiv = document.createElement("div");
      let firstP = document.createElement("p");
      let secondP = document.createElement("p");
      // значения
      firstP.appendChild(document.createTextNode(message));
      secondP.appendChild(document.createTextNode(Date()));
      img.src = image;
      img.width = 35;
      img.height = 35;

      // стиль
      thirdDiv.className = "bg-light rounded py-2 px-3 mb-2";
      img.className = "rounded-circle";
      img.style.objectFit = "cover";
      firstP.className = "text-small mb-0 text-muted";
      secondP.className = "small text-muted";
      secondP.style.textAlign = "end";
      secondDiv.className = "media-body ml-1";
      firstDiv.className = "media w-100 mb-3";

      /* структура
      div
        div
          div
            p
          p
      */
      thirdDiv.appendChild(firstP);
      secondDiv.appendChild(thirdDiv);
      secondDiv.appendChild(secondP);
      firstDiv.appendChild(img);
      firstDiv.appendChild(secondDiv);

      // создает элемент <p> для сообщения пользователя
      //let elem = document.createElement("p");
      //elem.appendChild(firstDiv);
      //elem.appendChild(document.createTextNode(message));

      document.getElementById("chatroom").appendChild(firstDiv);
    });


    this.hubConnection.on("Notify", function (message) {

      //// создает элемент <p> для сообщения пользователя
      //let elem = document.createElement("p");
      //elem.appendChild(document.createTextNode(message));

      //var firstElem = document.getElementById("chatroom").firstChild;
      //document.getElementById("chatroom").insertBefore(elem, firstElem);
    });


    // начинаем соединение с хабом
    this.hubConnection.start();
  }

  public sendMessage(message: string): void {
    // отправка сообщения на сервер
    this.hubConnection.invoke("Send", message, this.receiver);
  }

  public chatUserSelected(userid: string) {
    console.log(userid);

    this.receiver = userid;
    this.selectedUserid = userid;

    document.getElementById("chatroom").innerHTML = '';

  }
}
