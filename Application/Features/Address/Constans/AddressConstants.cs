using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Address.Constans
{
    public static class AddressConstants
    {
        // General
        public const string Address = "Lütfen Adres giriniz.";
        public const string AddressTitle = "Lütfen Adres Başlığı giriniz.";
        public const string AddressTitleMinLength = "Adres Başlığı minimum 2 karakter olabilir.";
        public const string AddressTitleMaxLength = "Adres Başlığı maksimum 50 karakter olabilir.";
        public const string AddressMinLength = "Adres minimum 10 karakter olabilir.";
        public const string AddressMaxLength = "Adres maksimum 100 karakter olabilir.";
        public const string AddressLimitError = "Kullanıcı en fazla 3 adet adres ekleyebilir.";

        // NotFound
        public const string AddressNotFound = "Adres Bulunamadı.";

        // Create
        public const string AddressAddError = "Adres eklerken hata oluştu.";
        public const string AddressAddSuccess = "Adres başarıyla eklendi.";

        // Update
        public const string AddressUpdateError = "Adres güncellenirken hata oluştu.";
        public const string AddressUpdateSuccess = "Adres başarıyla güncellendi.";

        // Delete
        public const string AddressDeleteError = "Adres silinirken hata oluştu.";
        public const string AddressDeleteSuccess = "Adres başarıyla silindi.";
    }
}
