var mongoose = require('mongoose');
var Schema = mongoose.Schema;

var TaskSchema = new Schema({
  name: {
    type: String,
    required: [true, 'Kindly enter the name of the task']
  }
});
var TaskSchema1 = new Schema({
  name: {
    type: String,
    required: [true, 'Kindly enter the name of the task']
  },
  age:{type:String},
  address:{type:String}
});

var author = new Schema({
  name:String,
  stories:[{type:Schema.Types.ObjectId,ref:'Story'}]
});

var story = new Schema({
  title:String,
  author:{type:Schema.Types.ObjectId,ref:'Author'}
});

module.exports = mongoose.model('Tasks', TaskSchema);
module.exports = mongoose.model('Tasks1', TaskSchema1);
module.exports = mongoose.model('Author', author);
module.exports = mongoose.model('Story', story);




