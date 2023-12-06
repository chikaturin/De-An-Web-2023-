const opentb1 = document.getElementById('open_tb1')
const opentb2 = document.getElementById('open_tb2')
const tb1 = document.getElementById('table1')
const tb2 = document.getElementById('table2')

opentb1.addEventListener("click", function () {
    if (tb1.style.display = "none") {
        tb1.style.display = "block"
        tb2.style.display = "none"
    }
    else {
        tb2.style.display = "block"
    }
});
opentb2.addEventListener("click", function () {
    if (tb2.style.display = "none") {
        tb2.style.display = "block"
        tb1.style.display = "none"
    }
    else {
        tb2.style.display = "block"
    }
});