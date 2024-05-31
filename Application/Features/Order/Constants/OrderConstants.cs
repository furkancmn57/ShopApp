using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Order.Constants
{
    public static class OrderConstants
    {
        // General
        public const string CustomerName = "Lütfen Müşteri Adını giriniz.";
        public const string CustomerNameMinLength = "Müşteri Adı minimum 3 karakter olabilir.";
        public const string CustomerNameMaxLength = "Müşteri Adı maksimum 50 karakter olabilir.";

        // NotFound
        public const string OrderNotFound = "Sipariş Bulunamadı.";

        // Create
        public const string OrderAddError = "Sipariş eklerken hata oluştu.";
        public const string OrderAddSuccess = "Sipariş başarıyla eklendi.";

        // Update
        public const string OrderUpdateError = "Sipariş güncellenirken hata oluştu.";
        public const string OrderUpdate = "Sipariş başarıyla güncellendi.";

        // Delete
        public const string OrderDeleteError = "Sipariş silinirken hata oluştu.";
        public const string OrderDeleteSuccess = "Sipariş başarıyla silindi.";
    }
}
