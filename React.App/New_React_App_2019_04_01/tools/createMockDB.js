/* eslint-disable no-console */
const fs = require("fs");
const path = require("path");
const mockData = require("./mockData");

const {
  patientJournals,
  owners,
  emptyOwner,
  medicalServices,
  clinics,
  emptyClinic,
  doctors,
  emptyDoctor,
  emptyInsurance,
  insurance,
  insuranceCompany,
  emptyInsuranceCompany
} = mockData;

const data = JSON.stringify({
  patientJournals,
  owners,
  emptyOwner,
  medicalServices,
  clinics,
  emptyClinic,
  doctors,
  emptyDoctor,
  emptyInsurance,
  insurance,
  insuranceCompany,
  emptyInsuranceCompany
});

const filepath = path.join(__dirname, "db.json");

fs.writeFile(filepath, data, function(err) {
  err ? console.log(err) : console.log("Mock DB created.");
});

//K�r json server och applikationen samtidigt: npm start
//K�r bara json server: npm run start:api
