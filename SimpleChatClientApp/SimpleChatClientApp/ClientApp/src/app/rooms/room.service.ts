import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { RoomFilterModel } from "../shared/models/room-filter.model";
import { RoomModel } from "../shared/models/room.model";
import { BaseApiService } from "../shared/services/base-api.service";

@Injectable({
    providedIn: 'root'
})
export class RoomService extends BaseApiService {
    private readonly _roomApiUrl: string = 'api/rooms';
    constructor(http: HttpClient) {
        super(http);
    }

    public getAll(filter: RoomFilterModel = null): Observable<Array<RoomModel>> {
        let httpParams = new HttpParams();

        if (filter != null) {
            if (filter.roomId != null) {
                httpParams = httpParams.set('roomId', filter.roomId.toString());
            }

            if (filter.checkUserInRoom != null) {
                httpParams = httpParams.set('checkUserInRoom', filter.checkUserInRoom.toString());
            }

            if (filter.userId != null) {
                httpParams = httpParams.set('userId', filter.userId.toString());
            }
        }

        return this._http.get<RoomModel[]>(this._baseApiUrl + this._roomApiUrl, {
            params: httpParams,
            responseType: 'json',
            headers: new HttpHeaders({ 'Content-Type': 'application/json' })
        });
    }

    public createRoom(room: RoomModel): Observable<RoomModel> {
        return this._http.post<RoomModel>(this._baseApiUrl + this._roomApiUrl, room);
    }
}