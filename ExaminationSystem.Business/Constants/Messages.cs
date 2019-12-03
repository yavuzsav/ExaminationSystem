namespace ExaminationSystem.Business.Constants
{
    public static class Messages
    {
        public static string AddedSuccess = "Ekleme Başarılı.";
        public static string UpdatedSuccess = "Güncelleme Başarılı.";
        public static string DeletedSuccess = "Silme Başarılı.";
        public static string AuthorizationDenied = "Yetkiniz yok.";
        public static string UserNotFound = "Kullanıcı bulunamadı.";

        public static string EmailError =
            "Mail gönderimi sırasında bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.";

        public static string GeneralErrorMessage = "Bir hata meydana geldi. Lütfen daha sonra tekrar deneyiniz.";

        public static string NotAuthorize = "Lütfen Giriş Yapınız.";

        public static string DuplicationError = "Aynı Şekilde Başka Bir Kayıt Var.";

        public static string NotEnoughQuestions = "Bu Kategoride Yeterli Soru Yok.";
        public static string RoleAssignError = "Rol atama sırasında bir hata oluştu";
        public static string UserNotExists = "Kullanıcı Bulunamadı";
        public static string UserLocked = "Çok fazla geçersiz şifre denemesi yaptınız lütfen daha sonra tekrar deneyiniz.";
    }
}