import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { Tweet } from 'src/models/tweet';
import * as signalR from '@aspnet/signalr';
@Injectable({
  providedIn: 'root'
 })
 export class SignalRService {
  private message$: Subject<Tweet>;
  private connection: signalR.HubConnection;
  
  constructor() {
    this.message$ = new Subject<Tweet>();
    this.connection = new signalR.HubConnectionBuilder()
    .withUrl('https://localhost:5001/notify', 
     {
      skipNegotiation: true,
      transport: signalR.HttpTransportType.WebSockets
      })
    .build();
    this.connect();
  }
  private connect() {
    this.connection.start().catch(err => console.log(err));
    this.connection.on('broadcastMessage', (message) => {
      this.message$.next(message);
    });
  }
  public getMessage(): Observable<Tweet> {
    return this.message$.asObservable();
  }
  public disconnect() {
    this.connection.stop();
  }
 }