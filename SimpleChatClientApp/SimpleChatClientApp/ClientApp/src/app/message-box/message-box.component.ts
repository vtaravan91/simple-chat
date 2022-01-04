import { Component, NgZone, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { RoomService } from "../rooms/room.service";
import { MessageModel } from "../shared/models/message.model";
import { RoomModel } from "../shared/models/room.model";
import { MessagesHubService } from "../shared/services/messages-hub.service";
import { MessageService } from "./message.service";

@Component({
    selector: 'message-box',
    templateUrl: './message-box.component.html'
})
export class MessageBoxComponent implements OnInit {
    private room: RoomModel;

    messages: MessageModel[] = [];
    message: string;
    userInRoom: boolean = false;

    constructor(private _activatedRoute: ActivatedRoute,
        private _router: Router,
        private _messageService: MessageService,
        private _messagesHubService: MessagesHubService,
        private _roomService: RoomService,
        private _ngZone: NgZone
    ) {
    }

    ngOnInit(): void {
        this.messages = [];
        this.userInRoom = false;

        this._activatedRoute.params.subscribe(e => {
            let roomId = e['roomId'];
            this._roomService.getAll({
                roomId: roomId,
                checkUserInRoom: true,
                userId: parseInt(localStorage.getItem('userId'))
            }).subscribe(response => {
                if (response == null || response.length == 0) {
                    this._router.navigate(['/rooms']);
                } else {
                    this.room = response[0];
                    this.messages = [];
                    this.userInRoom = this.room.userInRoom;
                    if (this.room.userInRoom == true) {
                        this._messageService.getAll({ page: 0, take: 10, roomId: this.room.id }).subscribe(response => {
                            this.messages = response || [];
                        });
                    }
                }
            });
        });

        this._messagesHubService.messages.subscribe(m => {
            this._ngZone.run(_ => {
                this.messages.push(m);
            });
        });
    }

    enterRoom() {
        this._messagesHubService.enterRoom({
            userId: parseInt(localStorage.getItem('userId')),
            roomId: this.room.id
        }).then(e => {
            this.userInRoom = true;
            this._messageService.getAll({ page: 0, take: 10, roomId: this.room.id }).subscribe(response => {
                this.messages = response || [];
            });
        })
    }

    sendMessage() {
        let messageModel = new MessageModel();
        messageModel.message = this.message;
        messageModel.roomId = this.room.id;
        messageModel.userId = parseInt(localStorage.getItem('userId'));
        messageModel.user = { nickname: localStorage.getItem('nickname') };

        this._messagesHubService.sendMessage(messageModel).then(_ => {
            this.message = null;
        });        
    }
}