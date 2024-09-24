import Swiper from 'swiper/bundle';
import 'swiper/css';

let btn = document.getElementById("btn-to-top");

window.onscroll = function() {scrollFunction()};

function scrollFunction() {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
        btn.style.display = "flex";
    } else {
        btn.style.display = "none";
    }
}

const bannerSlide = new Swiper('.banner-slide', {
    // Optional parameters
    loop: true,
    autoplay: true,
    // If we need pagination
    pagination: {
        el: '.swiper-pagination',
    },
    // Navigation arrows
    navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev',
    },
    // If we need pagination
    slidesPerView: 5,
    spaceBetween: 0,
    watchSlidesProgress: true,
    centeredSlides: true,
});

const trendingSlide = new Swiper('.trending-slide', {
    loop: true,
    slidesPerView: 1.5,
    spaceBetween: 0,
    watchSlidesProgress: true,
    centeredSlides: true,
    pagination: {
        el: '.swiper-pagination',
    },
    // Navigation arrows
    navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev',
    },
});

const productSlide = new Swiper('.product-slide', {
    loop: true,
    slidesPerView: 1.5,
    spaceBetween: 0,
    watchSlidesProgress: true,
    centeredSlides: true,
    pagination: {
        el: '.swiper-pagination',
    },
    // Navigation arrows
    navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev',
    },
});

const section5Slide = new Swiper('.section-5-slide', {
    // Optional parameters
    loop: true,
    // autoplay: true,
    // If we need pagination
    pagination: {
        el: '.swiper-pagination',
    },
});

const bannerItem = document.getElementsByClassName('banner-item');
if (bannerItem.length > 0) {
    const galleryContainer = document.querySelector(".banner");
    const galleryItems = galleryContainer.querySelectorAll(".banner-item");
    if (galleryItems.length) {
        const updateBannerItems = () => {
            galleryItems.forEach((item) => {
                if (item.isHovered) {
                    document.querySelector('.banner-item.default').classList.remove('active')
                    item.classList.add("active");
                    item.querySelector('.banner-title').style = 'visibility: visible;animation-delay: 0.1s;animation-name: bounceInUp;'
                    item.querySelector('.banner-description').style = 'visibility: visible;animation-delay: 0.2s;animation-name: bounceInUp;'
                    item.querySelector('.shop-link').style = 'visibility: visible;animation-delay: 0.3s;animation-name: bounceInUp;'
                    item.querySelector('.banner-title').classList.add('bounceInUp')
                    item.querySelector('.banner-title').classList.add('animated')
                    item.querySelector('.banner-description').classList.add('bounceInUp')
                    item.querySelector('.banner-description').classList.add('animated')
                    item.querySelector('.shop-link').classList.add('bounceInUp')
                    item.querySelector('.shop-link').classList.add('animated')

                    window.wow.sync()

                } else {
                    item.classList.remove("active");
                    item.querySelector('.banner-title').style = ''
                    item.querySelector('.banner-description').style = ''
                    item.querySelector('.shop-link').style = ''
                    item.querySelector('.banner-title').classList.remove('bounceInUp')
                    item.querySelector('.banner-title').classList.remove('animated')
                    item.querySelector('.banner-description').classList.remove('bounceInUp')
                    item.querySelector('.banner-description').classList.remove('animated')
                    item.querySelector('.shop-link').classList.remove('bounceInUp')
                    item.querySelector('.shop-link').classList.remove('animated')

                }
            })
        }

        document.querySelector('.section-banner').addEventListener('mouseleave',function (){
            document.querySelector('.banner-item.active .banner-title').style = ''
            document.querySelector('.banner-item.active .banner-description').style = ''
            document.querySelector('.banner-item.active .shop-link').style = ''
            document.querySelector('.banner-item.active .banner-title').classList.remove('bounceInUp')
            document.querySelector('.banner-item.active .banner-title').classList.remove('animated')
            document.querySelector('.banner-item.active .banner-description').classList.remove('bounceInUp')
            document.querySelector('.banner-item.active .banner-description').classList.remove('animated')
            document.querySelector('.banner-item.active .shop-link').classList.remove('bounceInUp')
            document.querySelector('.banner-item.active .shop-link').classList.remove('animated')
            document.querySelector('.banner-item.active').classList.remove('active')
            document.querySelector('.banner-item.default').classList.add('active')
            document.querySelector('.banner-item.active .banner-title').style = 'visibility: visible;animation-delay: 0.1s;animation-name: bounceInUp;'
            document.querySelector('.banner-item.active .banner-description').style = 'visibility: visible;animation-delay: 0.2s;animation-name: bounceInUp;'
            document.querySelector('.banner-item.active .shop-link').style = 'visibility: visible;animation-delay: 0.3s;animation-name: bounceInUp;'
            document.querySelector('.banner-item.active .banner-title').classList.add('bounceInUp')
            document.querySelector('.banner-item.active .banner-title').classList.add('animated')
            document.querySelector('.banner-item.active .banner-description').classList.add('bounceInUp')
            document.querySelector('.banner-item.active .banner-description').classList.add('animated')
            document.querySelector('.banner-item.active .shop-link').classList.add('bounceInUp')
            document.querySelector('.banner-item.active .shop-link').classList.add('animated')


        })

        galleryItems[2].isHovered = true;
        updateBannerItems();
        galleryItems.forEach((item) => {
            item.addEventListener("mouseenter", () => {
                galleryItems.forEach((otherItem) => {
                    otherItem.isHovered = otherItem === item;
                })
                updateBannerItems();
            })
        })
    }
}

// var btn = $('#btn-to-top');
//
// $(window).scroll(function() {
//     if ($(window).scrollTop() > 300) {
//         btn.addClass('show');
//     } else {
//         btn.removeClass('show');
//     }
// });
//
// btn.on('click', function(e) {
//     e.preventDefault();
//     $('html, body').animate({scrollTop:0}, '300');
// });
