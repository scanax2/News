const API_BASE_URL_DEVELOPMENT = "https://localhost:7193";
const API_BASE_URL_PRODUCTION = "https://aspnetserverreactnewsserver.azurewebsites.net";

const DOMAIN_URL_DEVELOPMENT = "https://localhost:3000";
const DOMAIN_URL_PRODUCTION  = "https://localhost:3000";

const ENDPOINTS = {
    NEWS_CATEGORIES_API_CATEGORIES_GET: "api/NewsCategories/categories",
    NEWS_CATEGORIES_API_COUNTRIES_GET: "api/NewsCategories/countries",
    NEWS_POSTS_API_TOTAL_PAGES_GET: "api/NewsPosts/{pageSize}/{category}/{country}",
    NEWS_POSTS_API_GET: "api/NewsPosts/{pageNumber}/{pageSize}/{category}/{country}"
}

const DEVELOPMENT = {
    API_URL_GET_TOTAL_PAGES: `${API_BASE_URL_DEVELOPMENT}/${ENDPOINTS.NEWS_POSTS_API_TOTAL_PAGES_GET}`,
    API_URL_GET_NEWS: `${API_BASE_URL_DEVELOPMENT}/${ENDPOINTS.NEWS_POSTS_API_GET}`,
    API_URL_GET_CATEGORIES: `${API_BASE_URL_DEVELOPMENT}/${ENDPOINTS.NEWS_CATEGORIES_API_CATEGORIES_GET}`,
    API_URL_GET_COUNTRIES: `${API_BASE_URL_DEVELOPMENT}/${ENDPOINTS.NEWS_CATEGORIES_API_COUNTRIES_GET}`,
    DOMAIN_URL: `${DOMAIN_URL_DEVELOPMENT}`
};

const PRODUCTION = {
    API_URL_GET_NEWS: `${API_BASE_URL_PRODUCTION}/${ENDPOINTS.NEWS_POSTS_API_GET}`,
    API_URL_GET_TOTAL_PAGES: `${API_BASE_URL_PRODUCTION}/${ENDPOINTS.NEWS_POSTS_API_TOTAL_PAGES_GET}`,
    API_URL_GET_CATEGORIES: `${API_BASE_URL_PRODUCTION}/${ENDPOINTS.NEWS_CATEGORIES_API_CATEGORIES_GET}`,
    API_URL_GET_COUNTRIES: `${API_BASE_URL_PRODUCTION}/${ENDPOINTS.NEWS_CATEGORIES_API_COUNTRIES_GET}`,
    DOMAIN_URL: `${DOMAIN_URL_PRODUCTION}`
};

const Constants = process.env.NODE_ENV === "development" ? DEVELOPMENT : PRODUCTION;

export default Constants;
