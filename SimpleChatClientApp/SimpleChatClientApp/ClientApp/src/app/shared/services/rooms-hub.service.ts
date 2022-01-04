import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { Subject } from "rxjs";
import { RoomModel } from "../models/room.model";
import { BaseApiService } from "./base-api.service";

@Injectable({
    providedIn: 'root'
})
export class RoomsHubService extends BaseApiService {
    private _hubConnection: HubConnection;
    private _roomsHubUrl: string = 'hubs/rooms';

    public rooms: Subject<RoomModel> = new Subject();

    constructor(httpClient: HttpClient) {
        super(httpClient);

        this.buildConnection();
        this.startConnection();
    }

    private buildConnection() {
        this._hubConnection = new HubConnectionBuilder()
            .withUrl(this._baseApiUrl + this._roomsHubUrl)
            .build();
    }

    private startConnection() {
        this._hubConnection.start()
            .then(e => console.log('Rooms hub connection started'));

        this._hubConnection.on('ReceiveNewRoomAsync', (data: any) => {
            this.rooms.next(data);
        });
    }
}