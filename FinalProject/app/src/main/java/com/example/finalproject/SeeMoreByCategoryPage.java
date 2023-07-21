package com.example.finalproject;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.GridLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.content.Intent;
import android.os.Bundle;
import android.view.MenuItem;
import android.view.WindowManager;

import com.google.android.material.bottomnavigation.BottomNavigationView;

import java.util.ArrayList;

public class SeeMoreByCategoryPage extends AppCompatActivity {
    ArrayList<BookRecycleView> books = new ArrayList<>();
    RecyclerView recyclerView;
    BottomNavigationView nav;
    BookAdapter bookAdapter;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,
                WindowManager.LayoutParams.FLAG_FULLSCREEN);
        setContentView(R.layout.activity_see_more_page);
        GridLayoutManager gridLayoutManager = new GridLayoutManager(this,3);
        recyclerView = findViewById(R.id.rcv_book);
        bookAdapter = new BookAdapter(this);
        recyclerView.setLayoutManager(gridLayoutManager);

        // Bottom navigation Bar
        nav= findViewById(R.id.nav_bar);
        nav.setOnNavigationItemSelectedListener(new BottomNavigationView.OnNavigationItemSelectedListener() {
            @Override
            public boolean onNavigationItemSelected(@NonNull MenuItem item) {
                if (item.getItemId() == R.id.home) {
                    startActivity(new Intent(SeeMoreByCategoryPage.this, MainPage.class));
                    return true;
                } else if (item.getItemId()==R.id.book) {
                    startActivity(new Intent(SeeMoreByCategoryPage.this, SeeMorePage.class));
                    return true;
                } else if (item.getItemId()==R.id.setting) {
                    startActivity(new Intent(SeeMoreByCategoryPage.this, Setting.class));
                    return true;
                }
                return false;
            }
        });

        // Get book list from bundle to set on bookAdapter
        Bundle bundle = getIntent().getExtras();
        books = (ArrayList<BookRecycleView>) bundle.get("listBookByCategory");
        bookAdapter.setData(books);
        recyclerView.setAdapter(bookAdapter);
    }
}