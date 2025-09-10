namespace TrendWeight.Features.Profile.Models;

/// <summary>
/// Decorator for ProfileData that applies an additional start date filter
/// This allows filtering measurements by a start_date parameter while preserving
/// the original profile's filtering behavior
/// </summary>
public class ProfileDataWithStartDateFilter : ProfileData
{
    private readonly ProfileData _innerProfile;
    private readonly DateTime _startDate;

    public ProfileDataWithStartDateFilter(ProfileData innerProfile, DateTime startDate)
    {
        _innerProfile = innerProfile;
        _startDate = startDate;

        // Copy all properties from the inner profile
        FirstName = innerProfile.FirstName;
        GoalWeight = innerProfile.GoalWeight;
        PlannedPoundsPerWeek = innerProfile.PlannedPoundsPerWeek;
        DayStartOffset = innerProfile.DayStartOffset;
        UseMetric = innerProfile.UseMetric;
        ShowCalories = innerProfile.ShowCalories;
        SharingToken = innerProfile.SharingToken;
        SharingEnabled = innerProfile.SharingEnabled;
        IsMigrated = innerProfile.IsMigrated;
        IsNewlyMigrated = innerProfile.IsNewlyMigrated;

        // Apply the start date filter logic
        // If the inner profile already has filtering enabled, use the max of both dates
        // Otherwise, enable filtering with the provided start date
        if (innerProfile.HideDataBeforeStart && innerProfile.GoalStart.HasValue)
        {
            GoalStart = innerProfile.GoalStart.Value > startDate ? innerProfile.GoalStart.Value : startDate;
        }
        else
        {
            GoalStart = startDate;
        }
        
        HideDataBeforeStart = true;
    }
}