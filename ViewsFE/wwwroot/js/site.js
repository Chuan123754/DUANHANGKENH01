// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
window.initTinyMCE = () => {
    tinymce.init({
        selector: '#tinymce-textarea',
        plugins: 'link image code',
        toolbar: 'undo redo | bold italic | alignleft aligncenter alignright | code',
        menubar: false,
        apiKey: 'ti44eh9igs0fh68b9uati18sym28u6nln90uc4p5sylwkeb7' 
    });
};

window.getTinyMCEContent = () => {
    return tinymce.get('tinymce-textarea').getContent();
};

window.setTinyMCEContent = (content) => {
    tinymce.get('tinymce-textarea').setContent(content);
};
