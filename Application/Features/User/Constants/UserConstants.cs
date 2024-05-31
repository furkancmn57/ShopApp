using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Constants
{
    public static class UserConstants
    {
        // General
        public const string FirstNameNotEmpty = "Lütfen Adınızı giriniz.";
        public const string FirstNameMinLength = "Adınız minimum 3 karakter olabilir.";
        public const string FirstNameMaxLength = "Adınız maksimum 50 karakter olabilir.";

        public const string LastNameNotEmpty = "Lütfen Soyadınızı giriniz.";
        public const string LastNameMinLength = "Soyadınız minimum 3 karakter olabilir.";
        public const string LastNameMaxLength = "Soyadınız maksimum 50 karakter olabilir.";

        public const string EmailNotEmpty = "Lütfen E-Posta Adresinizi giriniz.";
        public const string EmailAddress = "Lütfen geçerli bir E-Posta Adresi giriniz.";
        public const string EmailMaxLength = "E-Posta Adresiniz maksimum 50 karakter olabilir.";

        public const string PasswordNotEmpty = "Lütfen Şifrenizi giriniz.";
        public const string PasswordMinLength = "Şifreniz minimum 8 karakter olabilir.";
        public const string PasswordMaxLength = "Şifreniz maksimum 64 karakter olabilir.";


        // NotFound
        public const string UserNotFound = "Kullanıcı bulunamadı.";

        // Exist
        public const string UserExist = "Bu E-Posta Adresi ile kayıtlı bir kullanıcı bulunmaktadır.";

        // Login
        public const string UserLoginError = "Giriş yapılırken hata oluştu.";
        public const string UserEmailOrPasswordError = "E-Posta Adresi veya Şifre hatalı.";

        // Create
        public const string UserCreateError = "Kullanıcı eklerken hata oluştu.";
        public const string UserCreateSuccess = "Kullanıcı başarıyla oluşturuldu.";

        // Update
        public const string UserUpdateError = "Kullanıcı güncellenirken hata oluştu.";
        public const string UserUpdateSuccess = "Kullanıcı başarıyla güncellendi.";

        // Delete
        public const string UserDeleteError = "Kullanıcı silinirken hata oluştu.";
        public const string UserDeleteSuccess = "Kullanıcı başarıyla silindi.";
    }
}
