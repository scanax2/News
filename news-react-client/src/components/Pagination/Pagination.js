import React from 'react';
import { Link } from 'react-router-dom';

const Pagination = ({ currentPage, totalPages }) => {

    var pageLinks = [];
    // Add links to each page in the pagination
    for (let i = 1; i <= totalPages; i++) {
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
