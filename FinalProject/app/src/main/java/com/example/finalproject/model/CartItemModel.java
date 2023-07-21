package com.example.finalproject.model;

public class CartItemModel {
    private String id;
    private int number;
    private String name;
    private String cover;
    private double price;

    public CartItemModel(String id, int number, String name, String cover, double price) {
        this.id = id;
        this.number = number;
        this.name = name;
        this.cover = cover;
        this.price = price;
    }

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public int getNumber() {
        return number;
    }

    public void setNumber(int number) {
        this.number = number;
    }

    public String getName() {
        return name;
    }

    public String getCover() {
        return cover;
    }

    public double getPrice() {
        return price;
    }
}