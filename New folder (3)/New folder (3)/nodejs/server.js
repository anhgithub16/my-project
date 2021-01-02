var express = require('express'),
    app = express(),
    port = process.env.PORT || 3001,
    mongoose = require("mongoose"),
    Task = require("./api/model/model")
    path = require("path"),
    fileUpload = require("express-fileupload")
    bodyParser = require('body-parser');
app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());
 //public duong dan
 app.use(express.static(path.join(__dirname, 'public/images')));
 //khai bao su dung file upload
 app.use(fileUpload({
     useTempFiles : true,
     tempFileDir : '/tmp/'
   }));

mongoose.Promise = global.Promise;
mongoose.connect('mongodb://localhost:27017/abc',
    { useNewUrlParser: true }).then(() => {
        console.log("Connected !!!")
    }).catch(err => {
        console.log(err);
    });
    
var routes = require('./api/routes/todoListRoutes');
routes(app);

app.use(function (req, res) {
    res.status(404).send({ url: req.originalUrl + 'not found' })
});

app.listen(port);

console.log('todo list RESTful API server started on: !!!!!!!!!!' + port);

//socket
var server = require('http').Server(app);
var io = require('socket.io')(server);

server.listen(3002);
io.on('connection',function(socket){
  io.sockets.emit("user_online", socket.id + ' is connected');
  socket.on('message',function(msg){
    socket.broadcast.emit("re_message", socket.id +": "+ msg);
  });
  socket.on('disconnect',function(msg){
    socket.broadcast.emit("re_message", socket.id +": is disconnected");
  });
});