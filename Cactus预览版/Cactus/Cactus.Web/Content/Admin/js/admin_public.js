var loadObj = $('#pure-dialog-load');
//可以改为其他弹框
var loadDialog = loadObj.pure_dialog({
    title: '提示',//
    content: '数据加载中...',//内容
    location: 'center',//upper-left 左上||upper-right 右上||center 中||lower-left 左下||lower-right 右下
    drag: false,//是否可拖动
    close: false,//是否有关闭
    width: '100%',
    height: 'auto',
    show: false,
});
var loadDialog_show = function () { loadObj.pure_dialog('showDialog'); };
var loadDialog_hide = function () { loadObj.pure_dialog('hideDialog'); };
var loadContent_Jq = function (id, url) {
    loadDialog_show();
    $("#" + id).load(url, function () {
        //alert("加载成功");
        loadDialog_hide();
    });
}



var tipTable = function (jq_obj, msg) {
    console.log('tipTable');
    var head = jq_obj.find('thead tr th');
    var tbody = jq_obj.find('tbody');
    tbody.html("");
    //无记录
    var tr01 = $("<tr align=\"center\"></tr>");
    $("<td colspan=\"" + head.length + "\">" + msg + "</td>").appendTo(tr01);
    tbody.append(tr01);
}
//自动表格初始
//jq_obj:jquery表格对象
//rows:数据集合
//columns:字段集合，根据字段处理数据，格式[{field: "Id", formatter:function(val, rec){}}]，val：数据，rec：序号
var renderTable = function (jq_obj, rows, columns) {
    console.log('bindTable')
    var tbody = jq_obj.find('tbody');
    tbody.html("");
    var head = jq_obj.find('thead tr th');
    console.log("columns:" + columns.length);
    if (rows[0] != undefined && rows[0] != null) {

        for (var i = 0; i < rows.length; i++) {
            var r = rows[i];
            var tr = $("<tr></tr>");
            for (var j = 0; j < columns.length; j++) {
                var fieldstr = columns[j].field;
                var valstr = r[fieldstr];
                if (valstr == undefined || valstr == null) {
                    if (fieldstr.indexOf('.') != -1) {
                        var arr = fieldstr.split(".");
                        //valstr = r[arr[0]][arr[1]];
                        switch (arr.length) {
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
                }

                if (columns[j].formatter != undefined && typeof columns[j].formatter === 'function') {
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