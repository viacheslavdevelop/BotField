mergeInto(LibraryManager.library,
{
  GetLanguage: function () {
    YaGames.init().then(ysdk => {
      console.log("GetLanguage");

      var lang = ysdk.environment.i18n.lang;

      MyGameInstance.SendMessage('PlayerInitializer', 'SetLanguage', lang);
    });
  },

  GetPlatform: function (){
    YaGames.init().then(ysdk => {
      console.log("GetPlatform");
    
      var platform = ysdk.deviceInfo.type;

      MyGameInstance.SendMessage('PlayerInitializer', 'SetPlatform', platform);
    });
  },

  ShowAdv: function (){
    ysdk.adv.showFullscreenAdv({
      callbacks: {
          onClose: function(wasShown) {
            MyGameInstance.SendMessage('AdManager', 'StopAd');
            console.log("Good");
          },
          onError: function(error) {
            MyGameInstance.SendMessage('AdManager', 'StopAd');
            console.log("Err");
          }
      }
    })
  },

  GameReady: function () {
    YaGames.init().then(ysdk => {
      ysdk.features.LoadingAPI.ready();
      console.log("Game Ready");
    });
  },

  GameStarted: function () {
    ysdk.features.GameplayAPI.start();
    console.log("Game Started");
  },

  GameStoped: function () {
    ysdk.features.GameplayAPI.stop();
    console.log("Game Stoped");
  },
});