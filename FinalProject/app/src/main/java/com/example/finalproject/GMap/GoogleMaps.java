package com.example.finalproject.GMap;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.MenuItem;
import android.view.View;
import android.view.WindowManager;
import android.widget.ImageView;

import com.example.finalproject.MainPage;
import com.example.finalproject.R;
import com.example.finalproject.Setting;
import com.google.android.gms.maps.CameraUpdateFactory;
import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.OnMapReadyCallback;
import com.google.android.gms.maps.SupportMapFragment;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.MarkerOptions;
import com.google.android.material.bottomnavigation.BottomNavigationView;


public class GoogleMaps extends AppCompatActivity implements OnMapReadyCallback {
    private GoogleMap myMap;
    BottomNavigationView nav;
    ImageView backButton;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_google_map);
        SupportMapFragment mapFragment = (SupportMapFragment) getSupportFragmentManager()
                .findFragmentById(R.id.map);
        mapFragment.getMapAsync(this);
        backButton = findViewById(R.id.btnBack); // Move this line inside onCreate()

        backButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                onBackPressed();
            }
        });
        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,
                WindowManager.LayoutParams.FLAG_FULLSCREEN);
        nav= findViewById(R.id.nav_bar);
        nav.setSelectedItemId(R.id.setting);
        nav.setOnNavigationItemSelectedListener(new BottomNavigationView.OnNavigationItemSelectedListener() {
        @Override
        public boolean onNavigationItemSelected(@NonNull MenuItem item) {
            if (item.getItemId() == R.id.home) {
                startActivity(new Intent(GoogleMaps.this, MainPage.class));
                return true;
            }
            else if (item.getItemId()==R.id.setting){
                startActivity(new Intent(GoogleMaps.this, Setting.class));
                return true;
            }
            return false;
        }
    });
}

    @Override
    public void onMapReady(@NonNull GoogleMap googleMap) {
        myMap = googleMap;
        // set mức zoom từ 2.0 tới 21.0
        float zoomLevel = 15.0f;
        LatLng store = new LatLng(10.8421301, 106.8100118);
        myMap.addMarker(new MarkerOptions().position(store).title("Store"));
        myMap.moveCamera(CameraUpdateFactory.newLatLngZoom(store, zoomLevel));
    }
}