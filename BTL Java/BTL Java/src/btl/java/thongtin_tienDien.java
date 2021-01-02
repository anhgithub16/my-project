/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package btl.java;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import javax.swing.JOptionPane;

/**
 *
 * @author Ba _Tran
 */
public class thongtin_tienDien extends javax.swing.JFrame {
    
    private static thongtin_tienDien frameThongTinTienDien;
    
    public static void openFrameThongTinTienDien(String idKH) {
        frameThongTinTienDien = new thongtin_tienDien();
        frameThongTinTienDien.setVisible(true);
        frameThongTinTienDien.showThongTinPhieuThu(idKH);
    }
    
    private void showTable_phieuThuTienDien(float chiSoMoi, float chiSoCu, String cachTinh) {

        Display_JTable.setTable(table_phieuThuTienDien);
        Display_JTable.setColumn(new String[] {"Chỉ số mới", "Chỉ số cũ", "Số điện thiêu thụ", "Đơn giá", "Thành tiền"});
        
        /* 
        * Kiểm tra xem khách hàng được tính tiền điện theo bậc hay theo đơn giá
        * nếu biến cachTinh là '0' thì khách hàng tính theo bậc còn không thì
        * ngược lại
        */
        if(cachTinh.charAt(0) != '0') {
            
            float donGia = Float.parseFloat(cachTinh);
            float soDienTieuThu = chiSoMoi - chiSoCu;
            float tienTruocThue = (chiSoMoi - chiSoCu) * donGia;
            float tienThue = (float) (tienTruocThue * 0.1);
            float tongTien = tienTruocThue + tienThue;
            
            Display_JTable.defaultTableModel.addRow(new String[] {String.valueOf(chiSoMoi), String.valueOf(chiSoCu), String.valueOf(soDienTieuThu), String.valueOf(donGia), String.valueOf(tienTruocThue)});
            label_tongSoDienTieuThu.setText(String.valueOf(chiSoMoi - chiSoCu));
            label_tongTienChuaThue.setText(String.valueOf(tienTruocThue));
            label_tienThue.setText(String.valueOf(tienThue));
            label_tongTienThanhToan.setText(String.valueOf(tongTien));
            
        } else {
            
            int i,j = 0;
            float soDienTieuThu = chiSoMoi - chiSoCu;
            
            
            // Lấy ra mản 2 chiều gồm giá và khoảng số điện theo từng bậc
            String bacThuTienDien[][] = DB_MySQL.select(new String[] {"gia", "min", "max"}, "bactiendien", "1");
            
            int soBac = bacThuTienDien.length;
            float tienDienTheoBac[] = new float[soBac];
            float soDienTheoBac[] = new float[soBac];
            float tienDienTungBac;
            float soDienTungBac;
            float tongTien = 0;
            
            /*
            * Tách phần tiền điện theo từng bậc sang mảng 1 chiều
            * Tách phần khoảng số điện theo từng bậc sang mảng 2 chiều
            */
            for(i = 0; i < soBac; i++) {
                tienDienTheoBac[i] = Float.parseFloat(bacThuTienDien[i][0]);
                soDienTheoBac[i] = Float.parseFloat(bacThuTienDien[i][2]) - Float.parseFloat(bacThuTienDien[i][1]);
            }
            
            Display_JTable.defaultTableModel.addRow(new String[] {String.valueOf(chiSoMoi), String.valueOf(chiSoCu), String.valueOf(soDienTieuThu)});
            
            
            for(i = 0; i < soBac; i++) {
                
                if(soDienTieuThu > soDienTheoBac[i]){
                    soDienTieuThu -= soDienTheoBac[i];
                    tienDienTungBac = soDienTheoBac[i] * tienDienTheoBac[i];
                    soDienTungBac = soDienTheoBac[i];
                } else {
                    tienDienTungBac = soDienTieuThu * tienDienTheoBac[i];
                    soDienTungBac = soDienTieuThu;
                    j = i;
                    i = soBac;
                }
                
                tongTien += tienDienTungBac;
                Display_JTable.defaultTableModel.addRow(new String[] {"", "", String.valueOf(soDienTungBac), String.valueOf(tienDienTheoBac[j]), String.valueOf(tienDienTungBac)});
                    
                      
            }
            
            float tienThue = (float) (tongTien * 0.1);
            
            label_tongSoDienTieuThu.setText(String.valueOf(chiSoMoi - chiSoCu));
            label_tongTienChuaThue.setText(String.valueOf(tongTien));
            label_tienThue.setText(String.valueOf(tienThue));
            label_tongTienThanhToan.setText(String.valueOf(tongTien + tienThue));
            
        }
       
    }
    
    private void showThongTinPhieuThu(String idKH) {
        String table = "khachhang, chisodien, datenhapchiso";
        String columns[] = new String[] {"hoTen", "gioiTinh", "soCMT", "soDienThoai", "diaChi", "tenPhuong", "soCongTo", "donGia","chisodien.chiSoMoi", "chisodien.chiSoCu", "datenhapchiso.chiSoMoi"};
        String where = "(khachhang.idKH = chisodien.idKH AND chisodien.idKH = datenhapchiso.idKH) AND khachhang.idKH = " + idKH;
        
        String data[][] = DB_MySQL.select(columns, table, where);
        
        label_hoTenKhachHang.setText(data[0][0]);
        label_maKH.setText(idKH);
        label_soCongTo.setText(data[0][6]);
        label_gioiTinh.setText(data[0][1]);
        label_soCMT.setText(data[0][2]);
        label_soDienThoai.setText(data[0][3]);
        label_phuongCuTru.setText(data[0][5]);
        label_diaChi.setText(data[0][4]);
        label_thoiGianThu.setText("2020-06-10");
        
        String cachTinhTienDien = "Theo bậc";
        if(data[0][7].charAt(0) != '0')
            cachTinhTienDien = data[0][7] + " VNĐ/Số";
        
        label_cachTinhTienDien.setText(cachTinhTienDien);
        
        if(data[0][8].charAt(0) != '0' && data[0][10] != "0000-00-00") {
            showTable_phieuThuTienDien(Float.parseFloat(data[0][8]), Float.parseFloat(data[0][9]), data[0][7]);
        } else {
            JOptionPane.showMessageDialog(this, "Khách hàng chưa có chỉ số điện mới !", "Cảnh báo", JOptionPane.WARNING_MESSAGE);
        }
        
    }

    /**
     * Creates new form thongtin_chiSoDien
     */
    public thongtin_tienDien() {
        initComponents();
    }

    /**
     * This method is called from within the constructor to initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is always
     * regenerated by the Form Editor.
     */
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        jLabel19 = new javax.swing.JLabel();
        jSeparator3 = new javax.swing.JSeparator();
        jLabel1 = new javax.swing.JLabel();
        jLabel2 = new javax.swing.JLabel();
        jLabel3 = new javax.swing.JLabel();
        jSeparator1 = new javax.swing.JSeparator();
        jLabel4 = new javax.swing.JLabel();
        label_hoTenKhachHang = new javax.swing.JLabel();
        jLabel6 = new javax.swing.JLabel();
        jLabel7 = new javax.swing.JLabel();
        label_gioiTinh = new javax.swing.JLabel();
        label_soDienThoai = new javax.swing.JLabel();
        jLabel10 = new javax.swing.JLabel();
        label_phuongCuTru = new javax.swing.JLabel();
        jLabel12 = new javax.swing.JLabel();
        label_diaChi = new javax.swing.JLabel();
        jLabel14 = new javax.swing.JLabel();
        label_maKH = new javax.swing.JLabel();
        jLabel16 = new javax.swing.JLabel();
        label_soCMT = new javax.swing.JLabel();
        jScrollPane1 = new javax.swing.JScrollPane();
        table_phieuThuTienDien = new javax.swing.JTable();
        jLabel18 = new javax.swing.JLabel();
        label_soCongTo = new javax.swing.JLabel();
        jLabel21 = new javax.swing.JLabel();
        label_thoiGianThu = new javax.swing.JLabel();
        jLabel23 = new javax.swing.JLabel();
        label_cachTinhTienDien = new javax.swing.JLabel();
        jLabel25 = new javax.swing.JLabel();
        jLabel26 = new javax.swing.JLabel();
        jLabel27 = new javax.swing.JLabel();
        label_tongSoDienTieuThu = new javax.swing.JLabel();
        label_tienThue = new javax.swing.JLabel();
        label_tongTienChuaThue = new javax.swing.JLabel();
        label_tongTienThanhToan = new javax.swing.JLabel();
        jSeparator4 = new javax.swing.JSeparator();
        jSeparator5 = new javax.swing.JSeparator();
        button_thuTien_frameThongTinTienDien = new javax.swing.JButton();
        jButton2 = new javax.swing.JButton();

        setDefaultCloseOperation(javax.swing.WindowConstants.EXIT_ON_CLOSE);

        jLabel19.setFont(new java.awt.Font("Times New Roman", 1, 24)); // NOI18N
        jLabel19.setHorizontalAlignment(javax.swing.SwingConstants.CENTER);
        jLabel19.setText("Phiếu thu tiền điện");

        jLabel1.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jLabel1.setText("Công ty điện lực Nam Từ Liêm - Hà Nội");

        jLabel2.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jLabel2.setText("Địa chỉ: Tổ dân phố số 5 Mễ Trì Hạ, Phường Mễ Trì, Quận Nam Từ Liêm, Thành phố Hà Nội");

        jLabel3.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jLabel3.setText("Số điện thoại: 024.37653950");

        jLabel4.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jLabel4.setText("Họ tên khách hàng:");

        label_hoTenKhachHang.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        label_hoTenKhachHang.setText("...");

        jLabel6.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jLabel6.setText("Số điện thoại:");

        jLabel7.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jLabel7.setText("Giới tính:");

        label_gioiTinh.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        label_gioiTinh.setText("...");

        label_soDienThoai.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        label_soDienThoai.setText("...");

        jLabel10.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jLabel10.setText("Phường cư trú:");

        label_phuongCuTru.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        label_phuongCuTru.setText("...");

        jLabel12.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jLabel12.setText("Địa chỉ:");

        label_diaChi.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        label_diaChi.setText("...");

        jLabel14.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jLabel14.setText("Mã khách hàng:");

        label_maKH.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        label_maKH.setText("...");

        jLabel16.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jLabel16.setText("Số chứng minh thư/căn cước:");

        label_soCMT.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        label_soCMT.setText("...");

        table_phieuThuTienDien.setModel(new javax.swing.table.DefaultTableModel(
            new Object [][] {
                {null, null, null, null, null},
                {null, null, null, null, null},
                {null, null, null, null, null},
                {null, null, null, null, null}
            },
            new String [] {
                "Chỉ số mới", "Chỉ số cũ", "Số điện tiêu thụ", "Đơn giá", "Thành tiền"
            }
        ));
        jScrollPane1.setViewportView(table_phieuThuTienDien);

        jLabel18.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jLabel18.setText("Số công tơ điện:");

        label_soCongTo.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        label_soCongTo.setText("...");

        jLabel21.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jLabel21.setText("Thời gian thu:");

        label_thoiGianThu.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        label_thoiGianThu.setText("...");

        jLabel23.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jLabel23.setText("Cách thức tính tiền điện: ");

        label_cachTinhTienDien.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        label_cachTinhTienDien.setText("...");

        jLabel25.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jLabel25.setText("Cộng:");

        jLabel26.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jLabel26.setText("Thuế GTGT 10%:");

        jLabel27.setFont(new java.awt.Font("Times New Roman", 1, 18)); // NOI18N
        jLabel27.setText("Tổng tiền thanh toán:");

        label_tongSoDienTieuThu.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        label_tongSoDienTieuThu.setText("...");

        label_tienThue.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        label_tienThue.setText("...");

        label_tongTienChuaThue.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        label_tongTienChuaThue.setText("...");

        label_tongTienThanhToan.setFont(new java.awt.Font("Times New Roman", 0, 18)); // NOI18N
        label_tongTienThanhToan.setText("...");

        button_thuTien_frameThongTinTienDien.setText("Thu tiền");
        button_thuTien_frameThongTinTienDien.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                button_thuTien_frameThongTinTienDienActionPerformed(evt);
            }
        });

        jButton2.setText("Đóng");
        jButton2.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jButton2ActionPerformed(evt);
            }
        });

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addComponent(jSeparator3, javax.swing.GroupLayout.Alignment.TRAILING)
            .addGroup(layout.createSequentialGroup()
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
                    .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                        .addGroup(layout.createSequentialGroup()
                            .addGap(495, 495, 495)
                            .addComponent(jLabel19))
                        .addGroup(layout.createSequentialGroup()
                            .addGap(42, 42, 42)
                            .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                .addComponent(jSeparator1, javax.swing.GroupLayout.PREFERRED_SIZE, 1163, javax.swing.GroupLayout.PREFERRED_SIZE)
                                .addComponent(jLabel2)
                                .addComponent(jLabel1)
                                .addComponent(jLabel3)
                                .addGroup(layout.createSequentialGroup()
                                    .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                        .addGroup(layout.createSequentialGroup()
                                            .addComponent(jLabel4)
                                            .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                                            .addComponent(label_hoTenKhachHang))
                                        .addGroup(layout.createSequentialGroup()
                                            .addComponent(jLabel18)
                                            .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                                            .addComponent(label_soCongTo))
                                        .addGroup(layout.createSequentialGroup()
                                            .addComponent(jLabel14)
                                            .addGap(18, 18, 18)
                                            .addComponent(label_maKH))
                                        .addGroup(layout.createSequentialGroup()
                                            .addComponent(jLabel7)
                                            .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                                            .addComponent(label_gioiTinh))
                                        .addGroup(layout.createSequentialGroup()
                                            .addComponent(jLabel16)
                                            .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                                            .addComponent(label_soCMT)))
                                    .addGap(170, 170, 170)
                                    .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                        .addGroup(layout.createSequentialGroup()
                                            .addComponent(jLabel23)
                                            .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                                            .addComponent(label_cachTinhTienDien))
                                        .addGroup(layout.createSequentialGroup()
                                            .addComponent(jLabel6)
                                            .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                                            .addComponent(label_soDienThoai))
                                        .addGroup(layout.createSequentialGroup()
                                            .addComponent(jLabel10)
                                            .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                                            .addComponent(label_phuongCuTru))
                                        .addGroup(layout.createSequentialGroup()
                                            .addComponent(jLabel12)
                                            .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                                            .addComponent(label_diaChi))
                                        .addGroup(layout.createSequentialGroup()
                                            .addComponent(jLabel21)
                                            .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                                            .addComponent(label_thoiGianThu))))
                                .addComponent(jScrollPane1, javax.swing.GroupLayout.PREFERRED_SIZE, 1163, javax.swing.GroupLayout.PREFERRED_SIZE)))
                        .addGroup(layout.createSequentialGroup()
                            .addGap(334, 334, 334)
                            .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
                                .addComponent(jLabel26)
                                .addComponent(jLabel25)
                                .addComponent(jLabel27))
                            .addGap(35, 35, 35)
                            .addComponent(label_tongSoDienTieuThu)
                            .addGap(437, 437, 437)
                            .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                .addComponent(label_tienThue)
                                .addComponent(label_tongTienChuaThue)
                                .addComponent(label_tongTienThanhToan)))
                        .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, layout.createSequentialGroup()
                            .addContainerGap()
                            .addComponent(jSeparator4, javax.swing.GroupLayout.PREFERRED_SIZE, 892, javax.swing.GroupLayout.PREFERRED_SIZE)))
                    .addComponent(jSeparator5, javax.swing.GroupLayout.PREFERRED_SIZE, 892, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addGroup(javax.swing.GroupLayout.Alignment.LEADING, layout.createSequentialGroup()
                        .addGap(428, 428, 428)
                        .addComponent(button_thuTien_frameThongTinTienDien, javax.swing.GroupLayout.PREFERRED_SIZE, 117, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addGap(72, 72, 72)
                        .addComponent(jButton2, javax.swing.GroupLayout.PREFERRED_SIZE, 121, javax.swing.GroupLayout.PREFERRED_SIZE)))
                .addContainerGap(44, Short.MAX_VALUE))
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addGap(18, 18, 18)
                .addComponent(jLabel19)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                .addComponent(jSeparator3, javax.swing.GroupLayout.PREFERRED_SIZE, 10, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(18, 18, 18)
                .addComponent(jLabel1)
                .addGap(18, 18, 18)
                .addComponent(jLabel2)
                .addGap(18, 18, 18)
                .addComponent(jLabel3)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                .addComponent(jSeparator1, javax.swing.GroupLayout.PREFERRED_SIZE, 10, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(layout.createSequentialGroup()
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(jLabel4)
                            .addComponent(label_hoTenKhachHang))
                        .addGap(18, 18, 18)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(jLabel14)
                            .addComponent(label_maKH))
                        .addGap(18, 18, 18)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(jLabel18)
                            .addComponent(label_soCongTo))
                        .addGap(18, 18, 18)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(jLabel7)
                            .addComponent(label_gioiTinh))
                        .addGap(18, 18, 18)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(jLabel16)
                            .addComponent(label_soCMT)
                            .addComponent(jLabel23)
                            .addComponent(label_cachTinhTienDien)))
                    .addGroup(layout.createSequentialGroup()
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(jLabel6)
                            .addComponent(label_soDienThoai))
                        .addGap(18, 18, 18)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(jLabel10)
                            .addComponent(label_phuongCuTru))
                        .addGap(18, 18, 18)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(jLabel12)
                            .addComponent(label_diaChi))
                        .addGap(18, 18, 18)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(jLabel21)
                            .addComponent(label_thoiGianThu))))
                .addGap(34, 34, 34)
                .addComponent(jScrollPane1, javax.swing.GroupLayout.PREFERRED_SIZE, 144, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jLabel25)
                    .addComponent(label_tongSoDienTieuThu)
                    .addComponent(label_tongTienChuaThue))
                .addGap(3, 3, 3)
                .addComponent(jSeparator4, javax.swing.GroupLayout.PREFERRED_SIZE, 10, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(5, 5, 5)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jLabel26)
                    .addComponent(label_tienThue))
                .addGap(3, 3, 3)
                .addComponent(jSeparator5, javax.swing.GroupLayout.PREFERRED_SIZE, 10, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(5, 5, 5)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(label_tongTienThanhToan)
                    .addComponent(jLabel27))
                .addGap(45, 45, 45)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                    .addComponent(jButton2, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(button_thuTien_frameThongTinTienDien, javax.swing.GroupLayout.PREFERRED_SIZE, 43, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addContainerGap(63, Short.MAX_VALUE))
        );

        pack();
    }// </editor-fold>//GEN-END:initComponents

    private void jButton2ActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jButton2ActionPerformed
        frameThongTinTienDien.setVisible(false);
    }//GEN-LAST:event_jButton2ActionPerformed

    private void button_thuTien_frameThongTinTienDienActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_button_thuTien_frameThongTinTienDienActionPerformed
        
        String idKH = label_maKH.getText();
        
        if(DB_MySQL.select(new String[] {"chiSoMoi"}, "chisodien", "idKH = " + idKH)[0][0].charAt(0) != '0' && DB_MySQL.select(new String[] {"chiSoMoi"}, "datenhapchiso", "idKH = " + idKH)[0][0] != "0000-00-00") {
        
            if(JOptionPane.showConfirmDialog(this, "Bạn có chắc muốn thanh toán !", "Cảnh báo", JOptionPane.YES_NO_OPTION) == JOptionPane.YES_OPTION) {

                DateFormat df = new SimpleDateFormat("yyyy-MM-dd");
                Calendar c = Calendar.getInstance();
                Date date = c.getTime();

                String tables = "chisodien, datenhapchiso";
                String columns[] = new String[] {"chisodien.chiSoMoi", "chisodien.chiSoCu", "datenhapchiso.chiSoMoi"};
                String where = "chisodien.idKH = datenhapchiso.idKH AND chisodien.idKH = " + idKH;

                String data[][] = DB_MySQL.select(columns, tables, where);

                String chiSoMoi = data[0][0];
                String dateChiSoMoi = data[0][2];
                String chiSoCu = data[0][1];

                DB_MySQL.update("chisodien", new String[] {"chiSoMoi", "chiSoCu"}, new String[] {"0", chiSoMoi}, "idKH = " + idKH);
                DB_MySQL.update("datenhapchiso", new String[] {"chiSoMoi", "chiSoCu"}, new String[] {"0000-00-00", dateChiSoMoi}, "idKH = " + idKH);
                DB_MySQL.insert("lichsugiaodich", new String[] {"idKH", "thangNam", "chiSoMoi", "chiSoCu", "tienThanhToan", "dateThanhToan"}, new String[] {idKH, dateChiSoMoi, chiSoMoi, chiSoCu, label_tongTienThanhToan.getText(), df.format(date)});
                
                JOptionPane.showMessageDialog(this, "Thanh toán tiền điện thành công !", "Thông báo", JOptionPane.INFORMATION_MESSAGE);
                button_thuTien_frameThongTinTienDien.setEnabled(false);

            }
        } else {
            JOptionPane.showMessageDialog(this, "Khách hàng chưa có chỉ số điện mới nên không thể thanh toán !", "Cảnh báo", JOptionPane.WARNING_MESSAGE);
        }
        
    }//GEN-LAST:event_button_thuTien_frameThongTinTienDienActionPerformed

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
            java.util.logging.Logger.getLogger(thongtin_tienDien.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (InstantiationException ex) {
            java.util.logging.Logger.getLogger(thongtin_tienDien.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (IllegalAccessException ex) {
            java.util.logging.Logger.getLogger(thongtin_tienDien.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (javax.swing.UnsupportedLookAndFeelException ex) {
            java.util.logging.Logger.getLogger(thongtin_tienDien.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        }
        //</editor-fold>
        //</editor-fold>
        /* Create and display the form */
        java.awt.EventQueue.invokeLater(new Runnable() {
            public void run() {
                new thongtin_tienDien().setVisible(true);
            }
        });
    }

    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JButton button_thuTien_frameThongTinTienDien;
    private javax.swing.JButton jButton2;
    private javax.swing.JLabel jLabel1;
    private javax.swing.JLabel jLabel10;
    private javax.swing.JLabel jLabel12;
    private javax.swing.JLabel jLabel14;
    private javax.swing.JLabel jLabel16;
    private javax.swing.JLabel jLabel18;
    private javax.swing.JLabel jLabel19;
    private javax.swing.JLabel jLabel2;
    private javax.swing.JLabel jLabel21;
    private javax.swing.JLabel jLabel23;
    private javax.swing.JLabel jLabel25;
    private javax.swing.JLabel jLabel26;
    private javax.swing.JLabel jLabel27;
    private javax.swing.JLabel jLabel3;
    private javax.swing.JLabel jLabel4;
    private javax.swing.JLabel jLabel6;
    private javax.swing.JLabel jLabel7;
    private javax.swing.JScrollPane jScrollPane1;
    private javax.swing.JSeparator jSeparator1;
    private javax.swing.JSeparator jSeparator3;
    private javax.swing.JSeparator jSeparator4;
    private javax.swing.JSeparator jSeparator5;
    private javax.swing.JLabel label_cachTinhTienDien;
    private javax.swing.JLabel label_diaChi;
    private javax.swing.JLabel label_gioiTinh;
    private javax.swing.JLabel label_hoTenKhachHang;
    private javax.swing.JLabel label_maKH;
    private javax.swing.JLabel label_phuongCuTru;
    private javax.swing.JLabel label_soCMT;
    private javax.swing.JLabel label_soCongTo;
    private javax.swing.JLabel label_soDienThoai;
    private javax.swing.JLabel label_thoiGianThu;
    private javax.swing.JLabel label_tienThue;
    private javax.swing.JLabel label_tongSoDienTieuThu;
    private javax.swing.JLabel label_tongTienChuaThue;
    private javax.swing.JLabel label_tongTienThanhToan;
    private javax.swing.JTable table_phieuThuTienDien;
    // End of variables declaration//GEN-END:variables
}
