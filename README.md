# Exam Tracking - Windows Application

This is a Visual Programming final project built with **C# (Windows Forms)** and **MySQL**, designed to help students track and analyze their LGS (High School Entrance Exam) results.

---

## 📌 Project Structure

- `Exam-Tracking-Windows-App/210706028_OmerFarukOzer_LGS_FinalProject.pdf` → Final project report (in PDF)
- `Exam-Tracking-Windows-App/Database.sql` → MySQL database structure

---

## 🚀 Features

### 👨‍🏫 Admin Panel
- Add / edit / delete student accounts
- Add exams manually, via PDF or OCR image
- View all students’ exam results
- Generate individual or bulk PDF reports
- Analyze student performance with customizable charts

### 👨‍🎓 Student Panel
- Add their own exams (manual entry)
- View only their own exam history
- See per-subject and total net performance
- Export personal PDF exam reports

---

## 🧰 Technologies Used
- **C# (Windows Forms UI)**
- **MySQL** (database)
- **iTextSharp** (PDF generation)
- **Charting** (System.Windows.Forms.DataVisualization)
- **Tesseract OCR** (image-based data extraction)
- Fixed resolution: **1024x576**

---

## 🗄️ Database Design

### `admins`  
| Column    | Type         |
|-----------|--------------|
| ID        | int (PK, AI) |
| Username  | varchar(50)  |
| Password  | varchar(100) |

### `students`  
| Column   | Type         |
|----------|--------------|
| ID       | int (PK, AI) |
| Username | varchar(50)  |
| Password | varchar(100) |
| Name     | varchar(100) |
| Surname  | varchar(100) |
| School   | varchar(100) |
| Class    | varchar(10)  |

### `exams`  
| Column         | Type         |
|----------------|--------------|
| ID             | int (PK, AI) |
| StudentID      | int (FK)     |
| Title          | varchar(100) |
| Date           | date         |
| Math           | float        |
| Science        | float        |
| Turkish        | float        |
| History        | float        |
| Religion       | float        |
| English        | float        |
| ...Correct/Wrong columns for all lessons |

---

## 📊 Reporting & Analytics

- Line and Column chart types
- Filter by subject
- Display total net line
- Per-student PDF report generation
- Multi-page bulk PDF report generation (all exams)

---

## 📥 Input Methods

- Manual input (text fields)
- Smart import from formatted **PDF report**
- OCR-based reading from **optical form images**

---

## 📂 Submission Contents

- ✅ Working `.exe` application
- ✅ All source code files
- ✅ `Database.sql` file
- ✅ [Final Project Report (PDF)](./210706028_OmerFarukOzer_LGS_FinalProject.pdf)
- ✅ Sample PDF outputs
- ✅ 5-minute project demo video

---

## ✅ Project Completion

All project requirements have been **fully implemented**.  
UI design follows the required color scheme and resolution.  
Security, usability, and clarity were prioritized in development.

---

## 👤 Developer

**Omer Faruk Ozer**  
Student No: 210706028  
Department: Software Engineering  
Maltepe University  
📧 ozeromerfaruk@gmail.com  
🌐 [GitHub](https://github.com/omerozerf)

---

> This project was developed as a final term project for the Visual Programming course in Spring 2025.
