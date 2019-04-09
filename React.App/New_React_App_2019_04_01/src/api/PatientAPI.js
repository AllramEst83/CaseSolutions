class PatientAPI {
  static ReturnPatientArray() {
    return [
      {
        patientJournal: {
          id: "5c5c6558-e73a-4ce6-78ce-08d6bc03a508",
          firstName: "Hulli",
          lastName: "Gulli",
          animalSSN: "15506305598",
          insurance: {
            id: "6c133325-4766-4e9c-698e-08d6bc03a4f9",
            insuranceCompany: {
              id: "d9327345-2614-4e31-d200-08d6bc03a4f5",
              name: "Government Standard Insurance For Magical Horses",
              adress: {
                id: 13474,
                streetAdress: "Fuskväg 1",
                zipCode: "12356",
                telephone: "0855498639"
              }
            },
            typeOfInsuranceWrapper: {
              id: 12492,
              typeOfInsurance: 5
            }
          },
          clinic: {
            id: "c73e5642-dd9a-428a-c469-08d6bc03a4f1",
            name: "Magical Creatures and other stuff",
            adress: {
              id: 13471,
              streetAdress: "Smittovägen 1",
              zipCode: "85236",
              telephone: "654873365"
            },
            doctors: [
              {
                id: "fde3adaa-d2a1-468d-7fe1-08d6bc03a4ed",
                firstName: "Su",
                lastName: "Wiberg",
                typeOfDoctorWrapper: {
                  id: 12490,
                  typeOfDoctor: 2
                }
              }
            ]
          },
          medicalServices: [],
          owners: []
        },
        statusCode: 200,
        error: "No error",
        description: "Returned patient journal matching the id.",
        code: "no_error"
      },
      {
        patientJournal: {
          id: "5c5c6558-e73a-4ce6-78ce-08d6bc03a508",
          firstName: "Hulli",
          lastName: "Gulli",
          animalSSN: "15506305598",
          insurance: {
            id: "6c133325-4766-4e9c-698e-08d6bc03a4f9",
            insuranceCompany: {
              id: "d9327345-2614-4e31-d200-08d6bc03a4f5",
              name: "Government Standard Insurance For Magical Horses",
              adress: {
                id: 13474,
                streetAdress: "Fuskväg 1",
                zipCode: "12356",
                telephone: "0855498639"
              }
            },
            typeOfInsuranceWrapper: {
              id: 12492,
              typeOfInsurance: 5
            }
          },
          clinic: {
            id: "c73e5642-dd9a-428a-c469-08d6bc03a4f1",
            name: "Magical Creatures and other stuff",
            adress: {
              id: 13471,
              streetAdress: "Smittovägen 1",
              zipCode: "85236",
              telephone: "654873365"
            },
            doctors: [
              {
                id: "fde3adaa-d2a1-468d-7fe1-08d6bc03a4ed",
                firstName: "Su",
                lastName: "Wiberg",
                typeOfDoctorWrapper: {
                  id: 12490,
                  typeOfDoctor: 2
                }
              }
            ]
          },
          medicalServices: [],
          owners: []
        },
        statusCode: 200,
        error: "No error",
        description: "Returned patient journal matching the id.",
        code: "no_error"
      }
    ];
  }

  // Generate Guid
  static Guid() {
    return "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx".replace(/[xy]/g, function(c) {
      var r = (Math.random() * 16) | 0,
        v = c == "x" ? r : (r & 0x3) | 0x8;
      return v.toString(16);
    });
  }

  // static ReturnPatientJournals() {
  //     return ReturnPatientArray();
  // }

  // static UpdatePatientJournal(patientJournal) {

  // }

  static CreatePatientJournal(patientJournal) {
    alert(patientJournal.firstName);
    return new Promise(
      resolve => {
        resolve(patientJournal);
      },
      error => {
        alert("error has occoured: " + error);
        throw error;
      }
    );
  }

  // static DeletePatientJournal() {

  // }

  static ReturnSingelPatientJournal(id) {
    return this.state.patientJournals.filter(
      patientJournal => patientJournal.patientJournal.id == id
    );
  }
}
export default PatientAPI;
