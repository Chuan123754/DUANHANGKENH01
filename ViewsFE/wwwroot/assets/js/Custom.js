//function openMediaModal() {
//    var modalElement = document.getElementById('mediaModal');
//    if (modalElement) {
//        if (!modalElement._bsModalInstance) {
//            modalElement._bsModalInstance = new bootstrap.Modal(modalElement);
//        }
//        modalElement._bsModalInstance.show();
//    } else {
//        console.error("Không tìm thấy phần tử với id 'mediaModal'.");
//    }
//}
//function closeMediaModal() {
//    var modalElement = document.getElementById('mediaModal');
//    if (modalElement && modalElement._bsModalInstance) {
//        modalElement._bsModalInstance.hide();
//    }
//}

//window.TagsList = [];
//window.CategoriesList = [];
//function initializeTagsList(tags) {
//    if (tags && tags.length > 0) {
//        window.TagsList = tags;
//        console.log('TagsList initialized:', window.TagsList);
//    } else {
//        console.log('Failed to initialize TagsList. Empty or undefined.');
//    }
//}
//function initializeCateList(cate) {
//    if (cate && cate.length > 0) {
//        window.CategoriesList = cate;
//        console.log('cate initialized:', window.CategoriesList);
//    } else {
//        console.log('Failed to initialize TagsList. Empty or undefined.');
//    }
//}

//function resetTomSelect(selector) {
//    const element = document.querySelector(selector);
//    if (element && element.tomselect) {
//        element.tomselect.destroy(); // Hủy bỏ instance hiện tại
//    }
//}

//function initializeTomSelect(selector) {
//    resetTomSelect(selector); // Đảm bảo reset trước khi khởi tạo mới

//    const element = document.querySelector(selector);
//    if (element) {
//        new TomSelect(selector, {
//            create: true, // Cho phép thêm mới cho từ khóa
//            persist: false,
//            hideSelected: true,
//            closeAfterSelect: true,
//            delimiter: ',',
//            plugins: ['remove_button'],
//            sortField: {
//                field: 'text',
//                direction: 'asc'
//            },
//            searchField: ['text'],
//            maxOptions: null,
//            preload: false, // Đặt thành false để chỉ tải kết quả khi người dùng tìm kiếm
//            load: function (query, callback) {
//                if (!window.TagsList || window.TagsList.length === 0) {
//                    console.log('TagsList is empty or undefined.');
//                    return callback();
//                }

//                // Chỉ hiển thị khi có nội dung được nhập
//                if (query.length > 0) {
//                    const results = window.TagsList.filter(tag =>
//                        tag?.title?.toLowerCase().includes(query.toLowerCase())
//                    );

//                    callback(results.map(tag => ({ value: tag.id, text: tag.title })));
//                } else {
//                    callback(); // Không trả về kết quả nếu không có query
//                }
//            }
//        });
//    }
//}

//function initializeTomSelectForCategories(selector) {
//    resetTomSelect(selector); // Đảm bảo reset trước khi khởi tạo mới

//    const element = document.querySelector(selector);
//    if (element) {
//        new TomSelect(selector, {
//            create: false,
//            persist: false,
//            hideSelected: true,
//            closeAfterSelect: false,
//            delimiter: ',',
//            plugins: ['remove_button'],
//            sortField: {
//                field: 'text',
//                direction: 'asc'
//            },
//            searchField: ['text'],
//            maxOptions: null,
//            preload: true,
//            load: function (query, callback) {
//                const results = query ?
//                    window.CategoriesList.filter(cate => cate?.title?.toLowerCase().includes(query.toLowerCase())) :
//                    window.CategoriesList;

//                callback(results.map(cate => ({ value: cate.id, text: cate.title })));
//            }
//        });
//    }
//}
//function getSelectedValues(selector) {
//    const element = document.querySelector(selector);
//    if (element && element.tomselect) {
//        return element.tomselect.getValue().join(','); // Chuyển đổi mảng thành chuỗi
//    }
//    return '';
//}