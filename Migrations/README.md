# EF Core migrations

Migrations được sinh theo đúng schema tại môi trường SQL Server của người chạy. Sau khi cấu hình `ConnectionStrings:DefaultConnection`, chạy từ thư mục `SourceCode`:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Không xóa migration sau khi đã triển khai database có dữ liệu.
