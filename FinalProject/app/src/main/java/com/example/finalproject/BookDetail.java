package com.example.finalproject;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import android.util.Log;
import android.view.KeyEvent;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;


import com.bumptech.glide.Glide;
import com.example.finalproject.Register.UserRepository;
import com.example.finalproject.api.UserService;
import com.example.finalproject.eventbus.MyUpdateCartEvent;
import com.example.finalproject.model.CartItemModel;
import com.example.finalproject.model.CartModel;
import com.google.gson.Gson;

import org.greenrobot.eventbus.EventBus;
import org.json.JSONArray;

import java.io.BufferedReader;
import java.io.File;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class BookDetail extends AppCompatActivity  {
    ImageView imageView;
    TextView bookTitle,authorName,price,publicYear,type,description;
    Button button;
    BookRecycleView book;

    UserService userService;
    @Override
    protected void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_book_detail);
        userService = UserRepository.getUserService();
        imageView = findViewById(R.id.ivBook);
        bookTitle = findViewById(R.id.tvBookTitle);
        authorName = findViewById(R.id.tvAuthor);
        price = findViewById(R.id.tvPrice);
        publicYear = findViewById(R.id.tvPublicYear);
        type = findViewById(R.id.tvBookType);
        description = findViewById(R.id.tvDetail);
        button = findViewById(R.id.btnAddToCart);
        Bundle bundle = getIntent().getExtras();
        book = (BookRecycleView) bundle.get("bookObject");
        if(book != null){
            Glide.with(BookDetail.this).load(book.getImage_URL()).into(imageView);
            bookTitle.setText(book.getBook_Title());
            authorName.setText(book.getBook_Author());
            price.setText(String.valueOf(book.getBook_Price()));
            publicYear.setText(String.valueOf(book.getBook_Year_Public()));
            type.setText(book.getCategory_Name());
            description.setText(book.getBook_Description());
        }

        button.setOnClickListener(v -> addToCart());
    }

    private void addToCart() {
        JSONArray jsonArray;
        Gson gson = new Gson();
        try {
            File file = new File(getApplicationContext().getFilesDir(), "cart.json");
            if (!file.exists()){
                file.createNewFile();
                List<CartItemModel> cartItems = new ArrayList<>();
                cartItems.add(new CartItemModel(book.getBook_Id(), 1,
                        book.getBook_Title(), book.getImage_URL(),
                        book.getBook_Price()));
                CartModel cartModel = new CartModel();
                cartModel.setItemModels(cartItems);
                String cartModelJson = gson.toJson(cartModel);
                OutputStream os = getApplicationContext().openFileOutput("cart.json", Context.MODE_PRIVATE);
                os.write(cartModelJson.getBytes());
                os.close();
                Toast.makeText(BookDetail.this, "Add successfully", Toast.LENGTH_SHORT).show();
            }else {
                InputStream is = getApplicationContext().openFileInput("cart.json");
                BufferedReader reader = new BufferedReader(new InputStreamReader(is));
                StringBuilder stringBuilder = new StringBuilder();
                String line;
                while ((line = reader.readLine()) != null) {
                    stringBuilder.append(line);
                }
                String jsonContent = stringBuilder.toString();
                if (!jsonContent.isEmpty()) {
                    CartModel cartModel = gson.fromJson(jsonContent, CartModel.class);
                    boolean flag = cartModel.addNewItemToCart(new CartItemModel(book.getBook_Id(), 1,
                            book.getBook_Title(), book.getImage_URL(),
                            book.getBook_Price()));
                    if(flag) {
                        String cartModelJson = gson.toJson(cartModel);
                        OutputStream os = getApplicationContext().openFileOutput("cart.json", Context.MODE_PRIVATE);
                        os.write(cartModelJson.getBytes());
                        os.close();
                        Toast.makeText(BookDetail.this, "Add successfully", Toast.LENGTH_SHORT).show();
                    }else{
                        Toast.makeText(BookDetail.this, "This is already in cart", Toast.LENGTH_SHORT).show();
                    }
                }

                is.close();
                EventBus.getDefault().postSticky(new MyUpdateCartEvent());
            }

        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }
}