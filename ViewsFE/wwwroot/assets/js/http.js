var headerDefault = {
    'X-Requested-With': 'XMLHttpRequest'
}
class Http {
    static async request(url, method = 'GET', headers = {}) {
        try {
            return await fetch(url, {
                method,
                headers: Object.assign({}, headerDefault, headers)
            });
        } catch (error) {
            console.error('GET request failed:', error);
            throw error;
        }
    }

    static async get(url, headers = {}) {
        try {
            const response = await fetch(url, {
                method: 'GET',
                headers: Object.assign({}, headerDefault, headers)
            });
            return this.handleResponse(response);
        } catch (error) {
            console.error('GET request failed:', error);
            throw error;
        }
    }

    static async postForm(url, data, headers = {}) {
        try {
            headers['X-CSRF-TOKEN'] = $('meta[name="csrf-token"]').attr('content')
            const response = await fetch(url, {
                method: 'POST',
                headers: Object.assign({}, headerDefault, headers),
                body: data
            });
            return this.handleResponse(response);
        } catch (error) {
            console.error('POST request failed:', error);
            throw error;
        }
    }

    static async post(url, data = {}, headers = {}) {
        try {
            console.log(data)
            const response = await fetch(url, {
                method: 'POST',
                headers: Object.assign({}, headerDefault, headers),
                body: data
            });
            return this.handleResponse(response);
        } catch (error) {
            console.error('POST request failed:', error);
            throw error;
        }
    }

    static async put(url, data, headers = {}) {
        try {
            headers['Content-Type'] = headers['Content-Type'] || 'application/json';
            const response = await fetch(url, {
                method: 'PUT',
                headers: Object.assign({}, headerDefault, headers),
                body: JSON.stringify(data)
            });
            return this.handleResponse(response);
        } catch (error) {
            console.error('PUT request failed:', error);
            throw error;
        }
    }

    static async del(url, headers = {}) {
        try {
            const response = await fetch(url, {
                method: 'DELETE',
                headers: Object.assign({}, headerDefault, headers)
            });
            return this.handleResponse(response);
        } catch (error) {
            console.error('DELETE request failed:', error);
            throw error;
        }
    }

    static async handleResponse(response) {
        if (response.ok) {
            try {
                return await response.json();
            } catch (error) {
                return response;
            }
        } else {
            const error = await response.text();
            throw new Error(error);
        }
    }

    static buildUrlWithParams(url, params) {
        return url + '?' + new URLSearchParams(params).toString();
    }

    static getAllUrlParams(url) {

        // get query string from url (optional) or window
        var queryString = url ? url.split('?')[1] : window.location.search.slice(1);

        // we'll store the parameters here
        var obj = {};

        // if query string exists
        if (queryString) {

            // stuff after # is not part of query string, so get rid of it
            queryString = queryString.split('#')[0];

            // split our query string into its component parts
            var arr = queryString.split('&');

            for (var i = 0; i < arr.length; i++) {
                // separate the keys and the values
                var a = arr[i].split('=');

                // set parameter name and value (use 'true' if empty)
                var paramName = a[0];
                var paramValue = typeof (a[1]) === 'undefined' ? true : a[1];

                // (optional) keep case consistent
                paramName = paramName.toLowerCase();
                if (typeof paramValue === 'string') paramValue = paramValue.toLowerCase();

                // if the paramName ends with square brackets, e.g. colors[] or colors[2]
                if (paramName.match(/\[(\d+)?\]$/)) {

                    // create key if it doesn't exist
                    var key = paramName.replace(/\[(\d+)?\]/, '');
                    if (!obj[key]) obj[key] = [];

                    // if it's an indexed array e.g. colors[2]
                    if (paramName.match(/\[\d+\]$/)) {
                        // get the index value and add the entry at the appropriate position
                        var index = /\[(\d+)\]/.exec(paramName)[1];
                        obj[key][index] = paramValue;
                    } else {
                        // otherwise add the value to the end of the array
                        obj[key].push(paramValue);
                    }
                } else {
                    // we're dealing with a string
                    if (!obj[paramName]) {
                        // if it doesn't exist, create property
                        obj[paramName] = paramValue;
                    } else if (obj[paramName] && typeof obj[paramName] === 'string') {
                        // if property does exist and it's a string, convert it to an array
                        obj[paramName] = [obj[paramName]];
                        obj[paramName].push(paramValue);
                    } else {
                        // otherwise add the property
                        obj[paramName].push(paramValue);
                    }
                }
            }
        }

        return obj;
    }
    static getQueryString(queryExtend = {}){
        const currentUrl = window.location.href;
        const queryParams = Http.getAllUrlParams(currentUrl);
        const query = Object.assign({}, queryParams, queryExtend)
        return new URLSearchParams(query).toString()
    }
}
