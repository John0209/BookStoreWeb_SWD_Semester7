
package com.example.finalproject;

public class ChildModelClass {
    private String book_Id;
    private String image_URL;
    private String book_Title;
    private String book_Author;

    public ChildModelClass(String image_URL){
        this.image_URL =image_URL;
    }

    public ChildModelClass(String book_Id, String image_URL, String book_Title, String book_Author) {
        this.book_Id = book_Id;
        this.image_URL = image_URL;
        this.book_Title = book_Title;
        this.book_Author = book_Author;
    }
    public String getBook_Id(){ return book_Id;}
    public String getBook_Title() {
        return book_Title;
    }

    public void setBook_Title(String book_Title) {
        this.book_Title = book_Title;
    }

    public String getBook_Author() {
        return book_Author;
    }

    public void setBook_Author(String book_Author) {
        this.book_Author = book_Author;
    }

    public String getImage_URL() {
        return image_URL;
    }

    public void setImage_URL(String image_URL) {
        this.image_URL = image_URL;
    }
}
