
import React, { PropTypes } from 'react';
import JournalForm from '../journal/journalForm';
import { browserHistory } from 'react-router';

class AddNewJournalEntry extends React.Component {

  constructor(props, context) {
    super(props, context)
    this.state = {
      patientJournal: { firstname: "Kurre", lastname: "Karlsson" },
      errors: { name: "" }
    };

    this.updatePatientJournal = this.updatePatientJournal.bind(this);
    this.updatePatientJournalState = this.updatePatientJournalState.bind(this);
    this.DeletePatientJournal = this.DeletePatientJournal.bind(this);
    this.redirectToPatienJournals = this.redirectToPatienJournals.bind(this);
  }
  updatePatientJournal() {
    alert("Saving!");
  }

  updatePatientJournalState(event){

    //Never change the object directly.Always exchange the object with Redux.
    let patientJournal = Object.assign({}, this.state.patientJournal);
    const field = event.target.name;
    patientJournal[field] = event.target.value;

    this.setState({ patientJournal: patientJournal });
  }

  redirectToPatienJournals() {
    browserHistory.push('/');
  }

  DeletePatientJournal() {
    alert("Deleting!");
  }

  onClickSave() {

  }


  render() {
    return (
      <div className="content-wrapper">
        <div className="row">
          <div className="col-md-offset-2 col-md-8">

            <h1>Journal</h1>

            <div className="panel panel-info">
              <div className="panel-heading">Skapa eller uppdatera en patient journal</div>
              <div className="panel-body">

                <JournalForm
                  onSave={this.updatePatientJournal}
                  onChange={this.updatePatientJournalState}
                  patientJournal={this.state.patientJournal}
                  errors={this.state.errors}
                  //allIllnessTypes={this.props.illnessTypes}
                  onClick={this.redirectToPatienJournals}
                  onDelete={this.DeletePatientJournal}
                  deleteclassName={"btn btn-warning backButton"}
                  className="btn btn-info backButton" />

              </div>

            </div>
          </div>

        </div>
      </div>
    );
  }
}

AddNewJournalEntry.propTypes = {
  patientJournal: PropTypes.object.isRequired

};

export default AddNewJournalEntry;
