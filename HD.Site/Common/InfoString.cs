namespace HD.Site.Common
{
    public class InfoString
    {
        private static InfoString _instance;

        public const string ERROR_SYSTEM = "Có lỗi xẩy ra trong quá trình xử lý hệ thống, hãy thử lại sau.";

        public const string CREATE_SUCCESSFULL = "Thêm mới thành công.";

        public const string UPDATE_SUCCESSFULL = "Cập nhật thành công.";

        public const string DELETE_SUCCESSFULL = "Xóa thành công.";

        public const string INVALID_INFO = "Dữ liệu đang sai định dạng quy định, hãy kiểm tra lại.";

        public static InfoString Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new InfoString();

                return _instance;
            }

            private set
            {
                _instance = value;
            }
        }

        public string SetContainString(string str)
        {
            return str + " này đã tồn tại, hãy thử lại với tên khác.";
        }
    }
}