import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { RoomService } from '../rooms/room.service';
import { RoomModel } from '../shared/models/room.model';

@Component({
    selector: 'create-room',
    templateUrl: './create-room.component.html'
})
export class CreateRoomComponent {
    private readonly _roomService: RoomService;
    private readonly _router: Router;

    public room: RoomModel = new RoomModel();

    constructor(roomService: RoomService, router: Router) {
        this._roomService = roomService;
        this._router = router;
    }

    public createRoom() {
        this._roomService.createRoom(this.room).subscribe(e => {
            this._router.navigate(['/rooms']);
        });
    }
}
