package com.example.finalproject;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.GridLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.content.Intent;
import android.os.Bundle;
import android.view.MenuItem;
import android.view.WindowManager;

import com.example.finalproject.Register.UserRepository;
import com.example.finalproject.api.UserService;
import com.google.android.material.bottomnavigation.BottomNavigationView;

import java.util.ArrayList;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class SeeMorePage extends AppCompatActivity {
    UserService userService;
    ArrayList<BookRecycleView> bookList = new ArrayList<BookRecycleView>();
    private RecyclerView recyclerView;
    private BookAdapter bookAdapter;

    BottomNavigationView nav;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,
                WindowManager.LayoutParams.FLAG_FULLSCREEN);
        setContentView(R.layout.activity_see_more_page);
        userService = UserRepository.getUserService();
        recyclerView = findViewById(R.id.rcv_book);
        bookAdapter = new BookAdapter(this);
        GridLayoutManager gridLayoutManager = new GridLayoutManager(this,3);
        recyclerView.setLayoutManager(gridLayoutManager);
        nav= findViewById(R.id.nav_bar);
        nav.setSelectedItemId(R.id.book);
        nav.setOnNavigationItemSelectedListener(new BottomNavigationView.OnNavigationItemSelectedListener() {
            @Override
            public boolean onNavigationItemSelected(@NonNull MenuItem item) {
                if (item.getItemId() == R.id.home) {
                    startActivity(new Intent(SeeMorePage.this, MainPage.class));
                    return true;
                } else if (item.getItemId()==R.id.setting) {
                    startActivity(new Intent(SeeMorePage.this, Setting.class));
                    return true;
                }
                else if (item.getItemId()==R.id.cart) {
                    startActivity(new Intent(SeeMorePage.this, CartActivity.class));
                    return true;
                }
                return false;
            }
        });
        userService.getAllBook().enqueue(new Callback<BookRecycleView[]>() {
            @Override
            public void onResponse(Call<BookRecycleView[]> call, Response<BookRecycleView[]> response) {

                BookRecycleView[] books = response.body();
                for(BookRecycleView book : books){
                    bookList.add(book);
                }
                bookAdapter.setData(bookList);
                recyclerView.setAdapter(bookAdapter);
            }

            @Override
            public void onFailure(Call<BookRecycleView[]> call, Throwable t) {

            }
        });

    }
}