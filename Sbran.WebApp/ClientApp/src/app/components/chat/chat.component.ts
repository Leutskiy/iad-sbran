import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ChatService } from '../../services/component-providers/chat/chat.service';
import { ChatRoomsInfo, Message, MyMessagesInRoom, User } from '../../contracts/login-data';
import * as signalR from '@microsoft/signalr';
import { Target } from '@angular/compiler';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { timer } from 'rxjs';
import { read } from 'fs';

declare var $: any;

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss'],
  providers: [ChatService, AuthService]
})

export class ChatComponent implements OnInit {

  errorMessage: string;
  accessToken: string;
  private hubConnection: signalR.HubConnection;
  profileId: string;
  employeeId: string;
  search: string;
  receiver: string;
  chatRoomId: string;
  chatDataJson: any[];
  messages: any[];
  selectedUserid: string;
  message: Message;
  messageSearch: string;
  @ViewChild('searchInput') searchInput: ElementRef;

  constructor(
    private activatedRoute: ActivatedRoute,
    private chatService: ChatService,
    private authService: AuthService) {
    this.message = new Message();
    this.chatDataJson = new Array(ChatRoomsInfo);
    this.search = "";
    this.messageSearch = "";
    this.messages = new Array(MyMessagesInRoom);
    // this.subscribeToEvents();
  }

  ngOnInit(): void {

    this.profileId = this.activatedRoute.snapshot.paramMap.get('profileId');
    this.employeeId = this.activatedRoute.snapshot.paramMap.get('employeeId');

    this.getMyChats(this.profileId);

    this.accessToken = this.authService.getToken();

    //console.log($.fn.jquery);

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


    this.hubConnection.on("Receive", (message, userName, chatRoomId) => {
      let element = new MyMessagesInRoom();
      element = JSON.parse(JSON.stringify(message));
      //console.log("profileId " + element.profileId);
      //console.log("profileTo " + element.profileTo);
      //console.log("this.profileId " + this.profileId);
      //console.log("this.receiver " + this.receiver);
      //console.log("userName " + userName);
      if (userName == this.receiver && element.profileId == this.profileId) {
        this.messages.push(element);
      }

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
    if (this.receiver != null) {
      // отправка сообщения на сервер
      this.searchInput.nativeElement.value = '';
      var objDiv = document.getElementById("chatroom");
      objDiv.scrollTop = 10000;
      this.chatService.setDataByIdd(this.receiver, message, this.chatRoomId, this.profileId).subscribe(
        response => {
          console.log(response);
          this.hubConnection.invoke("Send", response, this.receiver);
        },
        error => {
          //console.log(error);
        }
      );
    }
  }

  public fileChange(event) {
    if (this.receiver != null) {
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
          me.chatService.sendFile(me.receiver, me.profileId, me.chatRoomId, b64, file.name).subscribe(
            response => {
              me.hubConnection.invoke("Send", response, me.receiver);
            },
            error => {
              console.log(error);
            });
        }
      }
    }
  }


  //private subscribeToEvents(): void {
  //  this.hubConnection.on("Receive", function (message, userName, chatRoomId) {
  //    let element = new MyMessagesInRoom();
  //    element = JSON.parse(JSON.stringify(message));
  //    console.log(element);
  //    this.messages.push(element);
  //    //this.chatService.getDataByMyChats(chatRoomId, this.receiver).subscribe(userInfoResult => {
  //    //  //console.log(userInfoResult);
  //    //  this.messages = JSON.parse(JSON.stringify(userInfoResult));
  //    //  console.log(this.messages);
  //    //});
  //  });
  //}

  public chatUserSelected(userid: string, chatRoomId: string) {

    //document.getElementById("chatroom").innerHTML = '';
    //console.log(userid + ";" + chatRoomId);
    this.receiver = userid;
    this.chatRoomId = chatRoomId;
    this.selectedUserid = userid;
    this.chatService.getDataByMyChats(chatRoomId, this.receiver).subscribe(userInfoResult => {
      console.log(userInfoResult);
      this.messages = JSON.parse(JSON.stringify(userInfoResult));
      //console.log(this.messages);
      var objDiv = document.getElementById("chatroom");
      objDiv.scrollTop = 10000;
    });

  }

  public getDataByMyChatsForName(userid: string, name: string) {
    //console.log(userid);
    //console.log(name);
    if (name != "") {
      this.chatService.getDataByMyChatsForName(userid, name).subscribe(userInfoResult => {
        this.chatDataJson = JSON.parse(JSON.stringify(userInfoResult));
        //console.log(this.messages);
      });
    }
  }

  public getFile(fileId: string) {
    return this.chatService.getFile(fileId);
  }


  private getMyChats(profileId: string): void {
    this.chatService.getDataById(profileId).subscribe(userInfoResult => {
      console.log(userInfoResult);
      this.chatDataJson = JSON.parse(JSON.stringify(userInfoResult));
      //console.log(this.chatDataJson);
    });
  }

  private _base64ToArrayBuffer(base64): any[] {
    let byteArray = [];
    let binary_string = window.atob(base64);
    let length = binary_string.length;
    let bytes = new Uint8Array(length);

    for (let index = 0; index < length; index++) {
      byteArray.push(binary_string.charCodeAt(index));
    }

    return byteArray;
  }

  private getBase64(file, onLoadCallback) {
    return new Promise(function (resolve, reject) {
      var reader = new FileReader();
      reader.onload = function () { resolve(reader.result); };
      reader.onerror = reject;
      reader.readAsDataURL(file);
    });
  }
}
