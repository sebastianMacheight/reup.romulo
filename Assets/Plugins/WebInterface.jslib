mergeInto(LibraryManager.library, {

  SendStringToWeb: function (str) {
    const data = { type: "messageFromUnity", message: UTF8ToString(str) };
    globalThis.postMessage(data, 'http://localhost:4200');
    globalThis.postMessage(data, 'https://app-staging-reup.macheight.com/');
    globalThis.postMessage(data, 'https://app-prod-reup.macheight.com/');
  }

});