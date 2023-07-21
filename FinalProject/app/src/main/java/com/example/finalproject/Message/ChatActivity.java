package com.example.finalproject.Message;

import android.os.Bundle;
import android.view.View;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;

import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.finalproject.R;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

public class ChatActivity extends AppCompatActivity {

    private List<Message> messages;
    private RecyclerView recyclerView;
    private MessageAdapter adapter;
    private EditText messageEditText;
    private Button sendButton;
    private HashMap<String, String> keywordResponses = new HashMap<>();
    ImageView backButton;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.chat_screen);
        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,
                WindowManager.LayoutParams.FLAG_FULLSCREEN);
        keywordResponses.put("hello", "Hello! How can I assist you?");
        keywordResponses.put("product", "Our products are of high quality. How can I help you with a specific product?");
        keywordResponses.put("order", "Please provide your order details, and we will assist you accordingly.");

        recyclerView = findViewById(R.id.recyclerView);
        messageEditText = findViewById(R.id.messageEditText);
        sendButton = findViewById(R.id.sendButton);

        messages = new ArrayList<>();
        adapter = new MessageAdapter(messages);
        recyclerView.setLayoutManager(new LinearLayoutManager(this));
        recyclerView.setAdapter(adapter);

        sendButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String messageText = messageEditText.getText().toString().trim();
                if (!messageText.isEmpty()) {
                    // Add user message to the list
                    messages.add(new Message("You", messageText));
                    adapter.notifyItemInserted(messages.size() - 1);
                    recyclerView.smoothScrollToPosition(messages.size() - 1);
                    // Clear the input field
                    messageEditText.setText("");

                    // Simulate a response from the store
                    simulateStoreResponse();
                }
            }
        });

        backButton = findViewById(R.id.btnBack); // Move this line inside onCreate()

        backButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                onBackPressed();
            }
        });
    }

    private void simulateStoreResponse() {
        // Simulate a delay before receiving the store's response
        recyclerView.postDelayed(new Runnable() {
            @Override
            public void run() {
                // Add store's response to the list
                String userMessage = messages.get(messages.size() - 1).getMessage();
                String response = getKeywordResponse(userMessage.toLowerCase());

                // Add store's response to the list
                messages.add(new Message("Store", response));
                adapter.notifyItemInserted(messages.size() - 1);
                recyclerView.smoothScrollToPosition(messages.size() - 1);
            }
            private String getKeywordResponse(String userMessage) {
                for (String keyword : keywordResponses.keySet()) {
                    if (userMessage.contains(keyword)) {
                        return keywordResponses.get(keyword);
                    }
                }
                // Nếu không tìm thấy từ khóa trong tin nhắn, trả về một phản hồi mặc định
                return "Thank you for your message!";
            }

        }, 1000); // Simulate a 1-second delay
    }
}
