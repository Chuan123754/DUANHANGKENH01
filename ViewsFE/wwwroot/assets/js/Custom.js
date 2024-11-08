window.initializeDropzone = () => {
    if (typeof Dropzone !== 'undefined') {
        if (Dropzone.instances.length > 0) {
            Dropzone.instances.forEach(dz => dz.destroy());
        }

        var dropzone = new Dropzone("#dropzone-default", {
            url: "https://localhost:7011/api/Files/upload",  // URL của API
            method: "post",
            autoProcessQueue: true,
            addRemoveLinks: true,
            parallelUploads: 20,
            maxFilesize: 1, // Kích thước tối đa là 1 MB
            dictRemoveFile: "Xóa",
            init: function () {
                this.on("success", function (file, response) {
                    toastr.success('Thêm thành công.');
                    // Xóa tệp khỏi Dropzone sau khi upload thành công
                    this.removeFile(file);

                    setTimeout(() => {
                        location.reload(); // Reload the page after 2 seconds
                    }, 2000);
                });


                this.on("error", function (file, errorMessage) {
                    toastr.error('Thêm thất bại: ' + errorMessage);
                    this.removeFile(file); // Xóa tệp khỏi Dropzone nếu có lỗi
                });

                // Thông báo lỗi nếu kích thước tệp vượt quá mức tối đa
                this.on("maxfilesexceeded", function (file) {
                    toastr.error('Tệp vượt quá kích thước tối đa cho phép (1 MB).');
                    this.removeFile(file); // Xóa tệp khỏi Dropzone nếu vượt kích thước
                });
            }
        });
    } else {
        console.error("Dropzone is not defined. Make sure the script is included.");
    }
};