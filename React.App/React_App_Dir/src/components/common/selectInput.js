import React, { PropTypes } from 'react';


const SelectInput = ({ name, onChage, defaultOption, value, error, options }) => {
  return (
    <div className="form-group">

      <div className={field}>

        <select
          name={name}
          value={value}
          onChange={onChange}
          className="form-control">
          {/*<option value={defaultOption}>{defaultOption}</option>*/}
        </select>
        {error && <div className="alert alert-danger">{error}</div>}
      </div>
    </div>
  );
}

SelectInput.prototypes = {
  name: PropTypes.string.isRequired,
  label: PropTypes.string.isRequired,
  value: PropTypes.string.isRequired,
  onChange: PropTypes.func.isRequired,
  defaultOption: PropTypes.string,
  error: PropTypes.string,
  options: PropTypes.arrayOf(PropTypes.object)
}

export default SelectInput;
