function openMediaModal() {
    var modalElement = document.getElementById('mediaModal');
    if (modalElement) {
        if (!modalElement._bsModalInstance) {
            modalElement._bsModalInstance = new bootstrap.Modal(modalElement);
        }
        modalElement._bsModalInstance.show();
    }
}
function openMediaBannerModal() {
    var modalElement = document.getElementById('mediaBannerModal');
    if (modalElement) {
        if (!modalElement._bsModalInstance) {
            modalElement._bsModalInstance = new bootstrap.Modal(modalElement);
        }
        modalElement._bsModalInstance.show();
    }
}
function openMediaModalProduct() {
    var modalElement = document.getElementById('mediaModalProduct');
    if (modalElement) {
        if (!modalElement._bsModalInstance) {
            modalElement._bsModalInstance = new bootstrap.Modal(modalElement);
        }
        modalElement._bsModalInstance.show();
    }
}
function openMediaModalN(isMultiple) {
    console.log("OpenMediaModalN called with isMultiple: " + isMultiple);
    var modalElement = document.getElementById('mediaLibraryModal');
    if (modalElement) {
        modalElement.setAttribute('data-multiple', isMultiple ? "true" : "false");
        if (!modalElement._bsModalInstance) {
            modalElement._bsModalInstance = new bootstrap.Modal(modalElement);
        }
        modalElement._bsModalInstance.show();
    }
}

function closeMediaModal() {
    var modalElement = document.getElementById('mediaModal');
    if (modalElement && modalElement._bsModalInstance) {
        modalElement._bsModalInstance.hide();
    }
}
function closeMediaModalN() {
    var modalElement = document.getElementById('mediaLibraryModal');
    if (modalElement && modalElement._bsModalInstance) {
        modalElement._bsModalInstance.hide();
    }
}
function closeMediaBannerModal() {
    var modalElement = document.getElementById('mediaBannerModal');
    if (modalElement && modalElement._bsModalInstance) {
        modalElement._bsModalInstance.hide();
    }
}

function closeMediaModalProduct() {
    var modalElement = document.getElementById('mediaModalProduct');
    if (modalElement && modalElement._bsModalInstance) {
        modalElement._bsModalInstance.hide();
    }
}



window.TagsList = [];
function initializeTagsList(tags) {
    if (tags && tags.length > 0) {
        window.TagsList = tags;
    } else {
        console.log('Failed to initialize TagsList. Empty or undefined.');
    }
}
window.CategoriesList = [];
function initializeCateList(cate) {
    if (cate && cate.length > 0) {
        window.CategoriesList = cate;
    } else {
        console.log('Failed to initialize CateList. Empty or undefined.');
    }
}

function resetTomSelect(selector) {
    const element = document.querySelector(selector);
    if (element && element.tomselect) {
        element.tomselect.destroy(); // Hủy bỏ instance hiện tại
    }
}

function initializeTomSelect(selector) {
    resetTomSelect(selector); // Đảm bảo reset trước khi khởi tạo mới

    const element = document.querySelector(selector);
    if (element) {
        new TomSelect(selector, {
            create: false, // Cho phép thêm mới cho từ khóa
            persist: false,
            hideSelected: true,
            closeAfterSelect: true,
            delimiter: ',',
            plugins: ['remove_button'],
            sortField: {
                field: 'text',
                direction: 'asc'
            },
            searchField: ['text'],
            maxOptions: null,
            preload: false,
            load: function (query, callback) {
                if (!window.TagsList || window.TagsList.length === 0) {
                    console.log('TagsList is empty or undefined.');
                    return callback();
                }

                // Chỉ hiển thị khi có nội dung được nhập
                if (query.length > 0) {
                    const results = window.TagsList.filter(tag =>
                        tag?.title?.toLowerCase().includes(query.toLowerCase())
                    );

                    callback(results.map(tag => ({ value: tag.id, text: tag.title })));
                } else {
                    callback();
                }
            }
        });
    }
}

function initializeTomSelectForCategories(selector) {
    resetTomSelect(selector);

    const element = document.querySelector(selector);
    if (element) {
        new TomSelect(selector, {
            create: false,
            persist: false,
            hideSelected: true,
            closeAfterSelect: false,
            delimiter: ',',
            plugins: ['remove_button'],
            sortField: {
                field: 'text',
                direction: 'asc'
            },
            searchField: ['text'],
            maxOptions: null,
            preload: true,
            load: function (query, callback) {
                const results = query ?
                    window.CategoriesList.filter(cate => cate?.title?.toLowerCase().includes(query.toLowerCase())) :
                    window.CategoriesList;

                callback(results.map(cate => ({ value: cate.id, text: cate.title })));
            }
        });
    }
}
function getSelectedValues(selector) {
    const element = document.querySelector(selector);
    if (element && element.tomselect) {
        const values = element.tomselect.getValue();
        return values.length > 0 ? values.join(',') : '';
    }
    console.error(`TomSelect not found for ${selector}`);
    return '';
}