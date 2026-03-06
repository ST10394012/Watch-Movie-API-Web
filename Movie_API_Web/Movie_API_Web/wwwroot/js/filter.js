document.addEventListener('DOMContentLoaded', function () {
    // Get the search input if it exists
    const searchInput = document.getElementById('movieSearch');

    if (searchInput) {
        // Add event listener for real-time filtering
        searchInput.addEventListener('keyup', function () {
            filterMovies(this.value.toLowerCase());
        });
    }
});

/**
 * Filters movie cards based on search term
 * Compares search term against movie title and overview
 * 
 * @param {string} searchTerm - The term to search for
 */
function filterMovies(searchTerm) {
    // Get all movie cards
    const movieCards = document.querySelectorAll('.movie-card');

    movieCards.forEach(card => {
        // Get movie title from the card
        const title = card.querySelector('.card-title');
        const overview = card.querySelector('.card-text');

        if (title && overview) {
            // Convert to lowercase for case-insensitive search
            const titleText = title.textContent.toLowerCase();
            const overviewText = overview.textContent.toLowerCase();

            // Check if search term exists in title or overview
            if (titleText.includes(searchTerm) || overviewText.includes(searchTerm)) {
                // Show the card
                card.closest('.col-md-6, .col-lg-4, .col-lg-2').style.display = 'block';
            } else {
                // Hide the card
                card.closest('.col-md-6, .col-lg-4, .col-lg-2').style.display = 'none';
            }
        }
    });
}

/**
 * Formats date to readable format
 * Example: 2024-01-15 becomes January 15, 2024
 * 
 * @param {string} dateString - Date in YYYY-MM-DD format
 * @returns {string} Formatted date
 */
function formatDate(dateString) {
    if (!dateString) return 'Unknown Date';

    const options = { year: 'numeric', month: 'long', day: 'numeric' };
    const date = new Date(dateString);
    return date.toLocaleDateString('en-US', options);
}

/**
 * Truncates text to specified length and adds ellipsis
 * 
 * @param {string} text - Text to truncate
 * @param {number} length - Maximum length
 * @returns {string} Truncated text
 */
function truncateText(text, length = 100) {
    if (text.length > length) {
        return text.substring(0, length) + '...';
    }
    return text;
}