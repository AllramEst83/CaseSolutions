const RootUrl = {
    baseUrl: ""
};

class PatientAPI extends React.Component {

    // Generate Guid
    static Guid() {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    }

    static ReturnPatientJournals() {
        return {
            test: "test text"
        };
    }

}
export default PatientAPI;