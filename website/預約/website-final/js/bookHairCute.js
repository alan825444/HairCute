$(function() {
    
    /*預設日期為當天*/
    $(document).ready( function() {
        var now = new Date();
        var month = (now.getMonth() + 1);              
        var day = now.getDate();
        if (month < 10)
            month ="0" + month;
        if (day < 10)
            day ="0" + day;
        var today = now.getFullYear() + '-' + month + '-' + day;
        $('#bookDate').val(today);
    });
    $("[name='btn-reserve']").click(getReserve);
    $("[name='btn-myBook']").click(submitQuery);
      
        /*var date = $("#date").val();            //這是測試用=沒用的註解 怕忘記當初怎弄得
        var date1 = new Date("2021-11-05");     //這是測試用=沒用的註解 怕忘記當初怎弄得
        var date2 = date1.format("yyyy-MM-dd"); //這是測試用=沒用的註解 怕忘記當初怎弄得
        console.log(date1);*/                      //這是測試用=沒用的註解 怕忘記當初怎弄得
    


function getReserve() {  
    $(".bookselected").each(function() {  
        /*根據所在的行取得當前預約的時間段和預約的日期，並進行相應的初始化，轉化為預約開始時間和預約結束時間 */  
        var time = $(this).parent().find("th").text();  
        var hours = time.split("-")[0].split(":")[0];  
        var datetime = new Date($("#bookDate").val());
        datetime.setHours(parseInt(hours));  
        var startTime = datetime.format("yyyy-MM-dd hh:mm:ss");  
        datetime.setHours(parseInt(hours) + 1);  
        var endTime = datetime.format("yyyy-MM-dd hh:mm:ss");
        
        var designerId = 1;
        /**
         * 預約提交的參數
         * @Params
        */
        var params = {  
            "designerId": designerId,  
            "startTime": startTime,  
            "endTime": endTime  
        };  
        console.log(params);
        /*ajax回乎函式能訪問到當前表格單元，將其存入變數$td中*/  
        //var $td = $(this);  
        if(params!= null){$(".bookresult").append("<p>" + "會員：" + "阿賢預約成功" + "，時間：" + params.startTime +" 到 " + params.endTime + "</p>");}
        //$.post("#", params, function(data, status) {  
            /*如果預約成功，將原來的bookuseable變為bookoccupied，並且解除點擊事件，不可預約*/  
            //if(data.message.result == "success") {  
            //    $td.removeClass().addClass("bookoccupied").unbind("click");  
            //} 
            //var mesg = data.message.result == "success" ? "預約成功" : "預約失败";  
            /*在表格下方展示此次預約的结果，包括詳細資訊*/  
            //$(".bookresult").append("<p>" + "會員：" + "阿賢" + mesg + "，時間：" + params.startTime +" 到 " + params.endTime + "</p>");  
        //}, "json");
    });  
} 

function submitQuery() {  
      
    /*獲取查詢的日期和設計師ID*/  
    var date = $("#bookDate").val();
    console.log(date);

    var designerId = 1;
      
    /*ajax查詢已經預約的列表*/  
    //$.post("#", {"designerId": designerId, "queryDay": date}, function(data, status) {  
          
        //var book = data.data.book;
        var book = {
            openTime:"08:00:00",
            closeTime:"22:00:00",
            bookList:[{startTime:"10:00:00",endTime:"11:00:00"},
            {startTime:"13:00:00",endTime:"14:00:00"}]
        }
          
        /** 
        *   預約的資料
        *   @Params openTime 開放時間 
        *   @Params closeTime 結束時間 
        *   @Params bookList 指定日期的預約列表 
        */  
        var openTime = parseInt(book.openTime.split(":")[0]);  
        var closeTime = parseInt(book.closeTime.split(":")[0]);  
        if(closeTime==0){
            closeTime = 24;
        }
        var bookList = book.bookList;

        /*清空預約table*/  
        $("#bookTable").empty();  
          
        /*設置欄位*/
        var tbRow = "<tr><th>時間</th><th>預約</th></tr>"; 
        $("#bookTable").append(tbRow);  
          
        /*從開放時間到關閉時間，每一小時為一個預約時間*/
        for (var time = openTime; time < closeTime; time ++) {  
            var show_time = time + ":00-" + (time + 1) + ":00";  
            tbRow = "<tr><th>" + show_time + "</th>";  
            tbRow += "<td></td></tr>";   
            $("#bookTable").append(tbRow);  
        }  
  
        var first_row = openTime;  
        var today = new Date();
        /*判斷預約的時間是否為今天之前，是就將所有預約時間段設置過期，判斷預約日期如果是今天就將過期的時間設置為當前時間+1(奧爾斯)
          對table的每個td如果在過期時間之前的時間段，就將這一行td的class設置為bookouttime，表示不可預約，否則class設置為bookuseable表示可預約*/
        if(today.getFullYear() > (new Date(date)).getFullYear()){
            $("#bookTable tr").each(function(i) {  
                $(this).find("td").addClass("bookouttime");
            });
        }else if(today.getFullYear() == (new Date(date)).getFullYear())
        {    
            if(today.getMonth() > (new Date(date)).getMonth()){
                $("#bookTable tr").each(function(i) {  
                    $(this).find("td").addClass("bookouttime");
                });
            }else if(today.getMonth() == (new Date(date)).getMonth())
            {
                if(today.getDate() > (new Date(date)).getDate()){
                    $("#bookTable tr").each(function(i) {  
                        $(this).find("td").addClass("bookouttime");
                    });
                }
                if(today.getDate() == (new Date(date)).getDate()){
                    var end_row = parseInt((new Date()).getHours()) + 1;
                    $("#bookTable tr").each(function(i) {  
                        $(this).find("td").addClass(i + first_row <= end_row? "bookouttime":"bookuseable");
                    });
                }
                if(today.getDate() < (new Date(date)).getDate()){
                    $("#bookTable tr").each(function(i) {  
                        $(this).find("td").addClass("bookuseable");
                    });
                }
            }else{
                $("#bookTable tr").each(function(i) {  
                    $(this).find("td").addClass("bookuseable");
                });
            }
        }else{
            $("#bookTable tr").each(function(i) {  
                $(this).find("td").addClass("bookuseable");
            });
        }
        /*開始遍歷，根據預約紀錄的開始時間生成對應的行坐標，找到表格中對應位置的td删除本身的class，增加bookoccupied表示對應時段已經被預約*/
        $(bookList).each(function(i) {  
            var row = parseInt(bookList[i].startTime.split(":")[0]) - openTime + 1;  
            var col = 0;  
            console.log("row:"+row+",col:"+col);  
            $("#bookTable tr").eq(row).find("td").eq(col).removeClass().addClass("bookoccupied");  
        });  
          
        /*所有可以預約的td绑定一個click事件，即點擊表示選中，再點一次恢復原来狀態，直到點擊預約按钮預約成功後，解除click事件，並由bookuseable變為bookoccupied*/
        $("tr .bookuseable").bind("click", function() {  
            $(this).toggleClass("bookselected");
        });  
  
    //}, "json");  
}  

})

/*format(Date型別)=>(資料庫date型別"yyyy-MM-dd")*/
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
