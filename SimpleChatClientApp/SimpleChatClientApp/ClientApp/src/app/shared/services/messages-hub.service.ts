import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { Subject } from "rxjs";
import { MessageModel } from "../models/message.model";
import { UserRoomModel } from "../models/user-room.model";
import { BaseApiService } from "./base-api.service";

@Injectable({
    providedIn: 'root'
})
export class MessagesHubService extends BaseApiService {
    private _hubConnection: HubConnection;
    private _roomsHubUrl: string = 'hubs/messages';

    public messages: Subject<MessageModel> = new Subject();

    constructor(httpClient: HttpClient) {
        super(httpClient);

        this.buildConnection();
        this.startConnection();
    }


    enterRoom(userRoom: UserRoomModel) {
        return this._hubConnection.send("EnterRoomAsync", userRoom);
    }

    sendMessage(message: MessageModel) {
        return this._hubConnection.send("CreateMessageAsync", message);
    }

    private buildConnection() {
        this._hubConnection = new HubConnectionBuilder()
            .withUrl(this._baseApiUrl + this._roomsHubUrl)
            .build();
    }

    private startConnection() {
        this._hubConnection.start()
            .then(e => console.log('Messages hub connection started'));

        this._hubConnection.on('ReceiveMessageAsync', (data: any) => {
            console.log('ReceiveMessageAsync', data);
            this.messages.next(data);
        });
    }
}