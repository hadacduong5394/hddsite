using HD.IdentityManager.IService;
using HD.Infrastructure.UnitOfWork;
using System.Text;
using System.Text.RegularExpressions;

namespace HD.IdentityManager.ServiceImp
{
    public class BaseService : IBaseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BaseService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public void BeginTran()
        {
            _unitOfWork.BeginTran();
        }

        public void CommitChanges()
        {
            _unitOfWork.CommitChange();
        }

        public void CommitTran()
        {
            _unitOfWork.CommitTran();
        }

        public void RollbackTran()
        {
            _unitOfWork.RollbackTran();
        }

        public string StandardizedCode(string inputCode)
        {
            var codeUnsign = UnsignToString(inputCode);
            string[] str = codeUnsign.Split('-');
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                result.Append(str[i]);
            }
            return result.ToString();
        }

        public string UnsignToString(string input)
        {
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            input = input.Replace(".", "-");
            input = input.Replace(" ", "-");
            input = input.Replace(",", "-");
            input = input.Replace(";", "-");
            input = input.Replace(":", "-");
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            return str2.Replace("--", "-").ToLower();
        }
    }
}