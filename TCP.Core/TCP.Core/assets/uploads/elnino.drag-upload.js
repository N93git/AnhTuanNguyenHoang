/*
hdGuidName => HiddenField chứa ds guidName - tên mã hóa luu tren HDD
hdFileName => HiddenField chứa ds fileName - tên hiển thị của file
*/
var pnlContainer = '';
var pnlDragUpload = '';
var fileUploadControl = '';
var isFullUpload = '1';
//$(document).ready(function () {
//    ReloadDragUpload();
//});
function ShowLoading() {
    $("#loader").show();
}
var dd = "";
$.fn.bindDragUpload = function () {
    $(this).html5Uploader({
        name: "upfilethum",
        postUrl: "/Tool/Upload.ashx?isfll="+isFullUpload,
        onServerLoadStart: function (e, file) { // dau tien
            ShowLoading();
        },
        onServerProgress: function (e, file) {// thu 2
            ShowLoading();
        },
        onSuccess: function (e, file) {
         
            var data = $.parseJSON(e.currentTarget.response);
            if (data != '-1') {
                // add file GUID moi vao mảng
                var fileGuidList = $('#hdGuidName').val();
                // add file NAME moi vao mang
                var fileNameList = $('#hdFileName').val();
                // image text html
                var imgHTML = '';
                for (i = 0; i < data.length; i++) {
                    var filePath = data[i].uploadPath;
                    var fileName = data[i].fileName;
                    var fileGuid = data[i].guidName;
                    dd += fileGuid + ",";
                    if ($("#typeimage").val() == 2) {
                        savedd = savedd + fileGuid + ",";
                    }
                    
                 
                    imgHTML += '<img src="' + filePath + '" alt="' + fileName + '" width="150px" />';
                    fileGuidList = fileGuid;
                    fileNameList = fileName;
                }

                $('#' + pnlContainer).html(imgHTML);

                //$('#hdGuidName').val(fileGuidList);
                //$('#hdFileName').val(fileNameList);
                //$('#' + pnlContainer).removeAttr('style');
            }
            if (data == '-1') {
                alert('Hết phiên đăng nhập. Vui lòng đăng nhập lại.');
            }

            bien = bien - 1;
            //HideLoading();
            // console.log(dd);
            if ($("#typeimage").val() == 1) {
                dd = dd.replace(',', '');
                $("#hdFileName").val(dd);
                var newimg2 = dd.replace('.', '');
                $("#dd").html("<div class=\"myimage\" ><img src=\"/upload/temp/" + dd + "\"></img><a onclick=\"img_click('" + dd + "','Temp','" + newimg2 + "')\">x</a> </div>");

            }
            if ($("#typeimage").val() == 4) {
                dd = dd.replace(',', '');
                $("#hdFileName4").val(dd);
                var newimg2 = dd.replace('.', '');
                $("#dd4").html("<div class=\"myimage\" ><img src=\"/upload/temp/" + dd + "\"></img><a onclick=\"img_click('" + dd + "','Temp','" + newimg2 + "')\">x</a> </div>");

            }
            if ($("#typeimage").val() == 5) {
                dd = dd.replace(',', '');
                $("#hdFileName5").val(dd);
                var newimg2 = dd.replace('.', '');
                $("#dd5").html("<div class=\"myimage\" ><img src=\"/upload/temp/" + dd + "\"></img><a onclick=\"img_click('" + dd + "','Temp','" + newimg2 + "')\">x</a> </div>");

            }
            if ($("#typeimage").val() == 3) {
                dd = dd.replace(',', '');
                $("#hdFileName2").val(dd);
                var newimg2 = dd.replace('.', '');
                $("#dd2").html("<div class=\"myimage\" ><img src=\"/upload/temp/" + dd + "\"></img><a onclick=\"img_click('" + dd + "','Temp','" + newimg2 + "')\">x</a> </div>");

            }
            else {
                if (bien == 0) {
                    var listdd = dd.split(',');
                    for (i = 0; i < listdd.length - 1; i++) {
                        var newimg = listdd[i].replace('.', '');
                        $("#multidd").append("<div class=\"col-sm-2 mb-20\" id=\"" + newimg + "\"><div class=\"myimage \"><img src=\"/upload/temp/" + listdd[i] + "\"></img><a onclick=\"img_click2('" + listdd[i] + "','Temp','" + newimg+"')\">x</a></div></div>")
                    }
                }
            }
            
            
           
        }
    });
}

// gắn action kéo thả khi page_load
function ReloadDragUpload() {
    $('#' + pnlDragUpload).bindDragUpload();
    $('#' + fileUploadControl).bindDragUpload();
    $('#' + fileUploadControl2).bindDragUpload();
    $('#' + fileUploadControl3).bindDragUpload();
    $('#' + fileUploadControl4).bindDragUpload();
    $('#' + fileUploadControl5).bindDragUpload();
}

function RemoveFileFromList(removeName, hdFileName) {
    var curentFileList = $('#' + hdFileName).val();
    // chua co file thi ko xu ly
    if (curentFileList.length > 0) {
        var valResult = '';
        // dua vao mang
        var arr = curentFileList.split(':');
        // lap tung phan tu mang
        for (i = 0; i < arr.length; i++) {
            // lay ID
            var itName = arr[i];
            if (removeName != itName) {
                valResult += valResult.length > 0 ? ':' : '';
                valResult += itName;
            }
        }
        $('#' + hdFileName).val(valResult);
    }
}

$(document).on('click', 'span.cmd-del-file', function () {
    if (window.confirm('Bạn chắc chắn muốn xóa file này?') == false) {
        return false;
    }
    else {
        // bat loading
        ShowLoading();
        // lay ten file GUI cần xóa
        var fileGUID = $(this).attr('data-val');
        // lay ten file NAME cần xóa
        var fileName = $(this).attr('data-rel');
        // danh dau ket quả trả ve
        var boolRlt = false;
        // bat dau xoa file
        $.ajax({
            url: '/Tool/Upload.ashx?f=' + fileGUID + '&ac=DELETE',
            cache: false,
            success: function (result) {
                if (result == 1) boolRlt = true;
            },
            async: false
        });

        // tắt loading
        HideLoading();
        // if xoa file thành công
        if (boolRlt) {
            // remove parent div chứa thông tin file
            $(this).parent('div').remove();
            // xoa file GUID khoi ds
            RemoveFileFromList(fileGUID, 'hdGuidName');
            // xoa file NAME khoi ds
            RemoveFileFromList(fileName, 'hdFileName');
            
        }
    }
});