package com.example.finalproject.model;

import java.util.List;

public class CartModel {
    private int totalNumber;
    private float totalPrice;
    private List<CartItemModel> itemModels;

    public CartModel() {

    }

    public int getTotalNumber() {
        totalNumber = 0;
        itemModels.forEach(item -> totalNumber += item.getNumber());
        return totalNumber;
    }

    public float getTotalPrice() {
        totalPrice = 0;
        itemModels.forEach(item -> totalPrice += item.getNumber() * item.getPrice());
        return totalPrice;
    }

    public List<CartItemModel> getItemModels() {
        return itemModels;
    }

    public void setItemModels(List<CartItemModel> itemModels) {
        this.itemModels = itemModels;
    }

    public CartModel(List<CartItemModel> itemModels) {
        this.itemModels = itemModels;
        getTotalNumber();
        getTotalPrice();
    }

    public boolean addNewItemToCart(CartItemModel newItem){
        for (CartItemModel item: itemModels) {
            if(item.getId().equals(newItem.getId())){
                return false;
            }
        }
        itemModels.add(newItem);
        totalNumber +=1;
        totalPrice +=newItem.getPrice();
        return true;
    }
}