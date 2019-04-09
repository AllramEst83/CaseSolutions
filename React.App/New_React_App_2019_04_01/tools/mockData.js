const patientJournals = [
  {
    patientJournal: {
      id: "5c5c6558-e73a-4ce6-78ce-08d6bc03a508",
      firstName: "Hulli",
      lastName: "Gulli",
      animalSSN: "15506305598",
      insurance: insurance[1].insuranceTwo,
      clinic: clinics[1].clinicTwo,
      doctors: [doctors[1].doctorTwo],
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
      insurance: insurance[0].insurance,
      clinic: clinics[0].clinicOne,
      doctors: [doctors[0].doctorOne],
      medicalServices: []
    },

    owners: [],
    statusCode: 200,
    error: "No error",
    description: "Returned patient journal matching the id.",
    code: "no_error"
  }
];

const clinics = [
  {
    clinicTwo: {
      id: "c73e5642-dd9a-428a-c469-08d6bc03a4f1",
      name: "Magical Creatures and other stuff",
      adress: {
        id: 13471,
        streetAdress: "Smittovägen 1",
        zipCode: "85236",
        telephone: "654873365"
      }
    }
  },
  {
    clinicOne: {
      id: "c73e5642-dd9a-428a-c469-08d6bc03a4f1",
      name: "Magical Creatures and other stuff",
      adress: {
        id: 13471,
        streetAdress: "Smittovägen 1",
        zipCode: "85236",
        telephone: "654873365"
      }
    }
  }
];

const doctors = [
  {
    doctorOne: {
      id: "fde3adaa-d2a1-468d-7fe1-08d6bc03a4ed",
      firstName: "Su",
      lastName: "Wiberg",
      typeOfDoctorWrapper: {
        id: 12490,
        typeOfDoctor: 2
      }
    }
  },
  {
    doctorTwo: {
      id: "fde3adaa-d2a1-468d-7fe1-08d6bc03a4ed",
      firstName: "Su",
      lastName: "Wiberg",
      typeOfDoctorWrapper: {
        id: 12490,
        typeOfDoctor: 2
      }
    }
  }
];

const insurance = [
  {
    insuranceOne: {
      id: "6c133325-4766-4e9c-698e-08d6bc03a4f9",
      insuranceCompany: insuranceCompany[0].insuranceCompanyOne,
      typeOfInsuranceWrapper: {
        id: 12492,
        typeOfInsurance: 5
      }
    }
  },
  {
    insuranceTwo: {
      id: "6c133325-4766-4e9c-698e-08d6bc03a4f9",
      insuranceCompany: insuranceCompany[1].insuranceCompanyOTwo,
      typeOfInsuranceWrapper: {
        id: 12492,
        typeOfInsurance: 5
      }
    }
  }
];
const insuranceCompany = [
  {
    insuranceCompanyOne: {
      id: "d9327345-2614-4e31-d200-08d6bc03a4f5",
      name: "Government Standard Insurance For Magical Horses",
      adress: {
        id: 13474,
        streetAdress: "Fuskväg 1",
        zipCode: "12356",
        telephone: "0855498639"
      }
    }
  },
  {
    insuranceCompanyOTwo: {
      id: "d9327345-2614-4e31-d200-08d6bc03a4f5",
      name: "Government Standard Insurance For Magical Horses",
      adress: {
        id: 13474,
        streetAdress: "Fuskväg 1",
        zipCode: "12356",
        telephone: "0855498639"
      }
    }
  }
];

module.exports = [patientJournals];
