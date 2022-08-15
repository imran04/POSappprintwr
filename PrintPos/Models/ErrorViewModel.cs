using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PrintPos.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    public class Customer
    {

        public int Id { get; set; }

        
        public string Fullname { get; set; } = string.Empty;
       
        public string PhoneNo { get; set; } = string.Empty;
       

        public string Email { get; set; } = string.Empty;

        

        public string WristBandIds { get; set; } = string.Empty;

        

        public string Address { get; set; } = string.Empty;

    }
    public class Invoice
    {

        public int Id { get; set; }

       
        public string WristBandIds { get; set; } = string.Empty;
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public ICollection<LineItem>? LineItems { get; set; }

        public decimal? GrandTotal => LineItems?.Sum(o => o.Total);

        [StringLength(128), Column(TypeName = "VARCHAR(128)")]
        public string? OperatorName { get; set; }

        public string? OperatorId { get; set; }
        //public AppUser? Operator { get; set; }
        public bool IsDraft { get; set; } = false;
        public DateTimeOffset? OrderDate { get; set; }

        // This will be the payment transaction table
        public ICollection<InvoicePayment>? Payments { get; set; }


    }

    public class LineItemPhoto
    {
        public Guid Id { get; set; }
        public int ImageId { get; set; }
        public FileLog? Image { get; set; }
        public Guid LineItemId { get; set; }
        public LineItem? LineItem { get; set; }

        [StringLength(512), Column(TypeName = "VARCHAR(512)")]
        public string? ImagePath { get; set; }
    }


    public class FileLog 
    {
        public int Id { get; set; }
        [StringLength(256), Column(TypeName = "VARCHAR(256)")]

        public string FileName { get; set; } = string.Empty;
        [StringLength(64), Column(TypeName = "VARCHAR(64)")]

        public string FolderName { get; set; } = string.Empty;

        [StringLength(8), Column(TypeName = "VARCHAR(8)")]

        public string PhotoGrapherCode { get; set; } = string.Empty;

        [StringLength(64), Column(TypeName = "VARCHAR(64)")]

        public string PhotoGrapherName { get; set; } = string.Empty;

        [StringLength(256), Column(TypeName = "VARCHAR(256)")]

        public string PhysicalPath { get; set; } = string.Empty;

        public bool Selected { get; set; } = false;
        public bool Deleted { get; set; } = false;

        public int? PhotoShotLocationId { get; set; }
        

        public ICollection<LineItemPhoto>? ItemPhotos { get; set; }
    }
    public class LineItem
    {
        public Guid Id { get; set; }
        public int SortOrder { get; set; } = 1;

        public int InvoiceId { get; set; }
        public Invoice? Invoice { get; set; }

        public ICollection<LineItemPhoto>? Photos { get; set; }

        public int PhotoServiceId { get; set; }
        public PhotoService? PhotoService { get; set; }

        public int Quantity { get; set; } = 1;

        public decimal? Total => PhotoService?.InclusiveTax * Quantity;




    }

    public class PhotoService 
    {
        
        public int Id { get; set; }

       
        public string Name { get; set; } = string.Empty;

        
        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,10)")]
        public decimal Amount { get; set; }
        [Column(TypeName = "decimal(18,10)")]
        public decimal CGST { get; set; }
        [Column(TypeName = "decimal(18,10)")]
        public decimal SGST { get; set; }

        public DateTimeOffset? EffectiveFrom { get; set; }
        public DateTimeOffset? EffectiveTo { get; set; }

        [EnumDataType(typeof(ServiceDeliveryMode))]
        public ServiceDeliveryMode DeliveryMode { get; set; } = ServiceDeliveryMode.Physical;

        public decimal InclusiveTax => Amount + Amount * ((CGST + SGST) / 100);

       

        public ICollection<LineItem>? Items { get; set; }
    }
    public enum ServiceDeliveryMode
    {

        Digital = 1,
        Physical = 2,
    }
    public class PaymentType 
    {
        public int Id { get; set; }
        [StringLength(64), Column(TypeName = "VARCHAR(64)")]

        public string Name { get; set; } = string.Empty;
        public DateTimeOffset? EffectiveFrom { get; set; }
        public DateTimeOffset? EffectiveTo { get; set; }
    }
    public class InvoicePayment
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public Invoice? Invoice { get; set; }

        public int PaymentTypeId { get; set; }
        public PaymentType? PaymentType { get; set; }
        public decimal Amount { get; set; }
        public string Narration { get; set; } = string.Empty;// for transaction id/ cheque no etc

        public DateTimeOffset OnDateTime { get; set; }
        public bool IsPaid { get; set; } = true;

    }

}