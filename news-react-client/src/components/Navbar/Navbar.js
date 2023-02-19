import "./navbarStyle.css"
import React, { useState, useParams, useEffect } from 'react';
import { useNavigate, useSearchParams } from 'react-router-dom';
import { navbarCountryTabs, navbarCategoryTabs } from "./NavbarData";

function Navbar() {

    const [isOverlay, toggleOverlay] = useState(false);

    const [country, setCountry] = useState(navbarCountryTabs[0]);
    const [category, setCategory] = useState(navbarCategoryTabs[0]);
    const [searchParams, setSearchParams] = useSearchParams();

    const navigate = useNavigate();

    function setOverlay() {
        toggleOverlay(prevState => !prevState)
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
        const page = searchParams.get("page")
        const url = `News/${country}/${category}?page=${page}`;
        navigate(url);
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
                        {navbarCategoryTabs.map((category) => {
                            return (<li key={category} className="nav-item">
                                <button className="nav-link" onClick={() => handleCategoryClick(category)}>{category}</button>
                            </li>)
                        })}
                        <li className="nav-item dropdown">
                            <a className="nav-link dropdown-toggle" id="navbarDropdownMenuLink" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                Region
                            </a>
                            <div className="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
                                {navbarCountryTabs.map((country) => {
                                    return (<a key={country} className="dropdown-item" onClick={() => handleCountryClick(country)}>{country}</a>)
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
