import './postStyle.css'
import React from 'react';
import RenderSmoothImage from './RenderSmoothImage';

const Post = ({ post }) => {

    function getDateStr(date) {
        var myDate = new Date(date);
        var output = myDate.getDate() + "/" + (myDate.getMonth() + 1) + "/" + myDate.getFullYear();
        return output
    }

    function onImageError(e) {
        e.target.src = "https://via.placeholder.com/300x200?text=No%20Image";
    }

    return (
        <div className="post card mt-4 d-flex flex-column">
            <a href={post.url}>
                <RenderSmoothImage src={post.imageUrl} alt={post.title} />
            </a>
            <div className="card-body d-flex flex-column">
                <h3 className="card-title custom-post-title">{post.title}</h3>
                <p className="card-text custom-post-description">{post.description}</p>
                <div className="mt-auto">
                    <a href={post.url}>Read more</a>
                    <p className="card-text">Published at: {getDateStr(post.publishedAt)}</p>
                </div>
            </div>
        </div>
    );
};

export default Post;
