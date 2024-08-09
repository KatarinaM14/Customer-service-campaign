import React from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import SignIn from './pages/SignIn';
import SignUp from './pages/SignUp';
import RewardCustomer from './pages/RewardCustomer';
import { UserProvider } from './context/UserContext';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    {/* <App /> */}
    <UserProvider>
      <Router>
        <Routes>
          <Route path="/" element={<SignIn />} />
          <Route path="/signup" element={<SignUp />} />
          <Route path="/login" element={<SignIn />} />
          <Route path="/rewardCustomer" element={<RewardCustomer />} />
        </Routes>
      </Router>
    </UserProvider>
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
