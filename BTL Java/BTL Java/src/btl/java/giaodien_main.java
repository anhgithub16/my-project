/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package btl.java;

import java.sql.SQLException;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Arrays;
import java.util.Calendar;
import java.util.Collections;
import java.util.Date;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.swing.JOptionPane;
import javax.swing.table.DefaultTableModel;

/**
 *
 * @author Ba _Tran
 */

public class giaodien_main extends javax.swing.JFrame {

    /**
     * Creates new form giaodien
     */
    public giaodien_main() {
        initComponents();
    }
    
    private void table_tabQLTienDien(String w) {
        int i, j;
        float indexNew, indexOld; // Dùng cho C1
        
        Display_JTable.setTable(table_QLTienDien);
        Display_JTable.setColumn(new String[] {"STT", "Mã khách hàng", "Họ tên khách hàng", "Số chứng minh thư", "Số điện thoại", "Email", "Địa chỉ", "Phường", "Số công tơ", "Tiền điện (VNĐ)"});
        
        String table = "khachhang, chisodien, datenhapchiso";
        String columns[] = new String[] {"khachhang.idKH", "hoTen", "soCMT", "soDienThoai", "email", "diaChi", "tenPhuong", "soCongTo", "chisodien.chiSoMoi", "chisodien.chiSoCu", "donGia"};
        String where = "(khachhang.idKH = chisodien.idKH AND datenhapchiso.idKH = chisodien.idKH) AND chisodien.chiSoMoi <> '0' AND datenhapchiso.chiSoMoi <> '0000-00-00' " + " AND " + w;
        String data[][] = DB_MySQL.select(columns, table, where);
        
        if(data != null) {
            
            int stt = 0;
            int row = data.length;

            String maKH, hoTen, soCMT, soDienThoai, email, diaChi, tenPhuong, soCongTo;
            float donGia;
            float tienDien = 0;
            
            for(j = 0; j < row; j++) {
                ++stt;
                maKH = data[j][0];
                hoTen = data[j][1];
                soCMT = data[j][2];
                soDienThoai = data[j][3];
                email = data[j][4];
                diaChi = data[j][5];
                tenPhuong = data[j][6];
                soCongTo = data[j][7];
                indexNew = Float.parseFloat(data[j][8]);
                indexOld = Float.parseFloat(data[j][9]);
                donGia = Float.parseFloat(data[j][10]);
                
                if(donGia != 0) {
                        
                    tienDien = (indexNew - indexOld)* donGia;
                    tienDien += tienDien * 0.1;

                } else {

                    float soDienTieuThu = indexNew - indexOld;
                    
                    // Lấy ra mản 2 chiều gồm giá và khoảng số điện theo từng bậc
                    String bacThuTienDien[][] = DB_MySQL.select(new String[] {"gia", "min", "max"}, "bactiendien", "1");

                    int soBac = bacThuTienDien.length;
                    float tienDienTheoBac[] = new float[soBac];
                    float soDienTheoBac[] = new float[soBac];
                    float tienDienTungBac;

                    /*
                    * Tách phần tiền điện theo từng bậc sang mảng 1 chiều
                    * Tách phần khoảng số điện theo từng bậc sang mảng 2 chiều
                    */
                    for(i = 0; i < soBac; i++) {
                        tienDienTheoBac[i] = Float.parseFloat(bacThuTienDien[i][0]);
                        soDienTheoBac[i] = Float.parseFloat(bacThuTienDien[i][2]) - Float.parseFloat(bacThuTienDien[i][1]);
                    }


                    for(i = 0; i < soBac; i++) {

                        if(soDienTieuThu > soDienTheoBac[i]){
                            soDienTieuThu -= soDienTheoBac[i];
                            tienDienTungBac = soDienTheoBac[i] * tienDienTheoBac[i];
                        } else {
                            tienDienTungBac = soDienTieuThu * tienDienTheoBac[i];
                            i = soBac;
                        }

                        tienDien += tienDienTungBac;

                    }

                    tienDien += tienDien * 0.1;

                }
                
                Display_JTable.defaultTableModel.addRow(new String[] {String.valueOf(stt), maKH, hoTen, soCMT, soDienThoai, email, diaChi, tenPhuong, soCongTo, String.valueOf(tienDien)});
            }
            
        } else 
            JOptionPane.showMessageDialog(this, "Không có dữ liệu khách hàng cho bảng Quản Lý Tiền Điện", "Cảnh báo", JOptionPane.WARNING_MESSAGE);
        
    }
    
    private void table_tabQLChiSoDien(String w) {
        int i, j;
        
        float indexNew, indexOld;

        String table = "khachhang, chisodien, datenhapchiso";
        String columns[] = new String[] {"khachhang.idKH", "hoTen", "soCMT", "soDienThoai", "email", "diaChi", "tenPhuong", "soCongTo", "chisodien.chiSoMoi", "chisodien.chiSoCu", "datenhapchiso.chiSoMoi"};
        String where = "(khachhang.idKH = chisodien.idKH AND datenhapchiso.idKH = chisodien.idKH)" + " AND " + w;
        String data[][] = DB_MySQL.select(columns, table, where);
        
        if(data != null) {
            
            Display_JTable.setTable(table_QLChiSoDien);
            Display_JTable.setColumn(new String[] {"STT", "Mã khách hàng", "Họ tên khách hàng", "Số chứng minh thư", "Số điện thoại", "Email", "Địa chỉ", "Phường", "Số công tơ", "Số điệnt tiêu thụ (KW)", "Chỉ số mới (KW)", "Chỉ số cũ (KW)"});
            
            String dataNew[][] = new String[data.length][data[0].length];
            
            for(i = 0; i < data.length; i++) {
                for(j = 0; j < 8; j++)
                    dataNew[i][j] = data[i][j];
                
                if(data[i][8].charAt(0) != '0' && data[i][10] != "0000-00-00") {
                    indexNew = Float.parseFloat(data[i][8]);
                    indexOld = Float.parseFloat(data[i][9]);
                    dataNew[i][8] = String.valueOf(indexNew - indexOld);
                    
                } else {
                    dataNew[i][8] = "Chưa xác định";
                    data[i][8] = "Chưa có";
                }
                
                dataNew[i][9] = data[i][8];
                dataNew[i][10] = data[i][9];
             
            }
            
            Display_JTable.displayTable_Data2D(dataNew, true);
            
        } else 
            JOptionPane.showMessageDialog(this, "Không có dữ liệu khách hàng cho bảng Quản Lý Chỉ Số Điện", "Cảnh báo", JOptionPane.WARNING_MESSAGE);
        
        
    }
    
    private void table_tabQLKhachHang(String where) {
        
        String table = "khachhang";
        String columns[] = new String[] {"idKH", "hoTen", "ngaySinh", "soCMT", "soDienThoai", "email", "diaChi", "tenPhuong", "soCongTo"};
        
        String data[][] = DB_MySQL.select(columns, table, where);
        Display_JTable.setTable(table_QLKhachHang);
        Display_JTable.setColumn(new String[] {"STT", "Mã khách hàng", "Họ tên khách hàng", "Ngày sinh", "Số chứng minh thư", "Số điện thoại", "Email", "Địa chỉ", "Phường", "Số công tơ"});

        if(data != null) 
            Display_JTable.displayTable_Data2D(data, true);
        else 
            JOptionPane.showMessageDialog(this, "Không có dữ liệu khách hàng cho bảng Quản Lý Khách Hàng", "Cảnh báo", JOptionPane.WARNING_MESSAGE);
        
        
    }
    
    private void table_tabQLBacTienDien(String where) {
       
        String table = "bactiendien";
        String columns[] = new String[] {"bac", "gia", "min", "max", "ngaySua", "ngayTao"};
        
        String data[][] = DB_MySQL.select(columns, table, where);
        
        Display_JTable.setTable(table_tabQLBacTienDien);
        Display_JTable.setColumn(new String[] {"Loại bậc", "Giá/1kWh", "Số điện tối thiểu (kWh)", "Số điện tối đa (kWh)", "Ngày sửa", "Ngày tạo"});
        Display_JTable.displayTable_Data2D(data, false);
        
    }
    
    private void showBacTienDien(String where) {
        
        String table = "bactiendien";
        String columns[] = new String[] {"gia", "min", "max", "ngaySua", "ngayTao"};
        
        String data[][] = DB_MySQL.select(columns, table, where);
        
        txt_gia.setText(data[0][0]);
        txt_min.setText(data[0][1]);
        txt_max.setText(data[0][2]);
        label_ngayTao.setText(data[0][3]);
        label_ngaySua.setText(data[0][4]);
        
    }
    
    private void setComboBoxLoaiBacTienDien() {
        
        comboBox_loaiBac.removeAllItems();
        
        String data[][] = DB_MySQL.select(new String[] {"bac"}, "bactiendien", "1");
        
        for(int i = 0; i < data.length; i++) {
            comboBox_loaiBac.addItem("Bậc " + data[i][0]);
        }
        
    }
    
    private void showTabQLTienDien() {
        table_tabQLTienDien("1");
        
        DateFormat df_MM = new SimpleDateFormat("MM");
        DateFormat df_yyyy = new SimpleDateFormat("yyyy");
        Calendar c = Calendar.getInstance();
        Date date = c.getTime();
        label_thangHienTai_tabQLTienDien.setText(df_MM.format(date));
        label_namHienTai_tabQLTienDien.setText(df_yyyy.format(date));
        
        txt_timKiem_tabQLTienDien.setText("");
        comboBox_danhMucTimKiem_tabQLTienDien.setSelectedIndex(0);
        comboBox_phuongCuTru_tabQLTienDien.setSelectedIndex(0);
        
    }
    
    private void showTabQLChiSoDien() {
        table_tabQLChiSoDien("1");
        
        DateFormat df_MM = new SimpleDateFormat("MM");
        DateFormat df_yyyy = new SimpleDateFormat("yyyy");
        Calendar c = Calendar.getInstance();
        Date date = c.getTime();
        label_thangHienTai_tabQLChiSoDien.setText(df_MM.format(date));
        label_namHienTai_tabQLChiSoDien.setText(df_yyyy.format(date));
        
        txt_timKiem_tabQLChiSoDien.setText("");
        comboBox_danhMucTimKiem_tabQLChiSoDien.setSelectedIndex(0);
        comboBox_phuongCuTru_tabQLChiSoDien.setSelectedIndex(0);
    }
    
    
    private void showTabQLKhachHang() {
        
        table_tabQLKhachHang("1");
        
        txt_timKiem_tabQLKhachHang.setText("");
        comboBox_danhMucTimKiem_tabQLKhachHang.setSelectedIndex(0);
        comboBox_phuongCuTru_tabQLKhachHang.setSelectedIndex(0);
        comboBox_sapXep_tabQLKhachHang.setSelectedIndex(0);
        
    }
    
    private void showTabQLBacTienDien() {
        
        setComboBoxLoaiBacTienDien();
        table_tabQLBacTienDien("1");
        comboBox_loaiBac.setSelectedIndex(1);
        comboBox_loaiBac.setSelectedIndex(0);
        
    }
    
    private void displayThongTin_tabBaoCao() {
        
        String thangNam[][] = DB_MySQL.select(new String[] {"thangNam"}, "lichsugiaodich", "1 group by thangNam");
        Integer listNam[] = new Integer[thangNam.length];
        
        for(int i = 0; i < thangNam.length; i++) {
            listNam[i] = Integer.parseInt(thangNam[i][0].substring(0, 4));
        }
        
        int namMax = (int) Collections.max(Arrays.asList(listNam));
        int namMin = (int) Collections.min(Arrays.asList(listNam));
        
        String thang = String.valueOf(comboB_thang_tabBaoCao.getSelectedItem());
        String nam = String.valueOf(comboB_nam_tabBaoCao.getSelectedItem());
        
        String where = "1";
        
        if(thang == "Tất cả" && nam == "Tất cả" || nam == "null") {
            where = "1";
        } else if(nam == "Tất cả") {
            where = "thangNam between '" + namMin + "-" + thang + "-01'" + " and '" + namMax + "-" + thang + "-31'";
        } else if(thang == "Tất cả") {
            where = "thangNam between '" + nam + "-" + "01" + "-01'" + " and '" + nam + "-" + "12" + "-31'";
        } else 
            where = "thangNam between '" + nam + "-" + thang + "-01'" + " and '" + nam + "-" + thang + "-31'";
        
        System.out.print(where);
        
        String soLuongKhachHang = DB_MySQL.select(new String[] {"COUNT(idKH)"}, "khachHang", "1")[0][0];
        String sanLuongDienTieuThu = DB_MySQL.select(new String[] {"SUM(chiSoMoi - chiSoCu)"}, "lichsugiaodich", where)[0][0];
        
        if(sanLuongDienTieuThu != null) {
        
            String doanhThu = DB_MySQL.select(new String[] {"SUM(tienThanhToan)"}, "lichsugiaodich", where)[0][0];

            label_soLuongKhachhang_tabBaoCao.setText(soLuongKhachHang);
            label_sanLuongTieuThu_tabBaoCao.setText(sanLuongDienTieuThu + " kWh ");
            label_doanhThu_tabBaoCao.setText(doanhThu + " VNĐ ");

            String data[][] = DB_MySQL.select(new String[] {"thangNam", "chiSoMoi - chiSoCu", "chiSoMoi", "chiSoCu", "tienThanhToan", "dateThanhToan"}, "lichsugiaodich", where);

            Display_JTable.setTable(table_lichSuKhachHang_tabBaoCao);
            Display_JTable.setColumn(new String[] {"STT", "Điện Tháng/Năm", "Số điện tiêu thụ (kWh)","Chỉ số mới (kWh)", "Chỉ số cũ (kWh)", "Số tiền thanh toán (VNĐ)", "Thời gian thanh toán"});
            Display_JTable.displayTable_Data2D(data, true);
        } else {
            label_soLuongKhachhang_tabBaoCao.setText("...");
            label_sanLuongTieuThu_tabBaoCao.setText("...");
            label_doanhThu_tabBaoCao.setText("...");
            Display_JTable.setTable(table_lichSuKhachHang_tabBaoCao);
            Display_JTable.setColumn(new String[] {"STT", "Điện Tháng/Năm", "Số điện tiêu thụ (kWh)","Chỉ số mới (kWh)", "Chỉ số cũ (kWh)", "Số tiền thanh toán (VNĐ)", "Thời gian thanh toán"});
            JOptionPane.showMessageDialog(this, "Không có dữ liệu thích hợp cho báo cáo !", "Cảnh báo", JOptionPane.WARNING_MESSAGE);
        }
        
    }
    
    private void showTabBaoCao() {
        
        comboB_nam_tabBaoCao.removeAllItems();
        
        comboB_nam_tabBaoCao.addItem("Tất cả");
        
        String thangNam[][] = DB_MySQL.select(new String[] {"thangNam"}, "lichsugiaodich", "1 group by thangNam");
        
        for(int i = 0; i < thangNam.length; i++) {
            comboB_nam_tabBaoCao.addItem(thangNam[i][0].substring(0, 4));
        }
        
        displayThongTin_tabBaoCao();
    }
    
    

    /**
     * This method is called from within the constructor to initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is always
     * regenerated by the Form Editor.
     */
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        tab_QuanLy = new javax.swing.JTabbedPane();
        jPanel3 = new javax.swing.JPanel();
        jLabel1 = new javax.swing.JLabel();
        jSeparator1 = new javax.swing.JSeparator();
        jScrollPane1 = new javax.swing.JScrollPane();
        table_QLTienDien = new javax.swing.JTable();
        jLabel2 = new javax.swing.JLabel();
        label_thangHienTai_tabQLTienDien = new javax.swing.JLabel();
        jLabel4 = new javax.swing.JLabel();
        label_namHienTai_tabQLTienDien = new javax.swing.JLabel();
        jLabel6 = new javax.swing.JLabel();
        jLabel7 = new javax.swing.JLabel();
        txt_timKiem_tabQLTienDien = new javax.swing.JTextField();
        jLabel8 = new javax.swing.JLabel();
        comboBox_danhMucTimKiem_tabQLTienDien = new javax.swing.JComboBox<>();
        jLabel9 = new javax.swing.JLabel();
        comboBox_phuongCuTru_tabQLTienDien = new javax.swing.JComboBox<>();
        jButton1 = new javax.swing.JButton();
        jButton3 = new javax.swing.JButton();
        jPanel4 = new javax.swing.JPanel();
        jLabel10 = new javax.swing.JLabel();
        jSeparator2 = new javax.swing.JSeparator();
        jLabel11 = new javax.swing.JLabel();
        jLabel12 = new javax.swing.JLabel();
        label_thangHienTai_tabQLChiSoDien = new javax.swing.JLabel();
        jLabel14 = new javax.swing.JLabel();
        label_namHienTai_tabQLChiSoDien = new javax.swing.JLabel();
        jLabel16 = new javax.swing.JLabel();
        txt_timKiem_tabQLChiSoDien = new javax.swing.JTextField();
        jLabel17 = new javax.swing.JLabel();
        comboBox_danhMucTimKiem_tabQLChiSoDien = new javax.swing.JComboBox<>();
        jLabel18 = new javax.swing.JLabel();
        comboBox_phuongCuTru_tabQLChiSoDien = new javax.swing.JComboBox<>();
        jScrollPane2 = new javax.swing.JScrollPane();
        table_QLChiSoDien = new javax.swing.JTable();
        jButton2 = new javax.swing.JButton();
        button_themChiSoDien = new javax.swing.JButton();
        jButton16 = new javax.swing.JButton();
        jPanel5 = new javax.swing.JPanel();
        jLabel19 = new javax.swing.JLabel();
        jSeparator3 = new javax.swing.JSeparator();
        jScrollPane3 = new javax.swing.JScrollPane();
        table_QLKhachHang = new javax.swing.JTable();
        jLabel20 = new javax.swing.JLabel();
        txt_timKiem_tabQLKhachHang = new javax.swing.JTextField();
        jLabel21 = new javax.swing.JLabel();
        comboBox_danhMucTimKiem_tabQLKhachHang = new javax.swing.JComboBox<>();
        jLabel22 = new javax.swing.JLabel();
        comboBox_phuongCuTru_tabQLKhachHang = new javax.swing.JComboBox<>();
        jLabel23 = new javax.swing.JLabel();
        comboBox_sapXep_tabQLKhachHang = new javax.swing.JComboBox<>();
        button_lamMoi = new javax.swing.JButton();
        button_themKhachHang = new javax.swing.JButton();
        jButton17 = new javax.swing.JButton();
        jPanel6 = new javax.swing.JPanel();
        jLabel24 = new javax.swing.JLabel();
        jSeparator4 = new javax.swing.JSeparator();
        jLabel3 = new javax.swing.JLabel();
        comboBox_loaiBac = new javax.swing.JComboBox<>();
        jLabel5 = new javax.swing.JLabel();
        txt_gia = new javax.swing.JTextField();
        jLabel13 = new javax.swing.JLabel();
        jLabel15 = new javax.swing.JLabel();
        txt_min = new javax.swing.JTextField();
        txt_max = new javax.swing.JTextField();
        jLabel25 = new javax.swing.JLabel();
        jLabel26 = new javax.swing.JLabel();
        jLabel27 = new javax.swing.JLabel();
        button_suaHuy = new javax.swing.JButton();
        button_luu = new javax.swing.JButton();
        jButton6 = new javax.swing.JButton();
        jButton7 = new javax.swing.JButton();
        jScrollPane4 = new javax.swing.JScrollPane();
        table_tabQLBacTienDien = new javax.swing.JTable();
        jButton8 = new javax.swing.JButton();
        jLabel28 = new javax.swing.JLabel();
        jLabel29 = new javax.swing.JLabel();
        label_ngayTao = new javax.swing.JLabel();
        label_ngaySua = new javax.swing.JLabel();
        jLabel32 = new javax.swing.JLabel();
        jPanel2 = new javax.swing.JPanel();
        jLabel30 = new javax.swing.JLabel();
        jLabel31 = new javax.swing.JLabel();
        jLabel33 = new javax.swing.JLabel();
        jScrollPane5 = new javax.swing.JScrollPane();
        table_lichSuKhachHang_tabBaoCao = new javax.swing.JTable();
        label_soLuongKhachhang_tabBaoCao = new javax.swing.JLabel();
        label_sanLuongTieuThu_tabBaoCao = new javax.swing.JLabel();
        label_doanhThu_tabBaoCao = new javax.swing.JLabel();
        jLabel37 = new javax.swing.JLabel();
        jLabel34 = new javax.swing.JLabel();
        jSeparator5 = new javax.swing.JSeparator();
        jLabel35 = new javax.swing.JLabel();
        comboB_thang_tabBaoCao = new javax.swing.JComboBox<>();
        jLabel38 = new javax.swing.JLabel();
        jLabel39 = new javax.swing.JLabel();
        comboB_nam_tabBaoCao = new javax.swing.JComboBox<>();

        setDefaultCloseOperation(javax.swing.WindowConstants.EXIT_ON_CLOSE);
        setBackground(new java.awt.Color(255, 255, 255));

        tab_QuanLy.setFont(new java.awt.Font("Arial", 1, 14)); // NOI18N
        tab_QuanLy.addChangeListener(new javax.swing.event.ChangeListener() {
            public void stateChanged(javax.swing.event.ChangeEvent evt) {
                tab_QuanLyStateChanged(evt);
            }
        });

        jPanel3.setCursor(new java.awt.Cursor(java.awt.Cursor.DEFAULT_CURSOR));

        jLabel1.setFont(new java.awt.Font("Times New Roman", 1, 24)); // NOI18N
        jLabel1.setHorizontalAlignment(javax.swing.SwingConstants.CENTER);
        jLabel1.setText("Quản lý tiền điện");

        table_QLTienDien.setFont(new java.awt.Font("Times New Roman", 0, 14)); // NOI18N
        table_QLTienDien.setModel(new javax.swing.table.DefaultTableModel(
            new Object [][] {
                {null, null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null, null}
            },
            new String [] {
                "STT", "Mã khách hàng", "Họ tên khách hàng", "Số chứng minh thư", "Số điện thoại", "Email", "Địa chỉ", "Phường", "Số công tơ", "Tiền điện ( VNĐ )"
            }
        ));
        table_QLTienDien.addMouseListener(new java.awt.event.MouseAdapter() {
            public void mouseClicked(java.awt.event.MouseEvent evt) {
                table_QLTienDienMouseClicked(evt);
            }
        });
        jScrollPane1.setViewportView(table_QLTienDien);

        jLabel2.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jLabel2.setText("Tiền điện tháng: ");

        label_thangHienTai_tabQLTienDien.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        label_thangHienTai_tabQLTienDien.setForeground(new java.awt.Color(255, 0, 0));
        label_thangHienTai_tabQLTienDien.setText("...");

        jLabel4.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jLabel4.setText("Năm: ");

        label_namHienTai_tabQLTienDien.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        label_namHienTai_tabQLTienDien.setForeground(new java.awt.Color(255, 0, 0));
        label_namHienTai_tabQLTienDien.setText("...");

        jLabel6.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jLabel6.setText("Công ty điện lực Nam Từ Liêm - Hà Nội");

        jLabel7.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jLabel7.setText("Tìm kiếm:");

        txt_timKiem_tabQLTienDien.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        txt_timKiem_tabQLTienDien.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                txt_timKiem_tabQLTienDienActionPerformed(evt);
            }
        });

        jLabel8.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jLabel8.setText("theo");

        comboBox_danhMucTimKiem_tabQLTienDien.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        comboBox_danhMucTimKiem_tabQLTienDien.setModel(new javax.swing.DefaultComboBoxModel<>(new String[] { "Mã khách hàng", "Số công tơ", "Họ tên", "Số điện thoại", "Số chứng minh thư" }));

        jLabel9.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jLabel9.setText("Phường: ");

        comboBox_phuongCuTru_tabQLTienDien.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        comboBox_phuongCuTru_tabQLTienDien.setModel(new javax.swing.DefaultComboBoxModel<>(new String[] { "Tất cả", "Cầu Diễn", "Đại Mỗ", "Mễ Trì", "Mỹ Đình 1", "Mỹ Đình 2", "Phương Canh", "Phú Đô", "Trung Văn", "Tây Mỗ", "Xuân Phương" }));
        comboBox_phuongCuTru_tabQLTienDien.addItemListener(new java.awt.event.ItemListener() {
            public void itemStateChanged(java.awt.event.ItemEvent evt) {
                comboBox_phuongCuTru_tabQLTienDienItemStateChanged(evt);
            }
        });
        comboBox_phuongCuTru_tabQLTienDien.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                comboBox_phuongCuTru_tabQLTienDienActionPerformed(evt);
            }
        });

        jButton1.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jButton1.setText("Làm mới");
        jButton1.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jButton1ActionPerformed(evt);
            }
        });

        jButton3.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        jButton3.setText("Tìm");
        jButton3.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jButton3ActionPerformed(evt);
            }
        });

        javax.swing.GroupLayout jPanel3Layout = new javax.swing.GroupLayout(jPanel3);
        jPanel3.setLayout(jPanel3Layout);
        jPanel3Layout.setHorizontalGroup(
            jPanel3Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addComponent(jSeparator1)
            .addGroup(jPanel3Layout.createSequentialGroup()
                .addGap(39, 39, 39)
                .addGroup(jPanel3Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(jPanel3Layout.createSequentialGroup()
                        .addGroup(jPanel3Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addGroup(jPanel3Layout.createSequentialGroup()
                                .addComponent(jLabel4)
                                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                                .addComponent(label_namHienTai_tabQLTienDien))
                            .addGroup(jPanel3Layout.createSequentialGroup()
                                .addComponent(jLabel7)
                                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                                .addComponent(txt_timKiem_tabQLTienDien, javax.swing.GroupLayout.PREFERRED_SIZE, 314, javax.swing.GroupLayout.PREFERRED_SIZE)
                                .addGap(18, 18, 18)
                                .addComponent(jLabel8)
                                .addGap(18, 18, 18)
                                .addComponent(comboBox_danhMucTimKiem_tabQLTienDien, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                                .addGap(18, 18, 18)
                                .addComponent(jButton3, javax.swing.GroupLayout.PREFERRED_SIZE, 87, javax.swing.GroupLayout.PREFERRED_SIZE))
                            .addGroup(jPanel3Layout.createSequentialGroup()
                                .addComponent(jLabel9)
                                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                                .addComponent(comboBox_phuongCuTru_tabQLTienDien, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)))
                        .addGap(612, 612, 612)
                        .addComponent(jButton1, javax.swing.GroupLayout.PREFERRED_SIZE, 151, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addContainerGap(13, Short.MAX_VALUE))
                    .addGroup(jPanel3Layout.createSequentialGroup()
                        .addGroup(jPanel3Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addGroup(jPanel3Layout.createSequentialGroup()
                                .addComponent(jLabel2)
                                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                                .addComponent(label_thangHienTai_tabQLTienDien))
                            .addComponent(jLabel6))
                        .addGap(0, 0, Short.MAX_VALUE))))
            .addGroup(jPanel3Layout.createSequentialGroup()
                .addGroup(jPanel3Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(jPanel3Layout.createSequentialGroup()
                        .addGap(672, 672, 672)
                        .addComponent(jLabel1))
                    .addGroup(jPanel3Layout.createSequentialGroup()
                        .addContainerGap()
                        .addComponent(jScrollPane1, javax.swing.GroupLayout.PREFERRED_SIZE, 1506, javax.swing.GroupLayout.PREFERRED_SIZE)))
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );
        jPanel3Layout.setVerticalGroup(
            jPanel3Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel3Layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(jLabel1)
                .addGap(18, 18, 18)
                .addComponent(jSeparator1, javax.swing.GroupLayout.PREFERRED_SIZE, 10, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(18, 18, 18)
                .addComponent(jLabel6)
                .addGap(13, 13, 13)
                .addGroup(jPanel3Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(jLabel2, javax.swing.GroupLayout.Alignment.TRAILING)
                    .addComponent(label_thangHienTai_tabQLTienDien))
                .addGap(18, 18, 18)
                .addGroup(jPanel3Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, jPanel3Layout.createSequentialGroup()
                        .addComponent(jButton1, javax.swing.GroupLayout.PREFERRED_SIZE, 60, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addGap(33, 33, 33))
                    .addGroup(jPanel3Layout.createSequentialGroup()
                        .addGroup(jPanel3Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(jLabel4, javax.swing.GroupLayout.PREFERRED_SIZE, 22, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(label_namHienTai_tabQLTienDien))
                        .addGap(21, 21, 21)
                        .addGroup(jPanel3Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(jLabel7)
                            .addComponent(txt_timKiem_tabQLTienDien, javax.swing.GroupLayout.PREFERRED_SIZE, 33, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(jLabel8)
                            .addComponent(comboBox_danhMucTimKiem_tabQLTienDien, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(jButton3, javax.swing.GroupLayout.PREFERRED_SIZE, 33, javax.swing.GroupLayout.PREFERRED_SIZE))
                        .addGap(32, 32, 32)))
                .addGroup(jPanel3Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jLabel9)
                    .addComponent(comboBox_phuongCuTru_tabQLTienDien, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, 47, Short.MAX_VALUE)
                .addComponent(jScrollPane1, javax.swing.GroupLayout.PREFERRED_SIZE, 299, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(24, 24, 24))
        );

        tab_QuanLy.addTab("Quản lý tiền điện", jPanel3);

        jLabel10.setFont(new java.awt.Font("Times New Roman", 1, 24)); // NOI18N
        jLabel10.setHorizontalAlignment(javax.swing.SwingConstants.CENTER);
        jLabel10.setText("Quản lý chỉ số điện");

        jLabel11.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jLabel11.setText("Công ty điện lực Nam Từ Liêm - Hà Nội");

        jLabel12.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jLabel12.setText("Chỉ số điện tháng:");

        label_thangHienTai_tabQLChiSoDien.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        label_thangHienTai_tabQLChiSoDien.setForeground(new java.awt.Color(255, 0, 0));
        label_thangHienTai_tabQLChiSoDien.setText("...");

        jLabel14.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jLabel14.setText("Năm: ");

        label_namHienTai_tabQLChiSoDien.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        label_namHienTai_tabQLChiSoDien.setForeground(new java.awt.Color(255, 0, 0));
        label_namHienTai_tabQLChiSoDien.setText("...");

        jLabel16.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jLabel16.setText("Tìm kiếm:");

        txt_timKiem_tabQLChiSoDien.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N

        jLabel17.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jLabel17.setText("theo");

        comboBox_danhMucTimKiem_tabQLChiSoDien.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        comboBox_danhMucTimKiem_tabQLChiSoDien.setModel(new javax.swing.DefaultComboBoxModel<>(new String[] { "Mã khách hàng", "Số công tơ", "Họ tên", "Số điện thoại", "Số chứng minh thư", "Email" }));

        jLabel18.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jLabel18.setText("Phường: ");

        comboBox_phuongCuTru_tabQLChiSoDien.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        comboBox_phuongCuTru_tabQLChiSoDien.setModel(new javax.swing.DefaultComboBoxModel<>(new String[] { "Tất cả", "Cầu Diễn", "Đại Mỗ", "Mễ Trì", "Mỹ Đình 1", "Mỹ Đình 2", "Phương Canh", "Phú Đô", "Trung Văn", "Tây Mỗ", "Xuân Phương" }));
        comboBox_phuongCuTru_tabQLChiSoDien.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                comboBox_phuongCuTru_tabQLChiSoDienActionPerformed(evt);
            }
        });

        table_QLChiSoDien.setFont(new java.awt.Font("Times New Roman", 0, 14)); // NOI18N
        table_QLChiSoDien.setModel(new javax.swing.table.DefaultTableModel(
            new Object [][] {
                {null, null, null, null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null, null, null, null}
            },
            new String [] {
                "STT", "Mã khách hàng", "Họ tên khách hàng", "Số chứng minh thư", "Số điện thoại", "Email", "Địa chỉ", "Phường", "Số công tơ", "Số điện tiêu thụ ( KW )", "Chỉ số mới ( KW )", "Chỉ số cũ ( KW )"
            }
        ));
        table_QLChiSoDien.addMouseListener(new java.awt.event.MouseAdapter() {
            public void mouseClicked(java.awt.event.MouseEvent evt) {
                table_QLChiSoDienMouseClicked(evt);
            }
        });
        jScrollPane2.setViewportView(table_QLChiSoDien);

        jButton2.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jButton2.setText("Làm mới");
        jButton2.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jButton2ActionPerformed(evt);
            }
        });

        button_themChiSoDien.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        button_themChiSoDien.setText("Thêm chỉ số điện");
        button_themChiSoDien.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                button_themChiSoDienActionPerformed(evt);
            }
        });

        jButton16.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        jButton16.setText("Tìm");
        jButton16.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jButton16ActionPerformed(evt);
            }
        });

        javax.swing.GroupLayout jPanel4Layout = new javax.swing.GroupLayout(jPanel4);
        jPanel4.setLayout(jPanel4Layout);
        jPanel4Layout.setHorizontalGroup(
            jPanel4Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addComponent(jSeparator2)
            .addGroup(jPanel4Layout.createSequentialGroup()
                .addGap(647, 647, 647)
                .addComponent(jLabel10)
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
            .addGroup(jPanel4Layout.createSequentialGroup()
                .addGap(39, 39, 39)
                .addGroup(jPanel4Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(jPanel4Layout.createSequentialGroup()
                        .addGroup(jPanel4Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addGroup(jPanel4Layout.createSequentialGroup()
                                .addComponent(jLabel12)
                                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                                .addComponent(label_thangHienTai_tabQLChiSoDien))
                            .addComponent(jLabel11))
                        .addGap(0, 0, Short.MAX_VALUE))
                    .addGroup(jPanel4Layout.createSequentialGroup()
                        .addGroup(jPanel4Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING, false)
                            .addComponent(jScrollPane2, javax.swing.GroupLayout.Alignment.LEADING)
                            .addGroup(jPanel4Layout.createSequentialGroup()
                                .addGroup(jPanel4Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                    .addGroup(jPanel4Layout.createSequentialGroup()
                                        .addComponent(jLabel18)
                                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                                        .addComponent(comboBox_phuongCuTru_tabQLChiSoDien, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                                    .addGroup(jPanel4Layout.createSequentialGroup()
                                        .addGroup(jPanel4Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                            .addGroup(jPanel4Layout.createSequentialGroup()
                                                .addComponent(jLabel14)
                                                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                                                .addComponent(label_namHienTai_tabQLChiSoDien))
                                            .addGroup(jPanel4Layout.createSequentialGroup()
                                                .addComponent(jLabel16)
                                                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                                                .addComponent(txt_timKiem_tabQLChiSoDien, javax.swing.GroupLayout.PREFERRED_SIZE, 314, javax.swing.GroupLayout.PREFERRED_SIZE)
                                                .addGap(9, 9, 9)
                                                .addComponent(jLabel17)
                                                .addGap(18, 18, 18)
                                                .addComponent(comboBox_danhMucTimKiem_tabQLChiSoDien, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                                                .addGap(18, 18, 18)
                                                .addComponent(jButton16, javax.swing.GroupLayout.PREFERRED_SIZE, 90, javax.swing.GroupLayout.PREFERRED_SIZE)))
                                        .addGap(422, 422, 422)
                                        .addComponent(button_themChiSoDien)))
                                .addGap(18, 18, 18)
                                .addComponent(jButton2, javax.swing.GroupLayout.PREFERRED_SIZE, 151, javax.swing.GroupLayout.PREFERRED_SIZE)))
                        .addContainerGap(20, Short.MAX_VALUE))))
        );
        jPanel4Layout.setVerticalGroup(
            jPanel4Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel4Layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(jLabel10)
                .addGap(18, 18, 18)
                .addComponent(jSeparator2, javax.swing.GroupLayout.PREFERRED_SIZE, 10, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(18, 18, 18)
                .addComponent(jLabel11)
                .addGap(13, 13, 13)
                .addGroup(jPanel4Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(jLabel12, javax.swing.GroupLayout.Alignment.TRAILING)
                    .addComponent(label_thangHienTai_tabQLChiSoDien))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(jPanel4Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(jPanel4Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                        .addComponent(jButton2, javax.swing.GroupLayout.PREFERRED_SIZE, 62, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addComponent(button_themChiSoDien, javax.swing.GroupLayout.PREFERRED_SIZE, 62, javax.swing.GroupLayout.PREFERRED_SIZE))
                    .addGroup(jPanel4Layout.createSequentialGroup()
                        .addGroup(jPanel4Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addComponent(jLabel14, javax.swing.GroupLayout.PREFERRED_SIZE, 22, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(label_namHienTai_tabQLChiSoDien))
                        .addGap(18, 18, 18)
                        .addGroup(jPanel4Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
                            .addGroup(jPanel4Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                                .addComponent(jLabel16)
                                .addComponent(comboBox_danhMucTimKiem_tabQLChiSoDien, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                                .addComponent(jLabel17)
                                .addComponent(jButton16))
                            .addComponent(txt_timKiem_tabQLChiSoDien, javax.swing.GroupLayout.PREFERRED_SIZE, 32, javax.swing.GroupLayout.PREFERRED_SIZE))))
                .addGap(18, 18, 18)
                .addGroup(jPanel4Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jLabel18)
                    .addComponent(comboBox_phuongCuTru_tabQLChiSoDien, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addGap(18, 18, 18)
                .addComponent(jScrollPane2, javax.swing.GroupLayout.PREFERRED_SIZE, 334, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addContainerGap(92, Short.MAX_VALUE))
        );

        tab_QuanLy.addTab("Quản lý chỉ số điện", jPanel4);

        jLabel19.setFont(new java.awt.Font("Times New Roman", 1, 24)); // NOI18N
        jLabel19.setHorizontalAlignment(javax.swing.SwingConstants.CENTER);
        jLabel19.setText("Quản lý khách hàng");

        table_QLKhachHang.setFont(new java.awt.Font("Times New Roman", 0, 14)); // NOI18N
        table_QLKhachHang.setModel(new javax.swing.table.DefaultTableModel(
            new Object [][] {
                {null, null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null, null}
            },
            new String [] {
                "STT", "Mã khách hàng", "Họ tên khách hàng", "Ngày sinh", "Số chứng minh thư", "Số điện thoại", "Email", "Địa chỉ", "Phường", "Số công tơ"
            }
        ));
        table_QLKhachHang.addMouseListener(new java.awt.event.MouseAdapter() {
            public void mouseClicked(java.awt.event.MouseEvent evt) {
                table_QLKhachHangMouseClicked(evt);
            }
        });
        jScrollPane3.setViewportView(table_QLKhachHang);

        jLabel20.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jLabel20.setText("Tìm kiếm:");

        txt_timKiem_tabQLKhachHang.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N

        jLabel21.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jLabel21.setText("theo");

        comboBox_danhMucTimKiem_tabQLKhachHang.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        comboBox_danhMucTimKiem_tabQLKhachHang.setModel(new javax.swing.DefaultComboBoxModel<>(new String[] { "Mã khách hàng", "Số công tơ", "Họ tên", "Số điện thoại", "Số chứng minh thư" }));
        comboBox_danhMucTimKiem_tabQLKhachHang.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                comboBox_danhMucTimKiem_tabQLKhachHangActionPerformed(evt);
            }
        });

        jLabel22.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jLabel22.setText("Phường: ");

        comboBox_phuongCuTru_tabQLKhachHang.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        comboBox_phuongCuTru_tabQLKhachHang.setModel(new javax.swing.DefaultComboBoxModel<>(new String[] { "Tất cả", "Cầu Diễn", "Đại Mỗ", "Mễ Trì", "Mỹ Đình 1", "Mỹ Đình 2", "Phương Canh", "Phú Đô", "Trung Văn", "Tây Mỗ", "Xuân Phương" }));
        comboBox_phuongCuTru_tabQLKhachHang.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                comboBox_phuongCuTru_tabQLKhachHangActionPerformed(evt);
            }
        });

        jLabel23.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jLabel23.setText("Sắp xếp theo: ");

        comboBox_sapXep_tabQLKhachHang.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        comboBox_sapXep_tabQLKhachHang.setModel(new javax.swing.DefaultComboBoxModel<>(new String[] { "Đăng ký gần đây", "Đăng ký lâu" }));
        comboBox_sapXep_tabQLKhachHang.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                comboBox_sapXep_tabQLKhachHangActionPerformed(evt);
            }
        });

        button_lamMoi.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        button_lamMoi.setText("Làm mới");
        button_lamMoi.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                button_lamMoiActionPerformed(evt);
            }
        });

        button_themKhachHang.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        button_themKhachHang.setText("Tạo mới khách hàng");
        button_themKhachHang.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                button_themKhachHangActionPerformed(evt);
            }
        });

        jButton17.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        jButton17.setText("Tìm ");
        jButton17.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jButton17ActionPerformed(evt);
            }
        });

        javax.swing.GroupLayout jPanel5Layout = new javax.swing.GroupLayout(jPanel5);
        jPanel5.setLayout(jPanel5Layout);
        jPanel5Layout.setHorizontalGroup(
            jPanel5Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel5Layout.createSequentialGroup()
                .addGroup(jPanel5Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, jPanel5Layout.createSequentialGroup()
                        .addContainerGap()
                        .addComponent(jSeparator3))
                    .addGroup(jPanel5Layout.createSequentialGroup()
                        .addGroup(jPanel5Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addGroup(jPanel5Layout.createSequentialGroup()
                                .addGap(37, 37, 37)
                                .addGroup(jPanel5Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                                    .addGroup(jPanel5Layout.createSequentialGroup()
                                        .addComponent(jLabel23)
                                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                                        .addComponent(comboBox_sapXep_tabQLKhachHang, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                                    .addGroup(jPanel5Layout.createSequentialGroup()
                                        .addComponent(jLabel20)
                                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                                        .addComponent(txt_timKiem_tabQLKhachHang, javax.swing.GroupLayout.PREFERRED_SIZE, 314, javax.swing.GroupLayout.PREFERRED_SIZE)
                                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                                        .addComponent(jLabel21)
                                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                                        .addComponent(comboBox_danhMucTimKiem_tabQLKhachHang, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                                        .addGap(18, 18, 18)
                                        .addComponent(jButton17, javax.swing.GroupLayout.PREFERRED_SIZE, 82, javax.swing.GroupLayout.PREFERRED_SIZE))
                                    .addComponent(jScrollPane3, javax.swing.GroupLayout.PREFERRED_SIZE, 1509, javax.swing.GroupLayout.PREFERRED_SIZE)
                                    .addGroup(jPanel5Layout.createSequentialGroup()
                                        .addComponent(jLabel22)
                                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                                        .addComponent(comboBox_phuongCuTru_tabQLKhachHang, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                                        .addComponent(button_themKhachHang)
                                        .addGap(18, 18, 18)
                                        .addComponent(button_lamMoi, javax.swing.GroupLayout.PREFERRED_SIZE, 151, javax.swing.GroupLayout.PREFERRED_SIZE))))
                            .addGroup(jPanel5Layout.createSequentialGroup()
                                .addGap(669, 669, 669)
                                .addComponent(jLabel19)))
                        .addGap(0, 2, Short.MAX_VALUE)))
                .addContainerGap())
        );
        jPanel5Layout.setVerticalGroup(
            jPanel5Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel5Layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(jLabel19)
                .addGap(18, 18, 18)
                .addComponent(jSeparator3, javax.swing.GroupLayout.PREFERRED_SIZE, 10, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(18, 18, 18)
                .addGroup(jPanel5Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(jButton17, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addGroup(jPanel5Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                        .addComponent(jLabel20)
                        .addComponent(txt_timKiem_tabQLKhachHang, javax.swing.GroupLayout.DEFAULT_SIZE, 34, Short.MAX_VALUE)
                        .addComponent(jLabel21)
                        .addComponent(comboBox_danhMucTimKiem_tabQLKhachHang, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)))
                .addGap(18, 18, 18)
                .addGroup(jPanel5Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jLabel22)
                    .addComponent(comboBox_phuongCuTru_tabQLKhachHang, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(button_lamMoi, javax.swing.GroupLayout.PREFERRED_SIZE, 61, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(button_themKhachHang, javax.swing.GroupLayout.PREFERRED_SIZE, 55, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addGap(18, 18, 18)
                .addGroup(jPanel5Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jLabel23)
                    .addComponent(comboBox_sapXep_tabQLKhachHang, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addGap(50, 50, 50)
                .addComponent(jScrollPane3, javax.swing.GroupLayout.PREFERRED_SIZE, 354, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(62, 62, 62))
        );

        tab_QuanLy.addTab("Quản lý khách hàng", jPanel5);

        jPanel6.setEnabled(false);

        jLabel24.setFont(new java.awt.Font("Times New Roman", 1, 24)); // NOI18N
        jLabel24.setHorizontalAlignment(javax.swing.SwingConstants.CENTER);
        jLabel24.setText("Quản lý bậc tiền điện");

        jLabel3.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        jLabel3.setText("Loại bậc:");

        comboBox_loaiBac.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        comboBox_loaiBac.addItemListener(new java.awt.event.ItemListener() {
            public void itemStateChanged(java.awt.event.ItemEvent evt) {
                comboBox_loaiBacItemStateChanged(evt);
            }
        });
        comboBox_loaiBac.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                comboBox_loaiBacActionPerformed(evt);
            }
        });

        jLabel5.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        jLabel5.setText("Giá:");

        txt_gia.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        txt_gia.setEnabled(false);

        jLabel13.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        jLabel13.setText("/ 1 kWh");

        jLabel15.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        jLabel15.setText("Số điện tối thiểu của bậc:");

        txt_min.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        txt_min.setEnabled(false);

        txt_max.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        txt_max.setEnabled(false);

        jLabel25.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        jLabel25.setText("Số điện tối đa của bậc:");

        jLabel26.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        jLabel26.setText("kWh");

        jLabel27.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        jLabel27.setText("kWh");

        button_suaHuy.setFont(new java.awt.Font("Times New Roman", 0, 15)); // NOI18N
        button_suaHuy.setText("Sửa");
        button_suaHuy.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                button_suaHuyActionPerformed(evt);
            }
        });

        button_luu.setFont(new java.awt.Font("Times New Roman", 0, 15)); // NOI18N
        button_luu.setText("Lưu");
        button_luu.setEnabled(false);
        button_luu.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                button_luuActionPerformed(evt);
            }
        });

        jButton6.setFont(new java.awt.Font("Times New Roman", 0, 15)); // NOI18N
        jButton6.setText("Xóa");
        jButton6.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jButton6ActionPerformed(evt);
            }
        });

        jButton7.setFont(new java.awt.Font("Times New Roman", 0, 15)); // NOI18N
        jButton7.setText("Thêm mới");
        jButton7.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jButton7ActionPerformed(evt);
            }
        });

        table_tabQLBacTienDien.setModel(new javax.swing.table.DefaultTableModel(
            new Object [][] {
                {null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null}
            },
            new String [] {
                "STT", "Loại bậc", "Giá / 1 kWh", "Số điện tối thiểu (kWh)", "Số điện tối đa (kWh)", "Ngày sửa", "Ngày tạo"
            }
        ));
        jScrollPane4.setViewportView(table_tabQLBacTienDien);

        jButton8.setFont(new java.awt.Font("Times New Roman", 0, 15)); // NOI18N
        jButton8.setText("Làm mới");
        jButton8.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jButton8ActionPerformed(evt);
            }
        });

        jLabel28.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        jLabel28.setText("Ngày tạo:");

        jLabel29.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        jLabel29.setText("Ngày sửa:");

        label_ngayTao.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        label_ngayTao.setText("0000-00-00");

        label_ngaySua.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        label_ngaySua.setText("0000-00-00");

        jLabel32.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        jLabel32.setText("yyyy-mm-dd");

        javax.swing.GroupLayout jPanel6Layout = new javax.swing.GroupLayout(jPanel6);
        jPanel6.setLayout(jPanel6Layout);
        jPanel6Layout.setHorizontalGroup(
            jPanel6Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel6Layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(jPanel6Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(jSeparator4, javax.swing.GroupLayout.Alignment.TRAILING)
                    .addGroup(jPanel6Layout.createSequentialGroup()
                        .addGap(657, 657, 657)
                        .addComponent(jLabel24)
                        .addGap(0, 639, Short.MAX_VALUE)))
                .addGap(33, 33, 33))
            .addGroup(jPanel6Layout.createSequentialGroup()
                .addGroup(jPanel6Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(jPanel6Layout.createSequentialGroup()
                        .addGap(41, 41, 41)
                        .addGroup(jPanel6Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addGroup(jPanel6Layout.createSequentialGroup()
                                .addGroup(jPanel6Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
                                    .addComponent(jLabel25)
                                    .addComponent(jLabel15)
                                    .addComponent(jLabel5)
                                    .addComponent(jLabel3))
                                .addGap(18, 18, 18)
                                .addGroup(jPanel6Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                    .addComponent(comboBox_loaiBac, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                                    .addGroup(jPanel6Layout.createSequentialGroup()
                                        .addGroup(jPanel6Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING, false)
                                            .addComponent(txt_min, javax.swing.GroupLayout.Alignment.LEADING)
                                            .addComponent(txt_gia, javax.swing.GroupLayout.Alignment.LEADING)
                                            .addComponent(txt_max, javax.swing.GroupLayout.Alignment.LEADING, javax.swing.GroupLayout.DEFAULT_SIZE, 167, Short.MAX_VALUE))
                                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                                        .addGroup(jPanel6Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                            .addComponent(jLabel13)
                                            .addComponent(jLabel26)
                                            .addComponent(jLabel27)))))
                            .addGroup(jPanel6Layout.createSequentialGroup()
                                .addGap(109, 109, 109)
                                .addGroup(jPanel6Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
                                    .addComponent(jLabel28)
                                    .addComponent(jLabel29))
                                .addGap(18, 18, 18)
                                .addGroup(jPanel6Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                    .addComponent(label_ngayTao)
                                    .addComponent(label_ngaySua)))
                            .addGroup(jPanel6Layout.createSequentialGroup()
                                .addGap(18, 18, 18)
                                .addComponent(button_suaHuy, javax.swing.GroupLayout.PREFERRED_SIZE, 78, javax.swing.GroupLayout.PREFERRED_SIZE)
                                .addGap(18, 18, 18)
                                .addComponent(button_luu, javax.swing.GroupLayout.PREFERRED_SIZE, 72, javax.swing.GroupLayout.PREFERRED_SIZE)
                                .addGap(18, 18, 18)
                                .addComponent(jButton6, javax.swing.GroupLayout.PREFERRED_SIZE, 76, javax.swing.GroupLayout.PREFERRED_SIZE)
                                .addGap(18, 18, 18)
                                .addGroup(jPanel6Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                    .addComponent(jLabel32)
                                    .addComponent(jButton7)))))
                    .addGroup(jPanel6Layout.createSequentialGroup()
                        .addGap(166, 166, 166)
                        .addComponent(jButton8, javax.swing.GroupLayout.PREFERRED_SIZE, 129, javax.swing.GroupLayout.PREFERRED_SIZE)))
                .addGap(72, 72, 72)
                .addComponent(jScrollPane4, javax.swing.GroupLayout.PREFERRED_SIZE, 898, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );
        jPanel6Layout.setVerticalGroup(
            jPanel6Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel6Layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(jLabel24)
                .addGap(18, 18, 18)
                .addComponent(jSeparator4, javax.swing.GroupLayout.PREFERRED_SIZE, 10, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(30, 30, 30)
                .addGroup(jPanel6Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                    .addGroup(jPanel6Layout.createSequentialGroup()
                        .addGroup(jPanel6Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(jLabel3)
                            .addComponent(comboBox_loaiBac, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                        .addGap(18, 18, 18)
                        .addGroup(jPanel6Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(jLabel5)
                            .addComponent(txt_gia, javax.swing.GroupLayout.PREFERRED_SIZE, 29, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(jLabel13))
                        .addGap(18, 18, 18)
                        .addGroup(jPanel6Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(jLabel15)
                            .addComponent(txt_min, javax.swing.GroupLayout.PREFERRED_SIZE, 32, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(jLabel26))
                        .addGap(18, 18, 18)
                        .addGroup(jPanel6Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(jLabel25)
                            .addComponent(txt_max, javax.swing.GroupLayout.PREFERRED_SIZE, 32, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(jLabel27))
                        .addGap(18, 18, 18)
                        .addGroup(jPanel6Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(jLabel28)
                            .addComponent(label_ngayTao))
                        .addGap(5, 5, 5)
                        .addComponent(jLabel32)
                        .addGap(4, 4, 4)
                        .addGroup(jPanel6Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(jLabel29)
                            .addComponent(label_ngaySua))
                        .addGap(40, 40, 40)
                        .addGroup(jPanel6Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(button_suaHuy, javax.swing.GroupLayout.PREFERRED_SIZE, 31, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(button_luu, javax.swing.GroupLayout.PREFERRED_SIZE, 31, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(jButton6, javax.swing.GroupLayout.PREFERRED_SIZE, 31, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(jButton7, javax.swing.GroupLayout.PREFERRED_SIZE, 31, javax.swing.GroupLayout.PREFERRED_SIZE))
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addComponent(jButton8, javax.swing.GroupLayout.PREFERRED_SIZE, 48, javax.swing.GroupLayout.PREFERRED_SIZE))
                    .addComponent(jScrollPane4, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addContainerGap(142, Short.MAX_VALUE))
        );

        tab_QuanLy.addTab("Quản lý bậc tiền điện", jPanel6);

        jLabel30.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jLabel30.setText("Số lượng khách hàng:");

        jLabel31.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jLabel31.setText("Sản lượng điện tiêu thụ:");

        jLabel33.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jLabel33.setText("Doanh thu:");

        table_lichSuKhachHang_tabBaoCao.setModel(new javax.swing.table.DefaultTableModel(
            new Object [][] {
                {null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null}
            },
            new String [] {
                "STT", "Điện Tháng/Năm", "Số điện tiêu thụ (kWh)", "Chỉ số mới (kWh)", "Chỉ số cũ (kWh)", "Tiền điện thanh toán (VNĐ)", "Thời gian thanh toán"
            }
        ));
        jScrollPane5.setViewportView(table_lichSuKhachHang_tabBaoCao);

        label_soLuongKhachhang_tabBaoCao.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        label_soLuongKhachhang_tabBaoCao.setForeground(new java.awt.Color(255, 0, 0));
        label_soLuongKhachhang_tabBaoCao.setText("...");

        label_sanLuongTieuThu_tabBaoCao.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        label_sanLuongTieuThu_tabBaoCao.setForeground(new java.awt.Color(255, 0, 0));
        label_sanLuongTieuThu_tabBaoCao.setText("...");

        label_doanhThu_tabBaoCao.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        label_doanhThu_tabBaoCao.setForeground(new java.awt.Color(255, 0, 0));
        label_doanhThu_tabBaoCao.setText("...");

        jLabel37.setFont(new java.awt.Font("Times New Roman", 0, 15)); // NOI18N
        jLabel37.setText("Lịch sử giao dịch các khách hàng");

        jLabel34.setFont(new java.awt.Font("Times New Roman", 1, 24)); // NOI18N
        jLabel34.setHorizontalAlignment(javax.swing.SwingConstants.CENTER);
        jLabel34.setText("Báo cáo thống kê");

        jLabel35.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jLabel35.setText("Dữ liệu của");

        comboB_thang_tabBaoCao.setFont(new java.awt.Font("Times New Roman", 1, 15)); // NOI18N
        comboB_thang_tabBaoCao.setModel(new javax.swing.DefaultComboBoxModel<>(new String[] { "Tất cả", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" }));
        comboB_thang_tabBaoCao.addItemListener(new java.awt.event.ItemListener() {
            public void itemStateChanged(java.awt.event.ItemEvent evt) {
                comboB_thang_tabBaoCaoItemStateChanged(evt);
            }
        });
        comboB_thang_tabBaoCao.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                comboB_thang_tabBaoCaoActionPerformed(evt);
            }
        });

        jLabel38.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jLabel38.setForeground(new java.awt.Color(255, 0, 51));
        jLabel38.setText("Tháng:");

        jLabel39.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jLabel39.setForeground(new java.awt.Color(255, 0, 51));
        jLabel39.setText("Năm:");

        comboB_nam_tabBaoCao.setFont(new java.awt.Font("Times New Roman", 1, 15)); // NOI18N
        comboB_nam_tabBaoCao.setModel(new javax.swing.DefaultComboBoxModel<>(new String[] { "Tất cả" }));
        comboB_nam_tabBaoCao.addItemListener(new java.awt.event.ItemListener() {
            public void itemStateChanged(java.awt.event.ItemEvent evt) {
                comboB_nam_tabBaoCaoItemStateChanged(evt);
            }
        });
        comboB_nam_tabBaoCao.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                comboB_nam_tabBaoCaoActionPerformed(evt);
            }
        });

        javax.swing.GroupLayout jPanel2Layout = new javax.swing.GroupLayout(jPanel2);
        jPanel2.setLayout(jPanel2Layout);
        jPanel2Layout.setHorizontalGroup(
            jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addComponent(jSeparator5, javax.swing.GroupLayout.Alignment.TRAILING)
            .addGroup(jPanel2Layout.createSequentialGroup()
                .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(jPanel2Layout.createSequentialGroup()
                        .addGap(51, 51, 51)
                        .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addComponent(jLabel37)
                            .addGroup(jPanel2Layout.createSequentialGroup()
                                .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
                                    .addComponent(jLabel33)
                                    .addComponent(jLabel31)
                                    .addComponent(jLabel30)
                                    .addComponent(jLabel35))
                                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                                .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                    .addComponent(label_soLuongKhachhang_tabBaoCao)
                                    .addComponent(label_sanLuongTieuThu_tabBaoCao)
                                    .addComponent(label_doanhThu_tabBaoCao)
                                    .addGroup(jPanel2Layout.createSequentialGroup()
                                        .addComponent(jLabel38)
                                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                                        .addComponent(comboB_thang_tabBaoCao, javax.swing.GroupLayout.PREFERRED_SIZE, 98, javax.swing.GroupLayout.PREFERRED_SIZE)
                                        .addGap(18, 18, 18)
                                        .addComponent(jLabel39)
                                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                                        .addComponent(comboB_nam_tabBaoCao, javax.swing.GroupLayout.PREFERRED_SIZE, 98, javax.swing.GroupLayout.PREFERRED_SIZE))))
                            .addComponent(jScrollPane5, javax.swing.GroupLayout.PREFERRED_SIZE, 1185, javax.swing.GroupLayout.PREFERRED_SIZE)))
                    .addGroup(jPanel2Layout.createSequentialGroup()
                        .addGap(593, 593, 593)
                        .addComponent(jLabel34)))
                .addContainerGap(322, Short.MAX_VALUE))
        );
        jPanel2Layout.setVerticalGroup(
            jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, jPanel2Layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(jLabel34)
                .addGap(18, 18, 18)
                .addComponent(jSeparator5, javax.swing.GroupLayout.PREFERRED_SIZE, 10, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, 11, Short.MAX_VALUE)
                .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                        .addComponent(comboB_nam_tabBaoCao, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addComponent(jLabel39))
                    .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                        .addComponent(jLabel35)
                        .addComponent(comboB_thang_tabBaoCao, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addComponent(jLabel38)))
                .addGap(32, 32, 32)
                .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jLabel30)
                    .addComponent(label_soLuongKhachhang_tabBaoCao))
                .addGap(35, 35, 35)
                .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jLabel31)
                    .addComponent(label_sanLuongTieuThu_tabBaoCao))
                .addGap(28, 28, 28)
                .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jLabel33)
                    .addComponent(label_doanhThu_tabBaoCao))
                .addGap(29, 29, 29)
                .addComponent(jLabel37)
                .addGap(21, 21, 21)
                .addComponent(jScrollPane5, javax.swing.GroupLayout.PREFERRED_SIZE, 291, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(44, 44, 44))
        );

        tab_QuanLy.addTab("Báo cáo thống kê", jPanel2);

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(tab_QuanLy, javax.swing.GroupLayout.DEFAULT_SIZE, 1563, Short.MAX_VALUE)
                .addContainerGap())
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addComponent(tab_QuanLy, javax.swing.GroupLayout.PREFERRED_SIZE, 697, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(0, 0, Short.MAX_VALUE))
        );

        pack();
    }// </editor-fold>//GEN-END:initComponents

    private void txt_timKiem_tabQLTienDienActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_txt_timKiem_tabQLTienDienActionPerformed
        
    }//GEN-LAST:event_txt_timKiem_tabQLTienDienActionPerformed

    private void button_themKhachHangActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_button_themKhachHangActionPerformed
        them_khachHang.openFrameThemKhachHang();
    }//GEN-LAST:event_button_themKhachHangActionPerformed

    private void button_themChiSoDienActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_button_themChiSoDienActionPerformed
        them_chiSoDien.openFrameThemChiSoDien();
    }//GEN-LAST:event_button_themChiSoDienActionPerformed

    private void jButton1ActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jButton1ActionPerformed
        showTabQLTienDien();        
    }//GEN-LAST:event_jButton1ActionPerformed

    private void tab_QuanLyStateChanged(javax.swing.event.ChangeEvent evt) {//GEN-FIRST:event_tab_QuanLyStateChanged
        
        int tabIndex = tab_QuanLy.getSelectedIndex();
        
        switch(tabIndex) {
            
            case 0: 
                showTabQLTienDien();
            break;
            
            case 1: 
                showTabQLChiSoDien();
            break;
            
            case 2: 
                showTabQLKhachHang();
            break;
            case 3: 
                showTabQLBacTienDien();
            break;
            case 4: 
                showTabBaoCao();
            break;
        }
    }//GEN-LAST:event_tab_QuanLyStateChanged

    private void button_lamMoiActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_button_lamMoiActionPerformed
        showTabQLKhachHang();
    }//GEN-LAST:event_button_lamMoiActionPerformed

    private void table_QLChiSoDienMouseClicked(java.awt.event.MouseEvent evt) {//GEN-FIRST:event_table_QLChiSoDienMouseClicked
        thongtin_chiSoDien.openFrameThongTinChiSoDien((String) table_QLChiSoDien.getModel().getValueAt(table_QLChiSoDien.getSelectedRow(), 1));        
    }//GEN-LAST:event_table_QLChiSoDienMouseClicked

    private void table_QLTienDienMouseClicked(java.awt.event.MouseEvent evt) {//GEN-FIRST:event_table_QLTienDienMouseClicked
        thongtin_tienDien.openFrameThongTinTienDien((String) table_QLTienDien.getModel().getValueAt(table_QLTienDien.getSelectedRow(), 1));
    }//GEN-LAST:event_table_QLTienDienMouseClicked

    private void jButton3ActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jButton3ActionPerformed
        
        int danhMucTimKiem = comboBox_danhMucTimKiem_tabQLTienDien.getSelectedIndex();
        String where = "1";
        
        if(txt_timKiem_tabQLTienDien.getText().length() != 0) {
            switch(danhMucTimKiem) {
                case 0:
                    where = "khachhang.idKH = " + txt_timKiem_tabQLTienDien.getText();
                break;
                case 1:
                    where = "soCongTo = " + txt_timKiem_tabQLTienDien.getText();
                break;
                case 2:
                    where = "hoTen LIKE '%" + txt_timKiem_tabQLTienDien.getText() + "%'";
                break;
                case 3:
                    where = "soDienThoai = " + txt_timKiem_tabQLTienDien.getText();
                break;
                case 4:
                    where = "soCMT = " + txt_timKiem_tabQLTienDien.getText();
                break;
            }

            table_tabQLTienDien(where);
        } else
            JOptionPane.showMessageDialog(this, "Bạn chưa nhập ô tìm kiếm", "Cảnh báo", JOptionPane.WARNING_MESSAGE);
    }//GEN-LAST:event_jButton3ActionPerformed

    private void comboBox_phuongCuTru_tabQLTienDienItemStateChanged(java.awt.event.ItemEvent evt) {//GEN-FIRST:event_comboBox_phuongCuTru_tabQLTienDienItemStateChanged
        
    }//GEN-LAST:event_comboBox_phuongCuTru_tabQLTienDienItemStateChanged

    private void comboBox_phuongCuTru_tabQLTienDienActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_comboBox_phuongCuTru_tabQLTienDienActionPerformed

        String tenPhuong = String.valueOf(comboBox_phuongCuTru_tabQLTienDien.getSelectedItem());
        String where = "1";
        
        if(tenPhuong != "Tất cả")
            where = "tenPhuong = '" + tenPhuong + "'";
        
        table_tabQLTienDien(where);
        
    }//GEN-LAST:event_comboBox_phuongCuTru_tabQLTienDienActionPerformed

    private void jButton16ActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jButton16ActionPerformed
        
        int danhMucTimKiem = comboBox_danhMucTimKiem_tabQLChiSoDien.getSelectedIndex();
        String where = "1";
        
        if(txt_timKiem_tabQLChiSoDien.getText().length() != 0) {
            switch(danhMucTimKiem) {
                case 0:
                    where = "khachhang.idKH = " + txt_timKiem_tabQLChiSoDien.getText();
                break;
                case 1:
                    where = "soCongTo = " + txt_timKiem_tabQLChiSoDien.getText();
                break;
                case 2:
                    where = "hoTen LIKE '%" + txt_timKiem_tabQLChiSoDien.getText() + "%'";
                break;
                case 3:
                    where = "soDienThoai = " + txt_timKiem_tabQLChiSoDien.getText();
                break;
                case 4:
                    where = "soCMT = " + txt_timKiem_tabQLChiSoDien.getText();
                break;
            }

            table_tabQLChiSoDien(where);
        } else
            JOptionPane.showMessageDialog(this, "Bạn chưa nhập ô tìm kiếm", "Cảnh báo", JOptionPane.WARNING_MESSAGE);
        
    }//GEN-LAST:event_jButton16ActionPerformed

    private void comboBox_phuongCuTru_tabQLChiSoDienActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_comboBox_phuongCuTru_tabQLChiSoDienActionPerformed
        
        String tenPhuong = String.valueOf(comboBox_phuongCuTru_tabQLChiSoDien.getSelectedItem());
        String where = "1";
        
        if(tenPhuong != "Tất cả")
            where = "tenPhuong = '" + tenPhuong + "'";
        
        table_tabQLChiSoDien(where);
        
    }//GEN-LAST:event_comboBox_phuongCuTru_tabQLChiSoDienActionPerformed

    private void jButton2ActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jButton2ActionPerformed
        showTabQLChiSoDien();
    }//GEN-LAST:event_jButton2ActionPerformed

    private void jButton17ActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jButton17ActionPerformed
        
        int danhMucTimKiem = comboBox_danhMucTimKiem_tabQLKhachHang.getSelectedIndex();
        String where = "1";
        
        if(txt_timKiem_tabQLKhachHang.getText().length() != 0) {
            switch(danhMucTimKiem) {
                case 0:
                    where = "khachhang.idKH = " + txt_timKiem_tabQLKhachHang.getText();
                break;
                case 1:
                    where = "soCongTo = " + txt_timKiem_tabQLKhachHang.getText();
                break;
                case 2:
                    where = "hoTen LIKE '%" + txt_timKiem_tabQLKhachHang.getText() + "%'";
                break;
                case 3:
                    where = "soDienThoai = " + txt_timKiem_tabQLKhachHang.getText();
                break;
                case 4:
                    where = "soCMT = " + txt_timKiem_tabQLKhachHang.getText();
                break;
            }

            table_tabQLKhachHang(where);
        } else
            JOptionPane.showMessageDialog(this, "Bạn chưa nhập ô tìm kiếm", "Cảnh báo", JOptionPane.WARNING_MESSAGE);
        
    }//GEN-LAST:event_jButton17ActionPerformed

    private void comboBox_phuongCuTru_tabQLKhachHangActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_comboBox_phuongCuTru_tabQLKhachHangActionPerformed
        
        String tenPhuong = String.valueOf(comboBox_phuongCuTru_tabQLKhachHang.getSelectedItem());
        String where = "1";
        
        if(tenPhuong != "Tất cả")
            where = "tenPhuong = '" + tenPhuong + "'";
        
        table_tabQLKhachHang(where);
        
    }//GEN-LAST:event_comboBox_phuongCuTru_tabQLKhachHangActionPerformed

    private void comboBox_sapXep_tabQLKhachHangActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_comboBox_sapXep_tabQLKhachHangActionPerformed
        
        int sx = comboBox_sapXep_tabQLKhachHang.getSelectedIndex();
        String where = "1";

        switch(sx) {
            
            case 0:
                where = "1 order by idKH DESC";
            break;
            
            case 1:
                where = "1 order by idKH ASC";
            break;
            
        }
        
        table_tabQLKhachHang(where);
        
    }//GEN-LAST:event_comboBox_sapXep_tabQLKhachHangActionPerformed

    private void table_QLKhachHangMouseClicked(java.awt.event.MouseEvent evt) {//GEN-FIRST:event_table_QLKhachHangMouseClicked
        thongtin_khachHang.openFrameThongTinKH((String) table_QLKhachHang.getModel().getValueAt(table_QLKhachHang.getSelectedRow(), 1));
    }//GEN-LAST:event_table_QLKhachHangMouseClicked

    private void comboBox_loaiBacItemStateChanged(java.awt.event.ItemEvent evt) {//GEN-FIRST:event_comboBox_loaiBacItemStateChanged
        
        if(comboBox_loaiBac.getSelectedIndex() >= 0)
            showBacTienDien("bac = " + (comboBox_loaiBac.getSelectedIndex() + 1));
        else
            showBacTienDien("bac = 1");
        
    }//GEN-LAST:event_comboBox_loaiBacItemStateChanged

    private void button_suaHuyActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_button_suaHuyActionPerformed
        
        if(button_suaHuy.getText() == "Sửa") {
            
            button_suaHuy.setText("Hủy");
            button_luu.setEnabled(true);
            txt_gia.setEnabled(true);
            txt_min.setEnabled(true);
            txt_max.setEnabled(true);
            
        } else {
            
            button_suaHuy.setText("Sửa");
            button_luu.setEnabled(false);
            txt_gia.setEnabled(false);
            txt_min.setEnabled(false);
            txt_max.setEnabled(false);
            
        }
        
    }//GEN-LAST:event_button_suaHuyActionPerformed

    private void button_luuActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_button_luuActionPerformed
        
        if(Check_String.check(txt_gia.getText(), Check_String.NUMBER) && Check_String.check(txt_min.getText(), Check_String.NUMBER) && Check_String.check(txt_max.getText(), Check_String.NUMBER)) {
            
            if(JOptionPane.showConfirmDialog(this, "Bạn có chắc muốn sử không ?", "Xác nhận", JOptionPane.YES_OPTION) == JOptionPane.YES_OPTION) {
            
                DateFormat df = new SimpleDateFormat("yyyy-MM-dd");
                Calendar c = Calendar.getInstance();
                Date date = c.getTime();
                String ngaySua = df.format(date);

                DB_MySQL.update("bactiendien", new String[] {"gia", "min", "max", "ngaySua"}, new String[] {txt_gia.getText(), txt_min.getText(), txt_max.getText(), ngaySua}, "bac = " + String.valueOf(comboBox_loaiBac.getSelectedIndex() + 1));

                JOptionPane.showMessageDialog(this, "Cập nhật thành công !", "Thống báo", JOptionPane.INFORMATION_MESSAGE);

                button_suaHuy.setText("Sửa");
                button_luu.setEnabled(false);
                txt_gia.setEnabled(false);
                txt_min.setEnabled(false);
                txt_max.setEnabled(false);

            }
            
        } else
            JOptionPane.showMessageDialog(this, "Giá, số điện tối thiểu và tối đa phải là số !", "Cảnh báo", JOptionPane.WARNING_MESSAGE);
        
    }//GEN-LAST:event_button_luuActionPerformed

    private void jButton8ActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jButton8ActionPerformed
        
        showTabQLBacTienDien();
        
    }//GEN-LAST:event_jButton8ActionPerformed

    private void jButton6ActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jButton6ActionPerformed
        
        if(JOptionPane.showConfirmDialog(this, "Bạn có chắc muốn xóa chỉ số đang chọn ?", "Xác nhận", JOptionPane.YES_OPTION) == JOptionPane.YES_OPTION) {
            
            DB_MySQL.delete("bactiendien", "bac = " + String.valueOf(comboBox_loaiBac.getSelectedIndex() + 1));
            comboBox_loaiBac.removeItemAt(comboBox_loaiBac.getSelectedIndex());
            
            String data[][] = DB_MySQL.select(new String[] {"bac"}, "bactiendien", "1");
            
            int row = data.length;
            
            for(int i = 0; i < row; i++) {
                
                DB_MySQL.update("bactiendien", new String[] {"bac"}, new String[] {String.valueOf(i + 1)}, "bac = " + data[i][0]);
                
            }
            
            JOptionPane.showMessageDialog(this, "Xóa thành công !", "Thống báo", JOptionPane.INFORMATION_MESSAGE);
            
        }
        
        
    }//GEN-LAST:event_jButton6ActionPerformed

    private void jButton7ActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jButton7ActionPerformed
        
        them_bacTienDien.openFrameThemBacTienDien();
        
    }//GEN-LAST:event_jButton7ActionPerformed

    private void comboB_thang_tabBaoCaoItemStateChanged(java.awt.event.ItemEvent evt) {//GEN-FIRST:event_comboB_thang_tabBaoCaoItemStateChanged

    }//GEN-LAST:event_comboB_thang_tabBaoCaoItemStateChanged

    private void comboB_nam_tabBaoCaoItemStateChanged(java.awt.event.ItemEvent evt) {//GEN-FIRST:event_comboB_nam_tabBaoCaoItemStateChanged
        
    }//GEN-LAST:event_comboB_nam_tabBaoCaoItemStateChanged

    private void comboB_thang_tabBaoCaoActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_comboB_thang_tabBaoCaoActionPerformed
        displayThongTin_tabBaoCao();
    }//GEN-LAST:event_comboB_thang_tabBaoCaoActionPerformed

    private void comboB_nam_tabBaoCaoActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_comboB_nam_tabBaoCaoActionPerformed
        displayThongTin_tabBaoCao();
    }//GEN-LAST:event_comboB_nam_tabBaoCaoActionPerformed

    private void comboBox_loaiBacActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_comboBox_loaiBacActionPerformed
        // TODO add your handling code here:
    }//GEN-LAST:event_comboBox_loaiBacActionPerformed

    private void comboBox_danhMucTimKiem_tabQLKhachHangActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_comboBox_danhMucTimKiem_tabQLKhachHangActionPerformed
        // TODO add your handling code here:
    }//GEN-LAST:event_comboBox_danhMucTimKiem_tabQLKhachHangActionPerformed

    /**
     * @param args the command line arguments
     */
    public static void main(String args[]) {
        /* Set the Nimbus look and feel */
        //<editor-fold defaultstate="collapsed" desc=" Look and feel setting code (optional) ">
        /* If Nimbus (introduced in Java SE 6) is not available, stay with the default look and feel.
         * For details see http://download.oracle.com/javase/tutorial/uiswing/lookandfeel/plaf.html 
         */
        try {
            for (javax.swing.UIManager.LookAndFeelInfo info : javax.swing.UIManager.getInstalledLookAndFeels()) {
                if ("Nimbus".equals(info.getName())) {
                    javax.swing.UIManager.setLookAndFeel(info.getClassName());
                    break;
                }
            }
        } catch (ClassNotFoundException ex) {
            java.util.logging.Logger.getLogger(giaodien_main.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (InstantiationException ex) {
            java.util.logging.Logger.getLogger(giaodien_main.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (IllegalAccessException ex) {
            java.util.logging.Logger.getLogger(giaodien_main.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (javax.swing.UnsupportedLookAndFeelException ex) {
            java.util.logging.Logger.getLogger(giaodien_main.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        }
        //</editor-fold>
        //</editor-fold>

        /* Create and display the form */
        java.awt.EventQueue.invokeLater(new Runnable() {
            public void run() {
                new giaodien_main().setVisible(true);
            }
        });
    }

    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JButton button_lamMoi;
    private javax.swing.JButton button_luu;
    private javax.swing.JButton button_suaHuy;
    private javax.swing.JButton button_themChiSoDien;
    private javax.swing.JButton button_themKhachHang;
    private javax.swing.JComboBox<String> comboB_nam_tabBaoCao;
    private javax.swing.JComboBox<String> comboB_thang_tabBaoCao;
    private javax.swing.JComboBox<String> comboBox_danhMucTimKiem_tabQLChiSoDien;
    private javax.swing.JComboBox<String> comboBox_danhMucTimKiem_tabQLKhachHang;
    private javax.swing.JComboBox<String> comboBox_danhMucTimKiem_tabQLTienDien;
    private javax.swing.JComboBox<String> comboBox_loaiBac;
    private javax.swing.JComboBox<String> comboBox_phuongCuTru_tabQLChiSoDien;
    private javax.swing.JComboBox<String> comboBox_phuongCuTru_tabQLKhachHang;
    private javax.swing.JComboBox<String> comboBox_phuongCuTru_tabQLTienDien;
    private javax.swing.JComboBox<String> comboBox_sapXep_tabQLKhachHang;
    private javax.swing.JButton jButton1;
    private javax.swing.JButton jButton16;
    private javax.swing.JButton jButton17;
    private javax.swing.JButton jButton2;
    private javax.swing.JButton jButton3;
    private javax.swing.JButton jButton6;
    private javax.swing.JButton jButton7;
    private javax.swing.JButton jButton8;
    private javax.swing.JLabel jLabel1;
    private javax.swing.JLabel jLabel10;
    private javax.swing.JLabel jLabel11;
    private javax.swing.JLabel jLabel12;
    private javax.swing.JLabel jLabel13;
    private javax.swing.JLabel jLabel14;
    private javax.swing.JLabel jLabel15;
    private javax.swing.JLabel jLabel16;
    private javax.swing.JLabel jLabel17;
    private javax.swing.JLabel jLabel18;
    private javax.swing.JLabel jLabel19;
    private javax.swing.JLabel jLabel2;
    private javax.swing.JLabel jLabel20;
    private javax.swing.JLabel jLabel21;
    private javax.swing.JLabel jLabel22;
    private javax.swing.JLabel jLabel23;
    private javax.swing.JLabel jLabel24;
    private javax.swing.JLabel jLabel25;
    private javax.swing.JLabel jLabel26;
    private javax.swing.JLabel jLabel27;
    private javax.swing.JLabel jLabel28;
    private javax.swing.JLabel jLabel29;
    private javax.swing.JLabel jLabel3;
    private javax.swing.JLabel jLabel30;
    private javax.swing.JLabel jLabel31;
    private javax.swing.JLabel jLabel32;
    private javax.swing.JLabel jLabel33;
    private javax.swing.JLabel jLabel34;
    private javax.swing.JLabel jLabel35;
    private javax.swing.JLabel jLabel37;
    private javax.swing.JLabel jLabel38;
    private javax.swing.JLabel jLabel39;
    private javax.swing.JLabel jLabel4;
    private javax.swing.JLabel jLabel5;
    private javax.swing.JLabel jLabel6;
    private javax.swing.JLabel jLabel7;
    private javax.swing.JLabel jLabel8;
    private javax.swing.JLabel jLabel9;
    private javax.swing.JPanel jPanel2;
    private javax.swing.JPanel jPanel3;
    private javax.swing.JPanel jPanel4;
    private javax.swing.JPanel jPanel5;
    private javax.swing.JPanel jPanel6;
    private javax.swing.JScrollPane jScrollPane1;
    private javax.swing.JScrollPane jScrollPane2;
    private javax.swing.JScrollPane jScrollPane3;
    private javax.swing.JScrollPane jScrollPane4;
    private javax.swing.JScrollPane jScrollPane5;
    private javax.swing.JSeparator jSeparator1;
    private javax.swing.JSeparator jSeparator2;
    private javax.swing.JSeparator jSeparator3;
    private javax.swing.JSeparator jSeparator4;
    private javax.swing.JSeparator jSeparator5;
    private javax.swing.JLabel label_doanhThu_tabBaoCao;
    private javax.swing.JLabel label_namHienTai_tabQLChiSoDien;
    private javax.swing.JLabel label_namHienTai_tabQLTienDien;
    private javax.swing.JLabel label_ngaySua;
    private javax.swing.JLabel label_ngayTao;
    private javax.swing.JLabel label_sanLuongTieuThu_tabBaoCao;
    private javax.swing.JLabel label_soLuongKhachhang_tabBaoCao;
    private javax.swing.JLabel label_thangHienTai_tabQLChiSoDien;
    private javax.swing.JLabel label_thangHienTai_tabQLTienDien;
    private javax.swing.JTabbedPane tab_QuanLy;
    private javax.swing.JTable table_QLChiSoDien;
    private javax.swing.JTable table_QLKhachHang;
    private javax.swing.JTable table_QLTienDien;
    private javax.swing.JTable table_lichSuKhachHang_tabBaoCao;
    private javax.swing.JTable table_tabQLBacTienDien;
    private javax.swing.JTextField txt_gia;
    private javax.swing.JTextField txt_max;
    private javax.swing.JTextField txt_min;
    private javax.swing.JTextField txt_timKiem_tabQLChiSoDien;
    private javax.swing.JTextField txt_timKiem_tabQLKhachHang;
    private javax.swing.JTextField txt_timKiem_tabQLTienDien;
    // End of variables declaration//GEN-END:variables
}
