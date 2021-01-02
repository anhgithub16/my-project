package btl.java;

import javax.swing.table.DefaultTableModel;

class Display_JTable {
    
    private static int stt = 1;
    
    public static DefaultTableModel defaultTableModel ;
    
//    /*
//    * Biến chứ đối tượng JTable
//    */
//    private static javax.swing.JTable tableName;
    
    /*
    * setModel cho đối tượng JTable được truyền vào
    */
    public static void setTable(javax.swing.JTable tableName) {
        defaultTableModel = new DefaultTableModel();
        tableName.setModel(defaultTableModel);
//        Display_JTable.tableName = tableName;
    }
    
    /*
    * Tạo các tiêu đề cột cho bảng
    */
    public static void setColumn(String columns[]) {
        
        int i;
        int columnNumber = columns.length;
        
        for(i = 0; i < columnNumber; i++) {
            defaultTableModel.addColumn(columns[i]);
        }
       
    }
    
    /*
    * Hiển thị dữ liệu là mảng String 1 chiều ra table
    * và xác định xem table có cột stt không dựa trên
    * tham số truyền vào là stt
    */
    public static void displayTable_Data1D(String data[], boolean stt) {
        
        int i;
        int column = data.length; // Lấy số cột dữ liệu ban đầu
        
        // Kiểm tra xem bảng có cột STT không, nếu có thì tăm số cột dữ liệu thêm 1
        if(stt)
            ++column;
        
        // Tạo mảng 1 chiều String để lưu giá trị của từng cột
        String dataRow[] = new String[column];
        
        // Kiểm tra xem bảng có cột STT không, nếu không thì thêm dữ liệu vào bảng và hiển thị luôn
        if(stt) {
            
            // Thêm dữ liệu của từng cột vào mảng
            for(i = 0; i < column; i++) {
                
                /*
                * Xác định xem nếu là cột đầu tiên ( giá trị đầu tiên của mảng )
                * thì thêm stt, còn nếu không phải thì gán lần lượt giá trị
                * của mảng data truyền vào
                */
                if(i == 0) {
                    dataRow[i] = Integer.toString(Display_JTable.stt);
                    ++Display_JTable.stt;
                } else {
                    dataRow[i] = data[i - 1];
                }
            }
            
            // Tiến hành thêm và hiện thị dữ liệu
            defaultTableModel.addRow(dataRow);
            
            Display_JTable.stt = 1; // Trả lại giá trị ban đầu của stt
        } else
            defaultTableModel.addRow(data);
        
    }
    
    /*
    * Hiển thị dữ liệu là mảng String 1 chiều ra table
    * và xác định xem table có cột stt không dựa trên
    * tham số truyền vào là stt
    */
    public static void displayTable_Data2D(String data[][], boolean stt) {
        
        int row = data.length; // Lấy số lượng hàng dữ liệu
        int column = data[0].length; // Lấy cố lượng cột dữ liệu ban đầu
        int i, j;
        
        // Kiểm tra xem bảng có cột STT không, nếu có thì tăm số cột dữ liệu thêm 1
        if(stt)
            ++column;
        
        // Tạo mảng 1 chiều String để lưu giá trị của từng cột
        String dataRow[] = new String[column];
        
        // Lấy giá trị của từng dòng dữ liệu và hiển thị
        for(i = 0; i < row; i++) {
            
            // Kiểm tra xem bảng có cột STT không, nếu không thì thêm dữ liệu của hàng vào bảng và hiển thị luôn
            if(stt) {
                // Lấy dữ liệu của từng cột gán vào mảng
                for(j = 0; j < column; j++) {

                    // Xét xem nếu là dòng đầu tiên thì thêm stt, còn không thì sẽ thêm lần giá trị của hàng dữ liệu. 
                    if(j == 0) {
                        dataRow[j] = Integer.toString(Display_JTable.stt);
                        ++Display_JTable.stt;
                    }
                    else
                        dataRow[j] = data[i][j-1];
                }
                defaultTableModel.addRow(dataRow);
            } else 
                defaultTableModel.addRow(data[i]);
        }
        Display_JTable.stt = 1;
    }
    
    
}
