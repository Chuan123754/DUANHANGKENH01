﻿using ViewsFE.IServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ViewsFE.Data;
using Microsoft.JSInterop;
using ViewsFE.Services;
using Blazored.SessionStorage;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ViewsFE.Models;
using appAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<APP_DATA_DATN>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthorizationCore();
// Add services to the container.
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazorBootstrap();
builder.Services.AddRazorPages();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddHttpClient();
builder.Services.AddOptions();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer(options =>
       {
           options.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuer = true,
               ValidateAudience = true,
               ValidateLifetime = true,
               ValidateIssuerSigningKey = true,
               // Các thông số khác  
           };
       });

// Thêm cấu hình session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // thời gian timeout của session
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddServerSideBlazor()
    .AddHubOptions(options =>
    {
        options.ClientTimeoutInterval = TimeSpan.FromMinutes(2); // Tăng thời gian timeout
        options.HandshakeTimeout = TimeSpan.FromSeconds(30);
    });


// Thêm CORS nếu cần
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy =>
        {
            policy.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
builder.Services.AddDbContext<APP_DATA_DATN>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<Account, IdentityRole>()
    .AddEntityFrameworkStores<APP_DATA_DATN>()
    .AddDefaultTokenProviders();
builder.Services.AddHttpClient<IAddressServices, AddressService>();
builder.Services.AddScoped<IAddressServices, AddressService>();
builder.Services.AddScoped<ILogActivityHistoryService, LogActivityHistoryService>();
builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICategoriesServices, CategoriesServices>();
//builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IPostTagService, PostTagService>();
builder.Services.AddScoped<ITagsServices, TagsServices>();
//builder.Services.AddScoped<IPostMetaService, PostMetaService>();
builder.Services.AddScoped<IQaService, QaService>();
builder.Services.AddScoped<IDiscountServices, DiscountServices>();

builder.Services.AddScoped<IVoucherService, VoucherService>();
builder.Services.AddScoped<IUserVoucherService, UserVoucherService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<FilesIServices, FilesServices>();
builder.Services.AddScoped<IDesignerServices, DesignerServices>();
builder.Services.AddScoped<SeoIServices, SeoServices>();
builder.Services.AddScoped<IOptionsServices, OptionServices>();
builder.Services.AddScoped<IPostSer, PostServices>();
builder.Services.AddScoped<IColorServices, ColorServices>();
builder.Services.AddScoped<ISizeServices, SizeServices>();
builder.Services.AddScoped<IMaterialServices, MaterialServices>();
builder.Services.AddScoped<IStyleServices, StyleServices>();
builder.Services.AddScoped<ITextile_technologyServices, Textile_technologyServices>();
builder.Services.AddScoped<IloginServices, LoginServices>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<AddressService>();
builder.Services.AddScoped<IProductAttributeServices, ProductAttributeServices>();
builder.Services.AddScoped<IProductVariantServices, ProductVariantServices>();
builder.Services.AddScoped<OrderDetailsIServices, OrderDetailsServices>();
builder.Services.AddScoped<IOrderIServices, OrderServices>();
builder.Services.AddScoped<IBannerServices, BannerServices>();
builder.Services.AddScoped<IDiscountServices, DiscountServices>();
builder.Services.AddScoped<IAttributesDiscountServices, AttributesDiscountServices>();
builder.Services.AddScoped<IUploadService, UploadService>();
builder.Services.AddScoped<IPaymentServices, PaymentServices>();
builder.Services.AddScoped<IOrderTrackingService, OrderTrackingService>();
builder.Services.AddHttpClient<MomoService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddServerSideBlazor(options => options.DetailedErrors = true);
builder.Services.AddScoped<IContacServices, ContacServices>();
builder.Services.AddScoped<IAccsessViewscsServices, AccsessViewsServices>();
builder.Services.AddScoped<IProductsReturnedService, ProductsReturnedService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();



app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();