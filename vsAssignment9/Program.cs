using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using assignment8.Models;

var builder = WebApplication.CreateBuilder(args);

// ��ӷ�������
builder.Services.AddRazorPages();
builder.Services.AddDbContext<assignment8.Data.assignment8Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// ���� SeedData ��ʼ�����ݿ�
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        SeedData.Initialize(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the database.");
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
