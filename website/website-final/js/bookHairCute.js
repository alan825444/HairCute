$(function() {

    $("[name='btn-reserve']").click(getReserve);


function getReserve() {  
    $(".selected").each(function() {  
        /*根據所在的行取得當前預約的時間段和預約的日期，並進行相應的初始化，轉化為預約開始時間和預約結束時間 */  
        var time = $(this).parent().find("th").text();  
        var hours = time.split("-")[0].split(":")[0];  
        //var datetime = new Date($("#").attr("date"));
        var datetime = new Date("2021/11/01");
        datetime.setHours(parseInt(hours));  
        var startTime = datetime.format("yyyy-MM-dd hh:mm:ss");  
        datetime.setHours(parseInt(hours) + 1);  
        var endTime = datetime.format("yyyy-MM-dd hh:mm:ss");  
        
        var designerId = 1;
        //預約提交的參數
        var params = {  
            "designerId": designerId,  
            "startTime": startTime,  
            "endTime": endTime  
        };  
        console.log(params);
        //ajax回乎函式能訪問到當前表格單元，將其存入變數$td中  
        //var $td = $(this);  
        if(params!= null){$(".reserveResult").append("<p>" + "會員：" + "阿賢預約成功" + "，時間：" + params.startTime +" 到 " + params.endTime + "</p>");}
        /*$.post("#", params, function(data, status) {  
            //如果預約成功，將原來的“useable”變為“occupied”，並且解除點擊事件，不可預約  
            if(data.message.result == "success") {  
                $td.removeClass().addClass("occupied").unbind("click");  
            } 
            var mesg = data.message.result == "success" ? "預約成功" : "預約失败";  
            //在表格下方展示此次預約的结果，包括詳細資訊  
            $(".reserveResult").append("<p>" + mesg + "會員：" + "阿賢" + "，時間：" + params.startTime +" 到 " + params.endTime + "</p>");  
        }, "json");*/
    });  
} 

function submitQuery() {  
      
    /*獲取查詢的日期和設計師ID*/  
    //var date = $("#").attr("date");
    var date = new Date("2021/11/01");
    var designerId = 1;
      
    /*ajax查詢已經預約的列表*/  
    //$.post("#", {"designerId": designerId, "queryDay": date}, function(data, status) {  
          
        //var book = data.data.book;
        var book = {
            openTime:"09:00",
            closeTime:"20:00",
            bookList:[{startTime:"10:00",endTime:"11:00"},{startTime:"13:00",endTime:"14:00"}]
        }
          
        /** 
        *   預約的資料
        *   @Param openTime 開放時間 
        *   @Param closeTime 結束時間 
        *   @Param bookList 指定日期的預約列表 
        */  
        var openTime = parseInt(book.openTime.split(":")[0]);  
        var closeTime = parseInt(book.closeTime.split(":")[0]);  
          
        var bookList = book.bookList;  
          
        //清空預約table  
        $("#resourceTable").empty();  
          
        //設置欄位 
        var tbRow = "<tr><th></th><th>預約</th></tr>"; 
        $("#resourceTable").append(tbRow);  
          
        //從開放時間到關閉時間，每一小時為一個預約時間
        for (var time = openTime; time < closeTime; time ++) {  
            var show_time = time + ":00-" + (time + 1) + ":00";  
            tbRow = "<tr><th>" + show_time + "</th>";  
            tbRow += "<td></td></tr>";   
            $("#resourceTable").append(tbRow);  
        }  
  
        var first_row = openTime;  
        var today = new Date();  
        //判斷預約的時間是否為今天，如果是今天就將過期的時間設置為當前時間(小時)
        var end_row = today.getDate() < (new Date(date)).getDate()? first_row - 1: parseInt((new Date()).getHours());  
        //對table的每一行，如果在過期時間之前的時間段，就將這一行所有td的class設置為“outtime”，表示不可預約，否則class設置為“useable”表示可預約  
        $("#resourceTable tr").each(function(i) {  
                $(this).find("td").addClass(i + first_row <= end_row? "outtime": "useable");  
        });  
          
        //開始遍歷，根據預約紀錄的開始時間生成對應的行坐標，找到表格中對應位置的td删除本身的class，增加“occupied”表示對應時段已經被預約  
        $(bookList).each(function(i) {  
            var row = parseInt(bookList[i].startTime.split(":")[0]) - openTime + 1;  
            var col = 0;  
            console.log("row:"+row+",col:"+col);  
            $("#resourceTable tr").eq(row).find("td").eq(col).removeClass().addClass("occupied");  
        });  
          
        //所有可以預約的td绑定一個click事件，即點擊表示選中，再點一次恢復原来狀態，直到點擊預約按钮預約成功後，解除click事件，並由“useable”變為“occupied”  
        $("tr .useable").bind("click", function() {  
            $(this).toggleClass("selected");
        });  
  
    //}, "json");  
}  


Date.prototype.format = function (fmt) { 
	var o = {
		"M+": this.getMonth()+1, 
		"d+": this.getDate(), 
		"h+": this.getHours(), 
		"m+": this.getMinutes(), 
		"s+": this.getSeconds(), 
		"q+": Math.floor((this.getMonth() + 3) / 3),  
		"S": this.getMilliseconds() 
	};
	if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
	for (var k in o)
		if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
	return fmt;
}

submitQuery();
})
