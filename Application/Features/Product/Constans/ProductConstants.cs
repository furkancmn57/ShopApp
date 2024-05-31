using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Product.Constans
{
    public static class ProductConstants
    {
        // General
        public const string ProductName = "Lütfen Ürün Adını giriniz.";
        public const string ProductNameMaxLength = "Ürün Adı maksimum 50 karakter olabilir.";
        public const string ProductNameMinLength = "Ürün Adı minimum 3 karakter olabilir.";

        public const string ProductDescription = "Lütfen Ürün Açıklamasını giriniz.";
        public const string ProductDescriptionMaxLength = "Ürün Açıklaması maksimum 100 karakter olabilir.";
        public const string ProductDescriptionMinLength = "Ürün Açıklaması minimum 10 karakter olabilir.";

        public const string ProductPrice = "Lütfen Ürün Fiyatını giriniz.";
        public const string ProductPriceGreaterThanZero = "Ürün Fiyatı 0'dan büyük olmalıdır.";

        public const string ProductQuantity = "Lütfen Ürün Stok miktarını giriniz.";
        public const string ProductQuantityGreaterThanZero = "Ürün Stok miktarı 0'dan büyük olmalıdır.";

        // NotFound
        public const string ProductNotFound = "Ürün bulunamadı.";

        // Create
        public const string ProductCreateSuccess = "Ürün başarıyla oluşturuldu.";
        public const string ProductCreateError = "Ürün oluşturulurken hata oluştu.";

        // Update
        public const string ProductUpdateSuccess = "Ürün başarıyla güncellendi.";
        public const string ProductUpdateError = "Ürün güncellenirken hata oluştu.";

        // Delete
        public const string ProductDeleteSuccess = "Ürün başarıyla silindi.";
        public const string ProductDeleteError = "Ürün silinirken hata oluştu.";

    }
}
