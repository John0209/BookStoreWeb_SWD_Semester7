package com.example.finalproject.GMap;

import java.io.Serializable;

public class Item_mapstore implements Serializable {
    private String pic;
    private String title;
    private String address;
    private int distance;

    private String description;

    private boolean wifi;
    private double latitude;
    private double longtitude;

    public Item_mapstore() {
    }

    public Item_mapstore(String pic, String title, String address, int distance, String description, boolean wifi, double latitude, double longtitude) {
        this.pic = pic;
        this.title = title;
        this.address = address;
        this.distance = distance;
        this.description = description;
        this.wifi = wifi;
        this.latitude = latitude;
        this.longtitude = longtitude;
    }

    public double getLatitude() {
        return latitude;
    }

    public double getLongtitude() {
        return longtitude;
    }

    public String getPic() {
        return pic;
    }

    public String getTitle() {
        return title;
    }

    public String getAddress() {
        return address;
    }

    public int getDistance() {
        return distance;
    }

    public String getDescription() {
        return description;
    }
    
    public boolean isWifi() {
        return wifi;
    }
}
