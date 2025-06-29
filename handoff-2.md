# Tài liệu chuyển giao dự án - PetLuv (Lần 2)

Tài liệu này tóm tắt tiến độ, trạng thái hiện tại, và các bước tiếp theo của dự án PetLuv, tiếp nối từ tài liệu chuyển giao đầu tiên.

## 1. Tiến độ hiện tại (Các task đã hoàn thành)

Dựa trên `todolist.md`, các công việc sau đã được hoàn thành kể từ lần chuyển giao trước:

- **Module Người dùng & Xác thực:**
  - [x] Implement JWT và Role-based Authorization.
  - [x] API cho Đăng ký, Đăng nhập.
- **Module Quản lý Thú cưng:**
  - [x] Thiết lập cấu trúc cho API CRUD (Entities, DTOs, Repositories, Controller).

## 2. Trạng thái mã nguồn hiện tại

- **Xác thực & Phân quyền:**
  - Đã cài đặt và cấu hình đầy đủ `Microsoft.AspNetCore.Authentication.JwtBearer`.
  - `AuthService` được tạo để xử lý logic đăng ký (với mã hóa mật khẩu bằng BCrypt) và đăng nhập (tạo JWT).
  - `AuthController` đã được triển khai để xử lý các request `/api/auth/register` và `/api/auth/login`.
  - Các cảnh báo về non-nullable đã được xử lý trên toàn bộ solution.
- **Module Thú cưng:**
  - Đã tạo Entity `Pet` trong tầng Domain.
  - Đã tạo các DTOs (`PetDto`, `CreatePetRequestDto`, `UpdatePetRequestDto`) trong tầng Application.
  - Đã định nghĩa `IPetRepository` và triển khai `PetRepository` với các phương thức CRUD cơ bản sử dụng Dapper.
  - Đã tạo `PetController` với các action CRUD (chưa có logic nghiệp vụ) và yêu cầu xác thực (`[Authorize]`).
- **Cấu trúc & DI:**
  - `DapperContext` đã được tạo và đăng ký trong DI container để quản lý kết nối DB.
  - Các repository (`UserRepository`, `PetRepository`) và service (`AuthService`, `JwtService`) đã được đăng ký đầy đủ với DI.
- **Quản lý mã nguồn (Git):**
  - Tất cả các thay đổi đã được commit và đẩy lên remote repository (`main` branch).

## 3. Các bước tiếp theo

Dựa trên `todolist.md`, các công việc cần làm tiếp theo là:

1.  **Hoàn thiện Module Quản lý Thú cưng:**
    - Tạo `PetService` để chứa logic nghiệp vụ cho các hoạt động CRUD của thú cưng.
    - Triển khai logic trong `PetController` để gọi `PetService`. Cần xử lý việc lấy `OwnerId` từ JWT context của người dùng đang đăng nhập.
    - Viết Unit Test cho `PetService`.
2.  **Triển khai Module Quản lý hồ sơ y tế:**
    - Bắt đầu với việc tạo Entity, DTO, Repository và Controller cho hồ sơ y tế.

## 4. Các quyết định quan trọng đã đưa ra

- **Băm mật khẩu:** Sử dụng thư viện `BCrypt.Net-Next` để băm và xác minh mật khẩu người dùng.
- **Cấu trúc Module:** Tiếp tục tuân thủ kiến trúc DDD đã thiết lập, tạo các thành phần (Entity, DTO, Repository, Service, Controller) cho mỗi module mới.