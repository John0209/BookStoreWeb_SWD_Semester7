package com.example.finalproject.adapter;

import android.app.AlertDialog;
import android.content.Context;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.ViewGroup;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.bumptech.glide.Glide;
import com.example.finalproject.R;
import com.example.finalproject.eventbus.MyUpdateCartEvent;
import com.example.finalproject.model.CartItemModel;
import com.example.finalproject.model.CartModel;
import com.google.gson.Gson;

import org.greenrobot.eventbus.EventBus;

import java.io.OutputStream;
import java.util.List;

public class CartAdapter extends RecyclerView.Adapter<CartViewHolder>{

    private Context context;
    private CartModel cart;
    private List<CartItemModel> itemModels;
    private final Gson gson = new Gson();

    public CartAdapter(Context context, CartModel cart) {
        this.context = context;
        this.cart = cart;
        this.itemModels = cart.getItemModels();
    }

    public CartAdapter(Context context, List<CartItemModel> itemModels) {
        this.context = context;
        this.itemModels = itemModels;
    }

    @NonNull
    @Override
    public CartViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        return new CartViewHolder(LayoutInflater.from(context)
                .inflate(R.layout.cart_item, parent, false));
    }

    @Override
    public void onBindViewHolder(@NonNull CartViewHolder holder, int position) {
        CartItemModel item = itemModels.get(position);
        Glide.with(context)
                .load(item.getCover())
                .into(holder.itemCoverView);
        holder.itemNameView.setText(item.getName());
        holder.itemPriceView.setText(Double.toString(item.getPrice()) + "đ");
        holder.itemNumberView.setText(Integer.toString(item.getNumber()));
        holder.itemMinus.setOnClickListener(v ->{
            minusCartItem(holder, item, position);
        });
        holder.itemPlus.setOnClickListener(v ->{
            plusCartItem(holder, item, position);
        });
        holder.itemDelete.setOnClickListener(v -> {
            AlertDialog dialog = new AlertDialog.Builder(context)
                    .setTitle("Delete item")
                    .setMessage("Do you want to delete item")
                    .setNegativeButton("CANCEL", (dialog1, which) -> dialog1.dismiss())
                    .setPositiveButton("OK",(dialog12, which) -> {

                        //temp remove
                        notifyItemRemoved(position);
                        deleteCartItem(position);
                        dialog12.dismiss();
                    }).create();
            dialog.show();

        });
    }

    @Override
    public int getItemCount() {
        return itemModels.size();
    }

    private void minusCartItem(CartViewHolder holder, CartItemModel item, int position){
        if(item.getNumber() > 1){
            item.setNumber(item.getNumber() - 1);
            holder.itemNumberView.setText(new StringBuilder().append(item.getNumber()));
            //update database
            cart.getItemModels().get(position).setNumber(item.getNumber());
            cart.getTotalNumber();
            cart.getTotalPrice();
            itemModels.get(position).setNumber(item.getNumber());
            updateNumberInternalStorage();
        }

    }

    private void plusCartItem(CartViewHolder holder, CartItemModel item, int position){
        item.setNumber(item.getNumber() + 1);
        holder.itemNumberView.setText(new StringBuilder().append(item.getNumber()));
        //update database
        cart.getItemModels().get(position).setNumber(item.getNumber());
        cart.getTotalNumber();
        cart.getTotalPrice();
        itemModels.get(position).setNumber(item.getNumber());
        updateNumberInternalStorage();
    }

    private void deleteCartItem(int position){
        cart.getItemModels().remove(position);
        cart.getTotalNumber();
        cart.getTotalPrice();
        updateNumberInternalStorage();
    }

    private void updateNumberInternalStorage(){
        try {
            String cartModelJson = gson.toJson(cart);
            OutputStream os = context.openFileOutput("cart.json", Context.MODE_PRIVATE);
            os.write(cartModelJson.getBytes());
            os.close();
            EventBus.getDefault().postSticky(new MyUpdateCartEvent());
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }


}
