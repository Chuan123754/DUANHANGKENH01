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
using appAPI.Background_Service;
using System.Text.Json.Serialization;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

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
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
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
builder.Services.AddHostedService<VoucherExpiryChecker>();
//builder.Services.AddHostedService<DiscountStatusChecker>();
builder.Services.AddScoped<IColorReponsitory, ColorReponsitory>();
builder.Services.AddScoped<IMaterialReponsitory, MaterialReponsitory>();    
builder.Services.AddScoped<IStyleReponsitory, StyleReponsitory>();
builder.Services.AddScoped<ISizeReponsitory, SizeReponsitory>();
builder.Services.AddScoped<ITextile_technologyReponsitory,  Textile_technologyReponsitory>();
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

// Đăng ký CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        policyBuilder =>
        {
            policyBuilder.WithOrigins("https://localhost:7277", "https://localhost:7241") // Đảm bảo đây là URL đúng
                         .AllowAnyHeader()
                         .AllowAnyMethod();
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
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"])),
        RoleClaimType = ClaimTypes.Role
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting(); // Cần thiết cho routing

app.UseCors("AllowSpecificOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(@"E:\HangKenh\appAPI\FileMedia"),
    RequestPath = "/FileMedia"
});

app.MapControllers();

app.Run();
