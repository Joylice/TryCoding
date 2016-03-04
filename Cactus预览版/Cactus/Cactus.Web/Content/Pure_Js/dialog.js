//依赖pure，pure-extend.css，jquery
(function ($) {
    var state = {};
    $.fn.pure_dialog = function (options) {
        if (typeof options == "string") {
            return $.fn.pure_dialog.methods[options](this);
        }
        options = options || {};
        $(this).each(function () {
            state = $(this).data("dialog");
            if (state) {
                $.extend(state.options, options);
            }
            else {
                state = $(this).data("dialog", {
                    options: $.extend({}, $.fn.pure_dialog.defaults, options)
                });
                state = $(this).data("dialog");
            }
            bingDialog($(this), state.options);
        })
    };
    function bingDialog(obj, o_state) {
        //标题
        obj.find('.pure-dialog-title').html(o_state.title);
        //提示
        if (o_state.url.length > 0) {
            var iframe_html = "<iframe src=\"" + o_state.url + "\" frameborder=\"0\" scrolling=\"no\" style=\"margin:0;padding:0;border:0px;overflow:hidden;width:" + o_state.width + ";height:" + o_state.height + ";\"></iframe>";
            obj.find('.pure-dialog-hint').html(iframe_html);
        } else {
            obj.find('.pure-dialog-hint').html(o_state.content);
        }
        //位置
        // upper-left 左上||upper-right 右上||center 中||lower-left 左下||lower-right 右下
        if (o_state.location.length == 0) {
            o_state.location = 'center';
        }
        DialogLocation(obj, o_state.location, o_state.animate);
        $(window).resize(function () {
            setTimeout(function () {
                DialogLocation(obj, o_state.location, false);//保持位置不变
            }, 250);
        });
        //拖动
        if (o_state.drag) {
            bind_mouse(obj);
        } else {
            unbind_mouse(obj);
        }
        //关闭按钮
        obj.find('.pure-dialog-close').hide();
        obj.find('.pure-dialog-close').click(function () {
            obj.hide(300)
        })
        if (o_state.close) {
            obj.find('.pure-dialog-close').show();
        } else {
            obj.find('.pure-dialog-close').hide();
        }
        if (o_state.show) {
            if (o_state.mask) {
                $('.pure-mask').show();
            } else {
                $('.pure-mask').hide();
            }
            obj.show();
        } else {
            obj.hide();
        }
        //关闭按钮绑定关闭
        $(this).find('pure-dialog-close').click(function () {
            obj.pure_dialog('hideDialog');
            o_state.onClose();
        });
    }
    function DialogLocation(obj, location, isAnimate) {
        switch (location) {
            case "upper-left":
                //左上
                dialog_upper_left(obj, isAnimate);
                break;
            case "upper-right":
                //右上
                dialog_upper_right(obj, isAnimate);
                break;
            case "center":
                //居中
                dialog_center(obj, isAnimate);
                break;
            case "lower-left":
                //左下
                dialog_lower_left(obj, isAnimate);
                break;
            case "lower-right":
                //右下
                dialog_lower_right(obj, isAnimate);
                break;
        }
    }
    //左上
    function dialog_upper_left(obj, isAnimate) {
        if (isAnimate) {
            obj.css({
                top: 0,
                left: 0 - obj.outerWidth()
            });
            obj.animate({
                top: 0,
                left: 0
            });
        } else {
            obj.css({
                top: 0,
                left: 0
            });
        }
    }
    //右上
    function dialog_upper_right(obj, isAnimate) {
        if (isAnimate) {
            obj.css({
                top: 0,
                left: $(window).width()
            });
            obj.animate({
                top: 0,
                left: $(window).width() - obj.outerWidth()
            });
        } else {
            obj.css({
                top: 0,
                left: $(window).width() - obj.outerWidth()
            });
        }
    }
    //居中
    function dialog_center(obj, isAnimate) {
        var width = obj.outerWidth();
        var height = obj.outerHeight();
        if (isAnimate) {
            obj.css({
                top: $(window).height() / 2 - height / 2 + $(window).scrollTop(),
                left: $(window).width() / 2 - width / 2 + $(window).scrollLeft(),
                height: 0,
                width: 0
            });
            obj.animate({
                height: height,
                width: width
            });
        } else {
            obj.css({
                top: $(window).height() / 2 - height / 2 + $(window).scrollTop(),
                left: $(window).width() / 2 - width / 2 + $(window).scrollLeft(),
                height: height,
                width: width
            });
        }
    }
    //左下
    function dialog_lower_left(obj, isAnimate) {
        if (isAnimate) {
            obj.css({
                top: $(window).height() - obj.outerHeight(),
                left: 0 - obj.outerWidth()
            });
            obj.animate({
                top: $(window).height() - obj.outerHeight(),
                left: 0
            });
        } else {
            obj.css({
                top: $(window).height() - obj.outerHeight(),
                left: 0
            });
        }
    }
    //右下
    function dialog_lower_right(obj, isAnimate) {
        if (isAnimate) {
            obj.css({
                top: $(window).height() - obj.outerHeight(),
                left: $(window).width()
            });
            obj.animate({
                top: $(window).height() - obj.outerHeight(),
                left: $(window).width() - obj.outerWidth()
            });
        } else {
            obj.css({
                top: $(window).height() - obj.outerHeight(),
                left: $(window).width() - obj.outerWidth()
            });
        }
    }
    //绑定拖动
    function bind_mouse(obj) {
        obj.find('.pure-dialog-head').mousedown(function (e) {
            var dialog = obj;
            var winx = dialog.offset().left;
            var winy = dialog.offset().top;
            var _x = e.pageX - winx;
            var _y = e.pageY - winy;
            $(this).bind('mousemove', function (event) {
                var x = event.pageX - _x;//控件左上角到屏幕左上角的相对位置
                var y = event.pageY - _y;
                dialog.css({ top: y, left: x });
            });
        });
        obj.find('.pure-dialog-head').mouseup(function () {
            $(this).unbind('mousemove');
        })
    }
    //解除绑定拖动
    function unbind_mouse(obj) {
        obj.find('.pure-dialog-head').unbind('mousedown');
        obj.find('.pure-dialog-head').unbind('mousemove');
        obj.find('.pure-dialog-head').unbind('mouseup');
    }
    //方法
    $.fn.pure_dialog.methods = {
        showDialog: function (obj) {
            $(obj).show(300);
            $('.pure-mask').show();
        },
        hideDialog: function (obj) {
            $(obj).hide(300);
            $('.pure-mask').hide();
        }
    };
    //默认项
    $.fn.pure_dialog.defaults = $.extend({}, {
        title: '',//
        content: '',//内容
        url: '',//框架iframe
        location: 'center',//upper-left 左上||upper-right 右上||center 中||lower-left 左下||lower-right 右下
        drag: false,//是否可拖动
        close: true,//是否有关闭
        mask: true,//遮罩
        show: true,
        animate: true,//动画
        width: '450px',
        height: 'auto',
        onClose: function () { }
    });
})(jQuery);