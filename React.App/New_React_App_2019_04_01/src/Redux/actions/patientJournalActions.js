import * as actionTypes from "./actionTypes";
import PatientAPI from "../../api/PatientAPI";

export function _loadPatientJournalsSuccess(journals) {
  return { type: actionTypes.LOAD_PATIENTJOURNALS_SUCCESS, journals: journals };
}
export function _createPatientJournalSuccess(journal) {
  return { type: actionTypes.CREATE_PATENTJOURNAL_SUCCESS, journal: journal };
}
export function _updatePatientJournalSuccess(journal) {
  return { type: actionTypes.UPADE_PATENTJOURNAL_SUCCESS, journal: journal };
}
export function _deletePatientJournal(journal) {
  return { type: actionTypes.DELETE_PATENTJOURNAL_SUCCESS, journal: journal };
}

//Functions
export function loadPatientJournals() {
  return dispatch => {
    return PatientAPI.ReturnPatientJournals()
      .then(patientJournals => {
        dispatch(_loadPatientJournalsSuccess(patientJournals));
      })
      .catch(error => {
        throw error;
      });
  };
}

export function UpdatePatientJournal(patientJournal) {
  return dispatch => {
    return PatientAPI.UpdatePatientJournal(patientJournal)

      .then(updatedPatientJournal => {
        dispatch(_updatePatientJournalSuccess(updatedPatientJournal));
      })
      .catch(error => {
        throw error;
      });
  };
}

export function ACTIONS_CreatePatientJournal(patientJournal) {
  return dispatch => {
    return PatientAPI.CreatePatientJournal(patientJournal)
      .then(createdPatientJournal => {
        dispatch(_createPatientJournalSuccess(createdPatientJournal));
      })
      .catch(error => {
        throw error;
      });
  };
}

export function DeletePatientJournal(patientJournal) {
  return dispatch => {
    return PatientAPI.DeletePatientJournal(patientJournal)

      .then(deletedPatientJournal => {
        dispatch(_deletePatientJournal(deletedPatientJournal));
      })
      .catch(error => {
        throw error;
      });
  };
}
