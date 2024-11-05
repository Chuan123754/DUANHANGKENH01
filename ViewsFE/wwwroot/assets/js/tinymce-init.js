window.initTinyMCE = function () {
    if (typeof tinymce !== 'undefined') {
        setTimeout(function () {
            // Hủy instance cũ nếu có
            if (tinymce.get('tinymce-textarea')) {
                tinymce.get('tinymce-textarea').remove();
            }

            // Khởi tạo mới
            tinymce.init({
                selector: '#tinymce-textarea',
                // menubar: 'file edit view insert format tools table tc help',
                menubar: false,
                statusbar: false,
                // forced_root_block: 'span',
                plugins: 'preview searchreplace autolink autosave save directionality visualblocks visualchars fullscreen image link media table charmap pagebreak nonbreaking anchor insertdatetime advlist lists wordcount charmap quickbars emoticons code',
                mobile: {
                    plugins: 'preview searchreplace autolink autosave save directionality visualblocks visualchars fullscreen image link media table charmap pagebreak nonbreaking anchor insertdatetime advlist lists wordcount charmap quickbars emoticons'
                },
                menu: {
                    tc: {
                        title: 'Comments',
                        items: 'addcomment showcomments deleteallconversations'
                    }
                },
                toolbar: 'undo redo | fontfamily fontsize blocks | bold italic underline strikethrough | forecolor backcolor casechange removeformat | alignleft aligncenter alignright alignjustify | outdent indent |  numlist bullist  | pagebreak | charmap emoticons | fullscreen  preview save | insertfile image media template link anchor | a11ycheck ltr rtl | code',
                content_style: 'body { font-family: -apple-system, BlinkMacSystemFont, San Francisco, Segoe UI, Roboto, Helvetica Neue, sans-serif; font-size: 14px; -webkit-font-smoothing: antialiased; }',
                image_caption: true,
                quickbars_selection_toolbar: 'bold italic | quicklink h2 h3 blockquote quickimage quicktable',
                toolbar_mode: 'sliding',
                spellchecker_ignore_list: ['Ephox', 'Moxiecode'],
                contextmenu: 'link image table configur',
                a11y_advanced_options: true,
                image_title: true,
                /* enable automatic uploads of images represented by blob or data URIs*/
                automatic_uploads: true,
                file_picker_types: 'image',
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
        }, 500); // Đặt độ trễ 500ms trước khi khởi tạo TinyMCE
    } else {
        console.warn('TinyMCE chưa được tải.');
    }
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