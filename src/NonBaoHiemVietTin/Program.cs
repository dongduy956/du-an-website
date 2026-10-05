using Microsoft.EntityFrameworkCore;
using NonBaoHiemVietTin.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(o => { o.Cookie.HttpOnly=true; o.Cookie.IsEssential=true; o.IdleTimeout=TimeSpan.FromMinutes(30); });
var cs=builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("ConnectionStrings:DefaultConnection is required.");
builder.Services.AddDbContext<AppDbContext>(o=>o.UseSqlServer(cs));

var app=builder.Build();
if(!app.Environment.IsDevelopment()){app.UseExceptionHandler("/loi-404");app.UseHsts();}
app.UseHttpsRedirection(); app.UseStaticFiles(); app.UseRouting(); app.UseSession(); app.UseAuthorization();

void Route(string name,string pattern,string controller,string action)=>app.MapControllerRoute(name,pattern,new{controller,action});
Route("withdraw-cancel","huy-lenh-rut-tien","Accounts","DeleteWithdraw");
Route("withdraw","rut-tien","Wheel","Withdraw"); Route("recharge-confirm","xac-nhan-nap-tien","Wheel","ConfirmRecharge"); Route("recharge","nap-tien","Wheel","Recharge");
Route("account-withdraw","lich-su-rut-tien","Accounts","AccountWithdraw"); Route("account-recharge","lich-su-nap-tien","Accounts","AccountRecharge"); Route("account-wheel","lich-su-quay-thuong","Accounts","AccountWheel");
Route("wheel","quay-thuong","Wheel","Index"); Route("login","dang-nhap","Accounts","Login"); Route("logout","dang-xuat","Accounts","Logout");
Route("cart-update","cap-nhat-so-luong-gio-hang","Cart","UpdateItem"); Route("suggest","danh-sach-goi-y","Products","ListName"); Route("pay","thanh-toan","Cart","Pay");
Route("register","dang-ki","Accounts","Register"); Route("account-update","cap-nhat","Accounts","Update"); Route("subscribe","dang-ki-nhan-tin","Subscribe","News");
Route("change-password","doi-mat-khau","Accounts","ChangePassword"); Route("forgot","quen-mat-khau","Accounts","ForgetPass");
Route("wish-add","them-vao-yeu-thich","Wish","AddItem"); Route("cart-delete","xoa-gio-hang","Cart","DeleteItem"); Route("wish-delete","xoa-yeu-thich","Wish","DeleteItem"); Route("cart-add","them-vao-gio-hang","Cart","AddItem");
Route("rate","danh-gia-san-pham","Products","Ratting"); Route("feedback","phan-hoi","Contact","Feedback"); Route("not-found","loi-404","Error","NotFound");
Route("confirm-order","xac-nhan-don-hang","Cart","ConfirmOrder"); Route("account","thong-tin-tai-khoan","Accounts","AccountInfo"); Route("intro","gioi-thieu","Introduce","Index"); Route("contact","lien-he","Contact","Index");
Route("wish","yeu-thich","Wish","Index"); Route("cart","gio-hang","Cart","Index"); Route("search","tim-kiem","Products","Search");
app.MapControllerRoute("news-detail","tin-tuc/chi-tiet/{alias}",new{controller="News",action="NewsDetail"}); app.MapControllerRoute("product-detail","chi-tiet/{alias}",new{controller="Products",action="ProductDetail"});
app.MapControllerRoute("news","tin-tuc/{alias}",new{controller="News",action="Index"}); app.MapControllerRoute("product-group","{alias}/{alia}",new{controller="Products",action="GroupProducts"});
app.MapControllerRoute("product","{alias}",new{controller="Products",action="Index"}); app.MapControllerRoute("default","{controller=Home}/{action=Index}/{id?}");
app.Run();
