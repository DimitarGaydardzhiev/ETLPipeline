import { createAction, props } from '@ngrx/store';

export const startETL = createAction('[ETL] Start ETL');

export const startETLSuccess = createAction(
    '[ETL] Start ETL Success',
    props<{ message: string }>()
);
export const startETLFailure = createAction(
    '[ETL] Start ETL Failure',
    props<{ error: string }>()
);
