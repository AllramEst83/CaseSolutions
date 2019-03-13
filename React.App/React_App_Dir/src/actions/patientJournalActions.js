import * as actionTypes from './actionTypes';
import patientJournalAPI from '../api/patientJournalAPI';

//Action creator
export function loadPatientJournalsSuccess(unicorns) {
  return { type: actionTypes.LOAD_PAIENTJOURNALS_SUCCESS, patientJournals: patientJournals };
}
export function createPatientJournalSuccess(unicorn) {
  return { type: actionTypes.CREATE_PAIENTJOURNALS_SUCCESS, patientJournal: patientJournal }
}
export function updatePatientJournalSuccess(unicorn) {
  return { type: actionTypes.UPDATED_PAIENTJOURNALS_SUCCESS, patientJournal: patientJournal }
}
export function deletePatientJournalSuccess(unicorn) {
  return { type: actionTypes.DELETE_PAIENTJOURNALS_SUCCESS, patientJournal: patientJournal }
}


export function LoadPatientJournals() {
  return dispatch => {

    return patientJournalAPI.LoadPatientJournals.then(patientJournals => {

      dispatch(loadPatientJournalsSuccess(patientJournals));

    }).catch(error => {

      throw (error);

    });
  }
}
