//Dom.select(document).ready(function () {
//    // $('#mediaModal').modal('show')
//    var mediaMultiple = false;
//    var mediaReferId = '';
//    var mediaInputName = '';

//    Dom.click('.open-media-modal', function (e) {
//        e.preventDefault()
//        Dom.select('#mediaModal').modal('show')
//        mediaMultiple = Dom.select(this).attr('data-multiple')
//        mediaReferId = Dom.select(this).attr('data-refer-id')
//        mediaInputName = Dom.select(this).attr('data-input-name')
//        let mediaImageIds = Dom.select(mediaReferId).attr('data-image-ids').split(',')
//        console.log(mediaImageIds)
//        Dom.select(`.btn-choose-image`).removeClass('select')
//        for (let i = 0; i < mediaImageIds.length; ++i) {
//            Dom.select(`.btn-choose-image[data-file-id="${mediaImageIds[i]}"]`).addClass('select')
//        }

//    })
//    Dom.click('[data-dismiss="modal"]', function (e) {
//        Dom.select('#mediaModal').modal('hide')

//    })
//    Dom.click('.btn-choose-image', function () {
//        if (mediaMultiple === 'false') {
//            Dom.select('.btn-choose-image').removeClass('select')
//        }
//        Dom.select(this).toggleClass('select')

//    })

//    Dom.click('#fileConfirm', function (e) {
//        e.preventDefault()
//        Dom.select(`${mediaReferId} img`).remove()
//        Dom.select(`${mediaReferId} input`).remove()
//        if (mediaMultiple === 'false') {
//            Dom.select(`${mediaReferId} .icon`).addClass('d-none')
//        }

//        let html = ''
//        let fileIds = []
//        Dom.select('.btn-choose-image.select').each(function (index, item) {
//            let fileId = item.getAttribute('data-file-id')
//            let fileImage = item.getAttribute('data-file-image')
//            fileIds.push(fileId)
//            html += `
//                <div><input type="hidden" name="${mediaInputName}[${index}][id]" value="${fileId}">
//                <input type="hidden" name="${mediaInputName}[${index}][image]" value="${fileImage}">
//                <img
//                        src="${fileImage}"
//                        class="w-100 h-100 object-cover">
//                        </div>
//            `
//        })
//        if (!Dom.select(`${mediaReferId}`).hasClass('grid-template-columns-2')) {
//            Dom.select(`${mediaReferId}`).addClass('grid-template-columns-2')
//        }
//        Dom.select(`${mediaReferId}`).prepend(html)
//        Dom.select(`${mediaReferId}`).attr('data-image-ids', fileIds.join(','))
//        Dom.select('#mediaModal').modal('hide')

//    })
//})

