package com.example.finalproject.order;

public class OrderDetail {
    private String order_detail_id;
    private String order_Id;
    private String book_Id;
    private int order_Detail_Quantity;
    private double order_Detail_Amount;
    private double order_Detail_Price;

    public OrderDetail() {
    }

    public OrderDetail(String order_detail_id, String order_Id, String book_Id, int order_Detail_Quantity, double order_Detail_Amount, double order_Detail_Price) {
        this.order_detail_id = order_detail_id;
        this.order_Id = order_Id;
        this.book_Id = book_Id;
        this.order_Detail_Quantity = order_Detail_Quantity;
        this.order_Detail_Amount = order_Detail_Amount;
        this.order_Detail_Price = order_Detail_Price;
    }

    public String getOrder_detail_id() {
        return order_detail_id;
    }

    public void setOrder_detail_id(String order_detail_id) {
        this.order_detail_id = order_detail_id;
    }

    public String getOrder_Id() {
        return order_Id;
    }

    public void setOrder_Id(String order_Id) {
        this.order_Id = order_Id;
    }

    public String getBook_Id() {
        return book_Id;
    }

    public void setBook_Id(String book_Id) {
        this.book_Id = book_Id;
    }

    public int getOrder_Detail_Quantity() {
        return order_Detail_Quantity;
    }

    public void setOrder_Detail_Quantity(int order_Detail_Quantity) {
        this.order_Detail_Quantity = order_Detail_Quantity;
    }

    public double getOrder_Detail_Amount() {
        return order_Detail_Amount;
    }

    public void setOrder_Detail_Amount(double order_Detail_Amount) {
        this.order_Detail_Amount = order_Detail_Amount;
    }

    public double getOrder_Detail_Price() {
        return order_Detail_Price;
    }

    public void setOrder_Detail_Price(double order_Detail_Price) {
        this.order_Detail_Price = order_Detail_Price;
    }
}
