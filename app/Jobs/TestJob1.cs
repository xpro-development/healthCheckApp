using app.Scheduler;
using app.Services;
using slack;

namespace app.Jobs;

public class TestJob1 : CronJobService
{
  private readonly ILogger<TestJob1> _logger;
  private readonly ISlack _slack;

  public TestJob1(ISchedulerConfig<TestJob1> config, ILogger<TestJob1> logger, ISlack slack) :
    base(config.CronExpression, config.TimeZoneInfo)
  {
    _logger = logger;
    _slack = slack;
  }

  public override Task StartAsync(CancellationToken cancellationToken)
  {
    _logger.LogInformation("Starting Test Job 1");
    return base.StartAsync(cancellationToken);
  }

  protected override async Task Execute(CancellationToken cancellationToken)
  {
    _logger.LogInformation("{Now} Test job is running", DateTime.Now);
    await _slack.Testing();
  }

  public override Task StopAsync(CancellationToken cancellationToken)
  {
    _logger.LogInformation("Test job 1 is stopping");
    return base.StopAsync(cancellationToken);
  }
}