"use strict";function asyncGeneratorStep(e,t,n,r,a,o,i){try{var c=e[o](i),u=c.value}catch(e){return void n(e)}c.done?t(u):Promise.resolve(u).then(r,a)}function _asyncToGenerator(c){return function(){var e=this,i=arguments;return new Promise(function(t,n){var r=c.apply(e,i);function a(e){asyncGeneratorStep(r,t,n,a,o,"next",e)}function o(e){asyncGeneratorStep(r,t,n,a,o,"throw",e)}a(void 0)})}}function _createForOfIteratorHelper(e,t){var n,r="undefined"!=typeof Symbol&&e[Symbol.iterator]||e["@@iterator"];if(!r){if(Array.isArray(e)||(r=_unsupportedIterableToArray(e))||t&&e&&"number"==typeof e.length)return r&&(e=r),n=0,{s:t=function(){},n:function(){return n>=e.length?{done:!0}:{done:!1,value:e[n++]}},e:function(e){throw e},f:t};throw new TypeError("Invalid attempt to iterate non-iterable instance.\nIn order to be iterable, non-array objects must have a [Symbol.iterator]() method.")}var a,o=!0,i=!1;return{s:function(){r=r.call(e)},n:function(){var e=r.next();return o=e.done,e},e:function(e){i=!0,a=e},f:function(){try{o||null==r.return||r.return()}finally{if(i)throw a}}}}function _unsupportedIterableToArray(e,t){if(e){if("string"==typeof e)return _arrayLikeToArray(e,t);var n=Object.prototype.toString.call(e).slice(8,-1);return"Map"===(n="Object"===n&&e.constructor?e.constructor.name:n)||"Set"===n?Array.from(e):"Arguments"===n||/^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(n)?_arrayLikeToArray(e,t):void 0}}function _arrayLikeToArray(e,t){(null==t||t>e.length)&&(t=e.length);for(var n=0,r=new Array(t);n<t;n++)r[n]=e[n];return r}function _classCallCheck(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}function _defineProperties(e,t){for(var n=0;n<t.length;n++){var r=t[n];r.enumerable=r.enumerable||!1,r.configurable=!0,"value"in r&&(r.writable=!0),Object.defineProperty(e,r.key,r)}}function _createClass(e,t,n){return t&&_defineProperties(e.prototype,t),n&&_defineProperties(e,n),Object.defineProperty(e,"prototype",{writable:!1}),e}var Game=function(){var i={game:null};return{init:function(e,t,n,r){i.game={id:e},console.log("game init"),Game.API.init({id:e,player:t},"https://localhost:5000/"),Game.Data.init(),Game.Model.init(),Game.Reversi.init({id:e}),Game.Stats.init(),Game.Template.init(),Game.Reversi.update();for(var a="",o=0;o<64;o++)a+="<div class='field'><div class='fiche '></div></div>";document.getElementById("board").innerHTML=a,$(".field").on("click",function(e){for(var t=0,t=0;t<board.children.length&&document.getElementById("board").children[t].children[0]!=e.currentTarget.children[0];t++);Game.API.place(t%8,Math.floor(t/8))}),$("#skipBtn").on("click",function(){Game.API.get("api/spel/pas?id=".concat(i.game.id)).then(function(e){return console.log(e)})}),$("#giveupBtn").on("click",function(){Game.API.get("api/spel/opgeven?id=".concat(i.game.id,"&speler=").concat(t))}),$("#Color").text("Je bent ".concat(r,".")),n()}}}(),FeedbackWidget=function(){function t(e){_classCallCheck(this,t),this._elementId=e}return _createClass(t,[{key:"elementId",get:function(){return this._elementId}},{key:"show",value:function(e,t){var n=document.getElementById(this.elementId),n=(n.style.display="inline-block",n.classList.remove("move-out"),n.classList.add("move-in"),"#".concat(this.elementId));$(n).children("feedback-text").text(e),"succes"===t?$(n).addClass("alert-success"):"danger"===t&&$(n).addClass("alert-danger")}},{key:"hide",value:function(){var e=document.getElementById(this.elementId);e.classList.remove("move-in"),e.classList.add("move-out"),setTimeout(function(){e.style.display="none"},1e3)}},{key:"log",value:function(e){var t=localStorage.getItem(this.toString());this.removeLog(),(t=null==(t=JSON.parse(t))?[]:t).push(e),11===t.length&&t.shift(),localStorage.setItem(this.toString(),JSON.stringify(t))}},{key:"history",value:function(){var e,t=localStorage.getItem(this.toString()),n="",r=_createForOfIteratorHelper(t=null==(t=JSON.parse(t))?[]:t);try{for(r.s();!(e=r.n()).done;){var a=e.value;n+="<type |".concat(a.type,"|> - ").concat(a.message)}}catch(e){r.e(e)}finally{r.f()}this.show(n,"succes")}},{key:"removeLog",value:function(){localStorage.removeItem("".concat(this))}}]),t}();Game.API=function(){var r={game:null,baseurl:""};function e(){return(e=_asyncToGenerator(regeneratorRuntime.mark(function e(){return regeneratorRuntime.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:return e.abrupt("return",Game.Data.getNumberFact());case 1:case"end":return e.stop()}},e)}))).apply(this,arguments)}function a(e){return $.get(r.baseurl+e)}function n(){return(n=_asyncToGenerator(regeneratorRuntime.mark(function e(t,n){return regeneratorRuntime.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:return e.next=2,a("api/spel/zet?id=".concat(r.game.id,"&rijZet=").concat(n,"&kolomZet=").concat(t,"&spelerId=").concat(r.game.player)).then(function(e){Game.Reversi.update()},function(e){console.log(e.responseText)});case 2:case"end":return e.stop()}},e)}))).apply(this,arguments)}return{init:function(e,t){r.game=e,r.baseurl=t,console.log("API init")},getNumberFact:function(){return e.apply(this,arguments)},place:function(e,t){return n.apply(this,arguments)},get:a}}(),Game.Data=function(){var e={environment:"development",gameState:0};function t(){return(t=_asyncToGenerator(regeneratorRuntime.mark(function e(){return regeneratorRuntime.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:return e.next=2,$.get("https://meowfacts.herokuapp.com/");case 2:return e.abrupt("return",e.sent);case 3:case"end":return e.stop()}},e)}))).apply(this,arguments)}return{init:function(){setInterval(function(){e.gameState=Game.Model.getGameState(),Game.Reversi.update()},1e3),console.log("data init")},getNumberFact:function(){return t.apply(this,arguments)}}}(),Game.Model={init:function(){console.log("model init")},getGameState:function(){return"Paused"}},Game.Reversi=function(){var l={dogFactTemplate:"api.dogFact",turn:"black",game:null,finished:!1,boardData:[]};function e(){return(e=_asyncToGenerator(regeneratorRuntime.mark(function e(){var t;return regeneratorRuntime.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:return e.next=2,Game.API.getNumberFact();case 2:t=e.sent,$("#dogLbl").text(t.data[0]);case 4:case"end":return e.stop()}},e)}))).apply(this,arguments)}function n(e,t){7<e||7<t||(e=e+8*t,document.getElementById("board").children[e].children[0].className="fiche ".concat(l.turn))}function t(){return(t=_asyncToGenerator(regeneratorRuntime.mark(function e(){var t,n,r,a,o,i,c,u;return regeneratorRuntime.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:if(l.finished)return e.abrupt("return");e.next=2;break;case 2:return e.next=4,Game.API.get("api/spel/".concat(l.game.id));case 4:for(t=e.sent,$("#tegenstanderLbl").text(""==t.speler2Token?"Wachten...":"Aanwezig"),0!==t.winnaar?l.finished||(l.finished=!0,$("#btnDiv").prepend("<button class='btn' id='leaveBtn'>Spel verlaten</button>"),$("#leaveBtn").on("click",function(){window.location="".concat(window.location.origin,"/Endgame?id=").concat(t.id)}),$("#skipBtn").remove(),$("#giveupBtn").remove(),$("#Turn").text("Het spel is gewonnen door ".concat(s(t.winnaar),"!"))):$("#Turn").text("De beurt is aan ".concat(s(t.aandeBeurt),".")),n=0;n<t.bord.length;n++)l.boardData[n]=parseInt(t.bord[n],10);for(r=document.getElementById("board"),(a=[]).zwart=0,a.wit=0,o=0;o<l.boardData.length;o++)i=r.children[o].children[0],c=i.className.split(" ")[1],u=s(l.boardData[o]),i.className="fiche "+u,c!==u&&a[u]++;0!==a.zwart&&Game.Stats.sla("zwart",a.zwart),0!==a.wit&&Game.Stats.sla("wit",a.wit),Game.Stats.update(l.boardData);case 16:case"end":return e.stop()}},e)}))).apply(this,arguments)}function s(e){switch(e){default:return"";case 1:return"wit";case 2:return"zwart"}}return{init:function(e){console.log("reversi init"),l.game=e},updateNumberFact:function(){return e.apply(this,arguments)},show:n,place:function(e,t){"fiche "==document.getElementById("board").children[e+8*t].children[0].className&&(n(e,t,l.turn),Game.Stats.zet(l.turn),"black"===l.turn?l.turn="white":l.turn="black")},rotate:function(e,t){e+=8*t,(t=document.getElementById("board").children[e].children[0]).className="fiche "+("black"===t.className.split(" ")[1]?"white":"black")},update:function(){return t.apply(this,arguments)}}}(),Game.Stats=function(){var n={chartTemplate:"stats.stats",colourChartData:{zwart:0,wit:0,chart:"colourChart",title:"Verdeling van stukken",type:"doughnut"},conqueredChartData:{zwart:0,wit:0,chart:"conqueredChart",title:"Geslagen stukken",type:"bar"},colourChart:0,conqueredChart:0};function e(e){var t=document.getElementById(e.chart).getContext("2d");return new Chart(t,{type:e.type,data:{labels:["Zwart","Wit"],datasets:[{label:e.title,data:[e.zwart,e.wit],backgroundColor:["rgba(255, 99, 132, 0.2)","rgba(54, 162, 235, 0.2)"],borderColor:["rgba(255, 99, 132, 1)","rgba(54, 162, 235, 1)"],borderWidth:1}]},options:{scales:{y:{beginAtZero:!0}}}})}function r(e,t){e.data.datasets.forEach(function(e){e.data.pop()});var n=[t.zwart,t.wit];e.data.datasets.forEach(function(e,t){return e.data=n}),e.update()}return{init:function(){console.log("stats init"),n.colourChart=e(n.colourChartData),n.conqueredChart=e(n.conqueredChartData)},sla:function(e,t){console.log("".concat(e," slaat ").concat(t," stukken")),"wit"===e?n.conqueredChartData.wit+=t:"zwart"===e&&(n.conqueredChartData.zwart+=t),r(n.conqueredChart,n.conqueredChartData)},update:function(e){n.colourChartData.wit=0;for(var t=n.colourChartData.zwart=0;t<e.length;t++)1==e[t]?n.colourChartData.wit++:2==e[t]&&n.colourChartData.zwart++;r(n.colourChart,n.colourChartData)}}}(),Game.Template=function(){function n(e){var t,n=spa_templates.templates,r=_createForOfIteratorHelper(e.split("."));try{for(r.s();!(t=r.n()).done;)n=n[t.value]}catch(e){r.e(e)}finally{r.f()}return n}return{init:function(){console.log("template init")},getTemplate:n,parseTemplate:function(e,t){return n(e)(t)}}}();