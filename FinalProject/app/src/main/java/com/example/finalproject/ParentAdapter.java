package com.example.finalproject;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;



import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;
import androidx.annotation.NonNull;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

public class ParentAdapter extends RecyclerView.Adapter<ParentAdapter.ViewHolder> {
    private List<ParentModelClass> parentModelClassList;
    private List<ParentModelClass> originalParentModelClassList; // Store the original list for filtering
    private Context context;


    public ParentAdapter(List<ParentModelClass> parentModelClassList, Context context) {
        this.parentModelClassList = parentModelClassList;
        this.originalParentModelClassList = new ArrayList<>(parentModelClassList);
        this.context = context;
    }

    @NonNull
    @Override
    public ViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(context).inflate(R.layout.item_book, parent, false);
        return new ViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull ParentAdapter.ViewHolder holder, int position) {
        holder.tv_parent_title.setText(parentModelClassList.get(position).title);
        ChildAdapter childAdapter;
        childAdapter = new ChildAdapter(parentModelClassList.get(position).childList,context);
        holder.rv_child.setLayoutManager(new LinearLayoutManager(context,LinearLayoutManager.HORIZONTAL,false));
        holder.rv_child.setAdapter(childAdapter);
        childAdapter.notifyDataSetChanged();
        holder.tv_seeMore.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                // Put book list by category into bundle
                Bundle bundle = new Bundle();
                bundle.putSerializable("listBookByCategory", (Serializable) parentModelClassList.get(position).getChildModelClassList());
                Intent intent = new Intent(context, SeeMoreByCategoryPage.class);
                intent.putExtras(bundle);
                context.startActivity(intent);
            }
        });
    }

    @Override
    public int getItemCount() {
        return parentModelClassList.size();
    }

    public class ViewHolder extends RecyclerView.ViewHolder {
        RecyclerView rv_child;
        TextView tv_parent_title;
        TextView tv_seeMore;
        public ViewHolder(@NonNull View itemView){
            super(itemView);
            rv_child=itemView.findViewById(R.id.rv_child);
            tv_parent_title =itemView.findViewById(R.id.textView_book);
            tv_seeMore = itemView.findViewById(R.id.tvSeeMore);

        }
    }

}



