package com.example.finalproject.GMap;

import android.os.Bundle;
import android.view.View;
import android.view.WindowManager;
import android.widget.ImageView;

import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.finalproject.R;

import java.util.ArrayList;

public class ListStore extends AppCompatActivity {

    private RecyclerView recyclerViewPopular;
    private RecyclerView.Adapter adapterPopular;
    ImageView backButton;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,
                WindowManager.LayoutParams.FLAG_FULLSCREEN);
        setContentView(R.layout.activity_list_store);

        backButton = findViewById(R.id.btnBack); // Move this line inside onCreate()

        backButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                onBackPressed();
            }
        });

        initRecyclerView();
    }

    private void initRecyclerView() {

        ArrayList<Item_mapstore> itemList = new ArrayList<>();

        itemList.add(new Item_mapstore("sach_ca_chep", "Ca Chep Book Store", "211-213 Võ Văn Tần, Phường 5, Quận 3, TP. Hồ Chí Minh", 12, "Ca Chep Book Store offers a curated collection of books for readers of all ages and interests. From fiction to non-fiction, from bestsellers to hidden gems, Cá Chép Book Store has something for everyone. The knowledgeable and friendly staff are always ready to assist you in finding the perfect book or offering recommendations. Whether you're looking to get lost in a captivating story or " +
                "expand your knowledge. Come and immerse yourself in the world of literature " +
                "at Cá Chép Book Store.", true, 10.773366094348958,106.68734843590696));
        itemList.add(new Item_mapstore("fahasa", "Fahasa Bookstore Dist. 9", "138 Lê Văn Việt, Hiệp Phú, Quận 9, Thành phố Hồ Chí Minh 700000, Vietnam", 7, "Fahasa Bookstore ,with its wide selection of books and extensive collection, Fahasa Bookstore is a popular destination for book enthusiasts. Whether you're looking for novels, textbooks, or reference materials, Fahasa offers a diverse range of reading options. The bookstore provides a comfortable and inviting environment for browsing and exploring various genres. " +
                "Visit Fahasa Bookstore in District 9 for an enjoyable and enriching book shopping experience.", true,10.845339145110039,106.78017253964781));
        itemList.add(new Item_mapstore("sach_hai_an",
                "Hai An Bookstore",
                " 2B Nguyễn Thị Minh Khai, Phường Đa Kao, Quận 1, TP. Hồ Chí Minh",
                5,
                "Hai An Bookstore is a bookstore located at 2B Nguyen Thi Minh Khai, " +
                        "Da Kao Ward, District 1, Ho Chi Minh City. This bookstore not only serves as a convenient shopping destination " +
                        "for book lovers, but also provides a beautiful and cozy reading space. With a diverse range of book products and services, Hai An Bookstore offers a unique experience to customers, including sections dedicated to ethnic literature, children's books, foreign books, and souvenir shopping.", true,10.790571589432108,106.70482658012503));

        recyclerViewPopular = findViewById(R.id.viewPopular);

        recyclerViewPopular.setLayoutManager(new LinearLayoutManager(this, LinearLayoutManager.VERTICAL, false));

        adapterPopular = new MapStoreAdapter(itemList);

        recyclerViewPopular.setAdapter(adapterPopular);
    }

}
