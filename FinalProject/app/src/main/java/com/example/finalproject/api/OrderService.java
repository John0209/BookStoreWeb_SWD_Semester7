package com.example.finalproject.api;

import com.example.finalproject.BookRecycleView;
import com.example.finalproject.login.LoginRequest;
import com.example.finalproject.login.LoginResponse;
import com.example.finalproject.order.Order;
import com.example.finalproject.order.OrderDetail;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.POST;

public interface OrderService {

    @POST("api/order/createOrder/")
    Call<String> createOrder(@Body Order order);

    @POST("api/orderDetail/createOrderDetail/")
    Call<String> createOrderDetail(@Body OrderDetail orderDetail);

    @GET("api/order/getOrderIdJustCreated")
    Call<String> getOrderIdJustCreated();
}
