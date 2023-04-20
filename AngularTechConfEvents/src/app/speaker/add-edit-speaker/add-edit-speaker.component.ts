import { Component, Input, OnInit } from '@angular/core';
import { ApiserviceService } from '../../apiservice.service';

@Component({
  selector: 'app-add-edit-speaker',
  templateUrl: './add-edit-speaker.component.html',
  styleUrls: ['./add-edit-speaker.component.css']
})
export class AddEditSpeakerComponent implements OnInit {
  constructor(private service: ApiserviceService) { }
  @Input() speaker: any;
  speakerId = "";
  name = "";
  biography = "";
  socialLinks = "";
  
  //DepartmentList: any = [];


  ngOnInit(): void {
    this.speakerId = this.speaker.speakerId;
    this.name = this.speaker.name;
    this.socialLinks = this.speaker.socialLinks;
    this.biography = this.speaker.biography;   
  }

  addSpeaker() {
    var val = {
      speakerId: this.speakerId,
      name: this.name,
      socialLinks: this.socialLinks,
      biography: this.biography
    };

    this.service.addSpeaker(val).subscribe(res => {
      alert("Successfully Created!!!");
    });
  }

  updateSpeaker() {
    var val = {
      speakerId: this.speakerId,
      name: this.name,
      socialLinks: this.socialLinks,
      biography: this.biography
    };

    this.service.updateSpeaker(val).subscribe(res => {      
      alert("Successfully Updated!!!");
    });
  }


  // uploadPhoto(event: any) {
  //   var file = event.target.files[0];
  //   const formData: FormData = new FormData();
  //   formData.append('file', file, file.name);

  //   this.service.uploadPhoto(formData).subscribe((data: any) => {
  //     this.PhotoFileName = data.toString();
  //     this.PhotoFilePath = this.service.photoUrl + this.PhotoFileName;
  //   })
  // }

}
