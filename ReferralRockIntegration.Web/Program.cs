using ReferralRockIntegration.ApiWrapper;
using ReferralRockIntegration.ApiWrapper.Interfaces;
using System.Net.Http.Headers;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var appSettingsReferralRockSection = builder.Configuration.GetSection("ReferralRock");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<IReferralRockApiWrapper, ReferralRockApiWrapper>(_ =>
{
    _.BaseAddress = new Uri("https://api.referralrock.com");

    var privateApiKey = appSettingsReferralRockSection.GetValue<string>("PrivateApiKey");
    var publicApiKey = appSettingsReferralRockSection.GetValue<string>("PublicApiKey");
    var keyBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{publicApiKey}:{privateApiKey}"));

    _.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", keyBase64);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
