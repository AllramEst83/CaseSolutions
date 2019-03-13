import React, { PropTypes } from 'react';

const TextInput = ({ name, onChange, placeholder, value, error, spanText }) => {

  let wrapperClass = 'form-group';

  if (error && error.length > 0) {
    wrapperClass += " " + 'has-error';
  }

  return (
    <div className={wrapperClass}>

      <div className="field">

        <div className="input-group">

          <input
            type="text"
            name={name}
            className="form-control"
            placeholder={placeholder}
            value={value}
            onChange={onChange}
            aria-describedby={name} />

          <span
            className="input-group-addon"
            id={name}>{spanText}</span>

          {error && <div className="alert alert-danger">{error}</div>}

        </div>
      </div>
    </div>
  );
};

TextInput.propTypes = {
  name: PropTypes.string.isRequired,
  onChange: PropTypes.func.isRequired,
  placeholder: PropTypes.string,
  value: PropTypes.string,
  error: PropTypes.string
};

export default TextInput;
