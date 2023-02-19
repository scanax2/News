import { useNavigate } from "react-router-dom/dist";
import React, { useState, useEffect, useParams } from "react";


function HomePage() {
  const navigate = useNavigate();

  useEffect(() => {
    navigate("/News/World/Trending?page=1");
  }, [navigate]);

  return <div>Welcome to the news website!</div>;
}

export default HomePage;
