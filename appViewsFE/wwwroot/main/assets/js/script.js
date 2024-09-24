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

});


const trendingSlide = new Swiper('.trending-slide', {
    // Optional parameters
    loop: true,
    autoplay: true,
    // If we need pagination
    slidesPerView: 3,
    spaceBetween: 0,
    watchSlidesProgress: true,
    centeredSlides: true,
    pagination: {
        el: '.swiper-pagination',
    },
    navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev',
    },
});


const section5Slide = new Swiper('.section-5-slide', {
    // Optional parameters
    loop: true,
    autoplay: true,
    // If we need pagination
    slidesPerView: 3,
    spaceBetween: 0,
    watchSlidesProgress: true,
    centeredSlides: true,
    pagination: {
        el: '.swiper-pagination',
    },
});
