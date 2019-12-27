(function(){
    var _oldBuild = signalR.HubConnectionBuilder.prototype.build;

    signalR.HubConnectionBuilder.prototype.build = function() {
        var result = _oldBuild.call(this);
        console.log('NEW BUILD',result)

        result._pendingCallbacks = {};

        result.execute = function(route, args) {
            return new Promise(async (resolve, reject) => {
                var request = {
                    route,
                    callbackId: uuidv4(),
                    arguments: args
                }
    
                this._pendingCallbacks[request.callbackId] = {
                    resolve,
                    reject
                }
    
                try {
                    await this.invoke("AppFormExecute", request);
                } catch (e) {
                    delete this._pendingCallbacks[request.callbackId];
                }
            });
        }

        result._handleExecuteRespose = function(response) {
            console.debug("_handleExecuteRespose", response);
    
            var pendingCallback = this._pendingCallbacks[response.callbackId];
            if (!pendingCallback) {
                console.error(`unknown callbackId: ${response.callbackId}`);
                return;
            }
    
            try {
                if (response.error) {
                    pendingCallback.reject(response.error);
                } else {
                    pendingCallback.resolve(response.result);
                }
            }
            catch (e) {
                console.error("Failed to send callback");
                console.error(e);
            }
            finally {
                delete this._pendingCallbacks[response.callbackId];
            }
    
            console.debug('this._pendingCallbacks', this._pendingCallbacks);
        }

        result.on("$AfExecuteResult$", result._handleExecuteRespose);

        return result;
    }
    
    function uuidv4() {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    }
})();
