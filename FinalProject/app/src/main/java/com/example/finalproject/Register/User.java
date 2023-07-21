package com.example.finalproject.Register;

public class User {


    private String user_Email;
    private String user_Account;
    private String user_Password;
    private String user_Name;
    private String role_Id;
    private String user_Address;
    private String user_Phone;
    private String is_User_Gender;
    private Boolean is_User_Status;


    public User(String user_Account, String user_Password, String user_Name) {
        this.user_Account = user_Account;
        this.user_Password = user_Password;
        this.user_Name = user_Name;
        this.role_Id = role_Id;
        this.user_Address = user_Address;
        this.user_Email = user_Email;
        this.user_Phone = user_Phone;
        this.is_User_Gender = is_User_Gender;
        this.is_User_Status = is_User_Status;
    }

    public User() {

    }

    public String getUser_Account() {
        return user_Account;
    }

    public void setUser_Account(String user_Account) {
        this.user_Account = user_Account;
    }

    public String getUser_Password() {
        return user_Password;
    }

    public void setUser_Password(String user_Password) {
        this.user_Password = user_Password;
    }

    public String getUser_Name() {
        return user_Name;
    }

    public void setUser_Name(String user_Name) {
        this.user_Name = user_Name;
    }

    public String getRole_Id() {
        return role_Id;
    }

    public void setRole_Id(String role_Id) {
        this.role_Id = role_Id;
    }

    public String getUser_Address() {
        return user_Address;
    }

    public void setUser_Address(String user_Address) {
        this.user_Address = user_Address;
    }

    public String getUser_Email() {
        return user_Email;
    }

    public void setUser_Email(String user_Email) {
        this.user_Email = user_Email;
    }

    public String getUser_Phone() {
        return user_Phone;
    }

    public void setUser_Phone(String user_Phone) {
        this.user_Phone = user_Phone;
    }

    public String getIs_User_Gender() {
        return is_User_Gender;
    }

    public void setIs_User_Gender(String is_User_Gender) {
        this.is_User_Gender = is_User_Gender;
    }

    public Boolean getIs_User_Status() {
        return is_User_Status;
    }

    public void setIs_User_Status(Boolean is_User_Status) {
        this.is_User_Status = is_User_Status;
    }
}