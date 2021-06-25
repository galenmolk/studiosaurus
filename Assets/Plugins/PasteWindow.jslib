    mergeInto(LibraryManager.library, {
     
      PasteHereWindow: function (objectName) {
        objectName = Pointer_stringify(objectName);
        var pastedText = prompt("Enter Asset URL:", "https://www.google.com/image.png");
        if (pastedText !== '') {
          SendMessage(objectName, "GetPastedText", pastedText);
        }
      },
     
    });
