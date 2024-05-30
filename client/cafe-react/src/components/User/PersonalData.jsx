import React, {useEffect} from "react";
import '../../Style/style.scss';
import styles from "./PersonalData.module.scss";
import {selectCurrentUserData, setUserData} from "../../redux/slices/userSlice";
import {useDispatch, useSelector} from "react-redux";
import {useRegistrationMutation} from "../../redux/API/authApiSlice";
import {useGetUserDataMutation} from "../../redux/API/userApiSlice";
import {useNavigate} from "react-router-dom";

const PersonalData = () => {
  const userData = useSelector(state => state.user.userData);
  const dispatch = useDispatch();
  const [registration, { isLoading }] = useRegistrationMutation();
  const [getUserData] = useGetUserDataMutation();
  const navigate = useNavigate();

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await getUserData().unwrap();
        dispatch(setUserData(response));
      }
      catch (err) {
        if (!err.status) {
          console.log("No Server Response");
        } else if (err.status === 400) {
          console.log("Missing Username or Password");
        } else if (err.status === 401) {
          console.log("Unauthorized");
          navigate("/login");
        } else {
          console.log("Login Failed");
        }
      }
    }

    fetchData();
  }, []);

  const handleFirstNameInput = (e) => {
    let temp = { ...userData };
    temp[e.target.id] = e.target.value;
    dispatch(setUserData(temp));
  };

  const changeUserData = () => {

  }

  return (
    <>
      {isLoading ? (
        <h1>Loading...</h1>
      ) : (
        <form>
          <div className={styles.grid}>
            <div className={`formField ${styles.form}`}>
              <input
                value={userData.firstName}
                className={`input ${styles.input}`}
                type="text"
                id="firstName"
                placeholder="First Name"
                onChange={handleFirstNameInput}
                required
              />
            </div>
            <div className={`formField ${styles.form}`}>
              <input
                value={userData.lastName}
                className={`input ${styles.input}`}
                type="text"
                id="lastName"
                placeholder="Last Name"
                onChange={handleFirstNameInput}
                required
              />
            </div>
            <div className="formField">
              <input
                value={userData.email}
                className="input"
                type="email"
                id="email"
                placeholder="Email"
                onChange={handleFirstNameInput}
                required
              />
            </div>
            <div className="formField">
              <input
                value={userData.phoneNumber}
                className="input"
                type="tel"
                id="phoneNumber"
                placeholder="Phone Number"
                onChange={handleFirstNameInput}
                required
              />
            </div>
          </div>
          <div>
            <button type="submit" className="colorButton">
              Save Changes
            </button>
          </div>
        </form>
      )}
    </>
  );
};

export default PersonalData;
