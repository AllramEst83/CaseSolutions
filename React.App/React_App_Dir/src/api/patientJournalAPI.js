

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
    const PatientJournalId = this.Guid()
    const doctorId = this.Guid();
    return [
      {
        "id": PatientJournalId,
        "firstName": "Star",
        "lastName": "Beam",
        "animalSSN": this.Guid(),
        "insurance":
          {
            "id": this.Guid(),
            "insuranceCompany":
              {
                "id": this.Guid(),
                "name": "Ruffel och Båg försäkring",
                "adress":
                  {
                    "id": this.Guid(),
                    "streetAdress": "Lustikullavägen 5",
                    "zipCode": "132456789",
                    "telephone": "456789132"
                  }
              },
            "typeOfInsurance": "PointOfServicePlan"
          },
        "clinic":
          {
            "id": this.Guid(),
            "name": "Magical horse treatment center",
            "adress":
              {
                "id": this.Guid(),
                "streetAdress": "Hopplagatan 78",
                "zipCode": "4567894",
                "telephone": "12345679845",
                "doctors":
                  [
                    {
                      "id": doctorId,
                      "firstName": "Ulla",
                      "lastName": "Brolin",
                      "typeofDoctor": "RainbowGlitterDoctor"
                    }
                  ]
              }
          },
        "invoices":
          [
            {
              "id": this.Guid(),
              "patientJournalId": PatientJournalId,
              "totalSum": 5500,
              "discount": 0.66,
              "issueDate": "2019-03-13T14:50:45.781Z",
              "dueDate": "2019-03-25T00:00:00.000Z",
              "medicalServices":
                [
                  {
                    "id": this.Guid(),
                    "typeOfExamination": "RainbowSparkleExamination",
                    "doctor":
                      {
                        "id": doctorId,
                        "firstName": "Ulla",
                        "lastName": "Brolin",
                        "typeofDoctor": "RainbowGlitterDoctor"
                      },
                    "hourlyCost": 3000,
                    "examinationDuration": 45,
                    "startTime": "2019-03-13T14:50:45.781Z",
                    "EndTime": "2019-03-13T15:35:45.781Z",
                    "Illnesses":
                      [
                        {
                          "id": this.Guid(),
                          "illnessTitle": "Out of Sparkle",
                          "illnessSeverity": "High",
                        }
                      ],
                    "prescription":
                      {
                        "id": this.Guid(),
                        "name": "Quick-a-Sparkle",
                        "description": "For use when unicorns lose there spark (umff)"
                      }
                  }
                ],
            }
          ],
        "medicalServices":
          [
            {
              "id": this.Guid(),
              "typeOfExamination": "RainbowSparkleExamination",
              "doctor":
                {
                  "id": doctorId,
                  "firstName": "Ulla",
                  "lastName": "Brolin",
                  "typeofDoctor": "RainbowGlitterDoctor"
                },
              "hourlyCost": 3000,
              "examinationDuration": 45,
              "startTime": "2019-03-13T14:50:45.781Z",
              "EndTime": "2019-03-13T15:35:45.781Z",
              "Illnesses":
                [
                  {
                    "id": this.Guid(),
                    "illnessTitle": "Out of Sparkle",
                    "illnessSeverity": "High",
                  }
                ],
              "prescription":
                {
                  "id": this.Guid(),
                  "name": "Quick-a-Sparkle",
                  "description": "For use when unicorns lose there spark (umff)"
                }
            }
          ],
        "owner":
          [
            {
              "": this.Guid(),
              "firstName": "",
              "lastName": "",
              "telephone": "",
              "adress":
                {
                  "id": this.Guid(),
                  "streetAdress": "Umbravägen 55 5",
                  "zipCode": "98752",
                  "telephone": "07564841"

                },
              "SSN": "198312120432"
            }
          ]

      }
    ];
  }





}
export default PatientJournalAPI;
