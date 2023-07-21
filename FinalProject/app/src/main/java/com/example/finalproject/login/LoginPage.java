package com.example.finalproject.login;
import com.example.finalproject.MainPage;
import com.example.finalproject.R;
import com.example.finalproject.Register.SignUp;
import com.example.finalproject.Register.User;
import com.example.finalproject.api.ApiClient;
import com.google.gson.Gson;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.util.Log;
import android.view.View;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;
import androidx.appcompat.app.AppCompatActivity;

import java.io.File;
import java.io.OutputStream;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

@SuppressWarnings("DEPRECATION")
public class LoginPage extends AppCompatActivity {
    EditText username, password;
    Button btn;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,
                WindowManager.LayoutParams.FLAG_FULLSCREEN);
        setContentView(R.layout.activity_login_page);
        username = findViewById(R.id.editTextTextEmailAddress2);
        password = findViewById(R.id.editTextTextPassword);
        btn = findViewById(R.id.button1);
        TextView create = findViewById(R.id.textView3);
        btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                login();
            }
        });
        create.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent SignUp = new Intent(LoginPage.this, SignUp.class);
                startActivity(SignUp);
            }
        });
    }

    public void login() {
        LoginRequest loginRequest = new LoginRequest();
        loginRequest.setUser_Account(username.getText().toString());
        loginRequest.setUser_Password(password.getText().toString());
        Call<LoginResponse> loginResponseCall = ApiClient.getUserService().userLogin(loginRequest);
        loginResponseCall.enqueue(new Callback<LoginResponse>() {
            @Override
            public void onResponse(Call<LoginResponse> call, Response<LoginResponse> response) {
                if (response.isSuccessful()) {
                    Intent intentLogin = new Intent(LoginPage.this, MainPage.class);
                    startActivity(intentLogin);
                    LoginResponse loginResponse = response.body();
                    saveUserData(loginResponse);
                    new Handler().postDelayed(new Runnable() {
                        @Override
                        public void run() {
                        }
                    }, 700);
                } else {
                    Toast.makeText(LoginPage.this, "Wrong Username or password ", Toast.LENGTH_LONG).show();
                }
            }

            @Override
            public void onFailure(Call<LoginResponse> call, Throwable t) {
                Toast.makeText(LoginPage.this, "Throwable " + t.getLocalizedMessage(), Toast.LENGTH_LONG).show();
            }
        });
    }

    private void saveUserData(LoginResponse loginResponse){
        try {
            File file = new File(getApplicationContext().getFilesDir(), "userData.json");
            if (file.exists()) {
                file.delete();
            }
            file.createNewFile();
            Gson gson = new Gson();
            String userData = gson.toJson(loginResponse);
            OutputStream os = getApplicationContext().openFileOutput("userData.json", Context.MODE_PRIVATE);
            os.write(userData.getBytes());
            os.close();
        }catch (Exception e){
            e.printStackTrace();
        }
    }
}
