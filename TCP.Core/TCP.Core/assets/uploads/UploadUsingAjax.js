var id = 0;
$('#txtfile').change(function () {
  
    // Checking whether FormData is available in browser  
    if (window.FormData !== undefined) {  
        $('.loader').show();
        var fileUpload = $("#txtfile").get(0);
        var files = fileUpload.files;  
              
        // Create FormData object  
        var fileData = new FormData();  
  
        // Looping over all files and add it to FormData object  
        for (var i = 0; i < files.length; i++) {  
            fileData.append(files[i].name, files[i]);  
        }  
              
        // Adding one more key to FormData object  
        fileData.append('username', 'Manas');  
  
        $.ajax({  
            url: '/AdminArea/Stores/UploadFilesNow1',
            type: "POST",  
            contentType: false, // Not to set any content header  
            processData: false, // Not to process data  
            data: fileData,  
            success: function (result) {
                alert("Đã upload thành công");
                $("#result-img").append(result);
            },  
            error: function (err) {  
                alert(err.statusText);  
            }  
        });  
    } else {  
        alert("FormData is not supported.");  
    }  
});

$('#txtfile1').change(function () {

    // Checking whether FormData is available in browser  
    if (window.FormData !== undefined) {
        $('.loader').show();
        var fileUpload = $("#txtfile1").get(0);
        var files = fileUpload.files;

        // Create FormData object  
        var fileData = new FormData();

        // Looping over all files and add it to FormData object  
        for (var i = 0; i < files.length; i++) {
            fileData.append(files[i].name, files[i]);
        }

        // Adding one more key to FormData object  
        fileData.append('username', 'Manas');

        $.ajax({
            url: '/AdminArea/Stores/UploadFilesNow2',
            type: "POST",
            contentType: false, // Not to set any content header  
            processData: false, // Not to process data  
            data: fileData,
            success: function (result) {
                $("#ressrc").attr("src", "/upload/FileAttach/" + result);
                $("#ressrc").attr("data-src", result);
            },
            error: function (err) {
                alert(err.statusText);
            }
        });
    } else {
        alert("FormData is not supported.");
    }
});


$('#txtfile_profile').change(function () {

    // Checking whether FormData is available in browser  
    if (window.FormData !== undefined) {
        $('.loader').show();
        var fileUpload = $("#txtfile_profile").get(0);
        var files = fileUpload.files;

        // Create FormData object  
        var fileData = new FormData();

        // Looping over all files and add it to FormData object  
        for (var i = 0; i < files.length; i++) {
            fileData.append(files[i].name, files[i]);
        }

        // Adding one more key to FormData object  
        fileData.append('username', 'Manas');

        $.ajax({
            url: '/AdminArea/Stores/UploadFilesNow3',
            type: "POST",
            contentType: false, // Not to set any content header  
            processData: false, // Not to process data  
            data: fileData,
            success: function (result) {
                $("#file_att").val(result);
                $("#rd").html("<a class=\"d-block mt-1\"  target=\"_blank\" href=\"/upload/FileAttach/" + result + "\">Tải tài liệu</a>");
            },
            error: function (err) {
                alert(err.statusText);
            }
        });
    } else {
        alert("FormData is not supported.");
    }
});