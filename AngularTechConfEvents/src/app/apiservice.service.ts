import { Injectable } from '@angular/core';
import { HttpClient, HttpEvent, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Speaker } from './model';

@Injectable({
  providedIn: 'root'
})
export class ApiserviceService {
  readonly apiUrl = 'https://localhost:7270/';

  constructor(private http: HttpClient) { }

  // Events
  getEventList(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl + 'event/GetEvents');
  }

  addEvent(event: any): Observable<any> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post<any>(this.apiUrl + 'event/CreateEvent', event, httpOptions);
  }

  updateEvent(event: any): Observable<any> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.put<any>(this.apiUrl + 'event/UpdateEvent', event, httpOptions);
  }

  deleteEvent(eventId: string): Observable<number> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.delete<number>(this.apiUrl + 'event/' + eventId, httpOptions);
  }

  // Speaker
  getSpeakerList(): Observable<Speaker[]> {
    return this.http.get<Speaker[]>(this.apiUrl + 'speaker/GetSpeakers');
  }

  getSpeaker(speakerId: string): Observable<Speaker> {
    return this.http.get<Speaker>(this.apiUrl + 'speaker/'+ speakerId);
  }

  addSpeaker(speaker: any): Observable<any> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post<any>(this.apiUrl + 'speaker/CreateSpeaker', speaker, httpOptions);
  }

  updateSpeaker(speaker: any): Observable<any> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.put<any>(this.apiUrl + 'speaker/UpdateSpeaker', speaker, httpOptions);
  }

  deleteSpeaker(speakerId: number): Observable<number> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.delete<number>(this.apiUrl + 'speaker/' + speakerId, httpOptions);
  }

  // uploadPhoto(photo: any) {
  //   return this.http.post(this.apiUrl + 'employee/savefile', photo);
  // }

  // getAllDepartmentNames(): Observable<any[]> {
  //   return this.http.get<any[]>(this.apiUrl + 'employee/GetAllDepartmentNames');
  // }

}