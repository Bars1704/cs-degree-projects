
(function(window) {
  var bye = new Object();
  var speakWord = "Goodbye";
  bye.speak = function speak(name) {
      console.log(speakWord + " " + name);
  };
  window.bye = bye;
})(window);