const { request } = require('express');

var mongoose = require('mongoose'),
    Task = mongoose.model('Tasks'),
    Task1 = mongoose.model('Tasks1'),
    Author = mongoose.model('Author'),    
    Story = mongoose.model('Story');
//upload
exports.upload = function (req, res) {
    let path = './public/images' + "\\" + req.files.upload.name;
    console.log(req.files.upload.name);
    req.files.upload.mv(path, (err) => {
      if(!err){
        console.log("SUCCESS");
      } else {
        console.log(err);
      }
      res.status(200).json({ url: req.files.upload.name });
    });
  };

//get all
exports.list_all_tasks = function (req, res) {
    Task.find({}, (loi, Task) => {
        if (loi) {
            res.send(loi);
        }
        res.send(Task);
    });
};

exports.create_a_task = function (req, res) {
    var task = new Task(req.body);
    task.save((loi, task) => {
        if (loi) {
            res.send(loi);
        }
        res.send(task);
    });
};
//task2
exports.create_a_task1 = function (req, res) {
    var task = new Task1(req.body);
    task.save((loi, task) => {
        if (loi) {
            res.send(loi);
        }
        res.send(task);
    });
};
exports.tim_kiem_tasks1 = function (req, res) {
    var tuKhoaN = req.body.tkn;
    var tuKhoat = req.body.tkt;
    Task1.find({$or:[
        {name:  {$regex: tuKhoaN}},
        {age: {$gt:tuKhoat}}
    ]}).exec((err, data) => {
        if (err) { res.send(err); }
        res.json(data);
    });
};
exports.tim_kiem_tasks1_2 = function (req, res) {
    var tuKhoaN = req.body.tkn;
    var tuKhoaAdd = req.body.tka;
    Task1.find({
        name:  {$regex: tuKhoaN},
        address:  {$regex:tuKhoaAdd}
    }).exec((err, data) => {
        if (err) { res.send(err); }
        res.json(data);
    });
};
//
exports.phan_trang = async function (req, res) {
    let index = req.body.index;
    let limit = req.body.limit;
    let skip = (index - 1) * limit;
    let a,kq;
    await Task.find().exec((err, data) => {
        if (err) { res.send(err); }
        a=data.length;
        
    });

    kq= Math.ceil(a/limit);
    Task.find().skip(skip).limit(limit).exec((err, data) => {
        if (err) { res.send(err); }
        //res.json({"total":Math.ceil(a/limit),"du lieu": data});
        res.json({"total":kq,"du lieu": data});

    });
};
exports.tim_kiem = function (req, res) {
    var tuKhoa = req.body.tk;

    Task.find({ name: { $regex: tuKhoa, $options: 'i' } }).exec((err, data) => {
        if (err) { res.send(err); }
        res.json(data);
    });
};
exports.tim_kiem_va_phan_trang = function (req, res) {
    var tuKhoa = req.body.tk;
    var index = req.body.skip;
    var limit = req.body.limit;
    var skip = (index - 1) * limit;
    var a;
    Task.find({ name: { $regex: tuKhoa, $options: 'i' } }).exec((err, data) => {
        if (err) { res.send(err); }
        a = data.length;
    });
    Task.find({ name: { $regex: tuKhoa, $options: 'i' } }).skip(skip).limit(limit).exec((err, data) => {
        if (err) { res.send(err); }
        res.json({"total":Math.ceil(a/limit),"du lieu": data});
    });
};

exports.read_a_task = function (req, res) {
    res.send({ content: 'Đọc một task theo ID ' });
};

exports.update_a_task = function (req, res) {
    res.send({ content: 'Cập nhật task ' });
};

exports.delete_a_task = function (req, res) {
    res.send({ content: 'Xóa task ' });
};


exports.demoquery = function (req, res) {
    res.send({ id: req.query.id, name: req.query.name });
};
exports.demobody = function (req, res) {
    var dulieu1 = req.body;
    var dulieu2 = {
        id: req.body.id,
        name: req.body.name
    }
    res.send({
        dulieu1: dulieu1, 
        dulieu2: dulieu2
    });
};
exports.timkiem = function (req, res) {
    var task = new Task(req.body);
    Task.findById(task._id, (loi, dulieu) => {
        if (loi) {
            res.send(loi);
        }
        if (dulieu != null) {
            res.send(dulieu);
        }
        else
            res.send("khong tim thay!");
    });
};
exports.timsua = function (req, res) {
    var task = new Task(req.body);
    Task.findById(task._id, (loi, dulieu) => {
        if (loi) {
            res.send(loi);
        }
        if (dulieu != null) {
            Task.findByIdAndUpdate(task._id, task, (loi) => {
                if (loi) {
                    res.send(loi);
                }
                res.send("sua thanh cong!");
            });
        }
        else
            res.send("khong tim thay!");
    });
};
exports.timxoa = function (req, res) {
    var task = new Task(req.body);
    Task.findById(task._id, (loi, dulieu) => {
        if (loi) {
            res, send(loi);
        }
        if (dulieu != null) {
            Task.findByIdAndDelete(task._id, (loi) => {
                if (loi) {
                    res.send(loi);
                }
                res.send("xoa thanh cong!");
            });
        }
        else
            res.send("khong tim thay!");
    });
};

//author and story
exports.create_a_author = function (req, res) {
    var au = new Author(req.body);
    au.save((loi, data) => {
        if (loi) {
            res.send(loi);
        }
        res.send(data);
    });
};
exports.create_a_story = function (req, res) {
    var st = new Story (req.body);
    st.save((loi, data) => {
        if (loi) {
            res.send(loi);
        }
        res.send(data);
    });
};
exports.list_all_tasks = function (req, res) {
    Author.find({}, (loi, Task) => {
        if (loi) {
            res.send(loi);
        }
        res.send(Task);
    });
};
exports.list_all_tasks = function (req, res) {
    Story   .find({}, (loi, Task) => {
        if (loi) {
            res.send(loi);
        }
        res.send(Task);
    });
};