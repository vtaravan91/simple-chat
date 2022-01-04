import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable()
export abstract class BaseApiService {
    protected _http: HttpClient;
    protected readonly _baseApiUrl: string;

    constructor(http: HttpClient) {
        this._http = http;
        this._baseApiUrl = 'https://localhost:44378/';
    }
}