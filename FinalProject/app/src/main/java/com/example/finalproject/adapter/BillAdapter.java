package com.example.finalproject.adapter;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.ViewGroup;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.bumptech.glide.Glide;
import com.example.finalproject.R;
import com.example.finalproject.model.CartItemModel;
import com.example.finalproject.model.CartModel;

import java.util.List;

public class BillAdapter extends RecyclerView.Adapter<BillViewHolder> {

    private Context context;
    private CartModel cart;
    private List<CartItemModel> itemModels;

    public BillAdapter(Context context, CartModel cart) {
        this.context = context;
        this.cart = cart;
        this.itemModels = cart.getItemModels();
    }
    @NonNull
    @Override
    public BillViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        return new BillViewHolder(LayoutInflater.from(context)
                .inflate(R.layout.order_item, parent, false));
    }

    @Override
    public void onBindViewHolder(@NonNull BillViewHolder holder, int position) {
        CartItemModel item = itemModels.get(position);
        Glide.with(context)
                .load(item.getCover())
                .into(holder.itemCoverView);
        holder.itemNameView.setText(item.getName());
        holder.itemPriceView.setText(Double.toString(item.getPrice()) + "đ");
        holder.itemNumberView.setText(Integer.toString(item.getNumber()));
    }

    @Override
    public int getItemCount() {
        return itemModels.size();
    }
}
