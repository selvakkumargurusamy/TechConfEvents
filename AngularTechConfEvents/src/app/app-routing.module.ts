import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EventsComponent } from './events/events.component';
import { SpeakerComponent } from './speaker/speaker.component';

const routes: Routes = [
  { path: 'speaker', component: SpeakerComponent },
  { path: 'events', component: EventsComponent }  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
