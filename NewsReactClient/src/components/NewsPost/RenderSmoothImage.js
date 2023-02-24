import React from 'react';
import "./renderSmoothImage.css"

function RenderSmoothImage({ src, alt }) {
    const [imageLoaded, setImageLoaded] = React.useState(false);

    function onImageError(e) {
        e.target.src = "https://via.placeholder.com/300x200?text=No%20Image";
    }

    return (
        <div className="smooth-image-wrapper">
            <img
                src={src}
                alt={alt}
                className={`smooth-image card-img-top custom-img image-＄{
            imageLoaded ? 'visible' :  'hidden'
          }`}
                style={{ objectFit: "cover" }}
                onLoad={() => setImageLoaded(true)}
                onError={onImageError}
                target="_blank"
            />
            {!imageLoaded && (
                <div className="d-flex justify-content-center">
                    <div className="spinner-border" role="status">
                        <span className="sr-only"></span>
                    </div>
                </div>
            )}
        </div>
    )
}

export default RenderSmoothImage;
