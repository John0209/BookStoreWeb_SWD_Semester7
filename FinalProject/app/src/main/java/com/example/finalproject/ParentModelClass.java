package com.example.finalproject;

import java.util.List;

public class ParentModelClass {
    String title;
    List<BookRecycleView> childList;

    public ParentModelClass(String title, List<BookRecycleView> childList) {
        this.title = title;
        this.childList = childList;
    }

    public String getTitle() {
        return title;
    }

    public List<BookRecycleView> getChildModelClassList() {
        return childList;
    }


}
