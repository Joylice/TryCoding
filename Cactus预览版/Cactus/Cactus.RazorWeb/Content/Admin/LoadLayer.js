//创建简单遮罩层，显示load时的信息，配合Unobtrusive
; (function ($) {
        //设置背景层高
        function height() {
            var scrollHeight, offsetHeight;
            // handle IE 6
            if ($.browser.msie && $.browser.version < 7) {
                scrollHeight = Math.max(document.documentElement.scrollHeight, document.body.scrollHeight);
                offsetHeight = Math.max(document.documentElement.offsetHeight, document.body.offsetHeight);
                if (scrollHeight < offsetHeight) {
                    return $(window).height();
                } else {
                    return scrollHeight;
                }
                // handle "good" browsers
            }
            else if ($.browser.msie && $.browser.version == 8) {
                return $(document).height() - 4;
            }
            else {
                return $(document).height();
            }
        };
        //设置背景层宽
        function width() {
            var scrollWidth, offsetWidth;
            // handle IE
            if ($.browser.msie) {
                scrollWidth = Math.max(document.documentElement.scrollWidth, document.body.scrollWidth);
                offsetWidth = Math.max(document.documentElement.offsetWidth, document.body.offsetWidth);
                if (scrollWidth < offsetWidth) {
                    return $(window).width();
                } else {
                    return scrollWidth;
                }
                // handle "good" browsers
            }
            else {
                return $(document).width();
            }
        };
        /*==========全部遮罩=========*/
        function createLayer(load) {
            //背景遮罩层
            var layer = $("<div id=" + load.attr("data-loadlayer-id") + "></div>");
            layer.css({
                zIndex: 9998,
                position: "absolute",
                height: height() + "px",
                width: width() + "px",
                background: "black",
                top: 0,
                left: 0,
                filter: "alpha(opacity=30)",
                opacity: 0.3
            });

            //图片及文字层
            var content = $("<div id=" + load.attr("data-loadlayer-id") + "-content" + "></div>");
            content.css({
                textAlign: "left",
                position: "absolute",
                zIndex: 9999,
                //height: opt.height + "px",
                //width: opt.width + "px",
                top: "50%",
                left: "50%",
                verticalAlign: "middle",
                //background: opt.backgroudColor,"#ECECEC"
                background: "#ECECEC",
                borderRadius: "3px",
                padding:"2px 5px 2px 5px",
                fontSize: "13px"
            });

            //content.append("<img style='vertical-align:middle;margin:" + (opt.height / 4) + "px; 0 0 5px;margin-right:5px;' src='" + opt.backgroundImage + "' /><span style='text-align:center; vertical-align:middle;'>" + opt.text + "</span>");
            content.append("<span style='text-align:center; vertical-align:middle;color:#000000'>" + load.attr("data-loadlayer-msg") + "</span>");
            load.append(layer.append(content));
            var top = content.css("top").replace('px', '');
            var left = content.css("left").replace('px', '');
            content.css("top", (parseFloat(top) - parseFloat(content.css("height")) / 2)).css("left", (parseFloat(left) - parseFloat(content.css("width")) / 2));

            layer.hide();
            return this;
        }

        $(document).ready(function () {
            createLayer($("div[data-loadlayer=true]"))
        });
    //构造
        $.fn.Layer = function (options) {
            if (typeof options == "string") {
                return $.fn.Layer.methods[options](this);
            }
        };
    //方法
        $.fn.Layer.methods = {
            show: function (jq) {
                jq.css({
                    height: height() + "px",
                    width: width() + "px"
                });
                jq.show();
            }
        };
})(jQuery)