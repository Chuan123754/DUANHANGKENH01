class OrderList {
    constructor(endPoint, table, queryDefault = {}) {
        this.end_point = endPoint
        this.table = table

        this.forceSearch = false
        this.queryDefault = queryDefault
        this.headers = {
            'X-CSRF-TOKEN': Dom.select('meta[name="csrf-token"]').attr('content')
        }
    }

    async init() {
        await this.fetchData(this.end_point)
        this.onClickPagination(this.end_point)
        this.searchEnter()
        this.formSearchSubmit()
    }

    updateQueryDefault(query) {
        this.queryDefault = query
    }

    updateTable(table) {
        this.table = table
    }

    updateForceSearch(forceSearch) {
        this.forceSearch = forceSearch
    }

    async fetchData(endPoint, page = 1) {
        const currentUrl = window.location.href;
        const pathName = window.location.pathname
        const queryParams = Http.getAllUrlParams(currentUrl);
        const queryDefault = this.queryDefault
        let query
        if (this.forceSearch) {
            query = Object.assign({}, queryDefault, {
                page
            })
        } else {
            query = Object.assign({}, queryParams, queryDefault, {
                page
            })
        }

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

    async onSearch(endPoint, formData) {
        const formDataObj = {};
        formData.forEach((value, key) => (formDataObj[key] = value));
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

    searchEnter() {
        const that = this
        Dom.selectClass('search-form-input').keyup(async function (event) {
            event.preventDefault()

            if (event.keyCode === 13) {
                const formData = new FormData()
                formData.append('s', event.target.value)
                await that.onSearch(that.end_point, formData)
            }
        });

    }

    formSearchSubmit() {
        const that = this
        Dom.submit('.search-form', async function (e) {
            e.preventDefault()
            const formData = new FormData(this)
            await that.onSearch(that.end_point, formData)
        })
    }
}


