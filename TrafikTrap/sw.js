importScripts('/serviceworker-cache-polyfill.js');

this.addEventListener('install', function(event) {

  event.waitUntil(
    caches.open('trafic-v1').then(function(cache) {
      return cache.addAll([
        '/',
        '/Default.aspx',
        '/fallback.html',
        '/script.js',
        '/styles.css',
        '/svg/done.svg',
        '/svg/room.svg',
        '/svg/red-room.svg',
        '/svg/volume-mute.svg',
        '/svg/volume-up.svg',
        '/svg/gps-off.svg',
        '/svg/signal-wifi-off.svg',
        '/bower_components/google-map/google-map.html',
        '/bower_components/polymer/polymer.html',
        '/bower_components/polymer/layout.html',
        '/bower_components/polymer/polymer.js',
        '/bower_components/google-apis/google-maps-api.html',
        '/bower_components/core-shared-lib/core-shared-lib.html',
        '//http://fonts.googleapis.com/css?family=Roboto'
      ]);
    })
  );

});

this.addEventListener('fetch', function(event) {

  event.respondWith(
    caches.match(event.request).then(function(response) {
      if (response) {
        return response;
      }

      var fetchRequest = event.request.clone();

      return fetch(fetchRequest).catch(function() {
        if (/\.svg$/.test(event.request.url)) {
          return caches.match("/svg/no-img.svg");
        }
        else if (/\.html$/.test(event.request.url)) {
          return caches.match("/fallback.html");
        }
      })
    })
  );

});