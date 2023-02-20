import "./navbarStyle.css"
import React, { useState, useParams, useEffect } from 'react';
import { useNavigate, useSearchParams } from 'react-router-dom';
import { fetchFromServer } from "../../utilities/RequestManager";
import Constants from "../../utilities/Constants";

function Navbar() {

    const [isOverlay, toggleOverlay] = useState(false);

    const [countriesList, setCountriesList] = useState([])
    const [categoriesList, setCategoriesList] = useState([])

    const [country, setCountry] = useState();
    const [category, setCategory] = useState();
    const [searchParams, setSearchParams] = useSearchParams();

    const navigate = useNavigate();

    function setOverlay() {
        toggleOverlay(prevState => !prevState)
    }

    useEffect(() => {
        fetchFromServer(Constants.API_URL_GET_COUNTRIES, 'GET', handleCountriesLoad)
        fetchFromServer(Constants.API_URL_GET_CATEGORIES, 'GET', handleCategoriesLoad)
    }, [])

    function handleCountriesLoad(data) {
        const list = convertToList(data)
        console.log(list)
        setCountriesList(list)
        setCountry(list[0])
        navigateToUrl()
    }

    function handleCategoriesLoad(data) {
        const list = convertToList(data)
        console.log(list)
        setCategoriesList(list)
        setCategory(list[0])
        navigateToUrl()
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

    useEffect(() => {
        navigateToUrl()
    }, [country, category])

    function handleCountryClick(country) {
        setCountry(prevCountry => country);
    }

    function handleCategoryClick(category) {
        setCategory(prevCategory => category);
    }

    function navigateToUrl() {
        if (country != null && category != null) {
            const page = searchParams.get("page")
            const url = `News/${country.key}/${category.key}?page=${1}`;
            navigate(url);
        }
    }

    return (
        <>
            {
                isOverlay && drawOverlay()
            }
            <nav className="navbar navbar-expand-lg w-100 navbar-dark bg-primary bg-gradient px-4 fixed-top">
                <a className="navbar-brand" href="/">NEWS</a>
                <button onClick={() => { setOverlay() }} className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNavDropdown" aria-controls="navbarNavDropdown" aria-expanded="false" aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="collapse navbar-collapse position-absolute top-50 start-50 translate-middle" id="navbarNavDropdown">
                    <ul className="navbar-nav">
                        {categoriesList.map((category, index) => {
                            return (<li key={index} className="nav-item">
                                <button className="nav-link" onClick={() => handleCategoryClick(category)}>{category.key}</button>
                            </li>)
                        })}
                        <li className="nav-item dropdown">
                            <a className="nav-link dropdown-toggle" id="navbarDropdownMenuLink" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                Country
                            </a>
                            <div className="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
                                {countriesList.map((country, index) => {
                                    return (<a key={index} className="dropdown-item" onClick={() => handleCountryClick(country)}>{country.key}</a>)
                                })}
                            </div>
                        </li>
                    </ul>
                </div>
            </nav>
        </>
    );

    function drawOverlay() {
        return (
            <div className="w-100 h-100 position-absolute bg-dark">TESTSEST</div>
        );
    }
}

export default Navbar;
