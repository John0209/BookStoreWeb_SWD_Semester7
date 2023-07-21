package com.example.finalproject;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.finalproject.adapter.CartAdapter;
import com.example.finalproject.api.OrderService;
import com.example.finalproject.eventbus.MyUpdateCartEvent;
import com.example.finalproject.login.LoginResponse;
import com.example.finalproject.model.CartItemModel;
import com.example.finalproject.model.CartModel;
import com.example.finalproject.order.Order;
import com.example.finalproject.order.OrderDetail;
import com.example.finalproject.repository.OrderRepository;
import com.google.gson.Gson;

import org.greenrobot.eventbus.EventBus;
import org.greenrobot.eventbus.Subscribe;
import org.greenrobot.eventbus.ThreadMode;

import java.io.BufferedReader;
import java.io.File;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.sql.Timestamp;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class CartActivity extends AppCompatActivity {

    OrderService orderService;
    List<CartItemModel> items;
    private RecyclerView recyclerCart;

    private ImageView btnBack;
    private TextView subTotal, shipping, totalPrice;
    private CartModel cartModel;
    private Button btnCheckOut;
    String URL = "https://regres.in/api/users?page=1";

    @Override
    protected void onStart() {
        super.onStart();
        EventBus.getDefault().register(this);
    }

    @Override
    protected void onStop() {
        if(EventBus.getDefault().hasSubscriberForEvent(MyUpdateCartEvent.class))
            EventBus.getDefault().removeStickyEvent(MyUpdateCartEvent.class);
        EventBus.getDefault().unregister(this);
        super.onStop();
    }

    @Subscribe(threadMode = ThreadMode.MAIN, sticky = true)
    public void onUpdateCart(MyUpdateCartEvent event){
        loadJSONFromInternalData();
        subTotal.setText("$" + Double.toString(cartModel.getTotalPrice()));
        shipping.setText("30000" + "đ");
        double total = cartModel.getTotalPrice() + 30000;
        totalPrice.setText("$" + Double.toString(total));
    }


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,
                WindowManager.LayoutParams.FLAG_FULLSCREEN);
        setContentView(R.layout.activity_cart);
        recyclerCart = findViewById(R.id.recycler_cart);
        subTotal = findViewById(R.id.subtotal);
        shipping = findViewById(R.id.shipping);
        totalPrice = findViewById(R.id.total_price);
        btnBack = findViewById(R.id.btnBack);
        btnCheckOut = findViewById(R.id.btn_checkout);
        btnBack.setOnClickListener(v -> finish());
        orderService = OrderRepository.getOrderService();
        LinearLayoutManager layoutManager = new LinearLayoutManager(this);
        recyclerCart.setLayoutManager(layoutManager);

        loadJSONFromInternalData();

        if(cartModel != null) {
            items = cartModel.getItemModels();
            subTotal.setText(Double.toString(cartModel.getTotalPrice())  + "đ");
            shipping.setText("30000"  + "đ");
            double total = cartModel.getTotalPrice() + 30000;
            totalPrice.setText(Double.toString(total)  + "đ");

            CartAdapter adapter = new CartAdapter(CartActivity.this, cartModel);
            recyclerCart.setAdapter(adapter);
        }else{
            subTotal.setText("0"  + "đ");
            shipping.setText("0"  + "đ");
            totalPrice.setText("0"  + "đ");
        }

        btnCheckOut.setOnClickListener(v -> {
            if(cartModel==null){
                Toast.makeText(CartActivity.this, "Add unsuccessfully", Toast.LENGTH_SHORT).show();
            }
            else{
            saveOrderToDatabase();
            EventBus.getDefault().postSticky(new MyUpdateCartEvent());
            startActivity(new Intent(CartActivity.this, BillActivity.class));
            finish();
            }
        });
    }

    private void loadJSONFromInternalData(){
        try {
            File file = new File(getApplicationContext().getFilesDir(), "cart.json");
            if (file.exists()) {
                InputStream is = getApplicationContext().openFileInput("cart.json");
                BufferedReader reader = new BufferedReader(new InputStreamReader(is));
                StringBuilder stringBuilder = new StringBuilder();
                String line;
                while ((line = reader.readLine()) != null) {
                    stringBuilder.append(line);
                }
                String jsonContent = stringBuilder.toString();
                if (!jsonContent.isEmpty()) {
                    Gson gson = new Gson();
                    cartModel = gson.fromJson(jsonContent, CartModel.class);
                }
                is.close();
            }
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }

    private boolean saveOrderToDatabase(){
        Order order = new Order();
        try {
            File file = new File(getApplicationContext().getFilesDir(), "userData.json");
            if (file.exists()) {
                InputStream is = getApplicationContext().openFileInput("userData.json");
                BufferedReader reader = new BufferedReader(new InputStreamReader(is));
                StringBuilder stringBuilder = new StringBuilder();
                String line;
                while ((line = reader.readLine()) != null) {
                    stringBuilder.append(line);
                }
                String jsonContent = stringBuilder.toString();
                is.close();
                if (!jsonContent.isEmpty()) {
                    Gson gson = new Gson();
                    LoginResponse loginResponse = gson.fromJson(jsonContent, LoginResponse.class);
                    order.setUser_Id(loginResponse.getUser_Id());
                    order.setOrder_Customer_Name(loginResponse.getUser_Name());
                    order.setOrder_Customer_Address(loginResponse.getUser_Address());
                    order.setOrder_Customer_Phone(loginResponse.getUser_Phone());
                    Timestamp time = new Timestamp(System.currentTimeMillis());
                    String time2 = time.toString().replace(" ", "T");
                    order.setOrder_Date(time2);
                    order.setOrder_Quantity(cartModel.getTotalNumber());
                    order.setOrder_Amount(cartModel.getTotalPrice() + 30000);
                    List<CartItemModel> items = cartModel.getItemModels();
                    Call<String> call = orderService.createOrder(order);
                    call.enqueue(new Callback<String>() {
                        @Override
                        public void onResponse(Call<String> call, Response<String> response) {
                                addOrderDetailsByOrderId();
                        }

                        @Override
                        public void onFailure(Call<String> call, Throwable t) {
                            addOrderDetailsByOrderId();
                        }
                    });
                }else{
                    return false;
                }
                return true;
            }
        } catch (Exception e) {
            Log.d("Loi", e.getMessage());
        }
        return false;
    }

    private void addOrderDetailsByOrderId(){
        Call<String> call = orderService.getOrderIdJustCreated();
        call.enqueue(new Callback<String>() {
            @Override
            public void onResponse(Call<String> call, Response<String> response) {
                if(!response.body().isEmpty()){
                    for (CartItemModel item : items) {
                        OrderDetail orderDetail = new OrderDetail();
                        orderDetail.setOrder_Id(response.body());
                        orderDetail.setBook_Id(item.getId());
                        int number = item.getNumber();
                        double price = item.getPrice();
                        orderDetail.setOrder_Detail_Quantity(number);
                        orderDetail.setOrder_Detail_Amount(price * number);
                        orderDetail.setOrder_Detail_Price(price);
                        Call call1 = orderService.createOrderDetail(orderDetail);
                        call1.enqueue(new Callback() {
                            @Override
                            public void onResponse(Call call, Response response) {

                            }

                            @Override
                            public void onFailure(Call call, Throwable t) {
                                Log.d("Order Detail Error at " + item.getName(), t.getMessage());
                            }
                        });
                    }
                }
            }

            @Override
            public void onFailure(Call<String> call, Throwable t) {
                Toast.makeText(CartActivity.this, "Save Failed", Toast.LENGTH_SHORT).show();
            }
        });

    }
}