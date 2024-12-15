using appAPI.IRepository;
using appAPI.Repository;
using appAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
//using appAPI.Background_Service;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;

var builder = WebApplication.CreateBuilder(args);

// Thêm cấu hình Cookie
builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.Cookie.Name = "MyAppAuthCookie"; // Tên cookie
        options.Cookie.HttpOnly = true; // Cookie chỉ có thể được truy cập từ server
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // Chỉ gửi qua HTTPS nếu có
        options.Cookie.SameSite = SameSiteMode.Lax; // Ngăn chặn CSRF
        options.ExpireTimeSpan = TimeSpan.FromDays(30); // Thời gian sống của cookie
        options.LoginPath = "/admin/Loginclient"; // Đường dẫn đến trang đăng nhập
        options.LogoutPath = "/admin/Loginclient"; // Đường dẫn đến trang đăng xuất
    });

builder.Services.AddHttpClient();

// Add services to the container.
// Đăng ký DbContext và cấu hình chuỗi kết nối từ appsettings.json
builder.Services.AddDbContext<APP_DATA_DATN>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddControllers()
//    .AddJsonOptions(options =>
//    {
//        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
//    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "HangKenh API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

// Đăng ký các kho lưu trữ (repositories)
builder.Services.AddScoped<IAccountRepo, AccountRepo>();
builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddScoped<ITagsRepository, TagsRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IPostReponsetory, PostReponsetory>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
//builder.Services.AddHostedService<VoucherExpiryChecker>();
//builder.Services.AddHostedService<DiscountStatusChecker>();
builder.Services.AddScoped<IColorReponsitory, ColorReponsitory>();
builder.Services.AddScoped<IMaterialReponsitory, MaterialReponsitory>();
builder.Services.AddScoped<IStyleReponsitory, StyleReponsitory>();
builder.Services.AddScoped<ISizeReponsitory, SizeReponsitory>();
builder.Services.AddScoped<ITextile_technologyReponsitory, Textile_technologyReponsitory>();
builder.Services.AddScoped<FilesIRepository, FilesReponsetory>();
builder.Services.AddScoped<IBannerRepository, BannerRepository>();
builder.Services.AddScoped<SeoIReponsitory, SeoReponsitory>();
builder.Services.AddScoped<OrderIReponsitory, OrderReponsitory>();
builder.Services.AddScoped<OrderDetailsIReponsitory, OrderDetailsReponsitory>();
builder.Services.AddScoped<MenuIReponsitory, MenuReponsitory>();
builder.Services.AddScoped<IDesignerRepon, DesignerRepon>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IProductAttributesRepository, ProductAttributesRepository>();
builder.Services.AddScoped<IProductVariantsRepository, ProductVariantsRepository>();
builder.Services.AddScoped<IProductAttributeDiscountRepository, ProductAttributeDiscount>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.Configure<MomoOptionModel>(builder.Configuration.GetSection("MomoAPI"));
builder.Services.AddScoped<IAccsessViewsRepon,  AccsessViewsRepon>();  
builder.Services.AddScoped<IMomoRepository, MomoRepository>();
builder.Services.AddScoped<IVnPayService, VnPayService>();
builder.Services.AddScoped<IAdminRepo, AdminRep>();
builder.Services.AddScoped<IProduct_variants_wishlist_Reponsitory, Product_variants_wishlist_Reponsitory>();

builder.Services.AddControllers(options =>
{
    options.ModelMetadataDetailsProviders.Add(new SuppressChildValidationMetadataProvider(typeof(string)));
});
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});
builder.Services.AddScoped<IContactReponsetory, ContacReponsetory>();

// Đăng ký CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        policyBuilder =>
        {
            policyBuilder.WithOrigins("https://localhost:7277", "https://localhost:7032") // Đảm bảo đây là URL đúng
                         .AllowAnyHeader()
                         .AllowAnyMethod()
                        .AllowCredentials();
        });
});


// Cấu hình Identity cho Account và các role
builder.Services.AddIdentity<Account, IdentityRole>()
    .AddEntityFrameworkStores<APP_DATA_DATN>()
    .AddDefaultTokenProviders();

// Cấu hình xác thực JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"])),
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.None, // Cho phép tất cả SameSite
});


app.UseHttpsRedirection();

app.UseRouting(); // Cần thiết cho routing

app.UseCors("AllowSpecificOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(@"E:\HangKenh\appAPI\FileMedia"),
   // FileProvider = new PhysicalFileProvider(@"D:\DATN\DUANHANGKENH01\appAPI\FileMedia"),
    //FileProvider = new PhysicalFileProvider(@"I:\VIs Stu fille\DATN\DATN-Blazon\appAPI\FileMedia"),

    RequestPath = "/FileMedia"
});

app.MapControllers();

app.Run();
