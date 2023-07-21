package com.example.finalproject.Register;

import com.example.finalproject.api.ApiClient;
import com.example.finalproject.api.UserService;


public class UserRepository {
    public static UserService getUserService(){
        return ApiClient.getRetrofit().create(UserService.class);
    }
}
