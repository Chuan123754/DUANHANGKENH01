class Dom {
    static select(selector) {
        return window.$(selector)
    }

    static selectId(selector) {
        return Dom.select(`#${selector}`)
    }

    static selectClass(selector) {
        return Dom.select(`.${selector}`)
    }

    static click(selector, func) {
        Dom.select(document).on('click', selector, func)
    }

    static submit(selector, func) {
        Dom.select(document).on('submit', selector, func)
    }

    static change(selector, func) {
        Dom.select(document).on('change', selector, func)
    }
    static getSelectMultiValue(selector) {
        const selected = document.querySelectorAll(`#${selector} option:checked`);
        return Array.from(selected).map(el => el.value);
    }
    static changeImageBox(selector, selectorImageBox = '.image-box') {
        Dom.change(selector, function (event){
            const file = event.target.files[0];
            console.log(file)
            if (file) {
                const fileURL = URL.createObjectURL(file);
                Dom.select(selectorImageBox + ' img').removeClass('d-none')
                Dom.select(selectorImageBox + ' img').attr('src', fileURL);
                Dom.select(selectorImageBox + ' .icon').addClass('d-none');
            }
        })

    }
    static createSlug(string) {
        const a = 'àáäâãåăæąçćčđďèéěėëêęğǵḧìíïîįłḿǹńňñòóöôœøṕŕřßşśšșťțùúüûǘůűūųẃẍÿýźžż·/_,:;'
        const b = 'aaaaaaaaacccddeeeeeeegghiiiiilmnnnnooooooprrsssssttuuuuuuuuuwxyyzzz------'
        const p = new RegExp(a.split('').join('|'), 'g')
        return string.toString().toLowerCase()
            .replace(/á|à|ả|ạ|ã|ă|ắ|ằ|ẳ|ẵ|ặ|â|ấ|ầ|ẩ|ẫ|ậ/gi, 'a')
            .replace(/é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ/gi, 'e')
            .replace(/i|í|ì|ỉ|ĩ|ị/gi, 'i')
            .replace(/ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ/gi, 'o')
            .replace(/ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự/gi, 'u')
            .replace(/ý|ỳ|ỷ|ỹ|ỵ/gi, 'y')
            .replace(/đ/gi, 'd')
            .replace(/\s+/g, '-')
            .replace(p, c => b.charAt(a.indexOf(c)))
            .replace(/&/g, '-and-')
            .replace(/[^\w\-]+/g, '')
            .replace(/\-\-+/g, '-')
            .replace(/^-+/, '')
            .replace(/-+$/, '')
    }
}
