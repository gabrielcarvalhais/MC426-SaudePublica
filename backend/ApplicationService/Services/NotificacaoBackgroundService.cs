namespace MC426_Backend.ApplicationService.Services
{
    public class NotificacaoBackgroundService : BackgroundService
    {
        public NotificacaoBackgroundService(IServiceProvider serviceProvider) { }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    System.Console.WriteLine("Background Worker acionado.");
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("Could not send notifications.");
                    System.Console.Error.WriteLine(ex.Message);
                    continue;
                }

                await Task.Delay(60 * 1000, stoppingToken);
            }
        }
    }
}