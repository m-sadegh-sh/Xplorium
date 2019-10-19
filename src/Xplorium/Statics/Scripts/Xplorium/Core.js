var Xplorium = function () {
    return {
        Initialize: function () {
            $(document).hover(function () {
                $("[auto-hide|=true]").each(function () {
                    $(this).animate({ opacity: 1 }, 1000);
                });
            });

            //$("#first").Watermark("First");
           
            $("[watermark|=true]").each(function () {
                $(this).Watermark(this.title);
            });

        },
        NoneOutLine: function () {
            $("[none-outline|=true]").mousedown(function () {
                this.blur();
                this.hideFocus = true;
                this.style.outline = 'none';
            });
        },
        SetFocus: function () {
            $("[get-focus|=true]").first().focus();
        },
        GetAnimate: function () {
            $("[highlight|=true]").hide().delay(3500).effect("highlight", {}, 1000);
        }
    };
} (Xplorium);
//    $('.animate-to-top').click(function () {
//        $("body").fadeTo(1000, 0, function () {
//            $('html,body').animate({ scrollTop: top }, 0);
//        }).fadeTo(1000, 1);
//    });
//};
