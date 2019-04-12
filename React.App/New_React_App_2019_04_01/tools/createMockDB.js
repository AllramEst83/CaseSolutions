/* eslint-disable no-console */
const fs = require("fs");
const path = require("path");
const mockData = require("./mockData");

const { patientJournals } = mockData;
const data = JSON.stringify({ patientJournals });
const filepath = path.join(__dirname, "db.json");

fs.writeFile(filepath, data, function (err) {
  err ? console.log(err) : console.log("Mock DB created.");
});


//Kör json server och applikationen samtidigt: npm start
//Kör bara json server: npm run start:api