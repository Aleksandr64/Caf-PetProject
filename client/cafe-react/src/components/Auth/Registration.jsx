import React from "react";
import { useNavigate } from "react-router-dom";
import "../../Style/style.scss";
import styles from "./Registration.module.scss";
import { MdVisibility } from "react-icons/md";
import { MdVisibilityOff } from "react-icons/md";
import api from "../../API/apiConfig";

const Registration = () => {
  const [showPassword, setShowPassword] = React.useState(false);
  const [firstName, setFirstName] = React.useState("");
  const [lastName, setLastName] = React.useState("");
  const [userName, setUserName] = React.useState("");
  const [password, setPassword] = React.useState("");
  const [email, setEmail] = React.useState("");
  const [phoneNumber, setPhoneNumber] = React.useState("");

  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await api.post("Auth/RegisterUser", {
        firstName,
        lastName,
        userName,
        password,
        email,
        phoneNumber,
      });
      setPassword("");
      navigate("/login");
    } catch (err) {
      console.log(err);
      if (!err.response) {
        console.log("No Server Response");
      } else if (err.response.status === 400) {
        console.log("Missing Username or Password");
      } else if (err.response.status === 401) {
        console.log("Unauthorized");
      } else {
        console.log("Login Failed");
      }
    }
  };

  const handleFirstNameInput = (e) => setFirstName(e.target.value);
  const handleLastNameInput = (e) => setLastName(e.target.value);
  const handleUserNameInput = (e) => setUserName(e.target.value);
  const handlePwdInput = (e) => setPassword(e.target.value);
  const handleEmailInput = (e) => setEmail(e.target.value);
  const handlePhoneNumberInput = (e) => setPhoneNumber(e.target.value);
  const togglePasswordVisibility = () => setShowPassword(!showPassword);

  const content = (
    <>
      <div className={styles.gridContainer}>
        <p className={styles.signUpTitle}>Sign Up</p>
        <form onSubmit={handleSubmit} className={styles.formContainer}>
          <div className="formField">
            <input
              className={`input ${styles.input}`}
              type="text"
              id="firstName"
              placeholder="First Name"
              onChange={handleFirstNameInput}
              required
            />
          </div>
          <div className="formField">
            <input
              className={`input ${styles.input}`}
              type="text"
              id="lastName"
              placeholder="Last Name"
              onChange={handleLastNameInput}
              required
            />
          </div>
          <div className="formField">
            <input
              className={`input ${styles.input}`}
              type="text"
              id="userName"
              placeholder="UserName"
              onChange={handleUserNameInput}
              required
            />
          </div>
          <div className="formField">
            <input
              className={`input ${styles.input}`}
              type={showPassword ? "text" : "password"}
              id="password"
              placeholder="Password"
              onChange={handlePwdInput}
              required
            />
            <button
              type="button"
              className="roundButton"
              onClick={togglePasswordVisibility}
            >
              {showPassword ? (
                <MdVisibility className="iconButton" />
              ) : (
                <MdVisibilityOff className="iconButton" />
              )}
            </button>
          </div>
          <div className="formField">
            <input
              className={`input ${styles.input}`}
              type="email"
              id="email"
              placeholder="Email"
              onChange={handleEmailInput}
              required
            />
          </div>
          <div className="formField">
            <input
              className={`input ${styles.input}`}
              type="tel"
              id="phoneNumber"
              placeholder="Phone Number"
              onChange={handlePhoneNumberInput}
              required
            />
          </div>
          <div className={styles.buttonField}>
            <button type="submit" className="colorButton">
              Sign Up
            </button>
          </div>
        </form>
      </div>
    </>
  );
  return content;
};

export default Registration;
