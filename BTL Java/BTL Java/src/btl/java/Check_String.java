package btl.java;

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author Ba _Tran
 */
public class Check_String {
    
    final public static String EMAIL = "^([a-z0-9_\\.-]+)@([\\da-z\\.-]+)\\.([a-z\\.]{2,6})$";
    
    final public static String DATE__YYY_MM_DD = "\\d{4}[-]\\d{2}[-]\\d{2}";
    
    final public static String PHONE_NUMBER = "[0-9]{10}";
    
    final public static String STRING = "[^0-9]{1,}";
    
    final public static String NUMBER = "[0-9]{1,}";
    
    public static boolean check(String string, String regex) {
        
        if(regex != null && string != null) {
            
            return string.matches(regex);
            
        }
        
        return false;
        
    }
    
}
