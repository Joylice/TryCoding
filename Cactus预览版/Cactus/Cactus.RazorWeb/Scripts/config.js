
function loadContent(url,b) {
    //var myLoading = $.sobox.loading();
    if (b) { $("#load").Layer("show"); }
    //$("#load").show();
    $(".down-content").load(url, function () {
        //myLoading.close();
        //$("#load").hide(1000);
        if (b) { $("#load").hide(1000); }
        $('.down-content').unobtrusive('resetbind');
    });
}
//设置删除参数
function SetDel(obj) {
    //删除参数设置
    var url = $(obj).attr("data-ajax-url");
    var ids = "";
    $('input[name=aid][type=checkbox]:checked').each(function (index, element) {

        if (index == 0) {
            ids += $(this).val();
        } else {
            ids += "," + $(this).val();
        }
    })
    $(obj).attr("data-ajax-url", url + "?ids=" + ids);
    return true;
}



//总页数 当前页 可见页 参数 翻页执行后处理的函数
function PageTable(totalPages, currentPage, tableobj, url, where, columns) {
    function PageTableAjax() {
        initTable(tableobj);
        $.ajax({
            type: "POST",
            url: url,
            data: { where: where, page: currentPage },
            dataType: "json",
            success: function (rsp) {
                if (rsp.pass) {
                    totalPages = rsp.pagination.PageCount;
                    currentPage = rsp.pagination.PageIndex;
                    bindTable(tableobj, rsp.rows, columns)
                    console.log("PageTablePaginator")
                    PageTablePaginator();
                } else {
                    console.log(rsp.msg)
                }
            },
            error: function (e, s, t) {
                console.log("ajax error")
            }
        });
    }
    function PageTablePaginator() {
        $.jqPaginator('ul.pagination', {
            wrapper: '',
            first: '<li class="first"><a href="javascript:;">首页</a></li>',
            prev: '<li class="prev"><a href="javascript:;">&laquo;</a></li>',
            next: '<li class="next"><a href="javascript:;">&raquo;</a></li>',
            last: '<li class="last"><a href="javascript:;">尾页</a></li>',
            page: '<li class="page"><a href="javascript:;">{{page}}</a></li>',
            totalPages: totalPages,/*总数 */
            visiblePages: 5,/*可见分页数*/
            currentPage: currentPage,
            onPageChange: function (num, type) {
                if (type == "change") {
                    console.log('PageTableAjax')
                    PageTable(totalPages, num, tableobj, url, where, columns)
                }
            }
        });
    }
    PageTableAjax();
}
//table初始化状态
function initTable(obj) {
    console.log('initTable')
    var head = $(obj).find('thead tr th');
    var tbody = $(obj).find('tbody');
    tbody.html("");
    //无记录
    var tr01 = $("<tr align=\"center\"></tr>");
    $("<td colspan=\"" + head.length + "\">loading...</td>").appendTo(tr01);
    tbody.append(tr01);
    console.log('pagination:' + $('ul.pagination').length)
    if ($('ul.pagination').length == 0) {
        $(obj).after("<ul class=\"pagination\"></ul>");
    }
}
function bindTable(obj, rows, columns) {
    console.log('bindTable')
    var tbody = $(obj).find('tbody');
    tbody.html("");
    var head = $(obj).find('thead tr th');
    console.log("columns:" + columns.length);
    if (rows[0] != 'undefined' && rows[0] != null) {

        for (var i = 0; i < rows.length; i++) {
            var r = rows[i];
            var tr = $("<tr></tr>");
            for (var j = 0; j < columns.length; j++) {
                var fieldstr = columns[j].field;
                var valstr = r[fieldstr];
                if (fieldstr.indexOf('.') != -1) {
                    //console.log("indexOf:" + fieldstr.substring(fieldstr.indexOf('.'), fieldstr.length))
                    var arr = fieldstr.split(".");
                    valstr = r[arr[0]][arr[1]];
                    switch(arr.length)
                    {
                        case 2:
                            valstr = r[arr[0]][arr[1]];
                            break;
                        case 3:
                            valstr = r[arr[0]][arr[1]][arr[2]];
                            break;
                        case 4:
                            valstr = r[arr[0]][arr[1]][arr[2]][arr[3]];
                            break;
                        default:
                            valstr = r[arr[0]][arr[1]];
                    }
                }


                if (columns[j].formatter != 'undefined' && typeof columns[j].formatter === 'function') {
                    console.log('columns.formatter')
                    valstr = columns[j].formatter(valstr, i);
                }
                $("<td>" + valstr + "</td>").appendTo(tr);
            }
            tbody.append(tr);
        }
    } else {
        
        //无记录
        var tr = $("<tr align=\"center\"></tr>");
        $("<td colspan=\"" + head.length + "\">(暂无相关记录)</td>").appendTo(tr);
        tbody.append(tr);
    }
}

