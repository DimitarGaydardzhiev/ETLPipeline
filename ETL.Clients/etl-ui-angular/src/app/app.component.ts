import { Component } from '@angular/core';
import { EtlMonitorComponent } from './components/etl-monitor/etl-monitor.component';

@Component({
  selector: 'app-root',
  imports: [
    EtlMonitorComponent
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'etl-ui-angular';
}
