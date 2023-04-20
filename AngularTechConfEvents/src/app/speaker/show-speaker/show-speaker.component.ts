import { Component, OnInit } from '@angular/core';
import { ApiserviceService } from '../../apiservice.service';

@Component({
  selector: 'app-show-speaker',
  templateUrl: './show-speaker.component.html',
  styleUrls: ['./show-speaker.component.css']
})
export class ShowSpeakerComponent implements OnInit {

  constructor(private service: ApiserviceService) { }

  SpeakerList: any = [];
  ModalTitle = "";
  ActivateAddEditSpeakerComp: boolean = false;
  speaker: any;

  ngOnInit(): void {
    this.refreshEmpList();
  }

  addClick() {
    this.speaker = {
      SpeakerId: null,
      Name: "",
      Biography: "",
      SocialLinks: ""
    }
    this.ModalTitle = "Add Speaker";
    this.ActivateAddEditSpeakerComp = true;
  }

  editClick(item: any) {
    this.speaker = item;
    this.ModalTitle = "Edit Speaker";
    this.ActivateAddEditSpeakerComp = true;
  }

  deleteClick(item: any) {
    if (confirm('Are you sure??')) {
      this.service.deleteSpeaker(item.speakerId).subscribe(data => {
        alert("Successfully Deleted");
        this.refreshEmpList();
      })
    }
  }

  closeClick() {
    this.ActivateAddEditSpeakerComp = false;
    this.refreshEmpList();
  }

  refreshEmpList() {
    this.service.getSpeakerList().subscribe(data => {
      this.SpeakerList = data;
    });
  }
}
