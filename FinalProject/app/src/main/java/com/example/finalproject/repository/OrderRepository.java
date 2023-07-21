package com.example.finalproject.repository;

import com.example.finalproject.api.ApiClient;
import com.example.finalproject.api.OrderService;

public class OrderRepository {

    public static OrderService getOrderService(){
        return ApiClient.getRetrofit().create(OrderService.class);
    }
}
