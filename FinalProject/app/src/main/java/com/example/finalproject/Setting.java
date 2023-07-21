package com.example.finalproject;

import java.util.ArrayList;
import java.util.List;


import com.example.finalproject.Call.Call;
import com.example.finalproject.GMap.GoogleMaps;
import com.example.finalproject.GMap.ListStore;
import com.example.finalproject.Message.ChatActivity;
import com.google.android.material.bottomnavigation.BottomNavigationView;

import android.content.Intent;
import android.os.Bundle;
import android.view.MenuItem;
import android.view.View;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.ListView;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;

public class Setting extends AppCompatActivity {
    BottomNavigationView nav;
    ImageView backButton;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_setting);
        List<String> list = new ArrayList<>();
        list.add("List Of Stores");
        list.add("Contact With Us");
        list.add("Chat With Us");
        list.add("Store's Location");
        ListView listView = findViewById(R.id.listView);
        ArrayAdapter arrayAdapter = new ArrayAdapter(getApplicationContext(), android.R.layout.simple_list_item_1,list);
        listView.setAdapter(arrayAdapter);
        nav= findViewById(R.id.nav_bar);
        nav.setSelectedItemId(R.id.setting);
        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,
                WindowManager.LayoutParams.FLAG_FULLSCREEN);
        backButton = findViewById(R.id.btnBack); // Move this line inside onCreate()

        backButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                onBackPressed();
            }
        });


        nav.setOnNavigationItemSelectedListener(new BottomNavigationView.OnNavigationItemSelectedListener() {
            @Override
            public boolean onNavigationItemSelected(@NonNull MenuItem item) {
                if (item.getItemId() == R.id.home) {
                    startActivity(new Intent(Setting.this, MainPage.class));
                    return true;
                } else if (item.getItemId() == R.id.book) {
                    startActivity(new Intent(Setting.this, SeeMorePage.class));
                    return true;
                }
                else if (item.getItemId()==R.id.cart) {
                    startActivity(new Intent(Setting.this, CartActivity.class));
                    return true;
                }
                return false;
            }
        });
        listView.setOnItemClickListener(new AdapterView.OnItemClickListener(){
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                if(position==3){
                    Intent intent = new Intent(Setting.this, GoogleMaps.class);
                    startActivity(intent);
                }
                else if(position==2){
                    Intent intent = new Intent(Setting.this, ChatActivity.class);
                    startActivity(intent);
                }
                else if(position==1){
                    Intent intent = new Intent(Setting.this, Call.class);
                    startActivity(intent);
                }
                else if(position==0){
                    Intent intent = new Intent(Setting.this, ListStore.class);
                    startActivity(intent);
                }
        
            }
        });

    }
}