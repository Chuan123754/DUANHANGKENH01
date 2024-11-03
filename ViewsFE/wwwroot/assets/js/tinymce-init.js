window.initTinyMCE = function () {
    // Hủy instance cũ nếu có
    if (tinymce.get('tinymce-textarea')) {
        tinymce.get('tinymce-textarea').remove();
    }

    // Khởi tạo mới
    tinymce.init({
        selector: '#tinymce-textarea',
        plugins: 'link image code',
        toolbar: 'undo redo | bold italic | alignleft aligncenter alignright | code',
        height: 500,
        setup: function (editor) {
            editor.on('init', function () {
                console.log('TinyMCE initialized');
            });
        },
        init_instance_callback: function (editor) {
            editor.on('Change', function (e) {
                // Cập nhật nội dung khi có thay đổi
                let content = editor.getContent();
                // Nếu cần, bạn có thể gọi một phương thức .NET từ đây
            });
        }
    });
};

window.getTinyMCEContent = function () {
    const editor = tinymce.get('tinymce-textarea');
    return editor ? editor.getContent() : '';
};

window.setTinyMCEContent = function (content) {
    const editor = tinymce.get('tinymce-textarea');
    if (editor) {
        editor.setContent(content);
    }
};