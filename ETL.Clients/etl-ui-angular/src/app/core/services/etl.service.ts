import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class EtlService {
    private apiUrl = 'http://localhost:5266/api/etl';

    constructor(private http: HttpClient) { }

    startETL(): Observable<string> {
        return this.http.post<string>(`${this.apiUrl}/start`, {});
    }
}
