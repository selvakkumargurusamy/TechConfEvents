import { Component, OnInit } from '@angular/core';
import { ApiserviceService } from '../../apiservice.service';

@Component({
  selector: 'app-show-events',
  templateUrl: './show-events.component.html',
  styleUrls: ['./show-events.component.css']
})
export class ShowEventsComponent implements OnInit {

  constructor(private service: ApiserviceService) { }

  EventList: any = [];
  SpeakerList: any = [];
  ModalTitle = "";
  ActivateAddEditEventComp: boolean = false;
  events: any;

  EventIdFilter = "";
  EventTitleFilter = "";
  EventListWithoutFilter: any = [];

  ngOnInit(): void {
    this.refreshDepList();
  }

  addClick() {
    this.events = {
      eventId: null,
      eventTitle: ""
    }
    this.ModalTitle = "Add Event";
    this.ActivateAddEditEventComp = true;
  }

  editClick(item: any) {
    this.events = item;
    this.ModalTitle = "Edit Event";
    this.ActivateAddEditEventComp = true;
  }

  deleteClick(item: any) {
    if (confirm('Are you sure??')) {
      this.service.deleteEvent(item.eventId).subscribe(data => {
        alert("Deleted Sucessfully");
        this.refreshDepList();
      })
    }
  }

  closeClick() {
    this.ActivateAddEditEventComp = false;
    this.refreshDepList();
  }


  refreshDepList() {
    this.service.getEventList().subscribe(data => {
      this.EventList = data;
    });

    this.service.getSpeakerList().subscribe(data => {
      this.SpeakerList = data;
    });
  }

  // getSpeakerName(speakerId : string): any{
  //   return this.service.getSpeaker(speakerId).subscribe(data => {
  //     this.SpeakerList = data.name;
  //   });  
  // }
}
