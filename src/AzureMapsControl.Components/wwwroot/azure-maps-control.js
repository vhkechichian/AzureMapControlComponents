/*
MIT License

Copyright (c) 2021 Arnaud Leclerc

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

(function (exports, azmaps) {
    'use strict';

    var Extensions = /** @class */ (function () {
        function Extensions() {
        }
        return Extensions;
    }());

    var Core = /** @class */ (function () {
        function Core() {
            this._popups = [];
        }
        Core.addMap = function (mapId, configuration, serviceOptions) {
            if (configuration.authType === 'aad') {
                azmaps.setAuthenticationOptions({
                    authType: configuration.authType,
                    aadAppId: configuration.aadAppId,
                    aadTenant: configuration.aadTenant,
                    clientId: configuration.clientId
                });
            }
            else if (configuration.authType === 'subscriptionKey') {
                azmaps.setAuthenticationOptions({
                    authType: configuration.authType,
                    subscriptionKey: configuration.subscriptionKey
                });
            }
            else {
                azmaps.setAuthenticationOptions({
                    authType: configuration.authType,
                    getToken: Extensions.getTokenCallback
                });
            }
            new azmaps.Map(mapId, serviceOptions);
        };
        return Core;
    }());

    exports.Core = Core;

    Object.defineProperty(exports, '__esModule', { value: true });

}(this.amc = this.amc || {}, atlas));
