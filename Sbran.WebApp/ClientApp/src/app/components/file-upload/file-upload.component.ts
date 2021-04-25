import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup } from "@angular/forms";

/*[(avatar)]="profile.avatar"*/
@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.scss']
})
export class FileUploadComponent implements OnInit {
  imageURL: string;

  @Input() avatar: any[];
  @Input() editable: boolean;
  @Output() avatarChange = new EventEmitter<any>();

  constructor() {
    this.avatar = [];
  }

  ngOnInit(): void { }

  processAvatar(event): void {

    const file = (event.target as HTMLInputElement).files[0];

    let imageBytes = [];

    let reader = new FileReader();
    reader.onload = function() {
      let arrayBuffer = reader.result as ArrayBuffer;
      let array = new Uint8Array(arrayBuffer);
      for (let index = 0; index < array.length; index++) {
        imageBytes.push(array[index]);
      }
    }
    reader.readAsArrayBuffer(file);

    this.avatar = imageBytes;
    this.avatarChange.emit(imageBytes);
  }

  // Image Preview
  showPreview(event) {
    const file = (event.target as HTMLInputElement).files[0];

    // File Preview
    const reader = new FileReader();
    reader.onload = () => {
      this.imageURL = reader.result as string;
    }
    reader.readAsDataURL(file)
  }

}
