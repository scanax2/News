import "./newsPageStyle.css"
import React, { useState, useEffect } from "react";
import { useParams, useSearchParams } from "react-router-dom";
import Constants from "../utilities/Constants";
import Navbar from "../components/Navbar/Navbar";
import Post from "../components/NewsPost/Post";
import Pagination from "../components/Pagination/Pagination";


function NewsPage() {
  
  const rowsNumber = 3;
  const columnsNumber = 3;

  const { country, category } = useParams();
  const [page, setPage] = useState(0)
  const [news, setNews] = useState([]);
  const [searchParams, setSearchParams] = useSearchParams();

  const [totalPages, setTotal] = useState();

  useEffect(() => {
    console.log(`News for ${country}/${category}?page=${page}`)
    getNews(page)
  }, [country, category, page]);

  useEffect(() => {
    setTotal(prevTotal => 5)
  }, [totalPages]);

  useEffect(() => {
   setPage(prevPage => parseInt(searchParams.get("page")))
  }, [searchParams]);

  function getNews(pageNumber) {
    const url = Constants.API_URL_GET_NEWS
      .replace("{pageNumber}", pageNumber - 1)
      .replace("{pageSize}", rowsNumber*columnsNumber);

    fetch(url, {
      method: 'GET'
    })
      .then(response => response.json())
      .then(newsFromServer => {
        console.log(newsFromServer);
        setNews(newsFromServer);
      })
      .catch((error) => {
        console.log(error);
      });
  }

  return (
    <div className="container custom-container">
      <div className="row">
        {news.map((post, index) => (
          <div key={index} className="col-md-4">
            <Post post={post} />
          </div>
        ))}
      </div>
      <div className="d-flex justify-content-center mt-5 mb-5">
        <Pagination currentPage={page} totalPages={totalPages}/>
      </div>

    </div>
  );
}

export default NewsPage;
