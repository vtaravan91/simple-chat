import { Component, NgZone, OnInit } from '@angular/core';
import { RoomModel } from '../shared/models/room.model';
import { RoomsHubService } from '../shared/services/rooms-hub.service';
import { RoomService } from './room.service';

@Component({
    selector: 'rooms',
    templateUrl: './rooms.component.html'
})
export class RoomsComponent implements OnInit {
    private readonly _roomService: RoomService;
    private readonly _roomsSignalRService: RoomsHubService;
    private readonly _ngZone: NgZone;
    public rooms: RoomModel[] = [];

    constructor(roomService: RoomService,
        roomsSignalRService: RoomsHubService,
        ngZone: NgZone) {
        this._roomService = roomService;
        this._roomsSignalRService = roomsSignalRService;
        this._ngZone = ngZone;
    }

    ngOnInit(): void {
        this._roomService.getAll().subscribe(room => {
            this.rooms = room || [];
        });

        this._roomsSignalRService.rooms.subscribe((room: any) => {
            this._ngZone.run(_ => {
                this.rooms.push(<RoomModel>room);
            });
        });
    }

    trackRoomItem(index: number, item: RoomModel) {
        return item.id;
    }
}
