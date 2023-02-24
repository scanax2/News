import "./navbarStyle.css"
import React from 'react';

function Navbar({countries, categories, setCountry, setCategory}) {

    function handleCountryClick(country) {
        setCountry(prevCountry => country);
    }

    function handleCategoryClick(category) {
        setCategory(prevCategory => category);
    }

    return (
        <>
            <nav className="navbar navbar-expand-lg w-100 navbar-dark bg-primary bg-gradient px-4 fixed-top">
                <a className="navbar-brand" href="/">NEWS</a>
                <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNavDropdown" aria-controls="navbarNavDropdown" aria-expanded="false" aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="collapse navbar-collapse" id="navbarNavDropdown">
                    <ul className="navbar-nav">
                        {categories.map((category, index) => {
                            return (<li key={index} className="nav-item">
                                <button className="nav-link" onClick={() => handleCategoryClick(category)}>{category.key}</button>
                            </li>)
                        })}
                        <li className="nav-item dropdown">
                            <a className="nav-link dropdown-toggle" id="navbarDropdownMenuLink" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                Country
                            </a>
                            <div className="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
                                {countries.map((country, index) => {
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
            <div className="w-100 h-100 fixed-top bg-dark">TESTSEST</div>
        );
    }
}

export default Navbar;
