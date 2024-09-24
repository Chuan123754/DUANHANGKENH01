class Category {
    static get() {
        $.ajax({
            url,
            headers: {
                'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content')
            },
            processData: false,
            contentType: false,
            method: 'POST',
            data: formData,
            success: function(data){
                console.log(data)
            },
            error: function (err){
                if(err.status === 400){
                    const dataJson = err.responseJSON
                    toastr.error(dataJson.message)
                    const errors = dataJson.errors

                    Object.entries(errors).forEach(function ([index, item]){
                        let html = '';
                        item.map(function (message){
                            html += `<p class="text-danger mt-2 mb-0">${message}</p>`;
                        })
                        $(`#${index}_error`).append(html)
                    })
                }else{
                    toastr.error("Có lỗi xảy ra, vui lòng liên hệ admin")
                    console.error(err)
                }
            }
        })
    }
}
