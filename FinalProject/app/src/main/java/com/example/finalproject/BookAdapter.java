package com.example.finalproject;

import android.annotation.SuppressLint;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.RelativeLayout;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.bumptech.glide.Glide;
import com.bumptech.glide.load.engine.DiskCacheStrategy;

import java.util.ArrayList;

public class BookAdapter extends RecyclerView.Adapter<BookAdapter.BookViewHolder>{
    private Context context;
    private ArrayList<BookRecycleView> bookList;

    public BookAdapter(Context context) {
        this.context = context;
    }

    public void setData(ArrayList<BookRecycleView> bookList){
        this.bookList = bookList;
        notifyDataSetChanged();
    }

    @NonNull
    @Override
    public BookViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.child_rv_layout,parent,false);
        return new BookViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull BookViewHolder holder, @SuppressLint("RecyclerView") int position) {
        holder.nameBook.setText(bookList.get(position).getBook_Title());
        holder.nameAuthor.setText(bookList.get(position).getBook_Author());
        Glide.with(context)
                .load(bookList.get(position).getImage_URL())
                .diskCacheStrategy(DiskCacheStrategy.ALL) // Lưu cache ảnh
                .into(holder.iv_child_image);
        holder.rlt_book.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                onClickItem(bookList.get(position));
            }
        });
    }

    @Override
    public int getItemCount() {
        return bookList.size();
    }

    public class BookViewHolder extends RecyclerView.ViewHolder{
        private RelativeLayout rlt_book;
        private ImageView iv_child_image;
        private TextView nameBook,nameAuthor;
        public BookViewHolder(@NonNull View itemView) {
            super(itemView);
            iv_child_image = itemView.findViewById(R.id.iv_child_item);
            nameBook = itemView.findViewById(R.id.nameBook);
            nameAuthor = itemView.findViewById(R.id.nameAuthor);
            rlt_book = itemView.findViewById(R.id.rlt_layoutBook);
        }
    }
    public void onClickItem(BookRecycleView book){
        Intent intent = new Intent(context,BookDetail.class);
        Bundle bundle = new Bundle();
        bundle.putSerializable("bookObject", book);
        intent.putExtras(bundle);
        context.startActivity(intent);
    }
}
