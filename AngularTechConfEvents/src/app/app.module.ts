import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { EventsComponent } from './events/events.component';
import { AddEditEventsComponent } from './events/add-edit-events/add-edit-events.component';
import { ShowEventsComponent } from './events/show-events/show-events.component';
import { SpeakerComponent } from './speaker/speaker.component';
import { AddEditSpeakerComponent } from './speaker/add-edit-speaker/add-edit-speaker.component';
import { ShowSpeakerComponent } from './speaker/show-speaker/show-speaker.component';
import { ApiserviceService } from './apiservice.service';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    EventsComponent,
    AddEditEventsComponent,
    ShowEventsComponent,
    SpeakerComponent,
    AddEditSpeakerComponent,
    ShowSpeakerComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [ApiserviceService],
  bootstrap: [AppComponent]
})
export class AppModule { }
