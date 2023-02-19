import React, { useState } from "react";
import Navbar from "./components/Navbar/Navbar";
import NewsPage from "./pages/NewsPage";
import HomePage from "./pages/HomePage";
import TestNewsApiPage from "./pages/TestNewsApiPage";
import Constants from "./utilities/Constants";
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';


function App() {

  return (
    <Router>
      <Navbar/>
      <Routes>
        <Route path="/" element={<HomePage/>} />
        <Route path="/News/:country/:category" element={<NewsPage/>} />
      </Routes>
    </Router>
  );
}

export default App;
