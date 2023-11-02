mergeInto(LibraryManager.library, {

  SendMessageToWeb: function (str) {
      window.unityMessageHandlers.receiveMessageFromUnity(UTF8ToString(str));
  }

});