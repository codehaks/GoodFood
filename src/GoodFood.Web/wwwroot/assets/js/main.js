(function ($) {
    "use strict";
    /* -------------------------------------
               Prealoder
         -------------------------------------- */
    $(window).on('load', function (event) {
        $('.js-preloader').delay(500).fadeOut(500);
    });



    /* -------------------------------------
          Open Search
    -------------------------------------- */
    $('.searchbtn').on('click', function () {
        $('.search-area').addClass('open');
    });
    $('.close-searchbox').on('click', function () {
        $('.search-area').removeClass('open');
    });

    /* -------------------------------------
          Language Dropdown
    -------------------------------------- */
    $(".language-option").each(function () {
        var each = $(this)
        each.find(".lang-name").html(each.find(".language-dropdown-menu a:nth-child(1)").text());
        var allOptions = $(".language-dropdown-menu").children('a');
        each.find(".language-dropdown-menu").on("click", "a", function () {
            allOptions.removeClass('selected');
            $(this).addClass('selected');
            $(this).closest(".language-option").find(".lang-name").html($(this).text());
        });
    })
    $('.user-option').on('click', function () {
        $('.user-menuitem').toggleClass('open');
    });

    $('select').on('change', function () {
        if ($(this).val())
        $(this).css('color', 'white')
        else
        $(this).css('color', 'white')
    });

    /* -------------------------------------
              Counter 
    -------------------------------------- */
    $(".odometer").appear(function (e) {
        var odo = $(".odometer");
        odo.each(function () {
            var countNumber = $(this).attr("data-count");
            $(this).html(countNumber);
        });
    });
    /* -------------------------------------
           Product Quantity
     -------------------------------------- */
    var minVal = 1,
        maxVal = 20;
    $(".increaseQty").on('click', function () {
        var $parentElm = $(this).parents(".qtySelector");
        $(this).addClass("clicked");
        setTimeout(function () {
            $(".clicked").removeClass("clicked");
        }, 100);
        var value = $parentElm.find(".qtyValue").val();
        if (value < maxVal) {
            value++;
        }
        $parentElm.find(".qtyValue").val(value);
    });
    // Decrease product quantity on cart page
    $(".decreaseQty").on('click', function () {
        var $parentElm = $(this).parents(".qtySelector");
        $(this).addClass("clicked");
        setTimeout(function () {
            $(".clicked").removeClass("clicked");
        }, 100);
        var value = $parentElm.find(".qtyValue").val();
        if (value > 1) {
            value--;
        }
        $parentElm.find(".qtyValue").val(value);
    });




    /* -------------------------------------
           Product Slider  
   -------------------------------------- */
    var swiper = new Swiper(".mySwiper", {
        spaceBetween: 25,
        slidesPerView: 3,
        freeMode: true,
        watchSlidesVisibility: true,
        watchSlidesProgress: true,
        loop: true,
        navigation: {
            nextEl: ".product-slider-next",
            prevEl: ".product-slider-prev",
        }
    });
    var swiper2 = new Swiper(".mySwiper2", {
        spaceBetween: 10,
        loop: true,
        thumbs: {
            swiper: swiper,
        },
    });


    /* -------------------------------------
           Feature  Slider 
     -------------------------------------- */
    var feature_slider = new Swiper(".feature-slider", {
        loop: true,
        spaceBetween: 30,
        autoplay: {
            delay: 2000,
            disableOnInteraction: true,
        },
        speed: 1500,
        pagination: {
            el: ".feature-pagination",
            clickable: true,
        },
        breakpoints: {
            0: {
                slidesPerView: 1,

            },
            576: {
                slidesPerView: 1.5,

            },
            768: {
                slidesPerView: 1.8

            },
            992: {
                slidesPerView: 2.8

            },
            1200: {
                slidesPerView: 3.4


            },
            1600: {
                slidesPerView: 3.8


            }
        }
    });


    /* -------------------------------------
           Partner Slider 
     -------------------------------------- */
    var service_slider = new Swiper(".partner-slider", {
        loop: true,
        spaceBetween: 30,
        autoplay: {
            delay: 2000,
            disableOnInteraction: true,
        },
        speed: 1500,
        breakpoints: {
            0: {
                slidesPerView: 3,
                spaceBetween: 15

            },
            576: {
                slidesPerView: 3,
                spaceBetween: 15

            },
            768: {
                slidesPerView: 4

            },
            992: {
                slidesPerView: 5

            },
            1200: {
                slidesPerView: 5


            }
        }
    });
    /* ----------------------------------------
           Magnific Popup Video
     ------------------------------------------*/
    $('.video-play').magnificPopup({
        type: 'iframe',
        mainClass: 'mfp-fade',
        preloader: true,
    });
    /* -------------------------------------
           Testimonials Slider 
     -------------------------------------- */
    var testimonial_slider_one = new Swiper(".testimonial-slider-one", {
        loop: true,
        spaceBetween: 30,
        autoplay: {
            delay: 2000,
            disableOnInteraction: true,
        },
        slidesPerView: 3,
        speed: 1500,
        centeredSlides: true,
        pagination: {
            el: ".testimonial-one-pagination",
            clickable: true,
        },
        breakpoints: {
            0: {
                slidesPerView: 1

            },
            768: {
                slidesPerView: 2

            },
            1200: {
                slidesPerView: 3


            }
        }
    });
    var galleryThumbs = new Swiper('.testimonial-slider-thumbs', {
        spaceBetween: 20,
        slidesPerView: 3,
        loop: true,
        centeredSlides: true,
        watchSlidesVisibility: true,
        watchSlidesProgress: true,
        centerInsufficientSlides: true,
        slideToClickedSlide: true,
        speed: 2500,
    });
    var galleryTop = new Swiper('.testimonial-slider-two', {
        spaceBetween: 30,
        centeredSlides: true,
        slidesPerView: 1,
        speed: 2000,
        autoplay: {
            delay: 6000,
            disableOnInteraction: false,
        },
        navigation: {
            nextEl: '.testimonial-two-next',
            prevEl: '.testimonial-two-next',
        },
        thumbs: {
            swiper: galleryThumbs
        },
        on: {
            slideChange: function () {
                let activeIndex = this.activeIndex + 1;

                let activeSlide = document.querySelector(`.testimonial-slider-thumbs .swiper-slide:nth-child(${activeIndex})`);
                let nextSlide = document.querySelector(`.testimonial-slider-thumbs .swiper-slide:nth-child(${activeIndex + 1})`);
                let prevSlide = document.querySelector(`.testimonial-slider-thumbs .swiper-slide:nth-child(${activeIndex - 1})`);

                if (nextSlide && !nextSlide.classList.contains('swiper-slide-visible')) {
                    this.thumbs.swiper.slideNext()
                } else if (prevSlide && !prevSlide.classList.contains('swiper-slide-visible')) {
                    this.thumbs.swiper.slidePrev()
                }

            }
        }
    });


    /* -------------------------------------
          Mobile Topbar 
    -------------------------------------- */

    $('.mobile-top-bar').on('click', function () {
        $('.header-top-right').addClass('open')
    });
    $('.close-header-top').on('click', function () {
        $('.header-top-right').removeClass('open')
    });

    /* -------------------------------------
          sticky Header
    -------------------------------------- */
    var wind = $(window);
    var sticky = $('.header-wrap');
    wind.on('scroll', function () {
        var scroll = wind.scrollTop();
        if (scroll < 100) {
            sticky.removeClass('sticky');
        } else {
            sticky.addClass('sticky');
        }
    });

    /*---------------------------------
        Responsive mmenu
    ---------------------------------*/
    $('.mobile-menu a').on('click', function () {
        $('.main-menu-wrap').addClass('open');
        $('.mobile-bar-wrap.style2 .mobile-menu').addClass('open');
    });

    $('.mobile_menu a').on('click', function () {
        $(this).parent().toggleClass('open');
        $('.main-menu-wrap').toggleClass('open');
    });

    $('.menu-close').on('click', function () {
        $('.main-menu-wrap').removeClass('open')
    });
    $('.mobile-top-bar').on('click', function () {
        $('.header-top').addClass('open')
    });
    $('.close-header-top button').on('click', function () {
        $('.header-top').removeClass('open')
    });
    var $offcanvasNav = $('.main-menu'),
        $offcanvasNavSubMenu = $offcanvasNav.find('.sub-menu');
    $offcanvasNavSubMenu.parent().prepend('<span class="menu-expand"><i class="las la-angle-down"></i></span>');

    $offcanvasNavSubMenu.slideUp();

    $offcanvasNav.on('click', 'li a, li .menu-expand', function (e) {
        var $this = $(this);
        if (($this.attr('href') === '#' || $this.hasClass('menu-expand'))) {
            e.preventDefault();
            if ($this.siblings('ul:visible').length) {
                $this.siblings('ul').slideUp('slow');
            } else {
                $this.closest('li').siblings('li').find('ul:visible').slideUp('slow');
                $this.siblings('ul').slideDown('slow');
            }
        }
        if ($this.is('a') || $this.is('span') || $this.attr('class').match(/\b(menu-expand)\b/)) {
            $this.parent().toggleClass('menu-open');
        } else if ($this.is('li') && $this.attr('class').match(/\b('has-children')\b/)) {
            $this.toggleClass('menu-open');
        }
    });

    /*---------------------------------
         Scroll animation
    ----------------------------------*/
    AOS.init();
    /*-----------------------------------
         Scroll to top
    ----------------------------------*/

    // Show or hide the  button
    $(window).on('scroll', function (event) {
        if ($(this).scrollTop() > 600) {
            $('.back-to-top').fadeIn(300)
            $('.back-to-top').addClass('open')
        } else {
            $('.back-to-top').fadeOut(300)
            $('.back-to-top').removeClass('open')
        }
    });


    //Animate the scroll to top
    $('.back-to-top').on('click', function (event) {
        event.preventDefault();

        $('html, body').animate({
            scrollTop: 0,
        }, 1500);
    });


})(jQuery);


// function to set a given theme/color-scheme
function setTheme(themeName) {
    localStorage.setItem('theme', themeName);
    document.documentElement.className = themeName;
}

// function to toggle between light and dark theme
function toggleTheme() {
    if (localStorage.getItem('theme') === 'theme-dark') {
        setTheme('theme-light');
    } else {
        setTheme('theme-dark');
    }
}

// Immediately invoked function to set the theme on initial load
(function () {
    if (localStorage.getItem('theme') === 'theme-dark') {
        setTheme('theme-dark');
        document.getElementById('slider').checked = false;
    } else {
        setTheme('theme-light');
        document.getElementById('slider').checked = true;
    }
})();