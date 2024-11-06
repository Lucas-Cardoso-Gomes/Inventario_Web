var builder = WebApplication.CreateBuilder(args);
// Adiciona os serviços ao contêiner
builder.Services.AddControllersWithViews();

// Configuração do Kestrel para escutar em todas as interfaces de rede
builder.WebHost.ConfigureKestrel(serverOptions =>
{
serverOptions.ListenAnyIP(5223); // HTTP
serverOptions.ListenAnyIP(7297, listenOptions => listenOptions.UseHttps()); // HTTPS
});

var app = builder.Build();

// Configure o pipeline HTTP
if (!app.Environment.IsDevelopment())
{
app.UseExceptionHandler("/Home/Error");
app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
name: "default",
pattern: "{controller=Computadores}/{action=Index}/{id?}");

app.Run();