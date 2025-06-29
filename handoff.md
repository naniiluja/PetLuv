# Tài liệu chuyển giao dự án - PetLuv

Tài liệu này tóm tắt tiến độ, trạng thái hiện tại, và các bước tiếp theo của dự án PetLuv.

## 1. Tiến độ hiện tại (Các task đã hoàn thành)

Dựa trên `todolist.md`, các công việc sau đã được hoàn thành:

- **Giai đoạn 1: Khởi tạo và Kiến trúc**
  - [x] Thảo luận và chốt giải pháp State Management cho Frontend.
  - [x] Tạo cấu trúc thư mục dự án (`/backend`, `/frontend`).
  - [x] Tạo file `spec.md` và `todolist.md` trong thư mục gốc dự án.
  - [x] Thiết lập Git repository, cấu hình `.gitignore`, và đẩy mã nguồn lên GitHub.

- **Giai đoạn 2: Phát triển Backend (.NET 9.0)**
  - [x] Thiết lập project .NET theo kiến trúc DDD (Domain, Application, Infrastructure, API).
  - [x] Thiết lập kết nối Database (Oracle DB) với Dapper và User Secrets.

## 2. Trạng thái mã nguồn hiện tại

- **Cấu trúc Backend:**
  - Project được tổ chức theo kiến trúc Domain-Driven Design (DDD) với 4 project con: `PetLuv.Domain`, `PetLuv.Application`, `PetLuv.Infrastructure`, và `PetLuv.API`.
  - Các tham chiếu giữa các project đã được thiết lập đúng theo luồng phụ thuộc: API → Infrastructure → Application → Domain.
- **Cơ sở dữ liệu:**
  - Đã cài đặt các gói `Dapper` và `Oracle.ManagedDataAccess.Core`.
  - Lớp `DapperContext` đã được tạo để quản lý kết nối.
  - Chuỗi kết nối được lưu trữ an toàn bằng **.NET User Secrets**, không bị lộ trong mã nguồn.
- **API (Presentation Layer):**
  - Đã cài đặt `Swashbuckle.AspNetCore` để hỗ trợ Swagger UI.
  - `DapperContext` đã được đăng ký với Dependency Injection.
- **Quản lý mã nguồn (Git):**
  - Repository đã được khởi tạo và đẩy lên GitHub.
  - Nhánh mặc định là `main`.
  - Tệp `.gitignore` đã được cấu hình để bỏ qua các tệp tài liệu (`spec.md`, `todolist.md`), thư mục `.roo`, và các tệp build/nhạy cảm khác. Chỉ có thư mục `backend` và `frontend` được theo dõi.

## 3. Các bước tiếp theo

Dựa trên `todolist.md`, các công việc cần làm tiếp theo thuộc **Module Người dùng & Xác thực**:

1.  **Implement JWT và Role-based Authorization:**
    - Cài đặt các gói NuGet cần thiết (e.g., `Microsoft.AspNetCore.Authentication.JwtBearer`).
    - Cấu hình dịch vụ xác thực và phân quyền trong `Program.cs`.
    - Tạo các lớp và logic để sinh và xác thực JWT token.
    - Định nghĩa các vai trò (Admin, Pet Owner, Veterinarian).
2.  **API cho Đăng ký, Đăng nhập, Quản lý hồ sơ người dùng:**
    - Tạo các DTOs (Data Transfer Objects) cho request/response.
    - Tạo các endpoints trong `UserController`.
    - Implement các services và repositories tương ứng để xử lý logic.

## 4. Các quyết định quan trọng đã đưa ra

- **Công nghệ Frontend:** Sử dụng **React Query + Zustand** để quản lý trạng thái.
- **Cơ sở dữ liệu:** Sử dụng **Oracle Database**.
- **Bảo mật:** Chuỗi kết nối và các thông tin nhạy cảm khác được lưu trữ bằng **.NET User Secrets** trong môi trường phát triển.
- **Chiến lược Git:** Repository chỉ theo dõi mã nguồn trong thư mục `backend` và `frontend`. Các tài liệu dự án được giữ ở local và không được commit lên repository.