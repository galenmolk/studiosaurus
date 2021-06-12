var FileUploaderPlugin = {
  FileUploaderCaptureClick: function(objectName) {
  objectName = Pointer_stringify(objectName);
    if (!document.getElementById(objectName.concat('FileUploaderInput'))) {
      var fileInput = document.createElement('input');
      fileInput.setAttribute('type', 'file');
      fileInput.setAttribute('id', objectName.concat('FileUploaderInput'));
      fileInput.style.visibility = 'hidden';
      fileInput.onclick = function (event) {
        this.value = null;
      };
      fileInput.onchange = function (event) {
        SendMessage(objectName, 'FileSelected', URL.createObjectURL(event.target.files[0]));
      }
      document.body.appendChild(fileInput);
    }
    var OpenFileDialog = function() {
      document.getElementById(objectName.concat('FileUploaderInput')).click();
      document.getElementById('unity-canvas').removeEventListener('click', OpenFileDialog);
    };
    document.getElementById('unity-canvas').addEventListener('click', OpenFileDialog, false);
  }
};
mergeInto(LibraryManager.library, FileUploaderPlugin);