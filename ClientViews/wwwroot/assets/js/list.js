class ListCustom {
     constructor(endPoint, table, queryDefault = {}) {
        this.end_point = endPoint
        this.table = table
        this.queryDefault = queryDefault
        this.headers = {
            'X-CSRF-TOKEN': Dom.select('meta[name="csrf-token"]').attr('content')
        }
    }

    async init() {
        await this.fetchData(this.end_point)
        this.onClickPagination(this.end_point)
        this.onClickDelete()
        this.searchEnter()
        this.formSearchSubmit()
    }

    updateQueryDefault(query) {
        this.queryDefault = query
    }

    updateTable(table) {
        this.table = table
    }
    async fetchData(endPoint) {
        const currentUrl = window.location.href;
        const pathName = window.location.pathname
        const queryParams = Http.getAllUrlParams(currentUrl);
        const queryDefault = this.queryDefault
        const query = Object.assign({}, queryParams, queryDefault)
        const queryString = new URLSearchParams(query).toString()
        let fullUrl = endPoint + '?' + queryString;
        history.pushState(query, "", pathName + '?' + queryString)
        const response = await Http.request(fullUrl)
        const datable = await response.text()

        Dom.selectId(this.table).html(datable)

        new List(this.table, {
            sortClass: 'table-sort',
            listClass: 'table-tbody',
            valueNames: ['sort-title'
            ]
        })
    }

    async onSearch(endPoint) {
        const myFormData = new FormData(Dom.selectId('formSearch')[0]);
        const formDataObj = {};
        myFormData.forEach((value, key) => (formDataObj[key] = value));
        this.queryDefault = formDataObj
        await this.fetchData(endPoint)
    }

    onClickPagination(endPoint) {
        const that = this
        Dom.click('.pagination a.page-link', async function (e) {
            e.preventDefault();
            const rel = Dom.select(this).attr('rel');
            let page = Dom.select(this).text()
            if (rel === 'next') {
                page = parseInt(Dom.select('.pagination .page-item.active .page-link').text())
                page += 1;
            }
            if (rel === 'prev') {
                page = parseInt(Dom.select('.pagination .page-item.active .page-link').text())
                page -= 1;
            }

            await that.fetchData(endPoint, page)
        })
    }

    onClickDelete() {
        const that = this
        Dom.click('.cell-action-delete', function (e) {
            e.preventDefault()
            const link = Dom.select(this).attr('href')

            Swal.fire({
                title: "Bạn muốn xóa bản ghi này?",
                text: "Lưu ý, bản ghi này đã xóa sẽ không thể phục hồi!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#3085d6",
                cancelButtonColor: "#d33",
                confirmButtonText: "Xóa",
                cancelButtonText: "Đóng"
            }).then(async (result) => {
                if (result.isConfirmed) {
                    const res = await Http.request(link, 'GET', this.headers)
                    const data = await res.json()
                    if (data.success) {
                        Swal.fire(data.message, "", "success");
                        console.log(that.end_point)
                        await that.fetchData(that.end_point)
                    } else {
                        Swal.fire(data.message, "", "error");
                    }
                }

            });

        })
    }
    searchEnter() {
        const that = this
        Dom.selectId('searchInput').keyup(async function (event) {
            event.preventDefault()

            if (event.keyCode === 13) {
                await that.onSearch(that.end_point)
            }
        });

    }

    formSearchSubmit() {
        const that = this
        Dom.submit('#formSearch', async function (e) {
            e.preventDefault()
            await that.onSearch(that.end_point)
        })
    }
}


