package com.example.finalproject.order;


public class Order {
    private String order_Id;
    private String user_Id;
    private String order_Customer_Name;
    private String order_Customer_Address;
    private String order_Customer_Phone;
    private String order_Date;
    private int order_Quantity;
    private float order_Amount;
    private int is_Order_Status;

    public Order() {
    }

    public Order(String order_Id, String user_Id, String order_Customer_Name, String order_Customer_Address, String order_Customer_Phone, String order_Date, int order_Quantity, float order_Amount, int is_Order_Status) {
        this.order_Id = order_Id;
        this.user_Id = user_Id;
        this.order_Customer_Name = order_Customer_Name;
        this.order_Customer_Address = order_Customer_Address;
        this.order_Customer_Phone = order_Customer_Phone;
        this.order_Date = order_Date;
        this.order_Quantity = order_Quantity;
        this.order_Amount = order_Amount;
        this.is_Order_Status = is_Order_Status;
    }

    public String getOrder_Id() {
        return order_Id;
    }

    public void setOrder_Id(String order_Id) {
        this.order_Id = order_Id;
    }

    public String getUser_Id() {
        return user_Id;
    }

    public void setUser_Id(String user_Id) {
        this.user_Id = user_Id;
    }

    public String getOrder_Customer_Name() {
        return order_Customer_Name;
    }

    public void setOrder_Customer_Name(String order_Customer_Name) {
        this.order_Customer_Name = order_Customer_Name;
    }

    public String getOrder_Customer_Address() {
        return order_Customer_Address;
    }

    public void setOrder_Customer_Address(String order_Customer_Address) {
        this.order_Customer_Address = order_Customer_Address;
    }

    public String getOrder_Customer_Phone() {
        return order_Customer_Phone;
    }

    public void setOrder_Customer_Phone(String order_Customer_Phone) {
        this.order_Customer_Phone = order_Customer_Phone;
    }

    public String getOrder_Date() {
        return order_Date;
    }

    public void setOrder_Date(String order_Date) {
        this.order_Date = order_Date;
    }

    public int getOrder_Quantity() {
        return order_Quantity;
    }

    public void setOrder_Quantity(int order_Quantity) {
        this.order_Quantity = order_Quantity;
    }

    public float getOrder_Amount() {
        return order_Amount;
    }

    public void setOrder_Amount(float order_Amount) {
        this.order_Amount = order_Amount;
    }

    public int isIs_Order_Status() {
        return is_Order_Status;
    }

    public void setIs_Order_Status(int is_Order_Status) {
        this.is_Order_Status = is_Order_Status;
    }
}
