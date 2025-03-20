import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Transaction } from '../models/transaction.model';

@Injectable({
    providedIn: 'root',
})
export class EtlService {
    private apiUrl = 'http://localhost:5266/api/etl';

    constructor(private http: HttpClient) { }

    startETL(): Observable<Transaction[]> {
        return this.http.post<Transaction[]>(`${this.apiUrl}/start`, {});
    }
    
    clearData(): Observable<void> {
        return this.http.post<void>(`${this.apiUrl}/clear`, {});
    }
}
