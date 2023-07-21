package com.example.finalproject.Register;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;


import com.example.finalproject.R;
import com.example.finalproject.api.UserService;
import com.example.finalproject.login.LoginPage;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class SignUp extends AppCompatActivity implements View.OnClickListener {
    UserService traineeService;
    EditText etuserAccount, etpassWord, etemail;
    Button btn;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_sign_up);
        etemail = findViewById(R.id.editTextTextEmailAddress2);
        etuserAccount = findViewById(R.id.editTextTextPassword);
        etpassWord = findViewById(R.id.editTextTextPassword1);
        btn = findViewById(R.id.button);
        btn.setOnClickListener(this);
        traineeService = UserRepository.getUserService();
        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,
                WindowManager.LayoutParams.FLAG_FULLSCREEN);
    }

    @Override
    public void onClick(View v) {
        if (v.getId() == R.id.button) {
            save();
        }
    }

    private void save() {
        User trainee = new User();
        trainee.setUser_Email(etemail.getText().toString());
        trainee.setUser_Account(etuserAccount.getText().toString());
        trainee.setUser_Password(etpassWord.getText().toString());
        try {
            Call<User> call = traineeService.signUp(trainee);
            call.enqueue(new Callback<User>() {
                @Override
                public void onResponse(Call<User> call, Response<User> response) {
                    if (response.body() != null) {
                        Toast.makeText(SignUp.this, "Save Fail", Toast.LENGTH_SHORT).show();
                    }
                }
                @Override
                public void onFailure(Call<User> call, Throwable t) {
                    Intent intentSignUp = new Intent(SignUp.this, LoginPage.class);
                    startActivity(intentSignUp);
                    Toast.makeText(SignUp.this, "Save successfully", Toast.LENGTH_SHORT).show();
                }
            });
        } catch (Exception e) {
            Log.d("Loi", e.getMessage());
        }
    }
}