const slideBar = document.querySelector('.slider')
const imgs = document.querySelectorAll('.imgs')
const mainImg = document.getElementById('main_img')
var currentIndex = 0

const UpdateImgByIndex = (index) => {
    imgs.forEach(img => {
        img.classList.remove('active')
    })

    currentIndex = index
    mainImg.src = imgs[index].getAttribute('src')
    imgs[index].classList.add('active')
}
UpdateImgByIndex(0)

imgs.forEach((img, index) => {
    img.addEventListener('click', () => {
        UpdateImgByIndex(index)
    })
})


setInterval(() => {
    currentIndex++
    if (currentIndex == imgs.length) {
        currentIndex = 0
    }
    UpdateImgByIndex(currentIndex)
}, 3000)

let leftBtn = document.querySelector('.btn.left')
let rightBtn = document.querySelector('.btn.right')

leftBtn.addEventListener('click', () => {
    if (currentIndex == 0) {
        currentIndex = imgs.length - 1
    }
    else {
        currentIndex--
    }

    UpdateImgByIndex(currentIndex)
})
rightBtn.addEventListener('click', () => {
    currentIndex++
    if (currentIndex == imgs.length) {
        currentIndex = 0
    }

    UpdateImgByIndex(currentIndex)
})