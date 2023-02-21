import "./newsPageStyle.css"
import React, { useState, useEffect, useRef } from "react";
import { useParams, useSearchParams } from "react-router-dom";
import Constants from "../utilities/Constants";
import Post from "../components/NewsPost/Post";
import Pagination from "../components/Pagination/Pagination";
import { navbarCategoryTabs, navbarCountryTabs } from "../components/Navbar/NavbarData";
import { fetchFromServer } from "../utilities/RequestManager";


function NewsPage() {

  const ROWS_NUMBER = 3;
  const COLUMNS_NUMBER = 3;

  const isFirstRender = useRef(true);
  const isChangeCategoryRender = useRef(false);

  const [isReadyToRenderPosts, setReadyToRenderPosts] = useState(false);

  const { country, category } = useParams();
  const [page, setPage] = useState(0);
  const [news, setNews] = useState([]);
  const [searchParams, setSearchParams] = useSearchParams();

  const [totalPages, setTotalPages] = useState();

  useEffect(() => {
    if (isFirstRender.current) {
      isFirstRender.current = false;
      return;
    }
    if (isChangeCategoryRender.current) {
      isChangeCategoryRender.current = false;
      return;
    }
    handlePageSwitch()
  }, [page]);

  useEffect(() => {
    if (page != 1) {
      isChangeCategoryRender.current = true;
    }
    console.log(`News for ${country}/${category}?page=${page}`)
    handleCategoryChange()
  }, [country, category]);

  useEffect(() => {
    getTotalPages()
  }, [totalPages]);

  useEffect(() => {
    setPage(prevPage => parseInt(searchParams.get("page")))
  }, [searchParams]);

  useEffect(() => {
    if (news.length == 0) {
      return
    }
    setReadyToRenderPosts(true);
  }, [news]);

  function handleCategoryChange() {
    getTotalPages()
    setPage(1)
    getNews(1)
  }

  function handlePageSwitch() {
    getNews(page)
  }

  function getNews(pageNumber) {
    setReadyToRenderPosts(false);

    pageNumber -= 1

    const categoryValue = navbarCategoryTabs.filter(c => c.text === category)[0].value
    const countryValue = navbarCountryTabs.filter(c => c.text === country)[0].value

    const url = Constants.API_URL_GET_NEWS
      .replace("{pageNumber}", pageNumber)
      .replace("{pageSize}", ROWS_NUMBER * COLUMNS_NUMBER)
      .replace("{category}", categoryValue)
      .replace("{country}", countryValue)

    console.log(url)

    fetchFromServer(url, 'GET', setNews)
  }

  function getTotalPages() {
    const pageSize = ROWS_NUMBER * COLUMNS_NUMBER
    const categoryValue = navbarCategoryTabs.filter(c => c.text === category)[0].value
    const countryValue = navbarCountryTabs.filter(c => c.text === country)[0].value

    const url = Constants.API_URL_GET_TOTAL_PAGES
      .replace("{pageNumber}", 0)
      .replace("{pageSize}", pageSize)
      .replace("{category}", categoryValue)
      .replace("{country}", countryValue)
    fetchFromServer(url, 'GET', setTotalPages)
  }

  function renderPosts() {
    return news.map((post, index) => (
      <div key={index} className="col-md-4">
        <Post post={post} />
      </div>
    ))
  }

  return (
    <div className="container custom-container">
      {isReadyToRenderPosts &&
      <>
        <div className="row">
          {renderPosts()}
        </div>
        <div className="d-flex justify-content-center mt-5 mb-5">
          <Pagination currentPage={page} totalPages={totalPages} />
        </div>
      </>
      }
    </div>
  );
}

export default NewsPage;
