const patientJournals = [
  {
    patientJournal: {
      id: "5c5c6558-e73a-4ce6-78ce-08d6bc03a508",
      firstName: "Hulli",
      lastName: "Gulli",
      animalSSN: "15506305598",
      insurance: "6c133325-4766-4e9c-698e-08d6bc03a4f9",
      clinic: "c73e5642-dd9a-428a-c469-08d6bc03a4f1",
      doctors: ["fde3adaa-d2a1-468d-7fe1-08d6bc03a4ed"],
      medicalServices: "db86a88d-a425-40ab-ba6e-156526485069",
      owners: "1c68cca3-8426-4b1d-a7b7-08d6bce7d786"
    },
    statusCode: 200,
    error: "No error",
    description: "Returned patient journal matching the id.",
    code: "no_error"
  },
  {
    patientJournal: {
      id: "f6ab75b2-5ac7-11e9-8647-d663bd873d93",
      firstName: "Hulli",
      lastName: "Gulli",
      animalSSN: "15506305598",
      insurance: "6c133325-4766-4e9c-698e-08d6bc03a4f9",
      clinic: "c73e5642-dd9a-428a-c469-08d6bc03a4f1",
      doctors: ["fde3adaa-d2a1-468d-7fe1-08d6bc03a4ed"],
      medicalServices: "db86a88d-a425-40ab-ba6e-156526485069",
      owners: "84f9b525-4621-4fd1-a7b6-08d6bce7d786"
    },
    statusCode: 200,
    error: "No error",
    description: "Returned patient journal matching the id.",
    code: "no_error"
  }
];
const owners = [
  {
    id: "84f9b525-4621-4fd1-a7b6-08d6bce7d786",
    firstName: "Kurre",
    lastName: "Snigelfart",
    ssn: 198312120432,
    adress: {
      id: 145,
      streetAdress: "PastorsVägen 88",
      zipCode: "325698",
      telephone: "1234567825"
    }
  },
  {
    id: "1c68cca3-8426-4b1d-a7b7-08d6bce7d786",
    firstName: "Abbas",
    lastName: "Gringo",
    ssn: 196505126666,
    adress: {
      id: 146,
      streetAdress: "RulleGatan 7",
      zipCode: "45632",
      telephone: "074562365"
    }
  }
];
const emptyOwner = {
  id: null,
  firstName: "",
  lastName: "",
  adress: {
    id: null,
    streetAdress: "",
    zipCode: "",
    telephone: ""
  },
  ssn: null
};
const medicalServices = [
  {
    id: "db86a88d-a425-40ab-ba6e-156526485069",
    typeOfExaminationWrapper: {
      id: 51,
      typeOfExamination: 2
    },
    doctor: ["fde3adaa-d2a1-468d-7fe1-08d6bc03a4ed"],
    hourlyCost: 100,
    examinationDuration: "00:00:00",
    startTime: "2019-03-10T19:30:00",
    endTime: "2019-03-10T22:00:00",
    kindOfIllnes: {
      id: "f3c3ed3e-3845-41a6-0c2d-08d6bce7d781",
      title: "Hoof change",
      illnessSeverity: {
        id: 49,
        illnessSeverity: 2
      }
    },
    prescriptions: [
      {
        id: "ccfd067b-f844-4633-87b1-08d6bce7d77e",
        name: "Unicorn Sparkel Snarkel Remedy",
        description:
          "This remedey is for unicorns only. Appply cream to non magic parts."
      },
      {
        id: "b166fc58-7f28-4838-87b2-08d6bce7d77e",
        name: "Magic Tail In A Jiffy Spiffy",
        description:
          "Wash the unicorn tail with this shampoo to imbunde the tail with rainbow colors."
      }
    ]
  }
];
const emptyMedicalService = {
  id: "00000000-0000-0000-0000-000000000000",
  typeOfExaminationWrapper: {
    id: null,
    typeOfExamination: null
  },
  doctor: [],
  hourlyCost: null,
  examinationDuration: null,
  startTime: null,
  endTime: null,
  kindOfIllnes: {
    id: "00000000-0000-0000-0000-000000000000",
    title: "",
    illnessSeverity: {
      id: null,
      illnessSeverity: null
    }
  },
  prescriptions: [
    {
      id: "00000000-0000-0000-0000-000000000000",
      name: "",
      description: ""
    },
    {
      id: "00000000-0000-0000-0000-000000000000",
      name: "",
      description: ""
    }
  ]
};
const clinics = [
  {
    id: "c73e5642-dd9a-428a-c469-08d6bc03a4f1",
    name: "Magical Creatures and other stuff",
    adress: {
      id: 13471,
      streetAdress: "Smittovägen 1",
      zipCode: "85236",
      telephone: "654873365"
    }
  },
  {
    id: "c73e5642-dd9a-428a-c469-08d6bc03a4f1",
    name: "Magical Creatures and other stuff",
    adress: {
      id: 13471,
      streetAdress: "Smittovägen 1",
      zipCode: "85236",
      telephone: "654873365"
    }
  }
];
const emptyClinic = {
  id: "00000000-0000-0000-0000-000000000000",
  name: "",
  adress: {
    id: null,
    streetAdress: "",
    zipCode: "",
    telephone: ""
  }
};

const doctors = [
  {
    id: "fde3adaa-d2a1-468d-7fe1-08d6bc03a4ed",
    firstName: "Su",
    lastName: "Wiberg",
    typeOfDoctorWrapper: {
      id: 12490,
      typeOfDoctor: 2
    }
  },
  {
    id: "fde3adaa-d2a1-468d-7fe1-08d6bc03a4ed",
    firstName: "Su",
    lastName: "Wiberg",
    typeOfDoctorWrapper: {
      id: 12490,
      typeOfDoctor: 2
    }
  }
];
const emptyDoctor = {
  id: "00000000-0000-0000-0000-000000000000",
  firstName: "",
  lastName: "",
  typeOfDoctorWrapper: {
    id: null,
    typeOfDoctor: null
  }
};
const insurance = [
  {
    id: "6c133325-4766-4e9c-698e-08d6bc03a4f9",
    insuranceCompany: "d9327345-2614-4e31-d200-08d6bc03a4f5",
    typeOfInsuranceWrapper: {
      id: 12492,
      typeOfInsurance: 5
    }
  },
  {
    id: "6c133325-4766-4e9c-698e-08d6bc03a4f9",
    insuranceCompany: "d9327345-2614-4e31-d200-08d6bc03a4f5",
    typeOfInsuranceWrapper: {
      id: 12492,
      typeOfInsurance: 5
    }
  }
];
const emptyInsurance = {
  id: "00000000-0000-0000-0000-000000000000",
  insuranceCompany: [],
  typeOfInsuranceWrapper: {
    id: null,
    typeOfInsurance: null
  }
};
const insuranceCompany = [
  {
    id: "c",
    name: "Government Standard Insurance For Magical Horses",
    adress: {
      id: 13474,
      streetAdress: "Fuskväg 1",
      zipCode: "12356",
      telephone: "0855498639"
    }
  },
  {
    id: "d9327345-2614-4e31-d200-08d6bc03a4f5",
    name: "Government Standard Insurance For Magical Horses",
    adress: {
      id: 13474,
      streetAdress: "Fuskväg 1",
      zipCode: "12356",
      telephone: "0855498639"
    }
  }
];
const emptyInsuranceCompany = {
  id: "00000000-0000-0000-0000-000000000000",
  name: "",
  adress: {
    id: null,
    streetAdress: "",
    zipCode: "",
    telephone: ""
  }
};
const newPatientJournal = {
  patientJournal: {
    id: "00000000-0000-0000-0000-000000000000",
    firstName: "",
    lastName: "",
    animalSSN: "",
    insurance: insurance[0].emptyInsuranceCompany,
    clinic: null,
    doctors: null,
    medicalServices: null,
    owners: null
  },
  statusCode: 900,
  error: "empty object",
  description: "no description.",
  code: "empty_object"
};

module.exports = {
  newPatientJournal,
  emptyOwner,
  patientJournals,
  owners,
  medicalServices,
  emptyMedicalService,
  clinics,
  emptyClinic,
  doctors,
  emptyDoctor,
  insurance,
  emptyInsurance,
  insuranceCompany,
  emptyInsuranceCompany
};
