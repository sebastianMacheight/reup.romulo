mergeInto(LibraryManager.library, {

  SendMessageToWeb: function (str) {
    var data = { type: "messageFromUnity", message: UTF8ToString(str) };
    window.postMessage(data, 'http://localhost:4200');
  }

});