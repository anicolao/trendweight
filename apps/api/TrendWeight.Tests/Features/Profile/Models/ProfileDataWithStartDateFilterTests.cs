using FluentAssertions;
using TrendWeight.Features.Profile.Models;
using Xunit;

namespace TrendWeight.Tests.Features.Profile.Models;

public class ProfileDataWithStartDateFilterTests
{
    [Fact]
    public void Constructor_WithBasicProfile_AppliesStartDateFilter()
    {
        // Arrange
        var innerProfile = new ProfileData
        {
            FirstName = "John",
            UseMetric = true,
            HideDataBeforeStart = false,
            GoalStart = null
        };
        var startDate = new DateTime(2024, 1, 15);

        // Act
        var decorator = new ProfileDataWithStartDateFilter(innerProfile, startDate);

        // Assert
        decorator.FirstName.Should().Be("John");
        decorator.UseMetric.Should().BeTrue();
        decorator.HideDataBeforeStart.Should().BeTrue();
        decorator.GoalStart.Should().Be(startDate);
    }

    [Fact]
    public void Constructor_WithExistingFilterEarlierDate_UsesStartDateParameter()
    {
        // Arrange
        var innerProfile = new ProfileData
        {
            HideDataBeforeStart = true,
            GoalStart = new DateTime(2024, 1, 1) // Earlier than start date parameter
        };
        var startDate = new DateTime(2024, 1, 15);

        // Act
        var decorator = new ProfileDataWithStartDateFilter(innerProfile, startDate);

        // Assert
        decorator.HideDataBeforeStart.Should().BeTrue();
        decorator.GoalStart.Should().Be(startDate); // Should use the later date
    }

    [Fact]
    public void Constructor_WithExistingFilterLaterDate_UsesExistingDate()
    {
        // Arrange
        var innerProfile = new ProfileData
        {
            HideDataBeforeStart = true,
            GoalStart = new DateTime(2024, 2, 1) // Later than start date parameter
        };
        var startDate = new DateTime(2024, 1, 15);

        // Act
        var decorator = new ProfileDataWithStartDateFilter(innerProfile, startDate);

        // Assert
        decorator.HideDataBeforeStart.Should().BeTrue();
        decorator.GoalStart.Should().Be(new DateTime(2024, 2, 1)); // Should use the later date
    }

    [Fact]
    public void Constructor_CopiesAllPropertiesFromInnerProfile()
    {
        // Arrange
        var innerProfile = new ProfileData
        {
            FirstName = "Jane",
            GoalWeight = 150.5m,
            PlannedPoundsPerWeek = 1.5m,
            DayStartOffset = 6,
            UseMetric = false,
            ShowCalories = true,
            SharingToken = "test-token",
            SharingEnabled = true,
            IsMigrated = true,
            IsNewlyMigrated = false,
            HideDataBeforeStart = false,
            GoalStart = null
        };
        var startDate = new DateTime(2024, 1, 15);

        // Act
        var decorator = new ProfileDataWithStartDateFilter(innerProfile, startDate);

        // Assert
        decorator.FirstName.Should().Be("Jane");
        decorator.GoalWeight.Should().Be(150.5m);
        decorator.PlannedPoundsPerWeek.Should().Be(1.5m);
        decorator.DayStartOffset.Should().Be(6);
        decorator.UseMetric.Should().BeFalse();
        decorator.ShowCalories.Should().BeTrue();
        decorator.SharingToken.Should().Be("test-token");
        decorator.SharingEnabled.Should().BeTrue();
        decorator.IsMigrated.Should().BeTrue();
        decorator.IsNewlyMigrated.Should().BeFalse();
        decorator.HideDataBeforeStart.Should().BeTrue(); // Always enabled by decorator
        decorator.GoalStart.Should().Be(startDate);
    }
}