import { useState } from "react";
import { CSSTransition, TransitionGroup } from "react-transition-group";
import { Link } from 'react-router-dom';

const Pagination = ({ currentPage, totalPages }) => {

    const MAX_PAGES_ON_PAGE = 7

    const [prevPage, setPrevPage] = useState(currentPage);

    function handleSelectPage(page) {
        setPrevPage(currentPage);
        window.scrollTo(0, 0);
    }

    var pageLinks = [];
    // Add links to each page in the pagination

    let from = Math.max(1, currentPage - MAX_PAGES_ON_PAGE + 2);
    let to = Math.min(totalPages, from + MAX_PAGES_ON_PAGE - 1);

    function renderPageNumbers() {
        const pageNumbers = [];
        for (let i = from; i <= to; i++) {
            pageNumbers.push(
                <li key={i} className="page-item">
                        <Link
                            to={`?page=${i}`}
                            className={
                                "page-link" +
                                (i === currentPage ? " active" : "") +
                                (i === prevPage ? " prev" : "")
                            }
                            onClick={() => handleSelectPage(i)}
                        >
                            {i}
                        </Link>
                    </li>
            );
        }
        return pageNumbers;
    }

    return (
        <nav>
            <ul className="pagination pagination-lg">
                <li className={`page-item${currentPage === 1 ? ' disabled' : ''}`}>
                    <Link to={`?page=${currentPage - 1}`} className="page-link">&laquo;</Link>
                </li>
                {renderPageNumbers()}
                <li className={`page-item${currentPage === totalPages ? ' disabled' : ''}`}>
                    <Link to={`?page=${currentPage + 1}`} className="page-link">&raquo;</Link>
                </li>
            </ul>
        </nav>
    );
};

export default Pagination;
