using System.Text.Json;
namespace NonBaoHiemVietTin.Infrastructure;
public static class SessionExtensions { static readonly JsonSerializerOptions Options=new(JsonSerializerDefaults.Web); public static void SetJson<T>(this ISession s,string key,T value)=>s.SetString(key,JsonSerializer.Serialize(value,Options)); public static T? GetJson<T>(this ISession s,string key){var v=s.GetString(key);return string.IsNullOrEmpty(v)?default:JsonSerializer.Deserialize<T>(v,Options);} }
