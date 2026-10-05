using Microsoft.AspNetCore.Mvc; using Microsoft.EntityFrameworkCore; using NonBaoHiemVietTin.Data; using NonBaoHiemVietTin.Models;
namespace NonBaoHiemVietTin.Controllers.Api;
[ApiController, Route("api/[controller]")]
public abstract class CrudController<TEntity>(AppDbContext db):ControllerBase where TEntity:class {
 [HttpGet] public async Task<ActionResult<List<TEntity>>> Get()=>await db.Set<TEntity>().AsNoTracking().Take(1000).ToListAsync();
 [HttpGet("{id:int}")] public async Task<ActionResult<TEntity>> Get(int id){var x=await db.Set<TEntity>().FindAsync(id);return x==null?NotFound():x;}
 [HttpPost] public async Task<ActionResult<TEntity>> Post(TEntity value){db.Set<TEntity>().Add(value);await db.SaveChangesAsync();return Ok(value);}
 [HttpPut("{id:int}")] public async Task<IActionResult> Put(int id,TEntity value){var entry=db.Entry(value);var pk=entry.Metadata.FindPrimaryKey();if(pk==null||pk.Properties.Count!=1)return BadRequest("Composite keys require a dedicated endpoint.");entry.Property(pk.Properties[0].Name).CurrentValue=id;entry.State=EntityState.Modified;await db.SaveChangesAsync();return NoContent();}
 [HttpDelete("{id:int}")] public async Task<IActionResult> Delete(int id){var x=await db.Set<TEntity>().FindAsync(id);if(x==null)return NotFound();db.Remove(x);await db.SaveChangesAsync();return NoContent();}
}
public sealed class AccountController(AppDbContext db):CrudController<Account>(db); public sealed class ProductController(AppDbContext db):CrudController<Product>(db);
public sealed class CategoryController(AppDbContext db):CrudController<Category>(db); public sealed class GroupProductController(AppDbContext db):CrudController<GroupProduct>(db);
public sealed class ProductionController(AppDbContext db):CrudController<Production>(db); public sealed class OrderController(AppDbContext db):CrudController<Order>(db);
public sealed class RateController(AppDbContext db):CrudController<Rate>(db); public sealed class RoleController(AppDbContext db):CrudController<Role>(db);
public sealed class BrandController(AppDbContext db):CrudController<Brand>(db); public sealed class NewsController(AppDbContext db):CrudController<News>(db);
public sealed class NewsTypeController(AppDbContext db):CrudController<NewsType>(db); public sealed class ContactController(AppDbContext db):CrudController<Contact>(db);
public sealed class FeedbackController(AppDbContext db):CrudController<Feedback>(db); public sealed class IntroduceController(AppDbContext db):CrudController<Introduce>(db);
public sealed class PromotionController(AppDbContext db):CrudController<Promotion>(db); public sealed class ReceiptController(AppDbContext db):CrudController<Receipt>(db);
public sealed class SubscribeController(AppDbContext db):CrudController<Subscribe>(db); public sealed class WheelController(AppDbContext db):CrudController<Wheel>(db);
public sealed class RechargeController(AppDbContext db):CrudController<HistoryRecharge>(db); public sealed class WithdrawController(AppDbContext db):CrudController<HistoryWithdraw>(db);
