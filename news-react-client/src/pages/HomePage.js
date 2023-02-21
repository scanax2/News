import { useNavigate } from "react-router-dom/dist";
import React, { useState, useEffect, useParams } from "react";


function HomePage({ country, category }) {
  const navigate = useNavigate();

  useEffect(() => {
    if (country != null && category != null) {
      navigate(`/News/${country.key}/${category.key}?page=1`);
    }
  }, [navigate]);

  return <div>Welcome to the news website!</div>;
}

export default HomePage;
