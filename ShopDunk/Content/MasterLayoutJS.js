const open_find = document.getElementById('open_find')
const close_find = document.getElementById('find')
const search = document.getElementById('searchx')

open_find.addEventListener("click", function () {
    close_find.style.display = "block";
});
search.addEventListener("click", function () {
    close_find.style.display = "none";
});