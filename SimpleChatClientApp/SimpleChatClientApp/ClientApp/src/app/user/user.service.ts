import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { UserModel } from "../shared/models/user.model";
import { BaseApiService } from "../shared/services/base-api.service";

@Injectable({
    providedIn: 'root'
})
export class UserService extends BaseApiService {
    private _userApiUrl: string = 'api/users';

    constructor(http: HttpClient) {
        super(http);
    }

    public createUser(user: UserModel): Observable<any> {
        return this._http.post(this._baseApiUrl + this._userApiUrl, user);
    }
}