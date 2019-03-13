

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
        "firstName": "Lasse Maja ",
        "lastName": "Kurresson"
      },
      {
        "firstName": "Berra ",
        "lastName": "Gunnarsson"
      }
    ];
  }





}
export default PatientJournalAPI;
