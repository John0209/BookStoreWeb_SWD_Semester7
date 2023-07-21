package com.example.finalproject;


import android.annotation.SuppressLint;
import android.content.Intent;
import android.os.Bundle;
import android.view.MenuItem;
import android.view.View;
import android.view.WindowManager;
import android.widget.EditText;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.AppCompatButton;
import androidx.recyclerview.widget.GridLayoutManager;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.finalproject.Register.User;
import com.example.finalproject.Register.UserRepository;
import com.example.finalproject.api.UserService;
import com.example.finalproject.model.CartModel;
import com.google.android.material.bottomnavigation.BottomNavigationView;
import com.google.gson.Gson;

import java.io.BufferedReader;
import java.io.File;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.List;
import java.util.Locale;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class MainPage extends AppCompatActivity implements View.OnClickListener {
    private EditText editTextSearch;
    private AppCompatButton buttonSearch;
     RecyclerView recyclerView;
     GridLayoutManager gridLayoutManager;
     User user;

    ArrayList<ParentModelClass> parentModelClassArrayList;
    ArrayList<ChildModelClass> childModelClassArrayList;
    ArrayList<BookRecycleView> favoriteList;
    ArrayList<BookRecycleView> recentlyWatchedList;
    ArrayList<BookRecycleView> latesList;

    ArrayList<BookRecycleView> comicList;

    ArrayList<BookRecycleView> mysteryList;

    ArrayList<BookRecycleView> horrorList;
    UserService userService;

    List<BookRecycleView> listBook;
    BottomNavigationView nav;
    @SuppressLint("MissingInflatedId")
//    private NotificationBadge badge;

//    @Override
//    protected void onStart() {
//        super.onStart();
//        EventBus.getDefault().register(this);
//        countCartItem();
//    }
//
//    @Override
//    protected void onStop() {
//        if(EventBus.getDefault().hasSubscriberForEvent(MyUpdateCartEvent.class))
//            EventBus.getDefault().removeStickyEvent(MyUpdateCartEvent.class);
//        EventBus.getDefault().unregister(this);
//        super.onStop();
//    }
//
//    @Subscribe(threadMode = ThreadMode.MAIN, sticky = true)
//    public void onUpdateCart(MyUpdateCartEvent event){
//        countCartItem();
//    }
    @Override
    protected void onCreate(Bundle savedInstanceState) {


        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main_page);
        userService = UserRepository.getUserService();
        recyclerView =findViewById(R.id.rcv_book);
        childModelClassArrayList = new ArrayList<>();
        favoriteList= new ArrayList<BookRecycleView>();
        recentlyWatchedList= new ArrayList<BookRecycleView>();
        latesList = new ArrayList<BookRecycleView>();
        comicList = new ArrayList<BookRecycleView>();
        mysteryList = new ArrayList<BookRecycleView>();
        horrorList = new ArrayList<BookRecycleView>();
        parentModelClassArrayList = new ArrayList<>();
        editTextSearch = findViewById(R.id.editTextText);
        buttonSearch = findViewById(R.id.buttonSearch);
        buttonSearch.setOnClickListener(this);
        //Bottom nevigation
        nav= findViewById(R.id.nav_bar);
        nav.setSelectedItemId(R.id.home);
        nav.setOnNavigationItemSelectedListener(new BottomNavigationView.OnNavigationItemSelectedListener() {
            @Override
            public boolean onNavigationItemSelected(@NonNull MenuItem item) {
                if (item.getItemId() == R.id.book) {
                    startActivity(new Intent(MainPage.this,SeeMorePage.class));
                    return true;
                } else if (item.getItemId()==R.id.setting) {
                    startActivity(new Intent(MainPage.this, Setting.class));
                    return true;
                }else if (item.getItemId() == R.id.cart){
                    startActivity(new Intent(MainPage.this, CartActivity.class));
                    return true;
                }
                return false;
            }
        });
        TextView textView = findViewById(R.id.textView7);
        textView.setText("Welcome Back");
        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,
                WindowManager.LayoutParams.FLAG_FULLSCREEN);
        GetAll();
    }

    @Override
    public void onClick(View v) {
        if (v.getId() == R.id.buttonSearch) {
            String searchText = editTextSearch.getText().toString().trim();
            performSearch(searchText);
        }
    }
    private void performSearch(String searchText) {
        if (searchText.isEmpty()) {
            // Clear the search results and show the original list of products
            ParentAdapter parentAdapter = new ParentAdapter(parentModelClassArrayList, MainPage.this);
            recyclerView.setAdapter(parentAdapter);
        } else {
            ArrayList<BookRecycleView> searchResults = new ArrayList<>();
            for (ParentModelClass parentModel : parentModelClassArrayList) {
                for (BookRecycleView childModel : parentModel.getChildModelClassList()) {
                    if (childModel.getBook_Title().toLowerCase().contains(searchText.toLowerCase())) {
                        searchResults.add(childModel);
                    }
                }
            }

            ArrayList<ParentModelClass> updatedList = new ArrayList<>();
            if (!searchResults.isEmpty()) {
                // Add the "Search Results" category only if there are matching results
                ParentModelClass searchParentModel = new ParentModelClass("Search Results", searchResults);
                updatedList.add(searchParentModel);
            }

            // Update the RecyclerView with the search results
            ParentAdapter parentAdapter = new ParentAdapter(updatedList, MainPage.this);
            recyclerView.setAdapter(parentAdapter);

            // Hide the "See more" TextView in all child items
            View childItem = recyclerView.getLayoutManager().findViewByPosition(0);
            if (childItem != null) {
//                TextView seeMoreTextView = childItem.findViewById(R.id.textView_book1);
//                seeMoreTextView.setVisibility(View.GONE);
            }
        }
    }



    public void GetAll(){
        Call<BookRecycleView[]> call = (Call<BookRecycleView[]>) userService.getAllBook();
        call.enqueue(new Callback<BookRecycleView[]>() {
            @Override
            public void onResponse(Call<BookRecycleView[]> call, Response<BookRecycleView[]> response) {
                BookRecycleView[] books = response.body();
                if(books == null){
                    return;
                }
                for (BookRecycleView book : books) {
                        switch (book.getCategory_Id()) {
                            case "1":
                                latesList.add(book);
                                break;
                            case "2":
                                comicList.add(book);
                                break;
                            case "3":
                                favoriteList.add(book);
                                break;
                            case "4":
                                mysteryList.add(book);
                                break;
                            case "5":
                                recentlyWatchedList.add(book);
                                break;
                            case "6":
                                horrorList.add(book);
                                break;
                        }
                    }
                parentModelClassArrayList.add(new ParentModelClass("Novel Book",latesList));
                parentModelClassArrayList.add(new ParentModelClass("Detective Book",recentlyWatchedList));
                parentModelClassArrayList.add(new ParentModelClass("Romance Book",favoriteList));
                parentModelClassArrayList.add(new ParentModelClass("Comic Book",comicList));
                parentModelClassArrayList.add(new ParentModelClass("Mystery Book",mysteryList));
                parentModelClassArrayList.add(new ParentModelClass("Horror Book",horrorList));
                ParentAdapter parentAdapter = new ParentAdapter(parentModelClassArrayList,MainPage.this);
                recyclerView.setLayoutManager(new LinearLayoutManager(MainPage.this));
                recyclerView.setAdapter(parentAdapter);
                parentAdapter.notifyDataSetChanged();
            }
            @Override
            public void onFailure(Call<BookRecycleView[]> call, Throwable t) {

            }
        });
    }

//    private void countCartItem() {
//        try {
//            File file = new File(getApplicationContext().getFilesDir(), "cart.json");
//            if (!file.exists()){
//                badge.setNumber(0);
//            }else {
//                int total = 0;
//                InputStream is = getApplicationContext().openFileInput("cart.json");
//                BufferedReader reader = new BufferedReader(new InputStreamReader(is));
//                StringBuilder stringBuilder = new StringBuilder();
//                String line;
//                while ((line = reader.readLine()) != null) {
//                    stringBuilder.append(line);
//                }
//                String jsonContent = stringBuilder.toString();
//                if (!jsonContent.isEmpty()) {
//                    Gson gson = new Gson();
//                    CartModel cartModel = gson.fromJson(jsonContent, CartModel.class);
//                    badge.setNumber(cartModel.getTotalNumber());
//                } else {
//                    badge.setNumber(0);
//                }
//
//                is.close();
//            }
//
//        } catch (Exception ex) {
//            ex.printStackTrace();
//        }
//    }


}
