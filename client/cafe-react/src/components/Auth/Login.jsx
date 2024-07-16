import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { FaUserLock } from "react-icons/fa6";
import { useDispatch } from "react-redux";
import { setCredentials } from "../../redux/slices/authSlice";
import { Link } from "react-router-dom";
import styles from "./Login.module.scss";
import { MdVisibility, MdVisibilityOff } from "react-icons/md";
import api from "../../API/apiConfig";
import { jwtDecode } from "jwt-decode";

const Login = () => {
  const [showPassword, setShowPassword] = React.useState(false);
  const userRef = React.useRef();
  const errRef = React.useRef();
  const [userName, setUser] = useState("");
  const [password, setPwd] = useState("");
  const [errMsg, setErrMsg] = useState("");
  const navigate = useNavigate();

  const dispatch = useDispatch();

  useEffect(() => {
    if (userRef.current) {
      userRef.current.focus();
    }
  }, []);

  useEffect(() => {
    setErrMsg("");
  }, [userName, password]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      console.log("try");
      const response = await api.post("/Auth/Login", { userName, password });
      console.log(response.data);
      const accessToken = response.data;
      const decodeToken = jwtDecode(accessToken);
      dispatch(
        setCredentials({
          accessToken,
          userName,
          role: decodeToken["role"],
        }),
      );
      setPwd("");
      navigate("/accountPage");
    } catch (err) {
      console.log(err);
      if (!err.response) {
        setErrMsg("No Server Response");
      } else if (err.response.status === 400) {
        setErrMsg("Missing Username or Password");
      } else if (err.response?.status === 401) {
        setErrMsg("Unauthorized");
      } else {
        setErrMsg("Login Failed");
      }
      if (errRef.current) {
        errRef.current.focus();
      }
    }
  };

  const handleUserInput = (e) => setUser(e.target.value);
  const handlePwdInput = (e) => setPwd(e.target.value);
  const togglePasswordVisibility = () => {
    setShowPassword(!showPassword);
  };

  const content = (
    <>
      <div className={styles.gridContainer}>
        <FaUserLock className="iconButton" />
        <p className={styles.signUpTitle}>Sign IN</p>
        <form onSubmit={handleSubmit} className={styles.formContainer}>
          <div className="formField">
            <input
              className={`input ${styles.input}`}
              type="text"
              id="userName"
              placeholder="UserName"
              onChange={handleUserInput}
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
          <div className={styles.buttonField}>
            <button type="submit" className="colorButton">
              Sign IN
            </button>
          </div>
        </form>
        <div className={styles.linkForm}>
          <Link to="#">Forgot password?</Link>
          <Link to="/registration">Don't have an account? Sign Up</Link>
        </div>
      </div>
    </>
  );
  return content;
};

export default Login;
