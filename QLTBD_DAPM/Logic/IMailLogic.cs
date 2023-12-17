using ITShop.Models;

namespace ITShop.Logic
{
    public interface IMailLogic
    {
        Task SendEmail(MailInfo mailInfo);
        Task SendEmailDatHangThanhCong(DatHang datHang, MailInfo mailInfo);
    }
}
