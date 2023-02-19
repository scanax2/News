const API_BASE_URL_DEVELOPMENT = "https://localhost:7193";
const API_BASE_URL_PRODUCTION = "https://localhost:7193";

const DOMAIN_URL_DEVELOPMENT = "https://localhost:3000";
const DOMAIN_URL_PRODUCTION  = "https://localhost:3000";

const ENDPOINTS = {
    NEWS_API_GET: "NewsAPI/{pageNumber}/{pageSize}/{category}/{country}",
    NEWS_API_CLEAR: "Clear"
}

const DEVELOPMENT = {
    API_URL_GET_NEWS: `${API_BASE_URL_DEVELOPMENT}/${ENDPOINTS.NEWS_API_GET}`,
    DOMAIN_URL: `${DOMAIN_URL_DEVELOPMENT}`
};

const PRODUCTION = {
    API_URL_GET_NEWS: `${API_BASE_URL_PRODUCTION}/${ENDPOINTS.NEWS_API_GET}`,
    DOMAIN_URL: `${DOMAIN_URL_PRODUCTION}`
};

const Constants = process.env.NODE_ENV === "development" ? DEVELOPMENT : PRODUCTION;

export default Constants;
