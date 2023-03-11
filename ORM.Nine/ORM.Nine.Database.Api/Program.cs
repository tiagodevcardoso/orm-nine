namespace ORM.Nine.Database.API
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().UseKestrel(options =>
                    {
                        var config = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json", true, true)
                            .Build();

                        var port = config.GetValue<int>("Port_Api_Nine"); // port defaults 

                        if (args.Length > 0)
                            port = int.Parse(args[0]); // args #1 is port

                        if (args.Length > 2)
                            options.ListenAnyIP(port, listenOptions =>
                            {
                                listenOptions.UseHttps(args[1],
                                    args[2]); // args #2 and #3 are path to cert.pfx and its password
                            });
                        else
                            options.ListenAnyIP(port);
                    });
                });
        }
    }
}