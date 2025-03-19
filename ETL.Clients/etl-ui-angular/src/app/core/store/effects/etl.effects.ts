import { inject, Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, mergeMap, of } from 'rxjs';
import * as ETLActions from '../actions/etl.actions';
import { EtlService } from '../../services/etl.service';

@Injectable()
export class EtlEffects {
    private actions$ = inject(Actions);

    constructor(private etlService: EtlService) { }

    startETL$ = createEffect(() =>
        this.actions$.pipe(
            ofType(ETLActions.startETL),
            mergeMap(() =>
                this.etlService.startETL().pipe(
                    map((message) => ETLActions.startETLSuccess({ message })),
                    catchError((error) => of(ETLActions.startETLFailure({ error: error.message })))
                )
            )
        )
    );
}
