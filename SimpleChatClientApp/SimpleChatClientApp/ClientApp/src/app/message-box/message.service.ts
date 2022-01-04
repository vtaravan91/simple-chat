import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { MessageModel } from "../shared/models/message.model";
import { MessagesFilterModel } from "../shared/models/messages-filter.model";
import { BaseApiService } from "../shared/services/base-api.service";

@Injectable({
    providedIn: 'root'
})
export class MessageService extends BaseApiService {
    private readonly _messageApiUrl: string = 'api/messages';

    constructor(http: HttpClient) {
        super(http);
    }

    public getAll(filter: MessagesFilterModel): Observable<MessageModel[]> {
        let httpParams = new HttpParams();
        httpParams = httpParams.set('page', filter.page.toString());
        httpParams = httpParams.set('take', filter.take.toString());
        httpParams = httpParams.set('roomId', filter.roomId.toString());

        return this._http.get<MessageModel[]>(this._baseApiUrl + this._messageApiUrl, {
            params: httpParams,
            responseType: 'json',
            headers: new HttpHeaders({ 'Content-Type': 'application/json' })
        });
    }
}