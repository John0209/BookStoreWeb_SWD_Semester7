package com.example.finalproject;

import java.io.Serializable;

public class BookRecycleView implements Serializable {
    private String book_Id;
    private String request_Id;
    private String category_Id;
    private String category_Name;
    private String image_URL;
    private String book_Title;
    private String book_Author;
    private String book_Description;
    private float book_Price;
    private int book_Quantity;
    private int book_Year_Public;
    private int book_ISBN;
    private Boolean is_Book_Status;

    public BookRecycleView() {
    }

    public String getBook_Id() {
        return book_Id;
    }

    public void setBook_Id(String book_Id) {
        this.book_Id = book_Id;
    }

    public String getRequest_Id() {
        return request_Id;
    }

    public void setRequest_Id(String request_Id) {
        this.request_Id = request_Id;
    }

    public String getCategory_Id() {
        return category_Id;
    }

    public void setCategory_Id(String category_Id) {
        this.category_Id = category_Id;
    }

    public String getCategory_Name() {
        return category_Name;
    }

    public void setCategory_Name(String category_Name) {
        this.category_Name = category_Name;
    }

    public String getImage_URL() {
        return image_URL;
    }

    public void setImage_URL(String image_URL) {
        this.image_URL = image_URL;
    }

    public String getBook_Title() {
        return book_Title;
    }

    public void setBook_Title(String book_Title) {
        this.book_Title = book_Title;
    }

    public String getBook_Author() {
        return book_Author;
    }

    public void setBook_Author(String book_Author) {
        this.book_Author = book_Author;
    }

    public String getBook_Description() {
        return book_Description;
    }

    public void setBook_Description(String book_Description) {
        this.book_Description = book_Description;
    }

    public float getBook_Price() {
        return book_Price;
    }

    public void setBook_Price(float book_Price) {
        this.book_Price = book_Price;
    }

    public int getBook_Quantity() {
        return book_Quantity;
    }

    public void setBook_Quantity(int book_Quantity) {
        this.book_Quantity = book_Quantity;
    }

    public int getBook_Year_Public() {
        return book_Year_Public;
    }

    public void setBook_Year_Public(int book_Year_Public) {
        this.book_Year_Public = book_Year_Public;
    }

    public int getBook_ISBN() {
        return book_ISBN;
    }

    public void setBook_ISBN(int book_ISBN) {
        this.book_ISBN = book_ISBN;
    }

    public Boolean getIs_Book_Status() {
        return is_Book_Status;
    }

    public void setIs_Book_Status(Boolean is_Book_Status) {
        this.is_Book_Status = is_Book_Status;
    }
}
