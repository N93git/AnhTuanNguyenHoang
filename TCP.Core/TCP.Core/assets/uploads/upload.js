$.fn.bindDragUpload = function (callback) {
    $(this).html5Uploader({
        name: "upfilethum",
        postUrl: "/Tool/Upload.ashx",
        onServerLoadStart: function (e, file) { // dau tien
            $('.upload-wait').removeClass('hide');
        },
        onServerProgress: function (e, file) {// thu 2
            $('.upload-wait').removeClass('hide');
        },
        onSuccess: function (e, file) {
            var data = $.parseJSON(e.currentTarget.response);
            console.log(data);
            if (data != '-1') {
                //var listFiles = '';
                //if (isMultiFile) {
                //    for (i = 0; i < data.length; i++) {
                //        listFiles = data[i].FileName;
                //    }
                //}
                //else {
                //    listFiles = data[0].FileName;
                //}

                //$('#' + ctrlData).val(listFiles);
                callback(data);
            }

            if (data == '-1') {
                alert('Hết phiên đăng nhập. Vui lòng đăng nhập lại.');
            }
            $('.upload-wait').addClass('hide');
        }
    });
}

function ReloadDragUpload(containerName, callback) {
    var fuId = $('.' + containerName + " input[type=file]").attr('id');
    $('.' + containerName).bindDragUpload(callback);
    if(fuId!=undefined)
        $('#' + fuId).bindDragUpload(callback);
}


function RemoveImage(guidName) {
}