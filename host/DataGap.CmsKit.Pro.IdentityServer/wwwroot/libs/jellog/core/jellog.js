var jellog = jellog || {};
(function () {

    /* Application paths *****************************************/

    //Current application root path (including virtual directory if exists).
    jellog.appPath = jellog.appPath || '/';

    jellog.pageLoadTime = new Date();

    //Converts given path to absolute path using jellog.appPath variable.
    jellog.toAbsAppPath = function (path) {
        if (path.indexOf('/') == 0) {
            path = path.substring(1);
        }

        return jellog.appPath + path;
    };

    /* LOGGING ***************************************************/
    //Implements Logging API that provides secure & controlled usage of console.log

    jellog.log = jellog.log || {};

    jellog.log.levels = {
        DEBUG: 1,
        INFO: 2,
        WARN: 3,
        ERROR: 4,
        FATAL: 5
    };

    jellog.log.level = jellog.log.levels.DEBUG;

    jellog.log.log = function (logObject, logLevel) {
        if (!window.console || !window.console.log) {
            return;
        }

        if (logLevel != undefined && logLevel < jellog.log.level) {
            return;
        }

        console.log(logObject);
    };

    jellog.log.debug = function (logObject) {
        jellog.log.log("DEBUG: ", jellog.log.levels.DEBUG);
        jellog.log.log(logObject, jellog.log.levels.DEBUG);
    };

    jellog.log.info = function (logObject) {
        jellog.log.log("INFO: ", jellog.log.levels.INFO);
        jellog.log.log(logObject, jellog.log.levels.INFO);
    };

    jellog.log.warn = function (logObject) {
        jellog.log.log("WARN: ", jellog.log.levels.WARN);
        jellog.log.log(logObject, jellog.log.levels.WARN);
    };

    jellog.log.error = function (logObject) {
        jellog.log.log("ERROR: ", jellog.log.levels.ERROR);
        jellog.log.log(logObject, jellog.log.levels.ERROR);
    };

    jellog.log.fatal = function (logObject) {
        jellog.log.log("FATAL: ", jellog.log.levels.FATAL);
        jellog.log.log(logObject, jellog.log.levels.FATAL);
    };

    /* LOCALIZATION ***********************************************/

    jellog.localization = jellog.localization || {};

    jellog.localization.values =  jellog.localization.values || {};

    jellog.localization.localize = function (key, sourceName) {
        if (sourceName === '_') { //A convention to suppress the localization
            return key;
        }

        sourceName = sourceName || jellog.localization.defaultResourceName;
        if (!sourceName) {
            jellog.log.warn('Localization source name is not specified and the defaultResourceName was not defined!');
            return key;
        }

        var source = jellog.localization.values[sourceName];
        if (!source) {
            jellog.log.warn('Could not find localization source: ' + sourceName);
            return key;
        }

        var value = source[key];
        if (value == undefined) {
            return key;
        }

        var copiedArguments = Array.prototype.slice.call(arguments, 0);
        copiedArguments.splice(1, 1);
        copiedArguments[0] = value;

        return jellog.utils.formatString.apply(this, copiedArguments);
    };

    jellog.localization.isLocalized = function (key, sourceName) {
        if (sourceName === '_') { //A convention to suppress the localization
            return true;
        }

        sourceName = sourceName || jellog.localization.defaultResourceName;
        if (!sourceName) {
            return false;
        }

        var source = jellog.localization.values[sourceName];
        if (!source) {
            return false;
        }

        var value = source[key];
        if (value === undefined) {
            return false;
        }

        return true;
    };

    jellog.localization.getResource = function (name) {
        return function () {
            var copiedArguments = Array.prototype.slice.call(arguments, 0);
            copiedArguments.splice(1, 0, name);
            return jellog.localization.localize.apply(this, copiedArguments);
        };
    };

    jellog.localization.defaultResourceName = undefined;
    jellog.localization.currentCulture = {
        cultureName: undefined
    };

    var getMapValue = function (packageMaps, packageName, language) {
        language = language || jellog.localization.currentCulture.name;
        if (!packageMaps || !packageName || !language) {
            return language;
        }

        var packageMap = packageMaps[packageName];
        if (!packageMap) {
            return language;
        }

        for (var i = 0; i < packageMap.length; i++) {
            var map = packageMap[i];
            if (map.name === language){
                return map.value;
            }
        }

        return language;
    };

    jellog.localization.getLanguagesMap = function (packageName, language) {
        return getMapValue(jellog.localization.languagesMap, packageName, language);
    };

    jellog.localization.getLanguageFilesMap = function (packageName, language) {
        return getMapValue(jellog.localization.languageFilesMap, packageName, language);
    };

    /* AUTHORIZATION **********************************************/

    jellog.auth = jellog.auth || {};

    jellog.auth.policies = jellog.auth.policies || {};

    jellog.auth.grantedPolicies = jellog.auth.grantedPolicies || {};

    jellog.auth.isGranted = function (policyName) {
        return jellog.auth.policies[policyName] != undefined && jellog.auth.grantedPolicies[policyName] != undefined;
    };

    jellog.auth.isAnyGranted = function () {
        if (!arguments || arguments.length <= 0) {
            return true;
        }

        for (var i = 0; i < arguments.length; i++) {
            if (jellog.auth.isGranted(arguments[i])) {
                return true;
            }
        }

        return false;
    };

    jellog.auth.areAllGranted = function () {
        if (!arguments || arguments.length <= 0) {
            return true;
        }

        for (var i = 0; i < arguments.length; i++) {
            if (!jellog.auth.isGranted(arguments[i])) {
                return false;
            }
        }

        return true;
    };

    jellog.auth.tokenCookieName = 'Jellog.AuthToken';

    jellog.auth.setToken = function (authToken, expireDate) {
        jellog.utils.setCookieValue(jellog.auth.tokenCookieName, authToken, expireDate, jellog.appPath, jellog.domain);
    };

    jellog.auth.getToken = function () {
        return jellog.utils.getCookieValue(jellog.auth.tokenCookieName);
    }

    jellog.auth.clearToken = function () {
        jellog.auth.setToken();
    }

    /* SETTINGS *************************************************/

    jellog.setting = jellog.setting || {};

    jellog.setting.values = jellog.setting.values || {};

    jellog.setting.get = function (name) {
        return jellog.setting.values[name];
    };

    jellog.setting.getBoolean = function (name) {
        var value = jellog.setting.get(name);
        return value == 'true' || value == 'True';
    };

    jellog.setting.getInt = function (name) {
        return parseInt(jellog.setting.values[name]);
    };

    /* NOTIFICATION *********************************************/
    //Defines Notification API, not implements it

    jellog.notify = jellog.notify || {};

    jellog.notify.success = function (message, title, options) {
        jellog.log.warn('jellog.notify.success is not implemented!');
    };

    jellog.notify.info = function (message, title, options) {
        jellog.log.warn('jellog.notify.info is not implemented!');
    };

    jellog.notify.warn = function (message, title, options) {
        jellog.log.warn('jellog.notify.warn is not implemented!');
    };

    jellog.notify.error = function (message, title, options) {
        jellog.log.warn('jellog.notify.error is not implemented!');
    };

    /* MESSAGE **************************************************/
    //Defines Message API, not implements it

    jellog.message = jellog.message || {};

    jellog.message._showMessage = function (message, title) {
        alert((title || '') + ' ' + message);
    };

    jellog.message.info = function (message, title) {
        jellog.log.warn('jellog.message.info is not implemented!');
        return jellog.message._showMessage(message, title);
    };

    jellog.message.success = function (message, title) {
        jellog.log.warn('jellog.message.success is not implemented!');
        return jellog.message._showMessage(message, title);
    };

    jellog.message.warn = function (message, title) {
        jellog.log.warn('jellog.message.warn is not implemented!');
        return jellog.message._showMessage(message, title);
    };

    jellog.message.error = function (message, title) {
        jellog.log.warn('jellog.message.error is not implemented!');
        return jellog.message._showMessage(message, title);
    };

    jellog.message.confirm = function (message, titleOrCallback, callback) {
        jellog.log.warn('jellog.message.confirm is not properly implemented!');

        if (titleOrCallback && !(typeof titleOrCallback == 'string')) {
            callback = titleOrCallback;
        }

        var result = confirm(message);
        callback && callback(result);
    };

    /* UI *******************************************************/

    jellog.ui = jellog.ui || {};

    /* UI BLOCK */
    //Defines UI Block API and implements basically

    var $jellogBlockArea = document.createElement('div');
    $jellogBlockArea.classList.add('jellog-block-area');

    /* opts: { //Can be an object with options or a string for query a selector
     *  elm: a query selector (optional - default: document.body)
     *  busy: boolean (optional - default: false)
     *  promise: A promise with always or finally handler (optional - auto unblocks the ui if provided)
     * }
     */
    jellog.ui.block = function (opts) {
        if (!opts) {
            opts = {};
        } else if (typeof opts == 'string') {
            opts = {
                elm: opts
            };
        }

        var $elm = document.querySelector(opts.elm) || document.body;

        if (opts.busy) {
            $jellogBlockArea.classList.add('jellog-block-area-busy');
        } else {
            $jellogBlockArea.classList.remove('jellog-block-area-busy');
        }

        if (document.querySelector(opts.elm)) {
            $jellogBlockArea.style.position = 'absolute';
        } else {
            $jellogBlockArea.style.position = 'fixed';
        }

        $elm.appendChild($jellogBlockArea);

        if (opts.promise) {
            if (opts.promise.always) { //jQuery.Deferred style
                opts.promise.always(function () {
                    jellog.ui.unblock({
                        $elm: opts.elm
                    });
                });
            } else if (opts.promise['finally']) { //Q style
                opts.promise['finally'](function () {
                    jellog.ui.unblock({
                        $elm: opts.elm
                    });
                });
            }
        }
    };

    /* opts: {
     *
     * }
     */
    jellog.ui.unblock = function (opts) {
        var element = document.querySelector('.jellog-block-area');
        if (element) {
            element.classList.add('jellog-block-area-disappearing');
            setTimeout(function () {
                if (element) {
                    element.classList.remove('jellog-block-area-disappearing');
                    element.parentElement.removeChild(element);
                }
            }, 250);
        }
    };

    /* UI BUSY */
    //Defines UI Busy API, not implements it

    jellog.ui.setBusy = function (opts) {
        if (!opts) {
            opts = {
                busy: true
            };
        } else if (typeof opts == 'string') {
            opts = {
                elm: opts,
                busy: true
            };
        }

        jellog.ui.block(opts);
    };

    jellog.ui.clearBusy = function (opts) {
        jellog.ui.unblock(opts);
    };

    /* SIMPLE EVENT BUS *****************************************/

    jellog.event = (function () {

        var _callbacks = {};

        var on = function (eventName, callback) {
            if (!_callbacks[eventName]) {
                _callbacks[eventName] = [];
            }

            _callbacks[eventName].push(callback);
        };

        var off = function (eventName, callback) {
            var callbacks = _callbacks[eventName];
            if (!callbacks) {
                return;
            }

            var index = -1;
            for (var i = 0; i < callbacks.length; i++) {
                if (callbacks[i] === callback) {
                    index = i;
                    break;
                }
            }

            if (index < 0) {
                return;
            }

            _callbacks[eventName].splice(index, 1);
        };

        var trigger = function (eventName) {
            var callbacks = _callbacks[eventName];
            if (!callbacks || !callbacks.length) {
                return;
            }

            var args = Array.prototype.slice.call(arguments, 1);
            for (var i = 0; i < callbacks.length; i++) {
                callbacks[i].apply(this, args);
            }
        };

        // Public interface ///////////////////////////////////////////////////

        return {
            on: on,
            off: off,
            trigger: trigger
        };
    })();


    /* UTILS ***************************************************/

    jellog.utils = jellog.utils || {};

    /* Creates a name namespace.
    *  Example:
    *  var taskService = jellog.utils.createNamespace(jellog, 'services.task');
    *  taskService will be equal to jellog.services.task
    *  first argument (root) must be defined first
    ************************************************************/
    jellog.utils.createNamespace = function (root, ns) {
        var parts = ns.split('.');
        for (var i = 0; i < parts.length; i++) {
            if (typeof root[parts[i]] == 'undefined') {
                root[parts[i]] = {};
            }

            root = root[parts[i]];
        }

        return root;
    };

    /* Find and replaces a string (search) to another string (replacement) in
    *  given string (str).
    *  Example:
    *  jellog.utils.replaceAll('This is a test string', 'is', 'X') = 'ThX X a test string'
    ************************************************************/
    jellog.utils.replaceAll = function (str, search, replacement) {
        var fix = search.replace(/[.*+?^${}()|[\]\\]/g, "\\$&");
        return str.replace(new RegExp(fix, 'g'), replacement);
    };

    /* Formats a string just like string.format in C#.
    *  Example:
    *  jellog.utils.formatString('Hello {0}','Tuana') = 'Hello Tuana'
    ************************************************************/
    jellog.utils.formatString = function () {
        if (arguments.length < 1) {
            return null;
        }

        var str = arguments[0];

        for (var i = 1; i < arguments.length; i++) {
            var placeHolder = '{' + (i - 1) + '}';
            str = jellog.utils.replaceAll(str, placeHolder, arguments[i]);
        }

        return str;
    };

    jellog.utils.toPascalCase = function (str) {
        if (!str || !str.length) {
            return str;
        }

        if (str.length === 1) {
            return str.charAt(0).toUpperCase();
        }

        return str.charAt(0).toUpperCase() + str.substr(1);
    }

    jellog.utils.toCamelCase = function (str) {
        if (!str || !str.length) {
            return str;
        }

        if (str.length === 1) {
            return str.charAt(0).toLowerCase();
        }

        return str.charAt(0).toLowerCase() + str.substr(1);
    }

    jellog.utils.truncateString = function (str, maxLength) {
        if (!str || !str.length || str.length <= maxLength) {
            return str;
        }

        return str.substr(0, maxLength);
    };

    jellog.utils.truncateStringWithPostfix = function (str, maxLength, postfix) {
        postfix = postfix || '...';

        if (!str || !str.length || str.length <= maxLength) {
            return str;
        }

        if (maxLength <= postfix.length) {
            return postfix.substr(0, maxLength);
        }

        return str.substr(0, maxLength - postfix.length) + postfix;
    };

    jellog.utils.isFunction = function (obj) {
        return !!(obj && obj.constructor && obj.call && obj.apply);
    };

    /**
     * parameterInfos should be an array of { name, value } objects
     * where name is query string parameter name and value is it's value.
     * includeQuestionMark is true by default.
     */
    jellog.utils.buildQueryString = function (parameterInfos, includeQuestionMark) {
        if (includeQuestionMark === undefined) {
            includeQuestionMark = true;
        }

        var qs = '';

        function addSeperator() {
            if (!qs.length) {
                if (includeQuestionMark) {
                    qs = qs + '?';
                }
            } else {
                qs = qs + '&';
            }
        }

        for (var i = 0; i < parameterInfos.length; ++i) {
            var parameterInfo = parameterInfos[i];
            if (parameterInfo.value === undefined) {
                continue;
            }

            if (parameterInfo.value === null) {
                parameterInfo.value = '';
            }

            addSeperator();

            if (parameterInfo.value.toJSON && typeof parameterInfo.value.toJSON === "function") {
                qs = qs + parameterInfo.name + '=' + encodeURIComponent(parameterInfo.value.toJSON());
            } else if (Array.isArray(parameterInfo.value) && parameterInfo.value.length) {
                for (var j = 0; j < parameterInfo.value.length; j++) {
                    if (j > 0) {
                        addSeperator();
                    }

                    qs = qs + parameterInfo.name + '[' + j + ']=' + encodeURIComponent(parameterInfo.value[j]);
                }
            } else {
                qs = qs + parameterInfo.name + '=' + encodeURIComponent(parameterInfo.value);
            }
        }

        return qs;
    }

    /**
     * Sets a cookie value for given key.
     * This is a simple implementation created to be used by JELLOG.
     * Please use a complete cookie library if you need.
     * @param {string} key
     * @param {string} value
     * @param {Date} expireDate (optional). If not specified the cookie will expire at the end of session.
     * @param {string} path (optional)
     */
    jellog.utils.setCookieValue = function (key, value, expireDate, path) {
        var cookieValue = encodeURIComponent(key) + '=';

        if (value) {
            cookieValue = cookieValue + encodeURIComponent(value);
        }

        if (expireDate) {
            cookieValue = cookieValue + "; expires=" + expireDate.toUTCString();
        }

        if (path) {
            cookieValue = cookieValue + "; path=" + path;
        }

        document.cookie = cookieValue;
    };

    /**
     * Gets a cookie with given key.
     * This is a simple implementation created to be used by JELLOG.
     * Please use a complete cookie library if you need.
     * @param {string} key
     * @returns {string} Cookie value or null
     */
    jellog.utils.getCookieValue = function (key) {
        var equalities = document.cookie.split('; ');
        for (var i = 0; i < equalities.length; i++) {
            if (!equalities[i]) {
                continue;
            }

            var splitted = equalities[i].split('=');
            if (splitted.length != 2) {
                continue;
            }

            if (decodeURIComponent(splitted[0]) === key) {
                return decodeURIComponent(splitted[1] || '');
            }
        }

        return null;
    };

    /**
     * Deletes cookie for given key.
     * This is a simple implementation created to be used by JELLOG.
     * Please use a complete cookie library if you need.
     * @param {string} key
     * @param {string} path (optional)
     */
    jellog.utils.deleteCookie = function (key, path) {
        var cookieValue = encodeURIComponent(key) + '=';

        cookieValue = cookieValue + "; expires=" + (new Date(new Date().getTime() - 86400000)).toUTCString();

        if (path) {
            cookieValue = cookieValue + "; path=" + path;
        }

        document.cookie = cookieValue;
    }

    /**
     * Escape HTML to help prevent XSS attacks. 
     */
    jellog.utils.htmlEscape = function (html) {
        return typeof html === 'string' ? html.replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;').replace(/"/g, '&quot;') : html;
    }

    /* SECURITY ***************************************/
    jellog.security = jellog.security || {};
    jellog.security.antiForgery = jellog.security.antiForgery || {};

    jellog.security.antiForgery.tokenCookieName = 'XSRF-TOKEN';
    jellog.security.antiForgery.tokenHeaderName = 'RequestVerificationToken';

    jellog.security.antiForgery.getToken = function () {
        return jellog.utils.getCookieValue(jellog.security.antiForgery.tokenCookieName);
    };

    /* CLOCK *****************************************/
    jellog.clock = jellog.clock || {};

    jellog.clock.kind = 'Unspecified';

    jellog.clock.supportsMultipleTimezone = function () {
        return jellog.clock.kind === 'Utc';
    };

    var toLocal = function (date) {
        return new Date(
            date.getFullYear(),
            date.getMonth(),
            date.getDate(),
            date.getHours(),
            date.getMinutes(),
            date.getSeconds(),
            date.getMilliseconds()
        );
    };

    var toUtc = function (date) {
        return Date.UTC(
            date.getUTCFullYear(),
            date.getUTCMonth(),
            date.getUTCDate(),
            date.getUTCHours(),
            date.getUTCMinutes(),
            date.getUTCSeconds(),
            date.getUTCMilliseconds()
        );
    };

    jellog.clock.now = function () {
        if (jellog.clock.kind === 'Utc') {
            return toUtc(new Date());
        }
        return new Date();
    };

    jellog.clock.normalize = function (date) {
        var kind = jellog.clock.kind;

        if (kind === 'Unspecified') {
            return date;
        }

        if (kind === 'Local') {
            return toLocal(date);
        }

        if (kind === 'Utc') {
            return toUtc(date);
        }
    };
    
    /* FEATURES *************************************************/

    jellog.features = jellog.features || {};

    jellog.features.values = jellog.features.values || {};

    jellog.features.isEnabled = function(name){
        var value = jellog.features.get(name);
        return value == 'true' || value == 'True';
    }

    jellog.features.get = function (name) {
        return jellog.features.values[name];
    };
    
    /* GLOBAL FEATURES *************************************************/

    jellog.globalFeatures = jellog.globalFeatures || {};

    jellog.globalFeatures.enabledFeatures = jellog.globalFeatures.enabledFeatures || [];

    jellog.globalFeatures.isEnabled = function(name){
        return jellog.globalFeatures.enabledFeatures.indexOf(name) != -1;
    }

})();
