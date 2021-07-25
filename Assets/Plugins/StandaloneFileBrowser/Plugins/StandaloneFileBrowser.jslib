var StandaloneFileBrowserWebGLPlugin = {
    // Open file.
    // gameObjectNamePtr: Unique GameObject name. Required for calling back unity with SendMessage.
    // methodNamePtr: Callback method name on given GameObject.
    // filter: Filter files. Example filters:
    //     Match all image files: "image/*"
    //     Match all video files: "video/*"
    //     Match all audio files: "audio/*"
    //     Custom: ".plist, .xml, .yaml"
    // multiselect: Allows multiple file selection
    UploadFile: function(gameObjectNamePtr, methodNamePtr, filterPtr, multiselect) {
        gameObjectName = Pointer_stringify(gameObjectNamePtr);
        console.log("gameObjectName: ");
        console.log(gameObjectName);
        methodName = Pointer_stringify(methodNamePtr);
        console.log("methodName: ");
        console.log(methodName);
        filter = Pointer_stringify(filterPtr);
        console.log("filter: ");
        console.log(filter);
        // Delete if element exist
        var fileInput = document.getElementById(gameObjectName)
        if (fileInput) {
             console.log("deleting fileInput element");
            document.body.removeChild(fileInput);
        }

        fileInput = document.createElement('input');
        fileInput.setAttribute('id', gameObjectName);
        fileInput.setAttribute('type', 'file');
        fileInput.setAttribute('style','display:none;');
        fileInput.setAttribute('style','visibility:hidden;');
        if (multiselect) {
            console.log("multiselect is on");

            fileInput.setAttribute('multiple', '');
        }
        if (filter) {
            console.log("adding accept filter");
            fileInput.setAttribute('accept', filter);
        }
        fileInput.onclick = function (event) {
            // File dialog opened
            console.log("file dialog opened");
            this.value = null;
        };
        fileInput.onchange = function (event) {
            // multiselect works
            var urls = [];
            for (var i = 0; i < event.target.files.length; i++) {
                urls.push(URL.createObjectURL(event.target.files[i]));
                console.log("file");
                console.log(i);
                console.log(urls[i]);
            }
            // File selected
            SendMessage(gameObjectName, methodName, urls.join());

            // Remove after file selected
            document.body.removeChild(fileInput);
        }
        document.body.appendChild(fileInput);

        document.onmouseup = function() {
            fileInput.click();
            document.onmouseup = null;
        }
    },

    // Save file
    // DownloadFile method does not open SaveFileDialog like standalone builds, its just allows user to download file
    // gameObjectNamePtr: Unique GameObject name. Required for calling back unity with SendMessage.
    // methodNamePtr: Callback method name on given GameObject.
    // filenamePtr: Filename with extension
    // byteArray: byte[]
    // byteArraySize: byte[].Length
    DownloadFile: function(gameObjectNamePtr, methodNamePtr, filenamePtr, byteArray, byteArraySize) {
        gameObjectName = Pointer_stringify(gameObjectNamePtr);
        methodName = Pointer_stringify(methodNamePtr);
        filename = Pointer_stringify(filenamePtr);

        var bytes = new Uint8Array(byteArraySize);
        for (var i = 0; i < byteArraySize; i++) {
            bytes[i] = HEAPU8[byteArray + i];
        }

        var downloader = window.document.createElement('a');
        downloader.setAttribute('id', gameObjectName);
        downloader.href = window.URL.createObjectURL(new Blob([bytes], { type: 'application/octet-stream' }));
        downloader.download = filename;
        document.body.appendChild(downloader);

        document.onmouseup = function() {
            downloader.click();
            document.body.removeChild(downloader);
        	document.onmouseup = null;

            SendMessage(gameObjectName, methodName);
        }
    }
};

mergeInto(LibraryManager.library, StandaloneFileBrowserWebGLPlugin);