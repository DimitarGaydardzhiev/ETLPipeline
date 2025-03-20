import { inject, Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, mergeMap, of, tap } from 'rxjs';
import * as ETLActions from '../actions/etl.actions';
import { EtlService } from '../../services/etl.service';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class EtlEffects {
    private actions$ = inject(Actions);

    constructor(private etlService: EtlService, private toastr: ToastrService) { }

    startETL$ = createEffect(() =>
        this.actions$.pipe(
            ofType(ETLActions.startETL),
            mergeMap(() =>
                this.etlService.startETL().pipe(
                    map((transactions) => ETLActions.startETLSuccess({
                        transactions
                    })),
                    catchError((error) => of(ETLActions.startETLFailure({ error: error.message })))
                )
            )
        )
    );

    clearData$ = createEffect(() =>
        this.actions$.pipe(
            ofType(ETLActions.clearData),
            mergeMap(() =>
                this.etlService.clearData().pipe(
                    map(() => ETLActions.clearDataSuccess()),
                    tap(() => {
                        this.toastr.success('Data cleared successfully!', 'Success');
                    }),
                    catchError((error) => {
                        this.toastr.error('Failed to clear data', 'Error');
                        return of(ETLActions.clearDataFailure({ error }));
                    })
                )
            )
        ));
}
