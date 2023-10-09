namespace Burkus.Mvvm.Maui.UnitTests.Models;

public class NavigationParametersTests
{
    public enum SampleEnum
    {
        Option1,
        Option2,
        Option3,
    }

    [Theory]
    [InlineData(true, true)]
    [InlineData(false, false)]
    public void UseAnimatedNavigation_GetSet_ReturnsSetValue(
        bool setValue,
        bool expectedValue)
    {
        // Arrange
        var navigationParameters = new NavigationParameters();
        navigationParameters.UseAnimatedNavigation = setValue;

        // Act
        var result = navigationParameters.UseAnimatedNavigation;

        // Assert
        Assert.Equal(expectedValue, result);
    }

    [Theory]
    [InlineData(true, true)]
    [InlineData(false, false)]
    public void UseModalNavigation_GetSet_ReturnsSetValue(
        bool setValue,
        bool expectedValue)
    {
        // Arrange
        var navigationParameters = new NavigationParameters();
        navigationParameters.UseModalNavigation = setValue;

        // Act
        var result = navigationParameters.UseModalNavigation;

        // Assert
        Assert.Equal(expectedValue, result);
    }

    [Fact]
    public void GetValue_ExistingBooleanParameter_ReturnsValue()
    {
        // Arrange
        var navigationParameters = new NavigationParameters();
        navigationParameters["MyBoolParam"] = true;

        // Act
        var result = navigationParameters.GetValue<bool>("MyBoolParam");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void GetValue_NonExistentParameter_ReturnsDefaultValue()
    {
        // Arrange
        var navigationParameters = new NavigationParameters();

        // Act
        var result = navigationParameters.GetValue<bool>("NonExistentParam");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void GetValue_InvalidConversion_ThrowsArgumentException()
    {
        // Arrange
        var navigationParameters = new NavigationParameters();
        navigationParameters["InvalidParam"] = "InvalidValue";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => navigationParameters.GetValue<int>("InvalidParam"));
    }

    [Fact]
    public void GetValue_EnumParameter_ReturnsEnumValue()
    {
        // Arrange
        var navigationParameters = new NavigationParameters();
        navigationParameters["EnumParam"] = SampleEnum.Option2;

        // Act
        var result = navigationParameters.GetValue<SampleEnum>("EnumParam");

        // Assert
        Assert.Equal(SampleEnum.Option2, result);
    }

    [Fact]
    public void GetValue_EnumParameterInvalidValue_ThrowsArgumentException()
    {
        // Arrange
        var navigationParameters = new NavigationParameters();
        navigationParameters["EnumParam"] = "InvalidValue";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => navigationParameters.GetValue<SampleEnum>("EnumParam"));
    }

    [Fact]
    public void GetValue_ObjectParameter_ReturnsObject()
    {
        // Arrange
        var navigationParameters = new NavigationParameters();
        var testObject = new object();

        navigationParameters["ObjectParam"] = testObject;

        // Act
        var result = navigationParameters.GetValue<object>("ObjectParam");

        // Assert
        Assert.Same(testObject, result);
    }

    [Fact]
    public void GetValue_ObjectParameter_NullValue_ReturnsNull()
    {
        // Arrange
        var navigationParameters = new NavigationParameters();
        navigationParameters["ObjectParam"] = null;

        // Act
        var result = navigationParameters.GetValue<object>("ObjectParam");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ToQueryString_ForEmptyParameters_ReturnsEmptyString()
    {
        // Arrange
        var navigationParameters = new NavigationParameters();

        // Act
        var result = navigationParameters.ToQueryString();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void ToQueryString_ForMultipleParameters_CreatesValidQueryString()
    {
        // Arrange
        var navigationParameters = new NavigationParameters()
        {
            { "param1", "value1" },
            { "param2", 123 },
            { "param3", true },
            { "param4", new DateTime(1994, 12, 12, 0, 0, 0, DateTimeKind.Utc) },
        };

        // Act
        var result = navigationParameters.ToQueryString();

        // Assert
        Assert.Equal("?param1=value1&param2=123&param3=True&param4=12%2f12%2f1994+00%3a00%3a00", result);
    }
}