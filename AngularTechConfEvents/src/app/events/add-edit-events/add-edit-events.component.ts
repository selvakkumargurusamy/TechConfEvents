import { Component, Input, OnInit } from '@angular/core';
import { Speaker } from 'src/app/model';
import { ApiserviceService } from '../../apiservice.service';

@Component({
  selector: 'app-add-edit-events',
  templateUrl: './add-edit-events.component.html',
  styleUrls: ['./add-edit-events.component.css']
})
export class AddEditEventsComponent implements OnInit {

  constructor(private service: ApiserviceService) { }

  @Input() events: any;
  eventId = "";
  eventTitle = "";
  description = "";
  eventStartDate = "";
  eventEndDate = "";
  isOnline = false;
  venue = "";
  website = "";
  linkForDetails = "";
  speakerId = "";

  selectedSpeaker: any;
  SpeakerList: Speaker[] = [];


  ngOnInit(): void {

    this.loadEventsList();
    
  }

  loadEventsList() {

    this.service.getSpeakerList().subscribe((data: Speaker[]) => {
      this.SpeakerList = data;

    this.eventId = this.events.eventId;
    this.eventTitle = this.events.eventTitle;
    this.description = this.events.description;
    this.eventStartDate = this.events.eventStartDate;
    this.eventEndDate = this.events.eventEndDate;
    this.isOnline = this.events.isOnline;
    this.venue = this.events.venue;
    this.website = this.events.website;
    this.linkForDetails = this.events.linkForDetails;
    this.speakerId = this.events.speakerId;  

    this.selectedSpeaker = this.SpeakerList.filter(x => x.speakerId == this.speakerId)[0];

    });
  }

  addEvent() {
    var events = {
      eventId: this.eventId,
      eventTitle: this.eventTitle,
      description: this.description,
      eventStartDate: this.eventStartDate,
      eventEndDate: this.eventEndDate,
      isOnline : this.isOnline,
      venue: this.venue,
      website: this.website,
      linkForDetails: this.linkForDetails,
      speakerId: this.speakerId
    };
    this.service.addEvent(events).subscribe(res => {
      alert("Event Created Successfully");
    });
  }

  updateEvent() {
    var events = {
      eventId: this.eventId,
      eventTitle: this.eventTitle,
      description: this.description,
      eventStartDate: this.eventStartDate,
      eventEndDate: this.eventEndDate,
      isOnline : this.events.isOnline,
      venue: this.events.venue,
      website: this.events.website,
      linkForDetails: this.events.linkForDetails,
      speakerId: this.events.speakerId
    };
    this.service.updateEvent(events).subscribe(res => {
      alert("Event Updated Successfully");
    });
  }

  onOptionsSelected(event: any)
  {
    this.selectedSpeaker = event;
    this.speakerId = event.speakerId;
  }

  onCheckboxChange(isChecked: any)
  {
    this.isOnline = isChecked;
  }

}
