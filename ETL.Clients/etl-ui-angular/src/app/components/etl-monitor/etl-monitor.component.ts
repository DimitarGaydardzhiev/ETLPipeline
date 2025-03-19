import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { startETL } from '../../core/store/actions/etl.actions';
import { CommonModule } from '@angular/common';
import { AppState } from '../../core/store/app.state';

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
  message$: Observable<string | null>;
  error$: Observable<string | null>;
  loading$: Observable<boolean>;

  constructor(private store: Store<AppState>) {
    this.message$ = store.select((state) => state.etlState.message);
    this.error$ = store.select((state) => state.etlState.error);
    this.loading$ = store.select((state) => state.etlState.loading);
  }

  startETL() {
    this.store.dispatch(startETL());
  }
}
