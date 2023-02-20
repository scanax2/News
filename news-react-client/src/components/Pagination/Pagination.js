import React from 'react';
import { Link } from 'react-router-dom';

const Pagination = ({ currentPage, totalPages }) => {

    const MAX_PAGES_ON_PAGE = 7

    var pageLinks = [];
    // Add links to each page in the pagination

    let from = Math.max(1, currentPage - MAX_PAGES_ON_PAGE + 2);
    let to = Math.min(totalPages, from + MAX_PAGES_ON_PAGE - 1);

    for (let i = from; i <= to; i++) {
        const isActive = i === currentPage;
        pageLinks.push(
            <li key={i} className={`page-item${isActive ? ' active' : ''}`}>
                <Link to={`?page=${i}`} className="page-link">{i}</Link>
            </li>
        );
    }

    return (
        <nav>
            <ul className="pagination pagination-lg">
                <li className={`page-item${currentPage === 1 ? ' disabled' : ''}`}>
                    <Link to={`?page=${currentPage - 1}`} className="page-link">&laquo;</Link>
                </li>
                {pageLinks}
                <li className={`page-item${currentPage === totalPages ? ' disabled' : ''}`}>
                    <Link to={`?page=${currentPage + 1}`} className="page-link">&raquo;</Link>
                </li>
            </ul>
        </nav>
    );
};

export default Pagination;
