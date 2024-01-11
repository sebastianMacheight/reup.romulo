mergeInto(LibraryManager.library, {

  SendStringToWeb: function (str) {
    const data = { type: "messageFromUnity", message: UTF8ToString(str) };
    globalThis.postMessage(data, '*');
  }
});