import React, { useState } from "react";
import Constants from "../utilities/Constants";


function TestNewsApiPage() {

  const [news, setNews] = useState([]);

  function getNews(pageNumber, pageSize) {
    const url = Constants.API_URL_GET_NEWS
      .replace("{pageNumber}", pageNumber)
      .replace("{pageSize}", pageSize);

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
        alert(error);
      });
  }

  function getNewsTest() {
    getNews(0, 5)
  }

  return (
    <div className="container">
      <div className="row min-vh-100">
        <div className="col d-flex flex-column justify-content-center align-items-center">
          <div>
            <h1>ASP.NET Core</h1>
            <div className="mt-5">
              <button onClick={getNewsTest} className="btn btn-dark btn-lg w-100">Get News from server</button>
            </div>
          </div>

          {news.length > 0 && renderPostsTable()}
        </div>
      </div>
    </div>
  );

  function renderPostsTable() {
    return (
      <div className="table-responsive mt-5">
        <table className="table table-bordered border-dark">
          <thead>
            <tr>
              <th scope="col">PK</th>
              <th scope="col">Title</th>
              <th scope="col">Description</th>
              <th scope="col">Url</th>
              <th scope="col">PublishedAt</th>
            </tr>
          </thead>
          <tbody>
            {news.map((n) => {
              return (
                <tr key={n.id}>
                  <th scope="row">{n.id}</th>
                  <td>{n.title}</td>
                  <td>{n.description}</td>
                  <td>{n.url}</td>
                  <td>{n.publishedAt}</td>
                </tr>)
            })}
          </tbody>
        </table>

        <button onClick={() => setNews([])} className="btn btn-secondary btn-lg w-100 mt-4">Clear</button>
      </div>
    );
  }
}

export default TestNewsApiPage;
