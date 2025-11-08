# vnpassword - TightVNC Password Generator

This is a simple Windows utility to create encrypted password hashes for TightVNC. TightVNC stores passwords in the registry in an encrypted format. This tool allows you to generate that encrypted value from a plain-text password.

## Usage

1.  Run the `vnpassword.exe` application.
2.  Enter your desired password into the password field.
3.  Click the "Encrypt" button.
4.  The encrypted password will be displayed.
5.  Copy this value and use it in your TightVNC configuration (e.g., in the Windows Registry).

## Building from Source

1.  Open `vnpassword.sln` in Visual Studio.
2.  Build the solution. The executable will be in the `bin/Debug` or `bin/Release` folder.

---

# vnpassword - Trình tạo mật khẩu TightVNC

Đây là một tiện ích Windows đơn giản để tạo mật khẩu đã được mã hóa cho TightVNC. TightVNC lưu trữ mật khẩu trong registry dưới dạng đã được mã hóa. Công cụ này cho phép bạn tạo ra giá trị đã mã hóa đó từ một mật khẩu văn bản thuần túy.

## Cách sử dụng

1.  Chạy ứng dụng `vnpassword.exe`.
2.  Nhập mật khẩu bạn muốn vào ô mật khẩu.
3.  Nhấp vào nút "Mã hóa".
4.  Mật khẩu đã mã hóa sẽ được hiển thị.
5.  Sao chép giá trị này và sử dụng nó trong cấu hình TightVNC của bạn (ví dụ: trong Windows Registry).

## Biên dịch từ mã nguồn

1.  Mở `vnpassword.sln` bằng Visual Studio.
2.  Build solution. Tệp thực thi sẽ nằm trong thư mục `bin/Debug` hoặc `bin/Release`.
