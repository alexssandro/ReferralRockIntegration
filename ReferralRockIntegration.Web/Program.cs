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

    var privateApiKey = appSettingsReferralRockSection.GetValue<string>("PrivateApiKey");
    var publicApiKey = appSettingsReferralRockSection.GetValue<string>("PublicApiKey");
    var keyBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{publicApiKey}:{privateApiKey}"));

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
