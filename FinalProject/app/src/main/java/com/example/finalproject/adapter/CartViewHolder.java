package com.example.finalproject.adapter;

import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.example.finalproject.R;

public class CartViewHolder extends RecyclerView.ViewHolder implements View.OnClickListener {

    ImageView itemCoverView;
    ImageView itemMinus, itemPlus, itemDelete;
    TextView itemNameView,itemPriceView, itemNumberView;

    public CartViewHolder(@NonNull View itemView) {
        super(itemView);
        itemCoverView = itemView.findViewById(R.id.cart_item_cover);
        itemNameView = itemView.findViewById(R.id.cart_item_name);
        itemPriceView = itemView.findViewById(R.id.cart_item_price);
        itemNumberView = itemView.findViewById(R.id.cart_item_number);
        itemMinus = itemView.findViewById(R.id.btnItemMinus);
        itemPlus = itemView.findViewById(R.id.btnItemPlus);
        itemDelete = itemView.findViewById(R.id.btnItemDelete);
    }

    @Override
    public void onClick(View v) {

    }
}
