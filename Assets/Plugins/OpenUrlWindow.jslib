    mergeInto(LibraryManager.library, {
     
      OpenUrlWindow: function (objectName) {
        objectName = Pointer_stringify(objectName);
        var pastedText = prompt("Enter Asset URL:", "https://www.google.com/image.png");
        if ((typeof pastedText).normalize() === "string") {
          SendMessage(objectName, "FileSelected", pastedText);
        }
      },
     
    });
