using ReferralRockIntegration.ApiWrapper;
using ReferralRockIntegration.ApiWrapper.Interfaces;
using ReferralRockIntegration.ApiWrapper.Models;
using System.Net.Http.Headers;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var appSettingsReferralRockSection = builder.Configuration.GetSection("ReferralRock");

var referralRockConfiguration = appSettingsReferralRockSection.Get<ReferralRockConfiguration>();

builder.Services.AddSingleton(referralRockConfiguration);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IReferralRockApiWrapper, ReferralRockApiWrapper>();
builder.Services.AddHttpClient("ReferralRock", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://api.referralrock.com");

    var keys = $"{referralRockConfiguration.PublicApiKey}:{referralRockConfiguration.PrivateApiKey}";
    var keyEnconding = Encoding.UTF8.GetBytes(keys);
    var keyBase64 = Convert.ToBase64String(keyEnconding);

    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", keyBase64);
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
