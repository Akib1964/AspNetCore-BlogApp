# BlogPosts – ASP.NET Core MVC Blog Application

This is a simple blog application built using **ASP.NET Core MVC** and **SQL Server**.  
Users can register, login, create blog posts, edit them, delete them, and add comments.

---

## 📁 Folder Structure (Short)

- **Controllers/**
  - AccountController.cs  
  - BlogPostController.cs  
  - CommentsController.cs  

- **Models/**
  - BlogPost.cs  
  - User.cs  
  - Comment.cs  
  - RegisterViewModel.cs  
  - LoginViewModel.cs  

- **Data/**
  - BlogDbContext.cs  

- **Views/**
  - Account/
  - BlogPost/
  - Comments/
  - Shared/

---

## 🔥 Features

- User Registration & Login (SHA-256 password hashing)  
- Create / Edit / Delete Blog Posts  
- Add Comments  
- Session-based Authentication  
- Admin user seeded by default  

**Default Admin:**  
- Email: `admin@gmail.com`  
- Password: `akib`

---

## ⚙️ How to Run

1. Open project in **Visual Studio 2022**  
2. Update SQL connection inside *appsettings.json*  
3. Run migrations:
Add-Migration Init
Update-Database
4. Press **F5** to run.

---

## 🚀 How to Upload to GitHub (Visual Studio 2022)

1. Open project → Go to **Git** → **Create Git Repository**  
2. Sign in to GitHub (Git → Settings → Accounts)  
3. Then click **Git → GitHub → Create & Push**  
4. Done — project will be uploaded.

---

Thanks for checking out this project!
