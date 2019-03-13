import React from 'react';
import TextInput from '../common/textInput';
import SelectInput from '../common/selectInput';

const JournalForm = ({ patientJournal, allIllnessTypes, onSave, onChange, saving, errors, onClick, onDelete, deleteclassName, className, spanText }) => {
  return (
    <div>
      <form>
        {/*formInputs*/}
        <TextInput
          name="firstname"
          value={patientJournal.firstname}
          onChange={onChange}
          error={errors.name}
          spanText="Förnamn" />

        <TextInput
          name="lastname"
          value={patientJournal.lastname}
          onChange={onChange}
          error={errors.name}
          spanText="Efternamn" />
        {/*formInputs*/}

        {/*Buttons*/}
        <input
          type="submit"
          disabled={saving}
          value={saving ? 'Saving...' : 'Save'}
          className="btn btn-primary"
          onClick={onSave} />

        <input
          type="button"
          value="Back"
          className={className}
          onClick={onClick} />

        <input
          type="button"
          value="Delete"
          className={deleteclassName}
          onClick={onDelete} />

        {/*Buttons*/}
      </form>
    </div>
    );
}

JournalForm.propTypes = {
  patientJournal: React.PropTypes.object.isRequired,
  allIllnessTypes: React.PropTypes.array,
  onSave: React.PropTypes.func.isRequired,
  onChange: React.PropTypes.func.isRequired,
  saving: React.PropTypes.bool,
  errors: React.PropTypes.object
};

export default JournalForm;
