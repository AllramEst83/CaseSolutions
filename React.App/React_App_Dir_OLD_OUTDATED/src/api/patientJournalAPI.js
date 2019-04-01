import { read } from "fs";


const Root_API_Url = {
  homeUrl: ""
};

class PatientJournalAPI {
  static Guid() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
      var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
      return v.toString(16);
    });
  }

  static ReturnPatientJournals() {
    return [
      {
        "id": this.Guid(),
        "firstName": "Star",
        "lastName": "Beam",
        "animalSSN": this.Guid(),
        "insurance":{},
        "clinic":{},
        "medicalServices": [],
        "owner":[]

      },
      {
        "id": this.Guid(),
        "firstName": "Gun",
        "lastName": "Powder",
        "animalSSN": this.Guid(),
        "insurance": {},
        "clinic": {},
        "medicalServices": [],
        "owner": []

      },
      {
        "id": this.Guid(),
        "firstName": "Storm",
        "lastName": "Eye",
        "animalSSN": this.Guid(),
        "insurance": {},
        "clinic": {},
        "medicalServices": [],
        "owner": []

      }
    ];
  }

  static GetAllPatientJournals() {
    return new Promise((resolve, reject) => {
      fetch(`https://${}/api/gateway`)
        .then()
        .then();
    })
  }




}
export default PatientJournalAPI;
