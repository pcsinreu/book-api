using Xunit;
using book_api.Interface.Input;

public class AuthorInputValidatorTests
{
    [Fact]
    public void Should_Fail_When_FirstName_Is_Empty()
    {
        var validator = new AuthorInputValidator();
        var input = new AuthorInput("", "Doe");
        var result = validator.Validate(input);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Should_Pass_When_All_Fields_Are_Valid()
    {
        var validator = new AuthorInputValidator();
        var input = new AuthorInput("John", "Doe");
        var result = validator.Validate(input);
        Assert.True(result.IsValid);
    }
}