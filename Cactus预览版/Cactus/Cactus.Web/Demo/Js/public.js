//js加载图片
var imgLoad = function (url, callback, errorfunc) {
    var img = new Image();
    img.src = url;
    if (img.complete) {
        callback(img);
    } else {
        img.onload = function () {
            callback(img);
            img.onload = null;
        };
        img.onerror = function () {
            errorfunc();
        }
    };
};
//js加载器
var loadJS = function (js_url, callback) {
    var script = document.createElement('script');
    script.type = "text/javascript";
    script.src = js_url;
    script.onload = function () {
        //干你的活
    };
    var head = document.getElementsByTagName('head')[0];
    head.appendChild(script);
};
$(function () {
    $('.pure-menu-setup').bind('click', function () {
        if ($('.pure-menu-dropdown-list').is(":hidden")) {
            $('.pure-menu-dropdown-list').show();
        } else {
            $('.pure-menu-dropdown-list').hide();
        }
    });
    //$('.pure-menu-setup').bind('touchstart', function () {
    //    if ($('.pure-menu-dropdown-list').is(":hidden")) {
    //        $('.pure-menu-dropdown-list').show();
    //    } else {
    //        $('.pure-menu-dropdown-list').hide();
    //    }
    //});
})




