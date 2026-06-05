# 📚 Ứng dụng Đăng ký Học phần (Course Registration Application)

Đồ án Giữa kỳ môn Lập trình Web xây dựng bằng **ASP.NET Core MVC (.NET 10)**, **Entity Framework Core (SQL Server)** và **ASP.NET Core Identity**.

---

## 👤 Thông tin Sinh viên
* **Họ và tên:** Nguyễn Phi Phúc
* **Mã số sinh viên (MSSV):** 2387701131
* **Đề bài:** Hệ thống đăng ký học phần cho sinh viên (KTGK)

---

## 🛠️ Công nghệ Sử dụng
* **Framework:** ASP.NET Core MVC (.NET 10)
* **ORM:** Entity Framework Core
* **Cơ sở dữ liệu:** SQL Server (LocalDB)
* **Bảo mật & Phân quyền:** ASP.NET Core Identity
* **Xác thực mở rộng:** Google OAuth 2.0 (Google Sign-In)
* **Giao diện:** Bootstrap 5, FontAwesome, CSS custom (hiệu ứng Glassmorphism & Modern Dark/Light Accent)

---

## ✨ Các Chức năng Đã Hoàn Thành (10/10 Câu)

1. **Trang Home (Câu 1 - 2.5đ):** Hiển thị danh sách học phần (Hình ảnh, Tên, Tín chỉ, Giảng viên). Có phân trang (5 học phần/trang) sử dụng LINQ `Skip()` và `Take()`.
2. **Quản lý Học phần CRUD (Câu 2 - 1.5đ):** Trang Admin quản lý danh sách học phần, thêm mới (có upload ảnh minh họa), chỉnh sửa và xóa học phần.
3. **Đăng ký Tài khoản (Câu 3 - 1.0đ):** Cho phép sinh viên đăng ký tài khoản mới. Tài khoản sau khi tạo được tự động gán vai trò `STUDENT` và tự động đăng nhập.
4. **Phân quyền người dùng (Câu 4 - 0.5đ):**
   * Chỉ vai trò `ADMIN` mới truy cập được các trang quản lý học phần `/admin`.
   * Chỉ vai trò `STUDENT` mới đăng ký và hủy đăng ký học phần được.
   * Hiển thị trang báo lỗi **Access Denied** đẹp mắt khi truy cập trái phép.
5. **Đăng nhập hệ thống (Câu 5 - 0.5đ):** Xác thực tài khoản bằng mật khẩu, hiển thị thông báo lỗi trực quan khi nhập sai.
6. **Đăng ký / Hủy đăng ký học phần (Câu 6 - 1.0đ):** Cho phép sinh viên nhấn "Đăng ký" học phần trực tiếp trên trang chủ. Sau khi đăng ký, nút chuyển sang màu đỏ "Hủy đăng ký" và hiển thị trạng thái "Đã đăng ký".
7. **Trang Học phần của tôi (Câu 7 - 0.5đ):** Hiển thị danh sách các học phần mà sinh viên hiện tại đã đăng ký kèm thời gian đăng ký.
8. **Tìm kiếm Học phần (Câu 8 - 0.5đ):** Bộ lọc tìm kiếm nhanh các học phần theo Tên môn học ngay tại trang chủ.
9. **Đăng nhập bằng Google (Câu 9 - 1.0đ):**
   * Tích hợp Google OAuth 2.0.
   * **Cơ chế giả lập (Testing Mode):** Tự động kích hoạt khi chưa điền API Key thật, giúp chấm bài nhanh bằng cách nhập email test và họ tên giả lập để đăng nhập trực tiếp.
10. **Trang Dashboard thống kê (Câu 10 - 1.0đ):** Trang tổng quan cho Admin hiển thị:
    * Tổng số học phần, tổng số sinh viên, tổng số lượt đăng ký.
    * Bảng thống kê số lượng học phần theo từng danh mục.
    * Danh sách Top 5 học phần được đăng ký nhiều nhất.

---

## 🚀 Hướng dẫn Cài đặt & Chạy ứng dụng

### 1. Yêu cầu hệ thống
* [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
* SQL Server LocalDB hoặc SQL Server Management Studio (SSMS)

### 2. Cấu hình Database & Khởi tạo
Mở Terminal tại thư mục gốc của dự án (`2387701131_NguyenPhiPhuc_KTGK`) và chạy lệnh tạo database & dữ liệu mẫu:

```bash
# Thực thi Migration để tạo Database và bảng tự động
dotnet ef database update
```
*(Hệ thống đã cấu hình Seed Data tự động chèn 4 Categories, 12 Courses, tạo vai trò ADMIN, STUDENT và 1 tài khoản quản trị mặc định)*

### 3. Tài khoản Admin Mặc định
* **Tên đăng nhập (Username):** `admin`
* **Mật khẩu (Password):** `Admin@123`

### 4. Cấu hình Google Login (Nếu muốn dùng API thật)
Mở file `appsettings.json` và thay thế thông tin Client của bạn:
```json
"Authentication": {
  "Google": {
    "ClientId": "204886884675-aukv6kn5ls5r49qan9dspkri7gj0sl9q.apps.googleusercontent.com",
    "ClientSecret": "MÃ_BÍ_MẬT_OAuth_CỦA_BẠN"
  }
}
```

### 5. Chạy ứng dụng
Chạy lệnh sau trong Terminal:
```bash
dotnet run
```
Truy cập ứng dụng tại địa chỉ: **`http://localhost:5129`** hoặc **`https://localhost:7018`**
