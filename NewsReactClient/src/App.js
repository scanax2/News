import React, { useState, useEffect } from "react";
import Navbar from "./components/Navbar/Navbar";
import NewsPage from "./pages/NewsPage";
import HomePage from "./pages/HomePage";
import Constants from "./utilities/Constants";
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { fetchFromServer } from "./utilities/RequestManager";


function App() {

  const [countriesList, setCountriesList] = useState([])
  const [categoriesList, setCategoriesList] = useState([])

  const [country, setCountry] = useState();
  const [category, setCategory] = useState();

  useEffect(() => {
    fetchFromServer(Constants.API_URL_GET_COUNTRIES, 'GET', handleCountriesLoad)
    fetchFromServer(Constants.API_URL_GET_CATEGORIES, 'GET', handleCategoriesLoad)
  }, [])

  function handleCountriesLoad(data) {
    const list = convertToList(data)
    setCountriesList(list)
    setCountry(list[0])
  }

  function handleCategoriesLoad(data) {
    const list = convertToList(data)
    setCategoriesList(list)
    setCategory(list[0])
  }

  function convertToList(dict) {
    var arr = [];
    for (var key in dict) {
      if (dict.hasOwnProperty(key)) {
        arr.push({ key: key, value: dict[key] });
      }
    }
    return arr;
  }

  return (
    <Router>
      <Navbar countries={countriesList} categories={categoriesList} setCountry={setCountry} setCategory={setCategory} />
      <Routes>
        {category != null && country != null &&
          <>
            <Route path="/" element={<HomePage country={country} category={category} />} />

            <Route path="/News/:country/:category" element={<NewsPage country={country} category={category} />} />
          </>
        }
      </Routes>
    </Router>
  );
}

export default App;
