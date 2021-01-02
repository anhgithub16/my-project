package btl.java;

import java.sql.*;
import java.util.Arrays;
import java.util.logging.Level;
import java.util.logging.Logger;

class DB_MySQL {
    
    private static String DB_NAME = "dienluc"; // Tên Data Base
    
    private static String USER_NAME = "dienluc"; // Tên người dùng
    
    private static String PASS = "Ba123456789"; // Mật khẩu người dùng
    
    private static String PORT = "3306"; // Cổng MySQL
    
    private static Connection conn; 
    
    private static Statement stmt;
    
    private boolean connected = false; // Kiểm tra đã connect chưa
    
    // Thực hiện kết nối với MySQL
    private static boolean connect_MySQL() {
        
        try {
            
            Class.forName("com.mysql.jdbc.Driver");
            conn = DriverManager.getConnection("jdbc:mysql://localhost:" + PORT + "/" + DB_NAME, USER_NAME, PASS);
            stmt = conn.createStatement();
            System.out.println("Kết nối thành công với DataBase !");
            return true;
            
        } catch (SQLException ex) {
            
            System.out.print("ERROR: Lỗi khi kết nối với DataBasE.");
            Logger.getLogger(DB_MySQL.class.getName()).log(Level.SEVERE, null, ex);
            
        } catch (Exception ex) {
            
            System.out.print("ERROR: Có lỗi khi thiết lập Class.forName().");
            
        }
        
        return false;
    }
    
    // Chuyển dữ liệu dạng ResultSet sang mảng dữ liệu 2 chiều kiểu chuỗi
    private static String[][] convertData(ResultSet rs) {
        
        int n = 0;
        int i;
        int row = 0;
        int column = 0;
        
        try {
            
            while(rs.next())
                row = rs.getRow();
            column = rs.getMetaData().getColumnCount();
            
        } catch (SQLException ex) {
            
            System.out.print("ERROR: Có lỗi khi tiến hành lấy số hàng số cột trong dữ liệu trả về.");
            Logger.getLogger(DB_MySQL.class.getName()).log(Level.SEVERE, null, ex);
            
        }
        
        String data[][] = new String[row][column]; // Mảng hai chiều chưa dữ liệu của các bộ dữ liệu
        
        try {
            
            rs.first(); // Đưa con trỏ về hàng đầu
            
            do {
                
                // Thực hiện việc ném dữ liệu của bộ vào trong mảng 2 chiều
                // Hàng trong mảng: Biểu thị cho từng bộ
                // Cột trong mảng: Biểu thị cho từng cột của bộ
                for(i = 0; i < column; i++) {
                    data[n][i] = rs.getString(i + 1);
                }
                ++n;
            } while(rs.next());
            
            return data;
            
        } catch (SQLException ex) {
            
            System.out.print("ERROR: Có lỗi khi tiến hành đọc dữ liệu trả về.");
            Logger.getLogger(DB_MySQL.class.getName()).log(Level.SEVERE, null, ex);
            
        }
        
        return null;
    }
    
    
    // Câu lệnh truy vấn SELECT
    public static String[][] select(String[] columns, String table, String where) {
        
        // Kiểm tra xem đã kết nối với DataBase chưa nếu chưa thì thực hiện connect
        if(conn == null) 
            connect_MySQL();
        
        // Kiểm tra xem đã kết nối với DataBase chưa nếu rồi thì tiền hành thực thi truy vấn
        if(conn != null) {
            
            // Chuyển mảng cột thành chuỗi các cột ngăn nhau bởi dấy ","
            String cl = Arrays.toString(columns);
            cl = cl.substring(1, cl.length() - 1);

            // Tạo câu truy vấn đầy đủ
            String query = "SELECT " + cl +  " FROM " + table + " WHERE " + where;

            try {

                ResultSet rs = stmt.executeQuery(query);
                return convertData(rs);

            } catch (SQLException ex) {

                System.out.print("ERROR: Lỗi khi thực thi câu lệnh SELECT.");
                Logger.getLogger(DB_MySQL.class.getName()).log(Level.SEVERE, null, ex);

            }
            
        } else
            System.out.print("ERROR: Kết nối với DataBase chưa thành công.");
        
        return null;
    }
    
    // Câu lệnh truy vấn INSERT
    public static boolean insert(String table, String[] columns, String[] values) {
        
        // Kiểm tra xem đã kết nối với DataBase chưa nếu chưa thì thực hiện connect
        if(conn == null) 
            connect_MySQL();
        
        // Kiểm tra xem đã kết nối với DataBase chưa nếu rồi thì tiền hành thực thi truy vấn
        if(conn != null) {
            
            int i;
        
            // Chuyển mảng cột thành chuỗi các cột ngăn nhau bởi dấy ","
            String cl = Arrays.toString(columns);
            cl = cl.substring(1, cl.length() - 1);

            // Chuyển mảng giá trị thành chuỗi các giá trị ngăn nhau bởi dấy "," và mỗi giá trị nằm trong dấu ngoặc kép ""
            String vl = "";
            for(i = 0; i < values.length; i++)
                vl = vl + "\"" + values[i] + "\"" + ",";
            vl = vl.substring(0, vl.length() - 1);

            // Tạo câu lệnh truy vấn
            String query = "INSERT INTO " + table + "(" + cl + ")" + " VALUES " + "(" + vl + ")";

            // Thực thi truy vấn
            try {

                stmt.executeUpdate(query);
                return true;

            } catch (SQLException ex) {

                System.out.print("ERROR: Lỗi khi thực thi câu lệnh INSERT.");
                Logger.getLogger(DB_MySQL.class.getName()).log(Level.SEVERE, null, ex);

            }   
            
        } else
            System.out.print("ERROR: Kết nối với DataBase chưa thành công.");
        
        return false;
    }
    
    // Câu lệnh truy vấn UPDATE
    public static boolean update(String table, String[] columns, String[] values, String where) {
        
        // Kiểm tra xem đã kết nối với DataBase chưa nếu chưa thì thực hiện connect
        if(conn == null) 
            connect_MySQL();
        
        // Kiểm tra xem đã kết nối với DataBase chưa nếu rồi thì tiền hành thực thi truy vấn
        if(conn != null) {
            
            int i;
            String vl = "";

            // Chuyển mảng các cột và mảng các giá trị mới thành một chuỗi
            for(i = 0; i < values.length; i++) 
                vl = vl + columns[i] + "=" + "\"" + values[i] + "\"" +", ";
            vl = vl.substring(0, vl.length() - 2);

            // Tạo câu lệnh Query đầy đủ
            String query = "UPDATE " + table + " SET " + vl + " WHERE " + where; 

            // Thực thi truy vấn
            try {

                stmt.executeUpdate(query);
                return true;

            } catch (SQLException ex) {

                System.out.print("ERROR: Lỗi khi thực thi câu lệnh UPDATE.");
                Logger.getLogger(DB_MySQL.class.getName()).log(Level.SEVERE, null, ex);

            }   
            
        } else
            System.out.print("ERROR: Kết nối với DataBase chưa thành công.");
        
        return false;
    }
    
    // Câu lệnh DELETE
    public static boolean delete(String table, String where) {
        
        // Kiểm tra xem đã kết nối với DataBase chưa nếu chưa thì thực hiện connect
        if(conn == null) 
            connect_MySQL();
        
        // Kiểm tra xem đã kết nối với DataBase chưa nếu rồi thì tiền hành thực thi truy vấn
        if(conn != null) {
            
            // Tạo câu truy vấn
            String query = "DELETE FROM " + table + " WHERE " + where;

            // Thực thi truy vấn
            try {

                stmt.executeUpdate(query);
                return true;

            } catch (SQLException ex) {

                System.out.print("ERROR: Lỗi khi thực thi câu lệnh DELETE.");
                Logger.getLogger(DB_MySQL.class.getName()).log(Level.SEVERE, null, ex);

            }  
            
        } else
            System.out.print("ERROR: Kết nối với DataBase chưa thành công.");
        
        return false;
    }
    
    // Đọc chuỗi mảng hai chiều
    public static void getArray_2D(String array[][]) {
        
        int i,j;
        int row = array.length;
        int column = array[0].length;
        
        for(i = 0; i < row; i++) {
            for(j = 0; j < column; j++) {
                System.out.print("[ " + array[i][j] + " ]");
            }
            System.out.print("\n");
        }
        
    }
    
}