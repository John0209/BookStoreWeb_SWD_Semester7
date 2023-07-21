package com.example.finalproject.Message;

import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;


import com.example.finalproject.R;

import java.util.List;

public class MessageAdapter extends RecyclerView.Adapter<MessageAdapter.MessageViewHolder> {

    private List<Message> messages;

    public MessageAdapter(List<Message> messages) {
        this.messages = messages;
    }

    @NonNull
    @Override
    public MessageViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.user_message_item, parent, false);
        return new MessageViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull MessageViewHolder holder, int position) {
        Message message = messages.get(position);
        if (message.getSender().equals("You")) {
            // Tin nhắn từ người dùng: hiển thị bên phải
            holder.senderTextView.setGravity(Gravity.END);
            holder.messageTextView.setGravity(Gravity.END);
            holder.messageTextView.setBackgroundResource(R.drawable.your_message_background);
        } else {
            // Tin nhắn từ cửa hàng: hiển thị bên trái
            holder.senderTextView.setGravity(Gravity.START);
            holder.messageTextView.setGravity(Gravity.START);
            holder.messageTextView.setBackgroundResource(R.drawable.store_message_background);
        }
        holder.senderTextView.setText(message.getSender());
        holder.messageTextView.setText(message.getMessage());
    }

    @Override
    public int getItemCount() {
        return messages.size();
    }

    public class MessageViewHolder extends RecyclerView.ViewHolder {
        TextView senderTextView;
        TextView messageTextView;

        public MessageViewHolder(@NonNull View itemView) {
            super(itemView);
            senderTextView = itemView.findViewById(R.id.senderTextView);
            messageTextView = itemView.findViewById(R.id.messageTextView);
        }
    }
}
