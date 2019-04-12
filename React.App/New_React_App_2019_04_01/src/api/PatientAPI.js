import { handleResponse, handleError } from "./apiUtils";
const baseUrl = process.env.API_URL + "/patientJournals/";

export function getPatientJournals() {
  return fetch(baseUrl)
    .then(handleResponse)
    .catch(handleError);
}

export function savePatientJournal(patientJournal) {
  return fetch(baseUrl + patientJournal.id || "", {
    method: patientJournal.id ? "PUT" : "POST",
    headers: { "content-type": "application/json" },
    body: JSON.stringify(patientJournal)
  })
    .then(handleResponse)
    .catch(handleError);
}

export function deletePatientJournal(patientJournalId) {
  return fetch(baseUrl + patientJournalId, {
    method: "DELETE"
  })
    .then(handleResponse)
    .catch(handleError);
}
