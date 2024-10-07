namespace CraftersCloud.Blueprint.Infrastructure.Tests;

[Category("unit")]
public class SettableTimeProviderFixture
{
    [Test]
    public async Task UtcNow_ShouldReturnCurrentTime()
    {
        // Arrange
        var timeProvider = new SettableTimeProvider();

        // Act
        var now = timeProvider.UtcNow;

        // Assert
        await Assert.That((DateTimeOffset.UtcNow - now).TotalSeconds < 1).IsTrue();
    }

    [Test]
    public async Task FixedUtcNow_ShouldReturnFixedTime_WhenSetNowIsCalled()
    {
        // Arrange
        var timeProvider = new SettableTimeProvider();
        var fixedTime = new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.Zero);

        // Act
        timeProvider.SetNow(fixedTime);
        var now = timeProvider.FixedUtcNow;

        // Assert
        await Assert.That(fixedTime).IsEqualTo(now);
    }

    [Test]
    public async Task FixedUtcNow_ShouldReturnCurrentTime_WhenSetNowIsNotCalled()
    {
        // Arrange
        var timeProvider = new SettableTimeProvider();

        // Act
        var now = timeProvider.FixedUtcNow;

        // Assert
        await Assert.That((DateTimeOffset.UtcNow - now).TotalSeconds).IsLessThan(1);
    }
}