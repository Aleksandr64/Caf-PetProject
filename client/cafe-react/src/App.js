import React from "react";
import Home from "./pages/Home";
import Header from "./components/Header/Header";
import { Route, Routes } from "react-router-dom";
import Cart from "./pages/Cart";
import NotFound from "./pages/NotFound";
import RequireAuth from "./components/Auth/RequireAuth";
import UserAccount from "./pages/UserAccount";
import Registration from "./components/Auth/Registration";
import Login from "./components/Auth/Login";
import "./Style/style.scss";

function App() {
  return (
    <>
      <Header />
      <div className="container">
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/order" element={<Cart />} />
          <Route path="/login" element={<Login />} />
          <Route path="/registration" element={<Registration />} />
          <Route element={<RequireAuth />}>
            <Route path="/accountPage" element={<UserAccount />} />
          </Route>
          <Route path="*" element={<NotFound />} />
        </Routes>
      </div>
    </>
  );
}

export default App;
