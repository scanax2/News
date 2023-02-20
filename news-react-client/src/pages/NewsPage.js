import "./newsPageStyle.css"
import React, { useState, useEffect } from "react";
import { useParams, useSearchParams } from "react-router-dom";
import Constants from "../utilities/Constants";
import Post from "../components/NewsPost/Post";
import Pagination from "../components/Pagination/Pagination";
import { navbarCategoryTabs, navbarCountryTabs } from "../components/Navbar/NavbarData";
import { fetchFromServer } from "../utilities/RequestManager";


function NewsPage() {

  const rowsNumber = 3;
  const columnsNumber = 3;

  const { country, category } = useParams();
  const [page, setPage] = useState(0);
  const [news, setNews] = useState([]);
  const [searchParams, setSearchParams] = useSearchParams();

  const [totalPages, setTotalPages] = useState();

  useEffect(() => {
    getNews(page)
  }, [page]);

  useEffect(() => {
    console.log(`News for ${country}/${category}?page=${page}`)
    getTotalPages()
    setPage(1)
    getNews(1)
  }, [country, category]);

  useEffect(() => {
    getTotalPages()
  }, [totalPages]);

  useEffect(() => {
    setPage(prevPage => parseInt(searchParams.get("page")))
  }, [searchParams]);

  function getNews(pageNumber) {
    pageNumber -= 1

    const categoryValue = navbarCategoryTabs.filter(c => c.text === category)[0].value
    const countryValue = navbarCountryTabs.filter(c => c.text === country)[0].value

    const url = Constants.API_URL_GET_NEWS
      .replace("{pageNumber}", pageNumber)
      .replace("{pageSize}", rowsNumber * columnsNumber)
      .replace("{category}", categoryValue)
      .replace("{country}", countryValue)

    console.log(url)

    fetchFromServer(url, 'GET', setNews)
  }

  function getTotalPages() {
    const pageSize = rowsNumber * columnsNumber
    const categoryValue = navbarCategoryTabs.filter(c => c.text === category)[0].value
    const countryValue = navbarCountryTabs.filter(c => c.text === country)[0].value

    const url = Constants.API_URL_GET_TOTAL_PAGES
      .replace("{pageNumber}", 0)
      .replace("{pageSize}", pageSize)
      .replace("{category}", categoryValue)
      .replace("{country}", countryValue)
    fetchFromServer(url, 'GET', setTotalPages)
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
        <Pagination currentPage={page} totalPages={totalPages} />
      </div>

    </div>
  );
}

export default NewsPage;
