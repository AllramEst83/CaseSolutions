import * as actionTypes from './actionTypes';
import PatientAPI from '../api/PatientAPI';

export function loadPatientJournalsSuccess(journals) {
    return { type: actionTypes.LOAD_PATIENTJOURNALS_SUCCESS, journals: journals };
}
export function createPatientJournalSuccess(journal) {
    return { type: actionTypes.CREATE_PATENTJOURNAL_SUCCESS, journal: journal };
}
export function updatePatientJournalSuccess(journal) {
    return { type: actionTypes.UPADE_PATENTJOURNAL_SUCCESS, journal: journal };
}
export function deletePatientJournal(journal) {
    return { type: actionTypes.DELETE_PATENTJOURNAL_SUCCESS, journal: journal };
}

export function loadPatientJournals() {
    return dispatch => {

        return PatientAPI.ReturnPatientJournals(patientJournals => {

            dispatch(patientJournals);

        }).then().catch(error => {

            throw (error);

        });

    };
}