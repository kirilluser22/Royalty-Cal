$(document).ready(function () {

    $(".droppable__float a").on("click", function (e) {
        e.preventDefault();
        $('.float__droppable--content.' + $(this).attr("data-drop")).css("top", "0px");

    });

    $(".float__droppable--content .close__modal").on("click", function (e) {
        e.preventDefault();
        $(this).closest(".float__droppable--content").css("top", '-100%');
        $(".float__menu").css("top", "-100%");
        $(".menu__button>a").removeClass("active__button");
        $("body,html").css("overflow-y", "auto");
    });
    $('.float__droppable--content .back__head').on("click", function (e) {
        e.preventDefault();
        $(this).closest(".float__droppable--content").css("top", "-100%");
    });

    if ($(window).width() < 575) {
        $('.navigation2 .navigation__tip').scrollLeft(57);
        $('.navigation3 .navigation__tip').scrollLeft(127);
        $(".tip__second .navigation__tip , .determine__block .navigation__tip , .contact__block .navigation__tip").scrollLeft(250);
    }

    $('.elem__sub--menu a').on('click', function (e) {
        e.preventDefault();
        $(".menu__button>a").removeClass("active__button");
        $(".float__menu").css("top", "-100%");
        $("body,html").css("overflow-y", "auto");
        $('html').animate({
            scrollTop: $("." + $(this).attr("data-navigation")).offset().top
        }, 400
        );
    });


    $(".navigation__tip ul li a").on("click", function (e) {
        e.preventDefault();
        $('html').animate({
            scrollTop: $("." + $(this).attr("data-navigation")).offset().top
        }, 400
        );
    });
    $(document).on("click", '.download__box .success__download .success__download--info>a', function (e) {
        e.preventDefault();
        $("body,html").css("overflow-y", "hidden");
        $(".modal__form").fadeIn(300);
    });

    $(".modal__form .head__modal>a").on("click", function (e) {
        e.preventDefault();
        $(this).closest(".modal__form").fadeOut(300);
        $("body,html").css("overflow-y", "auto");
    });

    //$(".modal__form .group__submit>.userInfo-btn").on("click", function (e) {
    //	e.preventDefault();
    //	$(this).closest(".modal__form").fadeOut(300);
    //	$("body,html").css("overflow-y", "auto");
    //});

    $('.float__menu .dropdown__header>a').on("click", function (e) {
        e.preventDefault();
        $(this).closest(".dropdown__header").find('.dropdown__box').slideToggle(400);
    });

    $('.menu__button>a').on("click", function (e) {
        e.preventDefault();
        $(this).toggleClass("active__button");
        if ($(this).hasClass('active__button')) {
            $('.float__menu').css("top", "0px");
            $("body,html").css("overflow-y", "hidden");
        } else {
            $(".float__menu").css("top", "-100%");
            $("body,html").css("overflow-y", "auto");
        }
    });

    $(document).on("click", ".head__success", function (e) {
        e.preventDefault();
        $(this).toggleClass("active__success");
        $(this).closest(".elem__success--small").find(".content__success").slideToggle(400);

    });


    //new js
    //$(".right__review--controls>a").on("click", function (e) {
    //    e.preventDefault();
    //    window.location.href = "congrat.html";
    //});

    $(".accept__content>a").on("click", function (e) {
        e.preventDefault();
        $(this).closest('.modal__block').fadeOut(400);
        $("body,html").css("overflow-y", "auto");
    });

    $(".terms__block a").on("click", function (e) {
        e.preventDefault();
        $(".modal__block").fadeIn(400);
        $("body,html").css("overflow-y", "hidden");
    });


    $(".left__review--controls .terms__block input").on("change", function () {
        if ($(this).prop("checked") == true) {
            $(".right__review--controls>.disabled__controls").removeClass("disabled__controls");
            $(".right__review--controls>a").addClass("verifySubmit");
        } else {
            $(".right__review--controls>a").addClass("disabled__controls");
            $(".right__review--controls>a").removeClass("verifySubmit");
        }
    });

    //$(".switch1 .group__submit input[type='button']").on("click", function (e) {
    //    e.preventDefault();
    //    $(".switch1").css("display", "none");
    //    $(".switch2").fadeIn(300);
    //    $('.switch__list ul li').removeClass("current__switch");
    //    $(".switch__list ul li:nth-child(2)").addClass("current__switch");
    //});
    //$(".switch2 .group__submit input[type='button']").on("click", function (e) {
    //    e.preventDefault();
    //    $(".switch2").css("display", "none");
    //    $(".switch3").fadeIn(300);
    //    $('.switch__list ul li').removeClass("current__switch");
    //    $(".switch__list ul li:nth-child(3)").addClass("current__switch");
    //});
    //$(".switch3 .submit__right input[type='submit']").on("click", function (e) {
    //    e.preventDefault();
    //    window.location.href = "informationreview.html";
    //});


    $(".content__modal").scroll(function () {
        var top_of_element = $(".content__modal>*:last-child").offset().top;
        var bottom_of_element = $(".content__modal>*:last-child").offset().top + $(".content__modal>*:last-child").outerHeight();
        var bottom_of_screen = $(window).scrollTop() + $(window).innerHeight();
        var top_of_screen = $(window).scrollTop();

        if ((bottom_of_screen > top_of_element) && (top_of_screen < bottom_of_element)) {
            // the element is visible, do something
            $(".accept__content>.disabled__accept").removeClass('disabled__accept');
        } else {
            // the element is not visible, do something else
        }
    });
    $(".inner__modal>.close__modal").on("click", function (e) {
        e.preventDefault();
        $(this).closest(".modal__block").fadeOut(400);
        $("body,html").css("overflow-y", "auto");
    });
    $(".location__controls>a").on("click", function (e) {
        e.preventDefault();
        if ($(this).closest(".location__content").hasClass("opened__location")) {
            $(this).closest(".location__content").removeClass("opened__location");
            $(this).closest(".location__content").find('.location__details').slideUp(400);
        } else {
            $(this).closest(".location__content").addClass("opened__location");
            $(this).closest(".location__content").find('.location__details').slideDown(400);
        }
    });


    $(".partner__hint>span , .partner__block").on("mouseenter", function (e) {
        e.preventDefault();
        $(this).closest(".partner__hint").find('.partner__block').fadeIn(100);
    });

    $(".partner__title").on("mouseleave", function (e) {
        e.preventDefault();
        $(this).find(".partner__block").fadeOut(100);
    });


    $(document).on("change", ".product__dropdown ul li input", function (e) {
        e.preventDefault();
        var currentField = $(this).closest('.group__dropdown').find(".dropdown__product>a");
        if ($(this).hasClass("other__input")) {
            if ($(this).is(":checked")) {
                $(this).closest(".product__dropdown").find(".product__other").slideDown(400);
            } else {
                $(this).closest(".product__dropdown").find(".product__other").slideUp(400);
            }
        } else {
            var newText = "";
            $(this).closest(".product__dropdown").find("ul>li").each(function (index, elem) {
                if ($(elem).find("input")) {
                    if (!$(elem).find("input").hasClass("other__input")) {
                        if ($(elem).find("input").is(":checked")) {
                            if (newText == "" || newText == " ") {
                                newText += $(elem).find(".form-group").attr("data-value");
                                $(currentField).text(newText);
                            } else {
                                newText += "," + $(elem).find(".form-group").attr("data-value");
                                $(currentField).text(newText);
                            }

                        }
                    }
                }
            });

        }
        if ($(this).closest(".product__dropdown").find(".dropdown__main--input:checked").length == 0 && $(this).closest(".product__dropdown").find('.other__input:checked').length == 0) {
            $(this).closest(".group__dropdown").find('.dropdown__product>a').text("Select all that apply");
        }
    });

    $(document).on("click", ".default__dropdown ul li a", function (e) {
        e.preventDefault();
        $(".default__dropdown , .product__dropdown , .default__dropdown , .dropdown__code , .timezone__dropdown").css("display", 'none');
        $(this).closest('.group__dropdown').find(".dropdown__default").html($(this).parent().html());
        $(this).closest(".default__dropdown").fadeOut(300);
    });




    $(document).on("click", ".group__dropdown>.dropdown__product>a", function (e) {
        e.preventDefault();
        $(".default__dropdown , .product__dropdown , .default__dropdown , .dropdown__code , .timezone__dropdown").css("display", 'none');
        $(this).closest(".group__dropdown").find(".product__dropdown").fadeIn(300);
    });


    $(document).on("click", ".group__dropdown>.dropdown__default>a", function (e) {
        e.preventDefault();
        $(".default__dropdown , .product__dropdown , .default__dropdown , .dropdown__code , .timezone__dropdown").css("display", 'none');
        $(this).closest(".group__dropdown").find(".default__dropdown").fadeIn(300);
    });


    $(document).on("click", ".phone__dropdown>a", function (e) {
        e.preventDefault();
        $(".default__dropdown , .product__dropdown , .default__dropdown , .dropdown__code , .timezone__dropdown").css("display", 'none');
        $(this).closest(".phone__dropdown").find(".dropdown__code").fadeIn(300);
    });


    $(document).click(function (event) {
        var $target = $(event.target);
        if (!$target.closest('.product__dropdown').length && !$target.closest('.dropdown__product').length) {
            $(".group__dropdown .product__dropdown").fadeOut(300);
        }
        if (!$target.closest('.phone__dropdown').length) {
            $(".phone__dropdown .dropdown__code").fadeOut(300);
        }
        if (!$target.closest('.group__dropdown').length) {
            $(".group__dropdown .timezone__dropdown").fadeOut(300);
        }
        if (!$target.closest('.group__dropdown').length) {
            $(".group__dropdown .default__dropdown").fadeOut(300);
        }
    });

    $(document).on("click", ".phone__dropdown .dropdown__code ul li a", function (e) {
        e.preventDefault();
        $(this).closest(".phone__dropdown").find(">a>span").text($(this).text());
        $(this).closest(".dropdown__code").fadeOut(300);
    });
    $(document).on("click", ".group__dropdown .dropdown__timezone>a", function (e) {
        e.preventDefault();
        $(".default__dropdown , .product__dropdown , .default__dropdown , .dropdown__code , .timezone__dropdown").css("display", 'none');

        $(this).closest(".group__dropdown").find(".timezone__dropdown").fadeIn(300);
    });
    $(document).on("click", ".timezone__dropdown  li a", function (e) {
        e.preventDefault();
        if (!$(this).hasClass("currencyList")) {
            $(this).closest('.group__dropdown').find(".dropdown__timezone>a").find(".timezone__flag>img").attr("src", $(this).find(".timezone__flag>img").attr("src"));
            $(this).closest('.group__dropdown').find(".dropdown__timezone>a").find(".timezone__text").text($(this).find(".tz__text").text());
            $(this).closest('.group__dropdown').find(".dropdown__timezone>a").find(".timezone__text")
                .attr("data-timezoneId", $(this).find(".tz__text").attr("data-timezone"));
        }
        else {
            $(this).closest('.group__dropdown').find(".dropdown__timezone>a").find(".timezone__flag>img").attr("src", $(this).find(".timezone__flag>img").attr("src"));
            $(this).closest('.group__dropdown').find(".dropdown__timezone>a").find(".timezone__text").html($(this).find(".tz__text").html());
        }
        $(this).closest(".timezone__dropdown").fadeOut(300);
    });
    //$(document).on("click", ".currency__dropdown  li a", function (e) {
    //    e.preventDefault();
    //    $(this).closest('.group__dropdown').find(".dropdown__currency>a").find(".timezone__flag>img").attr("src", $(this).find(".timezone__flag>img").attr("src"));
    //    $(this).closest('.group__dropdown').find(".dropdown__currency>a").find(".timezone__text").text($(this).find(".tz__text").text());
    //    $(this).closest('.group__dropdown').find(".dropdown__currency").html($(this).parent().html());
    //    $(this).closest(".currency__dropdown").fadeOut(300);
    //});
    $(".info__video>a").on("click", function (e) {
        e.preventDefault();
        $(this).closest(".info__video").find("iframe").fadeIn(300);
        $(this).closest(".info__video").find("iframe").attr("src", $(this).closest(".info__video").find("iframe").attr("data-src"));
    });

    $(".float__button").on("mouseenter", function (e) {
        e.preventDefault();
        if (!$(".float__box[data-box=" + $(this).attr("data-float") + "]:visible").length) {
            $(".float__box").css("display", "none");
            $(".float__box[data-box=" + $(this).attr("data-float") + "]").fadeIn(150);
            if ($(window).width() > 1200) {
                $(".float__box[data-box=" + $(this).attr("data-float") + "]").css({ "left": $(this).offset().left, "top": $(this).offset().top });
            } else {
                $(".float__box[data-box=" + $(this).attr("data-float") + "]").css({ "left": $(this).offset().left, "top": $(this).offset().top - $(".float__box[data-box=" + $(this).attr("data-float") + "]").css("height").slice(0, -2) + "px" });
            }
        }
    });
    $(".float__button").on("mouseleave", function (e) {
        $(".float__box").fadeOut(150);
    });
    $(".droppable__float a").on("click", function (e) {
        e.preventDefault();
        $('.float__droppable--content.' + $(this).attr("data-drop")).css("top", "0px");
    });
});