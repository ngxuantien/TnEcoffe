using System;
using System.Collections.Generic;
using System.Text;

namespace TnEcoffe.Domain.Common.Errors;

public enum ErrorType
{
    Validation,     // dữ liệu đầu vào sai, nhiều trường
    BadRequest,     // yêu cầu không hợp lệ (vd: từ ngày > đến ngày)
    BusinessRule,   // vi phạm quy tắc nghiệp vụ (vd: đơn đã hủy không thêm món)
    NotFound,
    Conflict,       // trùng dữ liệu (vd: trùng tên danh mục)
    Unauthorized,   // chưa đăng nhập
    Forbidden,      // đã đăng nhập nhưng không có quyền
}
