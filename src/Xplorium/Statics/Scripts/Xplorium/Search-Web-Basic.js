//$(document).ready(function () {    


//    var form = $("form#searchWeb");
//    var q = $("input#q");
//    var tipsDialog = $("div#tipsDialog");
//    var closeTimer = null;  
//      
//        tipsDialog.dialog({ autoOpen: false, modal: true, show: "drop", hide: "drop", width: 400, height: 140,
//            buttons: { "Try it now": function () { window.location = '<%= Url.RouteUrl("SearchWebAdvanced") %>'; },
//                "No thank's": function () { tipsDialog.dialog("close"); q.focus(); }
//            },
//            close: clearTimer,
//            draggable: true
//        });        

//        setTimeout(function () {
//            tipsDialog.dialog("open");
//            closeTimer = setTimeout(function () {
//                closeTimer = null;
//                tipsDialog.dialog("close");
//                q.focus();
//            }, 7500);
//        }, 7500);

//        function clearTimer() {
//            if (closeTimer) {
//                clearTimeout(closeTimer);
//                closeTimer = null;
//            }
//        }
//    });

//    form.submit(function () {
//        return QueryState();
//    });

//    q.change(function () {
//        QueryState();
//    });

//    QueryState = function () {
//        if (q.val() == '') {
//            q.addClass("highlight").focus();
//            return false;
//        }
//        else {
//            q.removeClass("highlight").focus();
//            return true;
//        }
//    };
//});