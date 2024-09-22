jQuery(document).ready(function () {




    let a = ($('._footer_bottom').offset().top) - ($('.header__mobile').offset().top);


    let width__ = $(window).width()
    if (width__ <= 768) {
        $(window).scroll(function (event) {


            let a = $('._footer_bottom').offset().top;
            let b = $(window).scrollTop();
            console.log(a - b);
            if (a - b > 1005) {

                $('.btn_maket_mobile').css('position', 'fixed');

            }
            else {

                $('.btn_maket_mobile').css('position', 'relative');
            }
        });

    }

    else if (width__ <= 576) {
        $(window).scroll(function (event) {


            let a = $('._footer_bottom').offset().top;
            let b = $(window).scrollTop();
            console.log(a - b);
            if (a - b > 617) {

                $('.btn_maket_mobile').css('position', 'fixed');

            }
            else {

                $('.btn_maket_mobile').css('position', 'relative');
            }
        });
    }
    $('.tab-product li:first-child').addClass('active');

    $(".filter").not('.du-an-noi-that-chung-cu').hide('3000');

    $(".tab-product li").click(function () {
        var value = $(this).attr('data-filter');

        if (value == "du-an-noi-that-chung-cu") {
            //$('.filter').removeClass('hidden');
            $('.filter.du-an-noi-that-chung-cu').show('3000');
            $(".filter").not('.' + value).hide('3000');
        }
        else {
            // $('.filter[filter-item="'+value+'"]').removeClass('hidden');
            // $(".filter").not('.filter[filter-item="'+value+'"]').addClass('hidden');
            $(".filter").not('.' + value).hide('3000');
            $('.filter').filter('.' + value).show('3000');

        }

        if ($(".tab-product li").removeClass("active")) {
            $(this).removeClass("active");
        }
        $(this).addClass("active");
    });




    $('.tab-product-1 li:first-child').addClass('active');
    $('.tab-content .tab-pane:first-child').addClass('in active show')
    $(".tab-product-1 li").click(function () {

        if ($(".tab-product-1 li").removeClass("active")) {
            $(this).removeClass("active");
        }
        $(this).addClass("active");
    });


    // Stick Menu
    jQuery(window).scroll(function () {

        var scroll = jQuery(window).scrollTop();
        if (scroll >= 300) {
            jQuery(".header__main").addClass("fixed");
            jQuery(".header__mobile").addClass("fixed");
            jQuery(".topbar").addClass("hide");


        } else {
            jQuery(".header__main").removeClass("fixed");
            jQuery(".header__mobile").removeClass("fixed");
            jQuery(".topbar").removeClass("hide");
        }

    });

    // Back To Top
    var offset = 300,
        offset_opacity = 1200,
        scroll_top_duration = 700,
        jQueryback_to_top = jQuery('.cd-top');
    jQuery(window).scroll(function () {
        (jQuery(this).scrollTop() > offset) ? jQueryback_to_top.addClass('cd-is-visible') : jQueryback_to_top.removeClass('cd-is-visible cd-fade-out');
        if (jQuery(this).scrollTop() > offset_opacity) {
            jQueryback_to_top.addClass('cd-fade-out');
        }
    });
    //smooth scroll to top
    jQueryback_to_top.on('click', function (event) {
        event.preventDefault();
        jQuery('body,html').animate({
            scrollTop: 0,
        }, scroll_top_duration
        );
    });

    // Test OWL
    jQuery("#id_slide_kh").owlCarousel({
        loop: true,
        autoplay: true,
        autoplayTimeout: 4000,
        autoplayHoverPause: true,
        margins: 400,
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 2,
                nav: false
            },
        },
        navigationText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"]
    });

    // company slide

    jQuery("#com_slide_id").owlCarousel({

        loop: true,

        margin: 30,

        autoplay: true,

        autoplayTimeout: 4000,

        autoplayHoverPause: true,

        stagePadding: 5,

        dots: false,

        responsive: {

            0: {

                items: 2,

                margin: 20,


            },

            600: {

                items: 3,
                margin: 20,

            },

            900: {

                items: 4,

            },

            1000: {

                items: 5,

            }

        },

        navText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"]

    });

    // end company slide

    jQuery("#com_prod_detail").owlCarousel({

        loop: true,

        margin: 30,

        autoplay: true,

        autoplayTimeout: 4000,

        autoplayHoverPause: true,

        stagePadding: 5,

        dots: false,

        responsive: {

            0: {

                items: 1,

                margin: 20,


            },

            600: {

                items: 1,
                margin: 20,

            },

            900: {

                items: 1,

            },

            1000: {

                items: 1,

            }

        },

        navText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"]

    });
    jQuery("#com_prod_detail2").owlCarousel({

        loop: true,

        margin: 30,

        autoplay: true,
        arrows: true,
        autoplayTimeout: 4000,

        autoplayHoverPause: true,

        stagePadding: 5,

        dots: false,

        responsive: {

            0: {

                items: 4,

                margin: 20,


            },

            600: {

                items: 4,
                margin: 20,

            },

            900: {

                items: 4,

            },

            1000: {

                items: 4,

            }

        },
            
        navText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"]

    });
    // slide_product tab 1
    jQuery(".product_slide").owlCarousel({

        loop: true,

        autoplay: true,

        autoplayTimeout: 4000,

        autoplayHoverPause: true,

        dots: false,

        responsive: {

            0: {

                items: 2,
                margin: 10,
            },

            600: {

                items: 2,
                margin: 20,

            },

            1000: {

                items: 4,



            }

        },

        navText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"]

    });
    // end slide_product tab 1

    // slide detail product

    $('.slide_product__').slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        arrows: false,
        fade: true,
        asNavFor: '.slider_nav'
    });

    $('.slider_nav').slick({
        slidesToShow: 4,
        slidesToScroll: 1,
        asNavFor: '.slide_product__',

        margin: 20,
        focusOnSelect: true,

    });

});

// var c = document.getElementById("tab_content").childNodes;
// console.log(c);

// console.log(c[1]);


// for(let i= 0; i<=c.length;i++){

//     console.log(c[i].nodeName);

//     c[i].nodeName === "#text" {
//         removeClass
//     }
// }


jQuery(function ($) {


    if (!String.prototype.getDecimals) {
        String.prototype.getDecimals = function () {
            var num = this,
                match = ('' + num).match(/(?:\.(\d+))?(?:[eE]([+-]?\d+))?$/);
            if (!match) {
                return 0;
            }
            return Math.max(0, (match[1] ? match[1].length : 0) - (match[2] ? +match[2] : 0));
        }
    }

    function wcqi_refresh_quantity_increments() {
        $('.quantity:not(.buttons_added), td.quantity:not(.buttons_added)').addClass('buttons_added').append('<input type="button" value="+" class="plus" />').prepend('<input type="button" value="-" class="minus" />');
    }


    $(document).on('updated_wc_div', function () {
        wcqi_refresh_quantity_increments();
    });

    $(document).on('click', '.plus, .minus', function () {
        // Get values
        var $qty = $(this).closest('.quantity').find('.qty'),
            currentVal = parseFloat($qty.val()),
            max = parseFloat($qty.attr('max')),
            min = parseFloat($qty.attr('min')),
            step = $qty.attr('step');

        console.log('a');

        $(':input[type="submit"]').prop('disabled', false);


        // Format values
        if (!currentVal || currentVal === '' || currentVal === 'NaN') currentVal = 0;
        if (max === '' || max === 'NaN') max = '';
        if (min === '' || min === 'NaN') min = 0;
        if (step === 'any' || step === '' || step === undefined || parseFloat(step) === 'NaN') step = 1;

        // Change the value
        if ($(this).is('.plus')) {
            if (max && (currentVal >= max)) {
                $qty.val(max);
            } else {
                $qty.val((currentVal + parseFloat(step)).toFixed(step.getDecimals()));
            }
        } else {
            if (min && (currentVal <= min)) {
                $qty.val(min);
            } else if (currentVal > 0) {
                $qty.val((currentVal - parseFloat(step)).toFixed(step.getDecimals()));
            }
        }

        // Trigger change event
        $qty.trigger('change');
    });
    wcqi_refresh_quantity_increments();


});


