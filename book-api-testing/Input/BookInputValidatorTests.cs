using Xunit;
using book_api.Interface.Input;
using System;
using System.Collections.Generic;

public class BookInputValidatorTests
{
    [Fact]
    public void Should_Fail_When_Title_Is_Empty()
    {
        var validator = new BookInputValidator();
        var input = new BookInput("", DateTime.Now, new List<AuthorInput> { new AuthorInput("John", "Doe") });
        var result = validator.Validate(input);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Should_Pass_When_All_Fields_Are_Valid()
    {
        var validator = new BookInputValidator();
        var input = new BookInput("Valid Title", DateTime.Now, new List<AuthorInput> { new AuthorInput("John", "Doe") });
        var result = validator.Validate(input);
        Assert.True(result.IsValid);
    }
}