import React from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { browserHistory } from 'react-router';
import * as patientJournalActions from '../../Redux/actions/patientJournalActions';

class AddPatientJournal extends React.Component {
    state = {
        patientJournal: {
            firstName: ""
        }
    };

    handleChage = event => {
        const patientJournal = { ...this.state.patientJournal, firstName: event.target.value };
        this.setState({ patientJournal: patientJournal });
    };

    handleSubmit = event => {
        event.preventDefault();
        this.props.CreatePatientJournal(this.state.patientJournal)
            .then(details => {
                browserHistory.push("/")
            })
            .catch();
        alert("Fix this submit button");
    };

    render() {
        const rowStyle = {
            "marginTop": "25px"
        }
        return (
            <div className="container" >
                <div className="row" style={rowStyle}>
                    <div className="col-md-8 offset-md-2">
                        <h2>Lägg till patient</h2>
                        <form onSubmit={this.handleSubmit}>

                            <div className="input-group">
                                <input type="text"
                                    onChange={this.handleChange}
                                    value={this.state.firstName}
                                    className="form-control" />

                                <span className="input-group-btn">
                                    <button onClick={this.handleSubmit} className="btn btn-success" type="submit">
                                        Skicka
                                </button>
                                </span>
                            </div>

                        </form>
                    </div>
                </div>
            </div>
        );
    }
}

AddPatientJournal.propTypes = {
    CreatePatientJournal: PropTypes.func.isRequired,
    patientJournals: PropTypes.array.isRequired,
}
function mapStateToProps(state) {
    return {
        patientJournals: state.patientJournals
    }
}

function mapDispatchToProps(dispatch) {
    return {
        actions: journal => dispatch(patientJournalActions.CreatePatientJournal(journal))
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(AddPatientJournal);