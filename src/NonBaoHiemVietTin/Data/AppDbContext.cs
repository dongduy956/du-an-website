using Microsoft.EntityFrameworkCore;
using NonBaoHiemVietTin.Models;

namespace NonBaoHiemVietTin.Data;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts => Set<Account>(); public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>(); public DbSet<GroupProduct> GroupProducts => Set<GroupProduct>();
    public DbSet<Production> Productions => Set<Production>(); public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>(); public DbSet<Rate> Rates => Set<Rate>();
    public DbSet<Role> Roles => Set<Role>(); public DbSet<Brand> Brands => Set<Brand>(); public DbSet<News> News => Set<News>();
    public DbSet<NewsType> NewsTypes => Set<NewsType>(); public DbSet<Contact> Contacts => Set<Contact>();
    public DbSet<Feedback> Feedback => Set<Feedback>(); public DbSet<Introduce> Introduces => Set<Introduce>();
    public DbSet<Promotion> Promotions => Set<Promotion>(); public DbSet<Receipt> Receipts => Set<Receipt>();
    public DbSet<ReceiptDetail> ReceiptDetails => Set<ReceiptDetail>(); public DbSet<Subscribe> Subscribes => Set<Subscribe>();
    public DbSet<Wheel> Wheels => Set<Wheel>(); public DbSet<HistoryRecharge> HistoryRecharges => Set<HistoryRecharge>();
    public DbSet<HistoryWithdraw> HistoryWithdraws => Set<HistoryWithdraw>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<Account>().ToTable("accounts").HasKey(x=>x.Id); b.Entity<Product>().ToTable("products").HasKey(x=>x.Id);
        b.Entity<Category>().ToTable("category").HasKey(x=>x.Id); b.Entity<GroupProduct>().ToTable("groupproduct").HasKey(x=>x.Id);
        b.Entity<Production>().ToTable("production").HasKey(x=>x.Id); b.Entity<Order>().ToTable("order").HasKey(x=>x.Id);
        b.Entity<OrderDetail>().ToTable("orderdetail").HasKey(x=>new{x.IdProduct,x.IdOrder}); b.Entity<Rate>().ToTable("rate").HasKey(x=>x.Id);
        b.Entity<Role>().ToTable("role").HasKey(x=>x.Id); b.Entity<Brand>().ToTable("brand").HasKey(x=>x.Id);
        b.Entity<News>().ToTable("news").HasKey(x=>x.Id); b.Entity<NewsType>().ToTable("newstype").HasKey(x=>x.Id);
        b.Entity<Contact>().ToTable("contact").HasKey(x=>x.Id); b.Entity<Feedback>().ToTable("feedback").HasKey(x=>x.Id);
        b.Entity<Introduce>().ToTable("introduce").HasKey(x=>x.Id); b.Entity<Promotion>().ToTable("promotion").HasKey(x=>x.Id);
        b.Entity<Receipt>().ToTable("receipt").HasKey(x=>x.Id); b.Entity<ReceiptDetail>().ToTable("receiptdetail").HasKey(x=>new{x.IdProduct,x.IdReceipt});
        b.Entity<Subscribe>().ToTable("subscribe").HasKey(x=>x.Id); b.Entity<Wheel>().ToTable("wheel").HasKey(x=>x.Id);
        b.Entity<HistoryRecharge>().ToTable("history_recharge").HasKey(x=>x.Id); b.Entity<HistoryWithdraw>().ToTable("history_withdraw").HasKey(x=>x.Id);
        foreach (var e in b.Model.GetEntityTypes())
            foreach (var p in e.GetProperties()) p.SetColumnName(ToLegacyColumn(p.Name));
    }
    private static string ToLegacyColumn(string name) => name switch {
        "IdRole"=>"idrole","IsSocial"=>"issocial","CreateDate"=>"create_date","DateAttendance"=>"date_attendance",
        "PromationPrice"=>"promationprice","ViewCount"=>"viewcount","CreatedDate"=>"createddate","FastSell"=>"fastsell","NewProduct"=>"newproduct",
        "IdCategory"=>"idcategory","IdProduction"=>"idproduction","IdGroupProduct"=>"idgroupproduct","IsDelete"=>"isdelete","MoreImage"=>"moreimage",
        "IdAccount"=>"idaccount","StatusPay"=>"statuspay","PaymentMethod"=>"paymentmethod","IdPromotion"=>"idpromotion","IdProduct"=>"idproduct","IdOrder"=>"idorder",
        "IdNewsType"=>"id_newstype","WorkTime"=>"worktime","WorkDay"=>"workday","StartDate"=>"start_date","EndDate"=>"end_date","CreateBy"=>"create_by",
        "QuantityUse"=>"quantity_use","IdReceipt"=>"idreceipt","GiftName"=>"gift_name","AmountMoney"=>"amount_money","ConfirmDate"=>"confirm_date",
        "BankNumber"=>"bank_number","BankName"=>"bank_name", _=>name.ToLowerInvariant()
    };
}
