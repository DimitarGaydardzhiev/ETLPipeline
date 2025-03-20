import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { clearData, startETL } from '../../core/store/actions/etl.actions';
import { CommonModule } from '@angular/common';
import { AppState } from '../../core/store/app.state';
import { Transaction } from '../../core/models/transaction.model';

@Component({
  selector: 'app-etl-monitor',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './etl-monitor.component.html',
  styleUrls: ['./etl-monitor.component.scss']
})
export class EtlMonitorComponent {
  count$: Observable<number | null>;
  error$: Observable<string | null>;
  loading$: Observable<boolean>;
  transactions$: Observable<Transaction[]>;

  constructor(private store: Store<AppState>) {
    this.count$ = store.select((state) => state.etlState.count);
    this.error$ = store.select((state) => state.etlState.error);
    this.loading$ = store.select((state) => state.etlState.loading);
    this.transactions$ = store.select((state) => state.etlState.transactions);
  }

  startETL() {
    this.store.dispatch(startETL());
  }

  clearData() {
    this.store.dispatch(clearData());
  }
}
