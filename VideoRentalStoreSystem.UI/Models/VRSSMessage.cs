namespace VideoRentalStoreSystem.UI.Models
{
    public static class VRSSMessage
    {

        /// <summary>
        /// Chưa chọn đĩa để cho thuê.
        /// </summary>
        public static string NotChooseDiskRent = "Chưa chọn đĩa để cho thuê.";
        /// <summary>
        /// Chưa chọn đĩa để bỏ.
        /// </summary>
        public static string NotChooseDiskRemove = "Chưa chọn đĩa để bỏ.";
        /// <summary>
        /// Chưa chọn khách hàng muốn thuê đĩa! Xin hay kiểm tra lại.
        /// </summary>
        public static string NotChooseCustomer = "Chưa chọn khách hàng! Xin hay kiểm tra lại.";
        /// <summary>
        /// Đã xảy ra lỗi khi tạo hóa đơn! Xin hay thử lại.
        /// </summary>
        public static string ErrorCreateRecord = "Đã xảy ra lỗi khi tạo hóa đơn! Xin hay thử lại.";
        /// <summary>
        /// Thông tin đã được lưu.
        /// </summary>
        public static string SavedInformation = "Thông tin đã được lưu.";
        /// <summary>
        /// Không có đĩa nào trong danh sách muốn thuê! Xin hay kiểm tra lại.
        /// </summary>
        public static string NoDiskInListRent = "Không có đĩa nào trong danh sách muốn thuê! Xin hay kiểm tra lại.";
        /// <summary>
        /// Trả đĩa thành công.
        /// </summary>
        public static string DiskReturnedSuccess = "Trả đĩa thành công.";
        /// <summary>
        /// Trả đĩa không thành công.
        /// </summary>
        public static string DiskReturnFail = "Trả đĩa không thành công.";
        /// <summary>
        /// Bạn chưa chọn đĩa cần trả lại.
        /// </summary>
        public static string NotChooseDiskReturn = "Bạn chưa chọn đĩa cần trả lại.";
        /// <summary>
        /// Tên khách hàng không được bỏ trống.
        /// </summary>
        public static string NameCustomerNotEmpty = "Tên khách hàng không được bỏ trống.";
        /// <summary>
        /// Tên khách hàng phải là chữ.
        /// </summary>
        public static string CustomerNameIsString = "Tên khách hàng phải là chữ.";
        /// <summary>
        /// Địa chỉ khách hàng không được bỏ trống.
        /// </summary>
        public static string AddressNotEmpty = "Địa chỉ khách hàng không được bỏ trống.";
        /// <summary>
        /// Số điện thoại khách hàng không được bỏ trống.
        /// </summary>
        public static string PhoneNumberNotEmpty = "Số điện thoại khách hàng không được bỏ trống.";
        /// <summary>
        /// Số điện thoại phải là số gồm 10 -> 11 số.
        /// </summary>
        public static string PhoneNumberLenght = "Số điện thoại phải là số gồm 10 -> 11 số.";
        /// <summary>
        /// Số điện thoại phải bắt đầu bằng số 0.
        /// </summary>
        public static string PhoneNumberBeginWith0 = "Số điện thoại phải bắt đầu bằng số 0.";
        /// <summary>
        /// Tên tựa đề không được chứa ký tự đặc biệt.
        /// </summary>
        public static string TitileNotSpecialCharacter = "Tên tựa đề không được chứa ký tự đặc biệt.";
        /// <summary>
        /// Tên tựa đề không được trống.
        /// </summary>
        public static string TitleNotEmpty = "Tên tựa đề không được trống.";
        /// <summary>
        /// Hãy chọn thể loại cho tựa đề.
        /// </summary>
        public static string ChooseTypeOfTitle = "Hãy chọn thể loại cho tựa đề.";
        /// <summary>
        /// Bạn phải chọn tiêu đề đĩa.
        /// </summary>
        public static string YouMustChooseTitle = "Bạn phải chọn tiêu đề đĩa.";
        /// <summary>
        /// Số lượng đĩa của tiêu đề không được bỏ trống.
        /// </summary>
        public static string NumberOfDiskNotEmpty = "Số lượng đĩa của tiêu đề không được bỏ trống.";
        /// <summary>
        /// Số lượng đĩa phải là số, lớn hơn 0 và nhỏ hơn 20.
        /// </summary>
        public static string NumberOfDishLargerThan0 = "Số lượng đĩa phải là số, lớn hơn 0 và nhỏ hơn 20.";
        /// <summary>
        /// Trả đĩa thành công. Đĩa này trễ hạn trả.
        /// </summary>
        public static string DiskReturnedSuccessAndLateCharge = "Trả đĩa thành công. Đĩa này trễ hạn trả. Bạn có muốn thanh toán phí trễ hạn không";
        /// <summary>
        /// Thông tin đăng nhập không đúng.
        /// </summary>
        public static string ErrorLogin = "Thông tin đăng nhập không đúng.";
        /// <summary>
        /// Đã đăng nhập vào hệ thống.
        /// </summary>
        public static string Logined = "Đã đăng nhập vào hệ thống.";
        /// <summary>
        /// Không tìm thấy cơ sở dữ liệu. Hệ thống sẽ tự động khởi tạo cơ sở dữ liệu.
        /// </summary>
        public static string NotFoundDB = "Không tìm thấy cơ sở dữ liệu. Hệ thống sẽ tự động khởi tạo cơ sở dữ liệu.";
        /// <summary>
        /// Không kết nối được với cơ sở dữ liệu. Tự tạo cơ sở dữ liệu thật bại.
        /// </summary>
        public static string CanNotConnectDB = "Không kết nối được với cơ sở dữ liệu. Tự tạo cơ sở dữ liệu thật bại.";
        /// <summary>
        /// Thêm không thành công.
        /// </summary>
        public static string AddFail = "Thêm không thành công.";
        /// <summary>
        /// Phí thuê là số nguyên dương
        /// </summary>
        public static string CostRentalIsNumber = "Phí thuê là số nguyên dương";
        /// <summary>
        /// Kỳ hạn là số nguyên dương
        /// </summary>
        public static string PeriodRentalIsNumber = "Kỳ hạn là số nguyên dương";
        /// <summary>
        /// Phí trễ hạn là số nguyên dương
        /// </summary>
        public static string CostLateChargeRentalIsNumber = "Phí trễ hạn là số nguyên dương";
        /// <summary>
        /// Cập nhật không thành công.
        /// </summary>
        public static string UdpdatedFail = "Cập nhật không thành công.";
        /// <summary>
        /// Tựa đề đang được thuê. Xóa thất bại
        /// </summary>
        public static string DeleteTitleFail = "<title> đang được thuê. Bạn có thật sự muốn xóa";
        /// <summary>
        /// Khách hàng vẫn còn đĩa chưa trả. Xóa thất bại
        /// </summary>
        public static string DeleteCustomerFail = "Khách hàng vẫn còn nợ:<latecharge>. Bạn có muốn xóa tất cả thông tin của khách hàng này";
        /// <summary>
        /// Đĩa này đang được giữ hoặc thuê. Bạn có muốn xóa không
        /// </summary>
        public static string DeleteDiskAmout = "Đĩa<id> đang được giữ, thuê hoặc có phí trễ hạn. Bạn có muốn xóa không";
        /// <summary>
        /// Ngày trả không được trước ngày cho thuê
        /// </summary>
        public static string DateReturnActualFail = "Ngày trả không được trước ngày cho thuê";
        /// <summary>
        /// Bạn chưa chọn tựa đề 
        /// </summary>
        public static string NotChooseTitle = "Bạn chưa chọn tựa đề ";
        /// <summary>
        /// Bạn chưa chọn
        /// </summary>
        public static string NotChooseReservation = "Chưa chọn mục đăt đĩa muốn xóa ";
        /// <summary>
        /// Giá thuê không được rỗng
        /// </summary>
        public static string CostEmpty = "Giá thuê không được rỗng";
        /// <summary>
        /// Kỳ hạn không được rỗng
        /// </summary>
        public static string PeriodEmpty = "Kỳ hạn không được rỗng";
        /// <summary>
        /// Phí trả trễ không được rỗng
        /// </summary>
        public static string LateChargeEmpty = "Phí trả trễ không được rỗng";
    }

}
