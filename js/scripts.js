// Load and display reviews
fetch("js/reviews.json")
  .then((response) => response.json())
  .then((data) => {
    const container = document.getElementById("reviews-container");
    data.forEach((review) => {
      const stars = "★".repeat(review.rating) + "☆".repeat(5 - review.rating);
      const card = `
        <div class="col-md-4 mb-4">
          <div class="card h-100 shadow-sm">
            <div class="card-body">
              <h5 class="card-title">${review.name}</h5>
              <p class="text-warning mb-2">${stars}</p>
              <p class="card-text">${review.text}</p>
            </div>
          </div>
        </div>`;
      container.innerHTML += card;
    });
  });

// Load and display menu
fetch("js/menu.json")
  .then((response) => response.json())
  .then((data) => {
    const menuContainer = document.getElementById("menu-container");
    data.forEach((item) => {
      const card = `
        <div class="col-md-4 mb-4">
          <div class="card h-100 shadow-sm">
            <img src="${item.image}" class="card-img-top" alt="${item.name}">
            <div class="card-body">
              <h5 class="card-title">${item.name} - ${item.price}</h5>
              <p class="card-text">${item.description}</p>
            </div>
          </div>
        </div>`;
      menuContainer.innerHTML += card;
    });
  });

// Live Search for Menu Items
document.addEventListener("DOMContentLoaded", function () {
  const searchInput = document.querySelector(".search-box input");
  const menuCards = document.querySelectorAll(".products .card");

  if (searchInput) {
    searchInput.addEventListener("input", function () {
      const query = this.value.toLowerCase();

      menuCards.forEach((card) => {
        const title = card.querySelector("h3").textContent.toLowerCase();
        card.style.display = title.includes(query) ? "block" : "none";
      });
    });
  }
});
