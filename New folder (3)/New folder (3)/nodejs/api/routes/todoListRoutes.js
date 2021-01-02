module.exports = function (app) {
    var todoList = require('../controllers/todoListController');
   

    // todoList Routes
    app.route('/tasks')
        .get(todoList.list_all_tasks)
        .post(todoList.create_a_task)
        
    app.route('/tasks/:taskId')
        .get(todoList.read_a_task)
        .put(todoList.update_a_task)
        .delete(todoList.delete_a_task);

    app.route('/demo-query')
        .get(todoList.demoquery);
    
    app.route('/demo-body')
        .post(todoList.demobody);
    
    app.route('/timkiem')
        .get(todoList.timkiem);

    app.route('/timsua')
        .put(todoList.timsua);

    app.route('/timxoa')
        .delete(todoList.timxoa);
    app.route('/demo6')
        .get(todoList.phan_trang)

    app.route('/demo7')
        .get(todoList.tim_kiem)
    app.route('/demo8')
        .get(todoList.tim_kiem_va_phan_trang)
    app.route('/demo9')
        .post(todoList.create_a_task1)
        .get(todoList.tim_kiem_tasks1);
    app.route('/demo10')
        .get(todoList.create_a_author)
    app.route('/upload-image')
        .post(todoList.upload)
};

